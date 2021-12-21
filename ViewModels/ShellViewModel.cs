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
        IHandle<SettingsUpdatedEvent>,
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
            if(breweryRecipe == null)
            {
                return;
            }
            _events.PublishOnUIThread(new RecipeOpened { openedRecipe = breweryRecipe });
        }

        public void SaveRecipe()
        {
            _events.PublishOnUIThread(new SaveRecipeEvent());
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

        public void NewRecipe()
        {
            BreweryRecipe newRecipe = FileInteraction.NewRecipe();
            _events.PublishOnUIThread(new RecipeOpened { openedRecipe = newRecipe});
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
        #endregion

        public ShellViewModel(SessionSettingsViewModel sessionSettingsVM, SessionViewModel sessionVM, ManualViewModel manualVM, BrewerySettingsViewModel brewerySettingsVM, DebugWindowViewModel debugWindowVM, IEventAggregator events)
        {
            // Set culture
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-SE");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-SE");
            FrameworkElement.LanguageProperty.OverrideMetadata(typeof(FrameworkElement), new FrameworkPropertyMetadata(
                        XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag)));

            // Upgrade settings
            Properties.Settings.Default.Upgrade();

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

        public void Handle(SettingsUpdatedEvent message)
        {
            _events.PublishOnUIThread(new ConnectionEvent { ConnectionStatus = ConnectionStatus });
        }

        public void Handle(RecipeToSaveEvent message)
        {
            FileInteraction.SaverRecipe(message.breweryRecipe);
        }

        #endregion
    }
}
