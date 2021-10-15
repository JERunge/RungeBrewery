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
using System.Windows.Threading;

namespace BrewUI.ViewModels
{
    public class ShellViewModel : Conductor<object>, 
        IHandle<SessionRunningEvent>, 
        IHandle<ConnectionEvent>, 
        IHandle<SerialReceivedEvent>, 
        IHandle<SerialToSendEvent>,
        IHandle<ActiveWindowEvent>,
        IHandle<RecipeToSaveEvent>,
        IHandle<SettingsUpdatedEvent>
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
            if(breweryRecipe == null)
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
                        recipeText += FileInteraction.AddProperty("NAME", grain.name);

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

                    recipeText += "<SPARGESTEP>\n" + FileInteraction.AddProperty("TEMPERATURE", currentRecipe.spargeStep.spargeTemp.ToString()) + FileInteraction.AddProperty("AMOUNT", currentRecipe.spargeStep.spargeWaterAmount.ToString()) + "</AMOUNT>\n";

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
        private WindowState _customWindowState;
        private DateTime _timeLastReceived;
        private MediaPlayer MP;
        private BreweryRecipe currentRecipe;
        #endregion

        #region Public variables
        private ConnectionHandler connectionHandler;

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

        private DispatcherTimer pingTimer { get; set; }
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

            // Load debug window at startup
            dynamic settings = new ExpandoObject();
            settings.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            manager.ShowWindow(_debugWindowVM);

            // Initialize connection handler
            connectionHandler = new ConnectionHandler(_events);
            _events.PublishOnUIThread(new ConnectionEvent { ConnectionStatus = MyEnums.ConnectionStatus.Disconnected });

            // Initialize ping timer
            pingTimer = new DispatcherTimer();
            pingTimer.Interval = TimeSpan.FromSeconds(5);
            pingTimer.Tick += PingTimer_Tick;
            pingTimer.Start();
        }

        private void PingTimer_Tick(object sender, EventArgs e)
        {
            if (ConnectionStatus == MyEnums.ConnectionStatus.Connected)
            {
                _events.PublishOnUIThread(new SerialToSendEvent { arduinoMessage = new ArduinoMessage { AIndex = 'T', AMessage = "" } });
            }
        }

        #region UI Methods
        public async Task ConnectButton()
        {
            if (ConnectionStatus == MyEnums.ConnectionStatus.Disconnected)
            {
                connectionHandler.ArduinoConnect();
            }
            else
            {
                connectionHandler.ArduinoDisconnect();
            }
        }
        #endregion

        #region Methods

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
                ConnectionIsBusy = false;
                ConnectionText = "Connect";
                ConnectButtonEnabled = true;
                if (Properties.Settings.Default.ConnectionType == "Bluetooth")
                {
                    BluetoothIcon = PackIconKind.BluetoothOff;
                }
                else
                {
                    BluetoothIcon = PackIconKind.WifiOff;
                }
                CurrentTemp = 0;
                PumpStatus = "Off";
                HeaterStatus = "Off";
            }
            else if (ConnectionStatus == MyEnums.ConnectionStatus.Searching)
            {
                ConnectionIsBusy = true;
                ConnectionText = "Cancel";
                ConnectButtonEnabled = false;
                if (Properties.Settings.Default.ConnectionType == "Bluetooth")
                {
                    BluetoothIcon = PackIconKind.BluetoothSearching;
                }
                else
                {
                    BluetoothIcon = PackIconKind.WifiStrength0Alert;
                }
                    
            }
            else if (ConnectionStatus == MyEnums.ConnectionStatus.Connecting)
            {
                ConnectionIsBusy = true;
                ConnectionText = "Cancel";
                ConnectButtonEnabled = false;
                if (Properties.Settings.Default.ConnectionType == "Bluetooth")
                {
                    BluetoothIcon = PackIconKind.BluetoothSearching;
                }
                else
                {
                    BluetoothIcon = PackIconKind.WifiStrength0Alert;
                }
                    
            }
            else if(ConnectionStatus == MyEnums.ConnectionStatus.Reconnecting)
            {
                //
            }
            else // Connected
            {
                ConnectionIsBusy = false;
                ConnectionText = "Disconnect";
                ConnectButtonEnabled = true;
                if (Properties.Settings.Default.ConnectionType == "Bluetooth")
                {
                    BluetoothIcon = PackIconKind.BluetoothConnected;
                }
                else
                {
                    BluetoothIcon = PackIconKind.Wifi;
                }
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

        public void Handle(SettingsUpdatedEvent message)
        {
            _events.PublishOnUIThread(new ConnectionEvent { ConnectionStatus = ConnectionStatus });
        }

        #endregion
    }
}
