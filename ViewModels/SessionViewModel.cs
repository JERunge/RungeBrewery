using BrewUI.Data;
using BrewUI.EventModels;
using BrewUI.Items;
using BrewUI.Models;
using Caliburn.Micro;
using LiveCharts;
using LiveCharts.Configurations;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using System.Linq;
using System.Dynamic;
using System.Text;

namespace BrewUI.ViewModels
{
    public class SessionViewModel : Screen, 
        IHandle<SerialReceivedEvent>, 
        IHandle<SessionRunningEvent>, 
        IHandle<CurrentProcessUpdatedEvent>,
        IHandle<AddingGrainsEvent>,
        IHandle<ConnectionEvent>,
        IHandle<RecipeOpened>
    {
        #region Variables

        private IEventAggregator _events;

        private bool continueTask;

        IWindowManager manager = new WindowManager();
        private AddGrainsViewModel _addGrainsVM;

        // Process tracking data
        private string _currentProcess;
        public string CurrentProcess
        {
            get { return _currentProcess; }
            set
            { 
                _currentProcess = value;
                NotifyOfPropertyChange(() => CurrentProcess);
                _events.PublishOnUIThread(new CurrentProcessUpdatedEvent { currentProcess = CurrentProcess });
                _events.PublishOnUIThread(new DebugDataUpdatedEvent { index = "CP", stringValue = CurrentProcess });
            }
        }

        private string _currentStep;
        public string CurrentStep
        {
            get { return _currentStep; }
            set 
            { 
                _currentStep = value;
                NotifyOfPropertyChange(() => CurrentStep);
                _events.PublishOnUIThread(new DebugDataUpdatedEvent { index = "CS", stringValue = CurrentStep });
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

        private bool _mashRunning;
        public bool MashRunning
        {
            get { return _mashRunning; }
            set
            { 
                _mashRunning = value;
                NotifyOfPropertyChange(() => MashRunning);
                _events.PublishOnUIThread(new DebugDataUpdatedEvent { index = "MR", boolValue = MashRunning });
            }
        }

        private bool _spargeRunning;
        public bool SpargeRunning
        {
            get { return _spargeRunning; }
            set
            {
                _spargeRunning = value;
                NotifyOfPropertyChange(() => SpargeRunning);
                _events.PublishOnUIThread(new DebugDataUpdatedEvent { index = "SPR", boolValue = SpargeRunning });
            }
        }

        private bool _spargeCanStart;

        public bool SpargeCanStart
        {
            get { return _spargeCanStart; }
            set 
            {
                _spargeCanStart = value;
                NotifyOfPropertyChange(() => SpargeCanStart);
            }
        }

        private bool _boilRunning;
        public bool BoilRunning
        {
            get { return _boilRunning; }
            set
            {
                _boilRunning = value;
                NotifyOfPropertyChange(() => BoilRunning);
                _events.PublishOnUIThread(new DebugDataUpdatedEvent { index = "BR", boolValue = BoilRunning });
            }
        }

        public bool strikeWaterAdded { get; set; }

        private bool _sessionRunning;
        public bool SessionRunning
        {
            get { return _sessionRunning; }
            set
            { 
                _sessionRunning = value;
                NotifyOfPropertyChange(() => SessionRunning);
                _events.PublishOnUIThread(new DebugDataUpdatedEvent { index = "SR", boolValue = SessionRunning });
            }
        }

        private bool _mashFinished;
        public bool MashFinished
        {
            get { return _mashFinished; }
            set
            {
                _mashFinished = value;
                NotifyOfPropertyChange(() => MashFinished);
            }
        }

        private bool _spargeFinished;
        public bool SpargeFinished
        {
            get { return _spargeFinished; }
            set 
            { 
                _spargeFinished = value;
                NotifyOfPropertyChange(() => SpargeFinished);
            }
        }

        private bool _boilFinished;
        public bool BoilFinished
        {
            get { return _boilFinished; }
            set
            {
                _boilFinished = value;
                NotifyOfPropertyChange(() => BoilFinished);
            }
        }

        public bool StopCurrentProcess { get; set; }

        public bool PumpControllerRunning { get; set; }

        // Progress tracking
        private int _sessionProgress;
        public int SessionProgress
        {
            get { return _sessionProgress; }
            set
            { 
                _sessionProgress = value;
                NotifyOfPropertyChange(() => SessionProgress);
            }
        }

        private TimeSpan _sessionDuration;
        public TimeSpan SessionDuration
        {
            get { return _sessionDuration; }
            set 
            { 
                _sessionDuration = value;
            }
        }

        public DispatcherTimer SessionProgressTimer { get; set; }

        public TimeSpan TotalSessionTime { get; set; }

        public TimeSpan ElapsedSessionTime { get; set; }

        public DateTime ProcessFinishedTime { get; set; }

        // Recipe data
        public BreweryRecipe currentRecipe { get; set; }

        private string _sessionName;
        public string SessionName
        {
            get { return _sessionName; }
            set
            {
                _sessionName = value;
                NotifyOfPropertyChange(() => SessionName);
            }
        }

        private string _brewMethod;
        public string BrewMethod
        {
            get
            {
                return _brewMethod;
            }
            set
            {
                _brewMethod = value;
                NotifyOfPropertyChange(() => BrewMethod);
            }
        }

        private string _beerStyle;
        public string BeerStyle
        {
            get { return _beerStyle; }
            set
            {
                _beerStyle = value;
                NotifyOfPropertyChange(() => BeerStyle);
            }
        }

        private double _batchSize;
        public double BatchSize
        {
            get { return _batchSize; }
            set
            {
                _batchSize = value;
                NotifyOfPropertyChange(() => BatchSize);
            }
        }

        private ObservableCollection<MashStep> _mashSteps;
        public ObservableCollection<MashStep> MashSteps
        {
            get { return _mashSteps; }
            set 
            {
                _mashSteps = value;
                NotifyOfPropertyChange(() => MashSteps);
            }
        }

        private ObservableCollection<Grain> _grainList;
        public ObservableCollection<Grain> GrainList
        {
            get { return _grainList; }
            set
            { 
                _grainList = value;
                NotifyOfPropertyChange(() => GrainList);
            }
        }

        private List<Hops> _hopsList;
        public List<Hops> HopsList
        {
            get { return _hopsList; }
            set 
            { 
                _hopsList = value;
                NotifyOfPropertyChange(() => HopsList);
            }
        }

        private ObservableCollection<BoilStep> _boilSteps;
        public ObservableCollection<BoilStep> BoilSteps
        {
            get { return _boilSteps; }
            set
            { 
                _boilSteps = value;
                NotifyOfPropertyChange(() => BoilSteps);
            }
        }

        public SpargeStep spargeStep { get; set; }

        public bool grainsAdded;

        private double _targetTemp;
        public double TargetTemp
        {
            get { return _targetTemp; }
            set
            {
                _targetTemp = value;
                NotifyOfPropertyChange(() => TargetTemp);
                SendToArduino('t', TargetTemp.ToString());
            }
        }

        private double _targetDuration;
        public double TargetDuration
        {
            get { return _targetDuration; }
            set { _targetDuration = value; }
        }

        private double _spargeTemp;
        public double SpargeTemp
        {
            get { return _spargeTemp; }
            set
            {
                _spargeTemp = value;
                NotifyOfPropertyChange(() => SpargeTemp);
            }
        }

        public TimeSpan spargeDur { get; set; }
        public DateTime spargeEndTime { get; set; }

        private string _spargeTimerText;
        public string SpargeTimerText
        {
            get { return _spargeTimerText; }
            set
            {
                _spargeTimerText = value;
                NotifyOfPropertyChange(() => SpargeTimerText);
            }
        }

        private DispatcherTimer SpargeTimer = new DispatcherTimer();

        public DispatcherTimer boilTimer { get; set; }

        private DateTime boilStartTime;
        private DateTime boilEndTime;

        private string _boilTimerText;
        public string BoilTimerText
        {
            get { return _boilTimerText; }
            set
            { 
                _boilTimerText = value;
                NotifyOfPropertyChange(() => BoilTimerText);
            }
        }

        private string _spargeStatus;
        public string SpargeStatus
        {
            get { return _spargeStatus; }
            set
            {
                _spargeStatus = value;
                NotifyOfPropertyChange(() => SpargeStatus);
            }
        }

        private string _boilStatus;
        public string BoilStatus
        {
            get { return _boilStatus; }
            set
            {
                _boilStatus = value;
                NotifyOfPropertyChange(() => BoilStatus);
            }
        }

        // Measured data
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

        // Temperature chart
        private double _axisMax;
        public double AxisMax
        {
            get { return _axisMax; }
            set
            {
                _axisMax = value;
                NotifyOfPropertyChange(() => AxisMax);
            }
        }

        private double _axisMin;
        public double AxisMin
        {
            get { return _axisMin; }
            set
            {
                _axisMin = value;
                NotifyOfPropertyChange(() => AxisMin);
            }
        }

        public ChartValues<TemperatureMeasure> chartValues { get; set; }
        public Func<double, string> DateTimeFormatter { get; set; }
        public double AxisStep { get; set; }
        public double AxisUnit { get; set; }
        public DateTime startTime { get; set; }
        private DateTime chartStartTime;

        // UI variables
        #region Process clicked
        private ICommand _mashClickedCommand;
        public ICommand MashClickedCommand
        {
            get
            {
                return _mashClickedCommand ?? (_mashClickedCommand = new RelayCommand(x => { MashClicked(); }));

            }
            set { _mashClickedCommand = value; }
        }

        private bool _mashOpen;
        public bool MashOpen
        {
            get { return _mashOpen; }
            set
            { 
                _mashOpen = value;
                NotifyOfPropertyChange(() => MashOpen);
            }
        }

        private ICommand _spargeClickedCommand;
        public ICommand SpargeClickedCommand
        {
            get
            {
                return _spargeClickedCommand ?? (_spargeClickedCommand = new RelayCommand(x => { SpargeClicked(); }));

            }
            set { _spargeClickedCommand = value; }
        }

        private bool _spargeOpen;
        public bool SpargeOpen
        {
            get { return _spargeOpen; }
            set
            { 
                _spargeOpen = value;
                NotifyOfPropertyChange(() => SpargeOpen);
            }
        }

        private ICommand _boilClickedCommand;
        public ICommand BoilClickedCommand
        {
            get { return _boilClickedCommand ?? (_boilClickedCommand = new RelayCommand(x => { BoilClicked(); })); ; }
            set { _boilClickedCommand = value; }
        }

        private bool _boilOpen;
        public bool BoilOpen
        {
            get { return _boilOpen; }
            set 
            { 
                _boilOpen = value;
                NotifyOfPropertyChange(() => BoilOpen);
            }
        }
        #endregion

        #region Mouse over process
        private ICommand _mashMouseEnteredCommand;
        public ICommand MashMouseEnteredCommand
        {
            get { return _mashMouseEnteredCommand ?? (_mashMouseEnteredCommand = new RelayCommand(x => { MashColorToggle(true); })); ; }
            set { _mashMouseEnteredCommand = value; }
        }

        private ICommand _mashMouseLeftCommand;
        public ICommand MashMouseLeftCommand
        {
            get { return _mashMouseLeftCommand ?? (_mashMouseLeftCommand = new RelayCommand(x => { MashColorToggle(false); })); ; }
            set { _mashMouseLeftCommand = value; }
        }

        private bool _mashMouseOver;
        public bool MashMouseOver
        {
            get { return _mashMouseOver; }
            set 
            {
                _mashMouseOver = value;
                NotifyOfPropertyChange(() => MashMouseOver);
            }
        }

        private ICommand _spargeMouseEnteredCommand;
        public ICommand SpargeMouseEnteredCommand
        {
            get { return _spargeMouseEnteredCommand ?? (_spargeMouseEnteredCommand = new RelayCommand(x => { SpargeColorToggle(true); })); ; }
            set { _spargeMouseEnteredCommand = value; }
        }

        private ICommand _spargeMouseLeftCommand;
        public ICommand SpargeMouseLeftCommand
        {
            get { return _spargeMouseLeftCommand ?? (_spargeMouseLeftCommand = new RelayCommand(x => { SpargeColorToggle(false); })); ; }
            set { _spargeMouseLeftCommand = value; }
        }

        private bool _spargeMouseOver;
        public bool SpargeMouseOver
        {
            get { return _spargeMouseOver; }
            set {
                _spargeMouseOver = value;
                NotifyOfPropertyChange(() => SpargeMouseOver);
            }
        }

        private ICommand _boilMouseEnteredCommand;
        public ICommand BoilMouseEnteredCommand
        {
            get { return _boilMouseEnteredCommand ?? (_boilMouseEnteredCommand = new RelayCommand(x => { BoilColorToggle(true); })); ; }
            set { _boilMouseEnteredCommand = value; }
        }

        private ICommand _boilMouseLeftCommand;
        public ICommand BoilMouseLeftCommand
        {
            get { return _boilMouseLeftCommand ?? (_boilMouseLeftCommand = new RelayCommand(x => { BoilColorToggle(false); })); ; }
            set { _boilMouseLeftCommand = value; }
        }

        private bool _boilMouseOver;
        public bool BoilMouseOver
        {
            get { return _boilMouseOver; }
            set
            {
                _boilMouseOver = value;
                NotifyOfPropertyChange(() => BoilMouseOver);
            }
        }

        #endregion

        #endregion

        public SessionViewModel(IEventAggregator events, AddGrainsViewModel addGrainsVM)
        {
            _addGrainsVM = addGrainsVM;
            Connected = false;

            // Eventhandler
            _events = events;
            _events.Subscribe(this);

            // Session info
            _sessionName = "";
            SessionRunning = false;

            // Start chart
            InitializeChart();

            // Set start values
            MashOpen = true;
            SpargeOpen = false;
            MashFinished = false;
            SpargeCanStart = false;

            // Initialize recipe data
            GrainList = new ObservableCollection<Grain>();
            MashSteps = new ObservableCollection<MashStep>();
            HopsList = new List<Hops>();
            BoilSteps = new ObservableCollection<BoilStep>();
            currentRecipe = new BreweryRecipe();
            grainsAdded = false;

            StopCurrentProcess = false;
            PumpControllerRunning = false;
        }

        #region UI Methods
        public void BackButton()
        {
            MessageBoxResult answer = MessageBox.Show("This will abort the current brew session. Continue?", "Warning", MessageBoxButton.YesNo);
            if(answer == MessageBoxResult.No)
            {
                return;
            }
            _events.PublishOnUIThread(new SessionRunningEvent { SessionRunning = false });
        }

        public async void LoadAddGrains()
        {
            dynamic addGrains = new ExpandoObject();
            addGrains.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            _events.PublishOnUIThread(new AddingGrainsEvent { grainList = GrainList});
            manager.ShowDialog(_addGrainsVM);
        }

        public async Task Mash()
        {
            SessionProgressTimer.Start();

            CurrentProcess = "Mash";
            startTime = DateTime.Now;
            chartStartTime = startTime;

            #region Add water, grains and preheat
            double grainWeight = 0;
            foreach (Grain grain in GrainList)
            {
                grainWeight += grain.amount;
            }

            
            TimeSpan boilDuration = TimeSpan.Zero;

            foreach (Hops hops in HopsList)
            {
                if (hops.BoilTime > boilDuration)
                {
                    boilDuration = hops.BoilTime;
                }
            }

            double waterAmount = Calculations.MashWater(grainWeight, BatchSize, boilDuration);

            // Add strike water and heat to strike temp
            if (!strikeWaterAdded)
            {
                TargetTemp = Calculations.StrikeTemp(BatchSize, MashSteps[0].stepTemp, grainWeight);
                MessageBoxResult answer = MessageBox.Show(string.Format("Strike temperature calculated to {0}°C.\n\nAdd {1}L of water and press OK.", TargetTemp, waterAmount), "Add strike water", MessageBoxButton.OKCancel);

                if (answer == MessageBoxResult.Cancel)
                {
                    MessageBoxResult messageBoxResult = MessageBox.Show("This will abort the mash. Do you want to proceed?", "Warning", MessageBoxButton.YesNo);
                    if (messageBoxResult == MessageBoxResult.Yes)
                    {
                        CloseMash();
                        return;
                    }
                }

                strikeWaterAdded = true;
                startTime = DateTime.Now;
                continueTask = false;

                CancellationTokenSource source = new CancellationTokenSource();
                source.CancelAfter(TimeSpan.FromSeconds(1));
                Task heatAndKeep = Task.Run(() => HeatAndKeepAsync(TimeSpan.Zero), source.Token);
                await heatAndKeep;

                MashStep ms = MashSteps[0]; // Create this to have something to give to MashStepCancelled
                while (!continueTask)
                {
                    // Check if process is exited
                    if (MashStepCancelled(ms)){
                        CloseMash();
                        return;
                    }
                    await Task.Delay(100);
                }

                continueTask = false;

                // Check again if mash is cancelled
                if (MashStepCancelled(ms))
                {
                    CloseMash();
                    return;
                }

                FileInteraction.PlaySound("Alarm.wav");
            }

            // Add grains
            if (!grainsAdded)
            {
                LoadAddGrains();
                if (!grainsAdded)
                {
                    MessageBox.Show("Grains not added. Mash aborted.");
                    CloseMash();
                    return;
                }
                
            }

            #endregion

            #region Perform mash steps
            startTime = DateTime.Now;

            foreach (MashStep item in MashSteps)
            {
                // Check if any steps are already finished. If so, exit step and go to next.
                if(item.Status == "Finished")
                {
                    continue;
                }

                item.startTime = DateTime.Now;
                TargetTemp = item.stepTemp;
                CurrentStep = item.stepName;

                if(item.Status == "Paused")
                {
                    TargetDuration = item.timeLeft.TotalMinutes;
                }
                else
                {
                    TargetDuration = item.stepDuration.TotalMinutes;
                }

                #region Preheat mashstep
                item.Status = "Preheating";

                await HeatAndKeepAsync(TimeSpan.Zero);

                // Check again if mash is cancelled
                if (MashStepCancelled(item, "Waiting"))
                {
                    CloseMash();
                    return;
                }

                continueTask = false;
                #endregion

                // Keep temperature for desired duration
                item.Status = "Mashing";
                item.startTime = DateTime.Now;
                item.endTime = item.startTime + TimeSpan.FromMinutes(TargetDuration);
                item.stepTimer.Start();

                List<Task> tasks = new List<Task>();
                tasks.Add(Task.Run(() => HeatAndKeepAsync(item.endTime - item.startTime)));
                tasks.Add(Task.Run(() =>
                {
                    // Check if process is exited
                    if (!SessionRunning || CurrentProcess.Length < 1)
                    {
                        foreach (MashStep step in MashSteps)
                        {
                            if (step.Status == "Mashing")
                            {
                                step.Status = "Paused";
                                step.stepTimer.Stop();
                                step.timeLeft = item.endTime.Subtract(DateTime.Now);
                            }
                        }
                        CloseMash();
                        return;
                    }
                    Task.Delay(100).Wait();
                }));

                await Task.WhenAll(tasks);

                item.Status = "Finished";
                item.stepTimer.Stop();
                item.TimerText = item.stepDuration.ToString("hh\\:mm\\:ss");
            }

            #endregion

            CurrentProcess = "";
            TargetTemp = 0;
            FileInteraction.PlaySound("Alarm.wav");
            MessageBox.Show("Mash finished!");
            MashFinished = true;
            SpargeCanStart = true;
            MashOpen = false;
            SpargeOpen = true;

            SessionProgressTimer.Stop();
            ProcessFinishedTime = DateTime.Now;
        }

        private bool MashStepCancelled(MashStep ms, String itemStatus = "")
        {
            if (!SessionRunning || MashRunning == false)
            {
                if(itemStatus != "")
                {
                    ms.Status = itemStatus;
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        public async void StartMash()
        {
            // Cancel mash
            if (MashRunning)
            {
                MessageBoxResult result = MessageBox.Show("This will abort the current mash process. Continue?", "Warning", MessageBoxButton.YesNo);
                if(result == MessageBoxResult.Yes)
                {
                    CloseMash();
                }
                return;
            }

            // Check if mash was paused previously
            foreach(MashStep step in MashSteps)
            {
                if(step.Status == "Paused" | step.Status == "Finished")
                {
                    MessageBoxResult answer_continue = MessageBox.Show("The mash was previously paused. Press Yes to proceed with the paused mash, or press No to restart.", "Warning", MessageBoxButton.YesNoCancel);
                    if(answer_continue == MessageBoxResult.No)
                    {
                        foreach(MashStep item in MashSteps)
                        {
                            item.Status = "Waiting";
                            item.TimerText = step.stepDuration.ToString("hh\\:mm\\:ss");
                        }
                    }
                    else if(answer_continue == MessageBoxResult.Cancel)
                    {
                        return;
                    }
                }
            }

            Mash();
        }

        public async void StartSparge()
        {
            // Initiate cancellation token for async tasks
            CancellationTokenSource source = new CancellationTokenSource();
            source.CancelAfter(TimeSpan.FromSeconds(1));

            // Cancel sparge if ongoing
            if (SpargeRunning)
            {
                MessageBoxResult msganswer = MessageBox.Show("This will abort the ongoing sparge.\n\nContinue?", "Cancel sparge", MessageBoxButton.YesNo, MessageBoxImage.Information);
                if(msganswer == MessageBoxResult.Yes)
                {
                    CloseSparge();
                    return;
                }
            }

            SessionProgressTimer.Start();

            // Start sparge
            CurrentProcess = "Sparge";
            chartStartTime = DateTime.Now;

            spargeStep = currentRecipe.spargeStep;
            SpargeStatus = "Preheating";
            spargeDur = spargeStep.spargeDur;
            SpargeTemp = spargeStep.spargeTemp;

            TargetDuration = spargeDur.TotalMinutes;
            TargetTemp = SpargeTemp;

            // Preheat
            Task heatAndKeep = Task.Run(() => HeatAndKeepAsync(TimeSpan.Zero), source.Token);
            await heatAndKeep;

            while (!continueTask)
            {
                // Check if process is exited
                if (!SessionRunning || CurrentProcess.Length < 1)
                {
                    CloseSparge();
                    return;
                }
                await Task.Delay(100);
            }
            continueTask = false;

            FileInteraction.PlaySound("Alarm.wav");
            MessageBoxResult answer =  MessageBox.Show("Sparge temperature reached. Please carry out the following steps.\n\n1. Lift up mash container above water level\n2. Grab hose to apply sparge water on mash (caution, hot)\n3. Press OK to initiate sparge", "Prepare sparge", MessageBoxButton.OKCancel);
            if(answer != MessageBoxResult.OK)
            {
                CloseSparge();
                return;
            }

            SpargeStatus = "Sparging";
            spargeStep.startTime = DateTime.Now;
            spargeEndTime = spargeStep.startTime + spargeDur;

            // Initialize sparge timer
            SpargeTimer.Interval = TimeSpan.FromSeconds(1);
            SpargeTimer.Tick += SpargeTimer_Tick;
            SpargeTimer.Start();

            SendToArduino('P', "1");
            Task heaterController = Task.Run(() => HeaterController(spargeDur, source.Token));
            await heaterController;

            while (!continueTask)
            {
                // Check if process is exited
                if (!SessionRunning || CurrentProcess.Length < 1)
                {
                    CloseSparge();
                    return;
                }
                await Task.Delay(100);
            }

            SendToArduino('P', "0");
            SpargeTimer.Stop();
            SpargeTimerText = TimeSpan.FromMinutes(TargetDuration).ToString("hh\\:mm\\:ss");
            SpargeStatus = "Finished";
            FileInteraction.PlaySound("Alarm.wav");
            MessageBox.Show("Sparge finished!");
            SpargeFinished = true;
            CurrentProcess = "";
            SpargeOpen = false;
            BoilOpen = true;

            SessionProgressTimer.Stop();
            ProcessFinishedTime = DateTime.Now;
        }

        public async void StartBoil()
        {
            // Check if boil is running
            if (BoilRunning)
            {
                MessageBoxResult answer = MessageBox.Show("This will abort the ongoing boil.\n\nContinue?", "Caution", MessageBoxButton.YesNo, MessageBoxImage.Information);
                if(answer == MessageBoxResult.Yes)
                {
                    CloseBoil();
                    return;
                }
            }

            CurrentProcess = "Boil";
            chartStartTime = DateTime.Now;

            TargetTemp = 100;
            TargetDuration = 0;

            // Get total boil time
            foreach(Hops hops in HopsList)
            {
                if(hops.BoilTime.TotalMinutes > TargetDuration)
                {
                    TargetDuration = hops.BoilTime.TotalMinutes;
                }
            }

            // Preheat
            BoilStatus = "Preheating";
            await HeatAndKeepAsync(TimeSpan.Zero);

            while (!continueTask)
            {
                // Check if process is exited
                if (!SessionRunning || CurrentProcess.Length < 1)
                {
                    CloseBoil();
                    return;
                }
                Task.Delay(100).Wait();
            }
            continueTask = false;

            #region Perform boil steps

            BoilStatus = "Boiling";

            DateTime now = DateTime.Now;
            boilStartTime = now;
            boilEndTime = boilStartTime + TimeSpan.FromMinutes(TargetDuration);
            DateTime endTime = boilStartTime + TimeSpan.FromMinutes(TargetDuration);
            TimeSpan timeLeft = TimeSpan.FromMinutes(TargetDuration);

            boilTimer = new DispatcherTimer();
            boilTimer.Interval = TimeSpan.FromSeconds(1);
            boilTimer.Tick += BoilTimer_Tick;
            boilTimer.Start();

            CancellationTokenSource source = new CancellationTokenSource();
            source.CancelAfter(TimeSpan.FromSeconds(1));

            List<Task> tasks = new List<Task>();
            tasks.Add(Task.Run(() => HeaterController(endTime - boilStartTime, source.Token)));
            tasks.Add(Task.Run(() =>
            {
                while (now < endTime)
                {
                    // Check if process is exited
                    if (!SessionRunning || CurrentProcess.Length < 1)
                    {
                        CloseBoil();
                        return;
                    }
                    Task.Delay(100).Wait();

                    now = DateTime.Now;
                    timeLeft = endTime - now;

                    foreach (BoilStep boilStep in BoilSteps)
                    {

                        if (boilStep.boilTime >= timeLeft && boilStep.added == false)
                        {
                            StringBuilder tempStr = new StringBuilder();
                            foreach (Hops hops in boilStep.hopsList)
                            {
                                string tempLine = hops.Amount + "g " + hops.Name;
                                tempStr.AppendLine(tempLine);
                            }
                            FileInteraction.PlaySound("Alarm.wav");
                            MessageBox.Show("Add the following hops and press Ok.\n\n" + tempStr, "Add hops");
                            boilStep.added = true;
                        }
                    }
                    now = DateTime.Now;
                    Task.Delay(100).Wait();
                }
            }));

            await Task.WhenAll(tasks);

            bool finished = true;
            foreach(BoilStep boilStep in BoilSteps)
            {
                MessageBox.Show(boilStep.added.ToString());
                if (boilStep.added == false)
                {
                    finished = false;
                }
            }

            if (!finished)
            {
                MessageBox.Show("Boil aborted.");
                CloseBoil();
                return;
            }

            FileInteraction.PlaySound("Alarm.wav");
            BoilStatus = "Finished";
            MessageBox.Show("Boil has finished!");
            BoilFinished = true;
            CloseBoil();
            #endregion
        }

        private void CloseMash()
        {
            CurrentProcess = "";
            CurrentStep = "";
            TargetTemp = 0;
            TargetDuration = 0;
            SendToArduino('H', "0");
            SendToArduino('P', "0");
            MashRunning = false;
            continueTask = false;
        }

        private void CloseSparge()
        {
            SpargeRunning = false;
            SpargeTimerText = TimeSpan.FromMinutes(TargetDuration).ToString("hh\\:mm\\:ss");
            SpargeStatus = "Waiting";
            CurrentProcess = "";
            SpargeTimer.Stop();
            TargetDuration = 0;
            SendToArduino('H', "0");
            SendToArduino('P', "0");
        }

        private void CloseBoil()
        {
            CurrentProcess = "";
            CurrentStep = "";
            BoilRunning = false;
            TargetTemp = 0;
            TargetDuration = 0;
            SendToArduino('H', "0");
            SendToArduino('P', "0");
            BoilStatus = "-";
            try
            {
                boilTimer.Stop();
            }
            catch
            {

            }
            continueTask = false;
        }

        private void BoilTimer_Tick(object sender, EventArgs e)
        {
            if(DateTime.Now <= boilEndTime)
            {
                BoilTimerText = (boilEndTime - DateTime.Now).ToString("hh\\:mm\\:ss");
            }
        }

        private void SpargeTimer_Tick(object sender, EventArgs e)
        {
            spargeDur = spargeEndTime - DateTime.Now;
            SpargeTimerText = spargeDur.ToString("hh\\:mm\\:ss");
        }

        private void SessionProgressTimer_Tick(object sender, EventArgs e)
        {
            ElapsedSessionTime += -TimeSpan.FromSeconds(1);
            SessionProgress = Convert.ToInt32(Math.Round((TotalSessionTime.TotalMinutes - ElapsedSessionTime.TotalMinutes)/TotalSessionTime.TotalMinutes));
        }

        public void MashClicked()
        {
            MashOpen = !MashOpen;
        }

        public void SpargeClicked()
        {
            SpargeOpen = !SpargeOpen;
        }

        public void BoilClicked()
        {
            BoilOpen = !BoilOpen;
        }

        public void MashColorToggle(bool mouseOver)
        {
            MashMouseOver = mouseOver;
        }

        public void SpargeColorToggle(bool mouseOver)
        {
            SpargeMouseOver = mouseOver;
        }

        public void BoilColorToggle(bool mouseOver)
        {
            BoilMouseOver = mouseOver;
        }
        #endregion

        #region Methods

        private async Task HeatAndKeepAsync(TimeSpan duration)
        {
            _events.PublishOnUIThread(new DebugDataUpdatedEvent { index = "HAKA Started", stringValue = "" });
            CancellationTokenSource source = new CancellationTokenSource();
            source.CancelAfter(TimeSpan.FromSeconds(1));

            List<Task> tasks = new List<Task>();
            
            tasks.Add(Task.Run(() => PumpController(duration, source.Token)));
            tasks.Add(Task.Run(() => HeaterController(duration, source.Token)));

            await Task.WhenAll(tasks);
            _events.PublishOnUIThread(new DebugDataUpdatedEvent { index = "HAKA Finished", stringValue = "" });
        }

        private void PumpController(TimeSpan duration, CancellationToken cancellationToken)
        {
            DateTime now = DateTime.Now;
            bool pumpOn = false;
            bool sendMessage = false;
            DateTime pumpEnd = now + TimeSpan.FromSeconds(1);

            ArduinoMessage arduinoMessage = new ArduinoMessage();
            arduinoMessage.AIndex = 'P';

            // Preheating
            try
            {
                while (CurrentTemp <= TargetTemp - 0.5)
                {
                    // Check if session has been aborted
                    if (!SessionRunning || CurrentProcess.Length < 1)
                    {
                        cancellationToken.ThrowIfCancellationRequested();
                    }

                    if (now >= pumpEnd)
                    {
                        sendMessage = true;
                    }

                    if (!pumpOn && sendMessage)
                    {
                        arduinoMessage.AMessage = "1";
                        _events.PublishOnUIThread(new SerialToSendEvent { arduinoMessage = arduinoMessage });
                        pumpEnd = now + TimeSpan.FromSeconds(Properties.Settings.Default.PumpOnDuration);
                        sendMessage = false;
                        pumpOn = !pumpOn;
                    }

                    if (pumpOn && sendMessage)
                    {
                        arduinoMessage.AMessage = "0";
                        _events.PublishOnUIThread(new SerialToSendEvent { arduinoMessage = arduinoMessage });
                        pumpEnd = now + TimeSpan.FromSeconds(Properties.Settings.Default.PumpOffDuration);
                        sendMessage = false;
                        pumpOn = !pumpOn;
                    }

                    now = DateTime.Now;
                    Task.Delay(300).Wait();
                }
            }
            catch (OperationCanceledException)
            {
                return;
            }

            _events.PublishOnUIThread(new DebugDataUpdatedEvent { index = "DUR", stringValue = duration.TotalSeconds.ToString() });

            if (duration == TimeSpan.Zero)
            {
                return;
            }

            // Keeping temp for duration
            DateTime endTime = now + duration;
            try
            {
                while (now < endTime)
                {
                    // Check if session has been aborted
                    if (!SessionRunning || CurrentProcess.Length < 1)
                    {
                        cancellationToken.ThrowIfCancellationRequested();
                    }

                    if (now >= pumpEnd)
                    {
                        sendMessage = true;
                    }

                    if (!pumpOn && sendMessage)
                    {
                        arduinoMessage.AMessage = "1";
                        _events.PublishOnUIThread(new SerialToSendEvent { arduinoMessage = arduinoMessage });
                        pumpEnd = now + TimeSpan.FromSeconds(Properties.Settings.Default.PumpOnDuration);
                        sendMessage = false;
                        pumpOn = !pumpOn;
                    }

                    if (pumpOn && sendMessage)
                    {
                        arduinoMessage.AMessage = "0";
                        _events.PublishOnUIThread(new SerialToSendEvent { arduinoMessage = arduinoMessage });
                        pumpEnd = now + TimeSpan.FromSeconds(Properties.Settings.Default.PumpOffDuration);
                        sendMessage = false;
                        pumpOn = !pumpOn;
                    }

                    now = DateTime.Now;
                    Task.Delay(300).Wait();
                }
            }
            catch (OperationCanceledException)
            {
                return;
            }

            // Turn pump off when finished
            arduinoMessage.AMessage = "0";
            _events.PublishOnUIThread(new SerialToSendEvent { arduinoMessage = arduinoMessage });
        }

        private void HeaterController(TimeSpan duration, CancellationToken cancellationToken)
        {
            DateTime now = DateTime.Now;
            bool sendMessage = true;
            bool heating = false;
            DateTime heaterEnd = now + TimeSpan.FromSeconds(1);

            ArduinoMessage arduinoMessage = new ArduinoMessage();
            arduinoMessage.AIndex = 'H';

            // Preheating
            while(CurrentTemp <= TargetTemp - 0.5)
            {
                // Check if session has been aborted
                if (!SessionRunning || CurrentProcess.Length < 1)
                {
                    return;
                }

                if (sendMessage == false)
                {
                    if (heating == false && CurrentTemp < TargetTemp - 0.5) // Heater off and temp too low
                    {
                        sendMessage = true;
                    }
                    else if (heating == true && CurrentTemp >= TargetTemp - 0.5) // Heater on and temp reach target
                    {
                        sendMessage = true;
                    }
                }

                if (heating == true && sendMessage == true) // Turn heater off
                {
                    arduinoMessage.AMessage = "0";
                    _events.PublishOnUIThread(new SerialToSendEvent { arduinoMessage = arduinoMessage });
                    sendMessage = false;
                    heating = false;
                }
                else if (heating == false && sendMessage == true) // Turn heater on
                {
                    arduinoMessage.AMessage = "1";
                    _events.PublishOnUIThread(new SerialToSendEvent { arduinoMessage = arduinoMessage });
                    sendMessage = false;
                    heating = true;
                }

                now = DateTime.Now;
                Task.Delay(300).Wait();
            }

            if (duration == TimeSpan.Zero)
            {
                continueTask = true;
                return;
            }

            // Keep temp for duration
            DateTime endTime = now + duration;
            while (now < endTime)
            {
                // Check if session has been aborted
                if (!SessionRunning || CurrentProcess.Length < 1)
                {
                    return;
                }

                if (sendMessage == false)
                {
                    if (heating == false && CurrentTemp < TargetTemp - 0.5) // Heater off and temp too low
                    {
                        sendMessage = true;
                    }
                    else if (heating == true && CurrentTemp >= TargetTemp - 0.5) // Heater on and temp reach target
                    {
                        sendMessage = true;
                    }
                }

                if (heating == true && sendMessage == true) // Turn heater off
                {
                    arduinoMessage.AMessage = "0";
                    _events.PublishOnUIThread(new SerialToSendEvent { arduinoMessage = arduinoMessage });
                    sendMessage = false;
                    heating = false;
                }
                else if (heating == false && sendMessage == true) // Turn heater on
                {
                    arduinoMessage.AMessage = "1";
                    _events.PublishOnUIThread(new SerialToSendEvent { arduinoMessage = arduinoMessage });
                    sendMessage = false;
                    heating = true;
                }

                now = DateTime.Now;
                Task.Delay(300);
            }

            arduinoMessage.AMessage = "0";
            _events.PublishOnUIThread(new SerialToSendEvent { arduinoMessage = arduinoMessage });
            continueTask = true;
        }

        public void SendToArduino(char _index, string _message = "")
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

            DateTimeFormatter = value => new TimeSpan((long)value).ToString(); // How to format the x-axis
            chartValues.Add(new TemperatureMeasure { measureTemp = 0, measureTime = TimeSpan.Zero });
        }
        
        #endregion

        #region Event handlers

        public void Handle(SessionRunningEvent message)
        {
            if (message.SessionRunning == true)
            {
                SessionRunning = true;
                currentRecipe = message.BreweryRecipe;
                _brewMethod = message.BreweryRecipe.sessionInfo.BrewMethod;
                _sessionName = message.BreweryRecipe.sessionInfo.sessionName;
                _beerStyle = message.BreweryRecipe.sessionInfo.style.Name;
                _batchSize = message.BreweryRecipe.sessionInfo.BatchSize;
                MashSteps = message.BreweryRecipe.mashSteps;
                TotalSessionTime = Calculations.SessionDuration(currentRecipe, CurrentTemp);
                MessageBox.Show(TotalSessionTime.TotalMinutes.ToString());

                MashMouseOver = false;

                SessionProgressTimer = new DispatcherTimer();
                SessionProgressTimer.Interval = TimeSpan.FromSeconds(1);
                SessionProgressTimer.Tick += SessionProgressTimer_Tick;

                strikeWaterAdded = false;

                startTime = DateTime.Now;

                foreach (MashStep step in MashSteps)
                {
                    step.Status = "Waiting";
                }

                GrainList = message.BreweryRecipe.grainList;
                spargeStep = message.BreweryRecipe.spargeStep;
                spargeDur = spargeStep.spargeDur;
                SpargeTimerText = spargeDur.ToString("hh\\:mm\\:ss");
                SpargeTemp = spargeStep.spargeTemp;

                // Retrieve hops list and create boil steps
                foreach (Hops hops in message.BreweryRecipe.hopsList)
                {
                    HopsList.Add(hops);
                }

                HopsList = HopsList.OrderByDescending(x => x.BoilTime).ToList();

                TimeSpan prevBoilTime = TimeSpan.Zero;
                int i = 0;

                foreach(Hops hops in HopsList)
                {
                    if(hops.BoilTime != prevBoilTime)
                    {
                        BoilSteps.Add(new BoilStep());
                        i = BoilSteps.Count -1;
                        BoilSteps[i].boilTime = hops.BoilTime;
                        BoilSteps[i].hopsList = new ObservableCollection<Hops>();
                    }
                    BoilSteps[i].hopsList.Add(hops);
                    BoilSteps[i].added = false;
                    prevBoilTime = hops.BoilTime;
                }
            }
            else
            {
                SessionRunning = false;
                MashRunning = false;
                SpargeRunning = false;
                BoilRunning = false;
                MashFinished = false;
                SpargeFinished = false;
                BoilFinished = false;
                grainsAdded = false;
                strikeWaterAdded = false;
                BoilSteps.Clear();
                HopsList.Clear();
                MashSteps.Clear();
                chartValues.Clear();
                GrainList.Clear();
                SendToArduino('H', "0");
                SendToArduino('P', "0");
                if(boilTimer != null)
                {
                    boilTimer.Stop();
                }
                SessionProgress = 0;
            }
        }

        public void Handle(SerialReceivedEvent message)
        {
            char _index = message.arduinoMessage.AIndex;
            string _value = message.arduinoMessage.AMessage;

            // Handle incomming data
            switch (_index)
            {
                case 'T':
                        CurrentTemp = Calculations.StringToDouble(_value);
                        chartValues.Add(new TemperatureMeasure { measureTemp = CurrentTemp, measureTime = DateTime.Now.Subtract(chartStartTime) });
                    break;
                default:

                    break;
            }
        }

        public void Handle(CurrentProcessUpdatedEvent processName)
        {
            switch (processName.currentProcess)
            {
                case "Mash":
                    MashRunning = true;
                    SpargeRunning = false;
                    BoilRunning = false;
                    break;
                case "Sparge":
                    MashRunning = false;
                    SpargeRunning = true;
                    BoilRunning = false;
                    break;
                case "Boil":
                    MashRunning = false;
                    SpargeRunning = false;
                    BoilRunning = true;
                    break;
                default:
                    MashRunning = false;
                    SpargeRunning = false;
                    BoilRunning = false;
                    break;
            }
            chartValues.Clear();
        }

        public void Handle(AddingGrainsEvent message)
        {
            grainsAdded = message.grainsAdded;
        }

        public void Handle(ConnectionEvent message)
        {
            if(message.ConnectionStatus == "Connected")
            {
                Connected = true;
            }
            else
            {
                Connected = false;
            }
        }

        public void Handle(RecipeOpened message)
        {
            _events.PublishOnUIThread(new SessionRunningEvent { SessionRunning = false });
        }


        #endregion
    }
}
