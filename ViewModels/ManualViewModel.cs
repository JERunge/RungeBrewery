using BrewUI.Data;
using BrewUI.EventModels;
using BrewUI.Items;
using BrewUI.Models;
using Caliburn.Micro;
using LiveCharts;
using LiveCharts.Configurations;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace BrewUI.ViewModels
{
    public class ManualViewModel : Screen, IHandle<SerialReceivedEvent>, IHandle<ConnectionEvent>
    {
        private readonly IEventAggregator _events;

        public WifiConnection wifiConnection;

        #region Variables

        private string _receivedMessage;
        public string ReceivedMessage
        {
            get 
            { 
                return _receivedMessage; 
            }
            set 
            { 
                _receivedMessage = value;
                NotifyOfPropertyChange(() => ReceivedMessage);
            }
        }

        private bool _pumpOn;
        public bool PumpOn
        {
            get { return _pumpOn; }
            set 
            {
                _pumpOn = value;
                NotifyOfPropertyChange(() => PumpOn);
            }
        }

        private bool _heaterOn;
        public bool HeaterOn
        {
            get { return _heaterOn; }
            set 
            {
                _heaterOn = value;
                NotifyOfPropertyChange(() => HeaterOn);
            }
        }

        private bool _connected;
        public bool Connected
        {
            get { return _connected; }
            set 
            {
                _connected = value;
                NotifyOfPropertyChange(() => Connected);
            }
        }

        private double _targetTemp;
        public double TargetTemp
        {
            get { return _targetTemp; }
            set
            {
                _targetTemp = value;
                NotifyOfPropertyChange(() => TargetTemp);
            }
        }

        private double _targetDuration;
        public double TargetDuration
        {
            get { return _targetDuration; }
            set { _targetDuration = value; }
        }

        private double _currentTemp;
        public double CurrentTemp
        {
            get { return _currentTemp; }
            set
            { 
                _currentTemp = value;
                NotifyOfPropertyChange(() => CurrentTemp);
            }
        }

        private string _currentAction;
        public string CurrentAction
        {
            get { return _currentAction; }
            set
            {
                _currentAction = value;
                NotifyOfPropertyChange(() => CurrentAction);
            }
        }

        public DateTime heatStartTime { get; set; }

        private TimeSpan _xAxisMax;
        public TimeSpan XAxisMax
        {
            get { return _xAxisMax; }
            set 
            { 
                _xAxisMax = value;
                NotifyOfPropertyChange(() => XAxisMax);
            }
        }

        private TimeSpan _xAxisMin;
        public TimeSpan XAxisMin
        {
            get { return _xAxisMin; }
            set 
            { 
                _xAxisMin = value;
                NotifyOfPropertyChange(() => XAxisMin);
            }
        }

        public ChartValues<TemperatureMeasure> chartValues { get; set; }
        public Func<double, string> DateTimeFormatter { get; set; }
        public double AxisStep { get; set; }
        public double AxisUnit { get; set; }
        public DateTime startTime { get; set; }

        #endregion

        protected override void OnActivate()
        {
            _events.PublishOnUIThread(new ActiveWindowEvent { activeWindow = "ManualVM" });
            base.OnActivate();
        }

        public ManualViewModel(IEventAggregator events)
        {
            _events = events;
            _events.Subscribe(this);

            _pumpOn = false;
            _heaterOn = false;
            InitializeChart();

            CurrentAction = "-";

            wifiConnection = new WifiConnection(events);
        }

        #region IU Methods
        public void TogglePump()
        {
            if (_pumpOn == false)
            {
                SendToArduino('P', "1");
                _pumpOn = true;
            }
            else
            {
                SendToArduino('P', "0");
                _pumpOn = false;
            }
        }

        public void ToggleHeater()
        {
            if(_heaterOn == false)
            {
                SendToArduino('H', "1");
                _heaterOn = true;
            }
            else
            {
                SendToArduino('H', "0");
                _heaterOn = false;
            }
        }

        public void GetTemp()
        {
            SendToArduino('T');
        }

        public async void HeatAndKeep()
        {
            chartValues.Clear();
            // Pre-heat
            CurrentAction = "Preheating";
            await Task.Run(() => Heat());

            // Keep temperature for desired duration
            CurrentAction = "Keeping temperature";
            heatStartTime = DateTime.Now;

            DateTime now = DateTime.Now;
            while (now < heatStartTime + TimeSpan.FromMinutes(TargetDuration))
            {
                if(CurrentTemp < TargetTemp - 0.5)
                {
                    await Task.Run(() => Heat());
                }
                await Task.Delay(TimeSpan.FromSeconds(Properties.Settings.Default.PumpOffDuration));
                SendToArduino('P', "1");
                await Task.Delay(TimeSpan.FromSeconds(Properties.Settings.Default.PumpOnDuration));
                SendToArduino('P', "0");
                now = DateTime.Now;
            }
            CurrentAction = "Done";
            System.Media.SystemSounds.Asterisk.Play();
        }

        #endregion

        #region Methods
        public void SendToArduino(char _index, string _message ="")
        {
            ArduinoMessage _arduinoMessage = new ArduinoMessage();
            _arduinoMessage.AIndex = _index;
            _arduinoMessage.AMessage = _message;
            Thread.Sleep(300);
            _events.PublishOnUIThread(new SerialToSendEvent { arduinoMessage = _arduinoMessage });
        }

        private void InitializeChart()
        {
            var mapper = Mappers.Xy<TemperatureMeasure>()
                .X(model => model.measureTime.Ticks)   //use measureTime.Ticks as X
                .Y(model => model.measureTemp);        //use the temp property as Y

            Charting.For<TemperatureMeasure>(mapper); // Save the mapper globally
            chartValues = new ChartValues<TemperatureMeasure>(); // chartValues will hold all measured values

            AxisStep = TimeSpan.FromMinutes(5).Ticks;
            AxisUnit = TimeSpan.TicksPerSecond;

            XAxisMax = TimeSpan.FromMinutes(10);
            XAxisMin = TimeSpan.Zero;

            DateTimeFormatter = value => new TimeSpan((long)value).ToString(); // How to format the x-axis
            chartValues.Add(new TemperatureMeasure { measureTemp = 0, measureTime = TimeSpan.Zero });
        }

        private async void Heat()
        {
            SendToArduino('H', "1");

            while (true)
            {
                

                // Pump on
                SendToArduino('P', "1");
                for(int i = 1; i<Properties.Settings.Default.PumpOnDuration; i++) // Set i max value to number of seconds
                {
                    Thread.Sleep(1000);
                    // Check if we have reached temp
                    if (CurrentTemp >= TargetTemp - 0.5)
                    {
                        SendToArduino('H', "0");
                        SendToArduino('P', "0");
                        return;
                    }
                }
                
                // Pump off
                SendToArduino('P', "0");
                for (int i = 1; i < Properties.Settings.Default.PumpOffDuration; i++) // Set i max value to number of seconds
                {
                    Thread.Sleep(1000);
                    // Check if we have reached temp
                    if (CurrentTemp >= TargetTemp)
                    {
                        SendToArduino('H', "0");
                        return;
                    }
                }
            }
        }
        #endregion

        #region Event handlers
        public void Handle(SerialReceivedEvent message)
        {
            char _index = message.arduinoMessage.AIndex;
            string _value = message.arduinoMessage.AMessage;

            ReceivedMessage += _index.ToString() + _value;

            // Handle all data
            switch (_index)
            {
                case 'T':
                    try
                    {
                        // Update current temperature and chart value
                        CurrentTemp = Calculations.StringToDouble(_value);
                        chartValues.Add(new TemperatureMeasure { measureTemp = CurrentTemp, measureTime = DateTime.Now.Subtract(startTime) });
                    }
                    catch
                    {

                    }
                    break;
                case 'H':
                    if (_value == "0")
                    {
                        _heaterOn = false;
                    }
                    else
                    {
                        _heaterOn = true;
                    }
                    break;
            }            
        }

        public void Handle(ConnectionEvent message)
        {
            if(message.ConnectionStatus == MyEnums.ConnectionStatus.Connected)
            {
                Connected = true;
                chartValues.Clear();
                startTime = DateTime.Now;
            }
            else
            {
                Connected = false;
            }
        }
        #endregion

        #region Wifi test
        private string _messageToWifi;
        public string MessageToWifi
        {
            get { return _messageToWifi; }
            set { 
                _messageToWifi = value;
                NotifyOfPropertyChange(() => MessageToWifi);
            }
        }

        public void SendToWifi()
        {
            wifiConnection.SendToWifi(MessageToWifi);
        }

        public async Task StartClient()
        {
            await Task.Run(() => wifiConnection.StartClient());
        }
        #endregion
    }
}
