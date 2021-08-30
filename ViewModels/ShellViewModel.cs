using BrewUI.Data;
using BrewUI.EventModels;
using BrewUI.Models;
using Caliburn.Micro;
using InTheHand.Net.Bluetooth;
using InTheHand.Net.Sockets;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;
using System.ComponentModel;
using MaterialDesignThemes.Wpf;
using System.IO;
using System.Collections.ObjectModel;
using BrewUI.Items;
using System.Windows.Media;

namespace BrewUI.ViewModels
{
    public class ShellViewModel : Conductor<object>, 
        IHandle<SessionRunningEvent>, 
        IHandle<ConnectionEvent>, 
        IHandle<SerialReceivedEvent>, 
        IHandle<SerialToSendEvent>,
        IHandle<ActiveWindowEvent>,
        IHandle<RecipeToSaveEvent>
    {
        IWindowManager manager = new WindowManager();

        #region Menu navigation
        public void LoadSessionSettings()
        {
            if (_sessionRunning == true)
            {
                ActivateItem(_sessionVM);
            }
            else
            {
                ActivateItem(_sessionSettingsVM);
            }
        }

        public void LoadManual()
        {
            ActivateItem(_manualVM);
        }

        public void LoadSettings()
        {
            dynamic settings = new ExpandoObject();
            settings.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            manager.ShowDialog(_brewerySettingsVM);
        }

        public void ImportRecipe()
        {
            BreweryRecipe breweryRecipe = FileInteraction.ImportRecipe();
            SessionInfo sessionInfo = new SessionInfo();
            sessionInfo = breweryRecipe.sessionInfo;
            try
            {
                int length = sessionInfo.sessionName.Length;
            }
            catch(Exception e)
            {
                return;
            }
            _events.PublishOnUIThread(new RecipeOpened { openedRecipe = breweryRecipe });
        }

        public void SaveRecipe()
        {
            _events.PublishOnUIThread(new SaveRecipeEvent { });
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Brewery Recipe|*.br";
            sfd.Title = "Save recipe";

            if(sfd.ShowDialog() == true)
            {
                if (sfd.FileName.Length != 0)
                {
                    string filePath = sfd.FileName;
                    string recipeText = "<RECIPE>\n<DATA>\n";

                    #region Add session info
                    SessionInfo si = new SessionInfo();
                    si = currentRecipe.sessionInfo;

                    recipeText += "<SESSIONINFO>\n";

                    // Recipe name
                    recipeText += FileInteraction.AddProperty("NAME", si.sessionName);

                    // Batch size
                    recipeText += FileInteraction.AddProperty("BATCHSIZE", si.BatchSize.ToString());

                    // Brew method
                    recipeText += FileInteraction.AddProperty("BREWMETHOD", si.BrewMethod);

                    // Style
                    recipeText += FileInteraction.AddProperty("STYLE", si.style.Name);

                    recipeText += "</SESSIONINFO>\n";
                    #endregion

                    #region Add grain list

                    ObservableCollection<Grain> gl = new ObservableCollection<Grain>();
                    gl = currentRecipe.grainList;

                    recipeText += "<GRAINLIST>\n";

                    foreach(Grain grain in gl)
                    {
                        recipeText += "<GRAIN>\n";

                        // Name
                        recipeText += FileInteraction.AddProperty("NAME", grain.grainName);

                        // Amount
                        recipeText += FileInteraction.AddProperty("AMOUNT", grain.amount.ToString());

                        recipeText += "</GRAIN>\n";
                    }

                    recipeText += "</GRAINLIST>\n";
                    #endregion

                    #region Add mash steps

                    ObservableCollection<MashStep> msl = new ObservableCollection<MashStep>();
                    msl = currentRecipe.mashSteps;

                    recipeText += "<MASHSTEPS>\n";

                    foreach (MashStep ms in msl)
                    {
                        recipeText += "<MASHSTEP>\n";

                        // Name
                        recipeText += FileInteraction.AddProperty("NAME", ms.stepName);

                        // Temperature
                        recipeText += FileInteraction.AddProperty("TEMPERATURE", ms.stepTemp.ToString());

                        // Duration
                        recipeText += FileInteraction.AddProperty("DURATION", ms.stepDuration.TotalMinutes.ToString());

                        recipeText += "</MASHSTEP>\n";
                    }

                    recipeText += "</MASHSTEPS>\n";

                    #endregion

                    #region Add sparge step

                    recipeText += "<SPARGESTEP>\n" + FileInteraction.AddProperty("TEMPERATURE", currentRecipe.spargeStep.spargeTemp.ToString()) + FileInteraction.AddProperty("DURATION", currentRecipe.spargeStep.spargeDur.TotalMinutes.ToString()) + "</SPARGESTEP>\n";

                    #endregion

                    #region Add hops list

                    ObservableCollection<Hops> hl = new ObservableCollection<Hops>();
                    hl = currentRecipe.hopsList;

                    recipeText += "<HOPSLIST>\n";

                    foreach(Hops hops in hl)
                    {
                        recipeText += "<HOPS>\n";

                        // Name
                        recipeText += FileInteraction.AddProperty("NAME", hops.Name);

                        // Amount
                        recipeText += FileInteraction.AddProperty("AMOUNT", hops.Amount.ToString());

                        // Boil time
                        recipeText += FileInteraction.AddProperty("BOILTIME", hops.BoilTime.TotalMinutes.ToString());

                        recipeText += "</HOPS>\n";
                    }

                    recipeText += "</HOPSLIST>\n";

                    #endregion

                    recipeText += "</DATA>\n</RECIPE>";

                    using (StreamWriter sw = new StreamWriter(filePath))
                    {
                        sw.Write(recipeText);
                        sw.Close();
                    }
                }
            }

            

        }

        public void CloseButton()
        {
            if (_sessionRunning == true)
            {
                if (MessageBox.Show("You have an ongoing session. Closing the application will abort the session. Continue?", "Caution", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    Application.Current.Shutdown();
                }
            }
            else
            {
                Application.Current.Shutdown();
            }
        }

        public void RestoreButton()
        {
            if (CustomWindowState == WindowState.Maximized)
            {
                CustomWindowState = WindowState.Normal;
            }
            else
            {
                CustomWindowState = WindowState.Maximized;
            }
        }

        public void MinimizeButton()
        {
            CustomWindowState = WindowState.Minimized;
        }

        public void OpenRecipe()
        {
            BreweryRecipe breweryRecipe = FileInteraction.OpenRecipe();
            try
            {
                int length = breweryRecipe.sessionInfo.sessionName.Length;
            }
            catch(NullReferenceException) // Handle if the user does not choose anything
            {
                return;
            }

            if (_sessionRunning)
            {
                MessageBoxResult answer = MessageBox.Show("You have an ongoing brew session. Opening a new recipe will end your current session.\n\nContinue?", "Warning", MessageBoxButton.YesNo);
                if(answer == MessageBoxResult.Yes)
                {
                    _events.PublishOnUIThread(new SessionRunningEvent { SessionRunning = false });
                }
                else
                {
                    return;
                }
            }

            _events.PublishOnUIThread(new RecipeOpened { openedRecipe = breweryRecipe });
        }
        #endregion

        #region Private variables
        private IEventAggregator _events;
        private SessionSettingsViewModel _sessionSettingsVM;
        private ManualViewModel _manualVM;
        private SessionViewModel _sessionVM;
        private BrewerySettingsViewModel _brewerySettingsVM;
        private DebugWindowViewModel _debugWindowVM;
        private string _currentStep;
        private bool _sessionRunning = false;
        private bool _connected;
        private WindowState _customWindowState;
        private BluetoothClient BTClient;
        private NetworkStream BTStream;
        private readonly BackgroundWorker BW_ReceiveData = new BackgroundWorker();
        private readonly BackgroundWorker BW_RequestData = new BackgroundWorker();
        private DateTime _timeLastReceived;
        private MediaPlayer MP;
        private BreweryRecipe currentRecipe;
        #endregion

        #region Public variables
        private string _sessionName;
        public string SessionName
        {
            get
            {
                return _sessionName;
            }
            set
            {
                _sessionName = value;
                NotifyOfPropertyChange(() => SessionName);
            }
        }

        public string CurrentStep;
        public bool Connected
        {
            get { return _connected; }
            set
            {
                _connected = value;
                NotifyOfPropertyChange(() => Connected);
            }
        }

        private bool _connectionIsBusy;
        public bool ConnectionIsBusy
        {
            get { return _connectionIsBusy; }
            set
            {
                _connectionIsBusy = value;
                NotifyOfPropertyChange(() => ConnectionIsBusy);
            }
        }

        private string _connectionText;
        public string ConnectionText
        {
            get { return _connectionText; }
            set
            {
                _connectionText = value;
                NotifyOfPropertyChange(() => ConnectionText);
            }
        }

        public DateTime pingTimeStamp { get; set; }

        private MyEnums.ConnectionStatus _connectionStatus;
        public MyEnums.ConnectionStatus ConnectionStatus
        {
            get { return _connectionStatus; }
            set
            {
                _connectionStatus = value;
                NotifyOfPropertyChange(() => ConnectionStatus);
            }
        }

        public WindowState CustomWindowState
        {
            get { return _customWindowState; }
            set
            {
                _customWindowState = value;
                NotifyOfPropertyChange(() => CustomWindowState);
            }
        }

        private bool _connectButtonEnabled;
        public bool ConnectButtonEnabled
        {
            get { return _connectButtonEnabled; }
            set
            {
                _connectButtonEnabled = value;
                NotifyOfPropertyChange(() => ConnectButtonEnabled);
            }
        }

        private string _outgoingMessage;
        public string TestOutgoing
        {
            get { return _outgoingMessage; }
            set
            {
                _outgoingMessage = value;
                NotifyOfPropertyChange(() => TestOutgoing);
            }
        }

        private char _recIndex;
        public char RecIndex
        {
            get { return _recIndex; }
            set 
            {
                _recIndex = value;
                NotifyOfPropertyChange(() => RecIndex);
            }
        }

        private string _recVal;
        public string RecVal
        {
            get { return _recVal; }
            set
            { 
                _recVal = value;
                NotifyOfPropertyChange(() => RecVal);
            }
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

        private string _pumpStatus;
        public string PumpStatus
        {
            get { return _pumpStatus; }
            set
            { 
                _pumpStatus = value;
                NotifyOfPropertyChange(() => PumpStatus);
            }
        }

        private string _heaterStatus;
        public string HeaterStatus
        {
            get { return _heaterStatus; }
            set
            {
                _heaterStatus = value;
                NotifyOfPropertyChange(() => HeaterStatus);
            }
        }

        private PackIconKind _bluetoothIcon;
        public PackIconKind BluetoothIcon
        {
            get { return _bluetoothIcon; }
            set 
            {
                _bluetoothIcon = value;
                NotifyOfPropertyChange(() => BluetoothIcon);
            }
        }

        private string _activeWindow;
        public string ActiveWindow
        {
            get { return _activeWindow; }
            set
            {
                _activeWindow = value;
                NotifyOfPropertyChange(() => ActiveWindow);
            }
        }
        #endregion

        #region Public constants
        public string BTName = "Runge Brewery";
        #endregion

        public ShellViewModel(SessionSettingsViewModel sessionSettingsVM, SessionViewModel sessionVM, ManualViewModel manualVM, BrewerySettingsViewModel brewerySettingsVM, DebugWindowViewModel debugWindowVM, IEventAggregator events)
        {
            // Set culture
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-SE");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-SE");
            FrameworkElement.LanguageProperty.OverrideMetadata(typeof(FrameworkElement), new FrameworkPropertyMetadata(
                        XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag)));

            // Subscribe to events
            _events = events;
            _events.Subscribe(this);

            // Background worker that reads incomming data
            BW_ReceiveData.DoWork += BW_BT;
            BW_ReceiveData.WorkerSupportsCancellation = true;

            // Backgroundworker that requests update data from Arduino
            BW_RequestData.DoWork += BW_RD;
            BW_RequestData.WorkerSupportsCancellation = true;

            // Initialize current recipe
            currentRecipe = new BreweryRecipe();

            // Menu definitions
            _sessionSettingsVM = sessionSettingsVM;
            _sessionVM = sessionVM;
            _manualVM = manualVM;
            _brewerySettingsVM = brewerySettingsVM;
            _debugWindowVM = debugWindowVM;

            // Startup view
            ActivateItem(_sessionSettingsVM);

            // Start values of variables
            Connected = false;
            ConnectionIsBusy = false;
            ConnectionText = "Connect";
            ConnectionStatus = MyEnums.ConnectionStatus.Disconnected;
            _events.PublishOnUIThread(new ConnectionEvent { ConnectionStatus = ConnectionStatus });
            _heaterStatus = "Off";
            _pumpStatus = "Off";
            BluetoothIcon = PackIconKind.BluetoothOff;

            // Load debug window at startup
            dynamic settings = new ExpandoObject();
            settings.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            manager.ShowWindow(_debugWindowVM);
        }

        #region UI Methods
        public void ConnectButton()
        {
            if (ConnectionStatus == MyEnums.ConnectionStatus.Disconnected)
            {
                ArduinoConnect();
            }
            else
            {
                ArduinoDisconnect();
            }
        }
        #endregion

        #region Methods
        private void SendToArduino(string message)
        {
            if(BTClient != null)
            {
                try
                {
                    BTStream = BTClient.GetStream();
                    if (Connected && BTStream != null)
                    {
                        var buffer = System.Text.Encoding.UTF8.GetBytes(ArduinoParse.ToParse(message));
                        BTStream.Write(buffer, 0, buffer.Length);
                        BTStream.Flush();
                    }
                }
                catch
                {

                }
            }
        }

        public bool connectionDone { get; set; }

        public async Task ArduinoConnect()
        {
            _events.PublishOnUIThread(new ConnectionEvent { ConnectionStatus = MyEnums.ConnectionStatus.Connecting });
            BTClient = new BluetoothClient();
            BluetoothDeviceInfo[] devices = await Task.Run(() => BTClient.DiscoverDevices());
            BluetoothDeviceInfo device = null;

            foreach (var dev in devices)
            {
                if(dev.DeviceName == BTName)
                {
                    device = dev;
                    break;
                }
            }

            if (!device.Authenticated)
            {
                BluetoothSecurity.PairRequest(device.DeviceAddress, "0000");
            }

            device.Refresh();
            _events.PublishOnUIThread(new DebugDataUpdatedEvent { index = $"Authenticated: {device.Authenticated.ToString()}"});

            try
            {
                await Task.Run(() => BTClient.Connect(device.DeviceAddress, BluetoothService.SerialPort));
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message, "Connection failed");
                _events.PublishOnUIThread(new ConnectionEvent { ConnectionStatus = MyEnums.ConnectionStatus.Disconnected });
                return;
            }

            _events.PublishOnUIThread(new ConnectionEvent { ConnectionStatus = MyEnums.ConnectionStatus.Connected });
        }

        private void ArduinoDisconnect()
        {
            SendToArduino("H0");
            Thread.Sleep(100);
            SendToArduino("P0");
            Thread.Sleep(100);
            _events.PublishOnUIThread(new ConnectionEvent { ConnectionStatus = MyEnums.ConnectionStatus.Disconnected });
            BTClient.Close();
        }

        private void BW_BT(object sender, DoWorkEventArgs e) // Do this in background
        {
            TimeSpan _discTimeSpan = TimeSpan.FromMilliseconds(2000);
            BTStream = BTClient.GetStream();
            BTStream.ReadTimeout = 1000;

            while (Connected == true)
            {
                //if (DateTime.Now - pingTimeStamp > TimeSpan.FromSeconds(5)) // Check if connection is lost. If so, try to reconnect.
                //{
                //    BTClient.Close();
                //    _events.PublishOnUIThread(new ConnectionEvent { ConnectionStatus = "Reconnecting" });
                //}

                if (BW_ReceiveData.CancellationPending) // Check if cancellation is pending and exit if so.
                {
                    return;
                }

                try
                {
                    if (BTStream.DataAvailable)
                    {
                        // Prepare byte array and string for received data
                        byte[] receive = new byte[1024];
                        Array.Clear(receive, 0, receive.Length);
                        string readMessage = "";

                        BTStream.Read(receive, 0, receive.Length);

                        // Start reading data to _receivedMessage if startmarker is detected
                        if (Encoding.ASCII.GetString(receive)[0] == '<')
                        {
                            do // Add read byte to array until endmarker appears
                            {

                                BTStream.Read(receive, 0, receive.Length);

                                readMessage += Encoding.ASCII.GetString(receive);
                            }
                            while (BTStream.DataAvailable && Encoding.ASCII.GetString(receive)[0] != '>');

                            ArduinoMessage _arduinoMessage = new ArduinoMessage();

                            // Read index value from read message
                            _arduinoMessage.AIndex = readMessage[0];

                            // Read value from read message
                            for (int i = 1; i < readMessage.Length; i++)
                            {
                                if (readMessage[i] != '>')
                                {
                                    _arduinoMessage.AMessage += readMessage[i];
                                }
                                else if (readMessage[i] == '>')
                                {
                                    break;
                                }
                            }

                            // Publish message on event
                            _events.PublishOnUIThread(new SerialReceivedEvent { arduinoMessage = _arduinoMessage });
                        }
                    }
                }
                catch
                {
                    //MessageBox.Show("Could not read incoming data");
                }
                
            }
        }

        private void BW_Completed(object sender, RunWorkerCompletedEventArgs e) // Do this when worker is finished
        {

        }

        private void BW_RD(object sender, DoWorkEventArgs e) // Do this in background
        {
            while (true)
            {
                if (BW_RequestData.CancellationPending) // Check if cancellation is pending and exit if so.
                {
                    return;
                }
                SendToArduino("T"); // Request temperature. Also functions as ping to brewery.
                Thread.Sleep(1000);
            }
        }
        #endregion

        #region Event handlers

        public void Handle(SessionRunningEvent message)
        {
            _sessionRunning = message.SessionRunning;
            if (_sessionRunning == true)
            {

                ActivateItem(_sessionVM);
            }
            else
            {
                DeactivateItem(_sessionVM, true);
                ActivateItem(_sessionSettingsVM);
            }
        }

        public void Handle(ConnectionEvent message)
        {
            ConnectionStatus = message.ConnectionStatus;

            if (ConnectionStatus == MyEnums.ConnectionStatus.Disconnected)
            {
                Connected = false;
                ConnectionIsBusy = false;
                ConnectionText = "Connect";
                ConnectButtonEnabled = true;
                BW_ReceiveData.CancelAsync();
                BW_RequestData.CancelAsync();
                BluetoothIcon = PackIconKind.BluetoothOff;
                CurrentTemp = 0;
            }
            else if (ConnectionStatus == MyEnums.ConnectionStatus.Searching)
            {
                Connected = false;
                ConnectionIsBusy = true;
                ConnectionText = "Cancel";
                ConnectButtonEnabled = false;
                BluetoothIcon = PackIconKind.BluetoothSearching;
            }
            else if (ConnectionStatus == MyEnums.ConnectionStatus.Connecting)
            {
                Connected = false;
                ConnectionIsBusy = true;
                ConnectionText = "Cancel";
                ConnectButtonEnabled = false;
                BluetoothIcon = PackIconKind.BluetoothSearching;
            }
            else if(ConnectionStatus == MyEnums.ConnectionStatus.Reconnecting)
            {
                ArduinoConnect();
            }
            else // Connected
            {
                Connected = true;
                ConnectionIsBusy = false;
                ConnectionText = "Disconnect";
                ConnectButtonEnabled = true;
                BW_ReceiveData.RunWorkerAsync();
                BW_RequestData.RunWorkerAsync();
                BluetoothIcon = PackIconKind.BluetoothConnected;
            }
        }

        public void Handle(SerialReceivedEvent message)
        {
            char _index = message.arduinoMessage.AIndex;
            string _value = message.arduinoMessage.AMessage;

            RecIndex = _index;
            RecVal = _value;

            switch (_index)
            {
                case 'T': // Temperature received
                    try
                    {
                        CurrentTemp = Calculations.StringToDouble(_value);
                        pingTimeStamp = DateTime.Now;
                    }
                    catch
                    {

                    }
                    break;
            }
        }

        public void Handle(SerialToSendEvent message)
        {
            char _index = message.arduinoMessage.AIndex;
            string _value = message.arduinoMessage.AMessage;
            string _message = _index.ToString() + _value;

            SendToArduino(_message);

            switch(_index)
            {
                case 'P':
                    if (_value == "1")
                    {
                        PumpStatus = "On";
                    }
                    else
                    {
                        PumpStatus = "Off";
                    }
                    break;
                case 'H':
                    if(_value == "1")
                    {
                        HeaterStatus = "On";
                    }
                    else
                    {
                        HeaterStatus = "Off";
                    }
                    break;
            }
        }

        public void Handle(ActiveWindowEvent AW)
        {
            ActiveWindow = AW.activeWindow;
        }

        public void Handle(RecipeToSaveEvent recipe)
        {
            currentRecipe = recipe.breweryRecipe;
            if(currentRecipe.sessionInfo.sessionName == null)
            {
                currentRecipe.sessionInfo.sessionName = "";
            }
        }

        #endregion
    }
}
