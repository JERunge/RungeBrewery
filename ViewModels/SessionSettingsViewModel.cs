using BrewUI.Data;
using BrewUI.EventModels;
using BrewUI.Items;
using BrewUI.Models;
using Caliburn.Micro;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Threading;

namespace BrewUI.ViewModels
{
    public class SessionSettingsViewModel : Screen, IHandle<SaveRecipeEvent>, IHandle<SessionRunningEvent>, IHandle<ConnectionEvent>, IHandle<SettingsUpdatedEvent>, IHandle<DatabaseUpdatedEvent>, IHandle<RecipeOpened>
    {
        #region Private variables

        private string _sessionName;
        private StringListItem _selectedBrewMethod;
        private BeerStyle _selectedBeerStyle;

        #endregion

        #region Public variables
        private double _inputProteinTemp;
        public double InputProteinTemp
        {
            get
            {
                return _inputProteinTemp;
            }
            set
            {
                _inputProteinTemp = value;
                NotifyOfPropertyChange(() => InputProteinTemp);
            }
        }

        private double _inputProteinDur;
        public double InputProteinDur
        {
            get
            {
                return _inputProteinDur;
            }
            set
            {
                _inputProteinDur = value;
                NotifyOfPropertyChange(() => InputProteinDur);
            }
        }

        private double _inputAcidTemp;
        public double InputAcidTemp
        {
            get
            {
                return _inputAcidTemp;
            }
            set
            {
                _inputAcidTemp = value;
                NotifyOfPropertyChange(() => InputAcidTemp);
            }
        }

        private double _inputAcidDur;
        public double InputAcidDur
        {
            get
            {
                return _inputAcidDur;
            }
            set
            {
                _inputAcidDur = value;
                NotifyOfPropertyChange(() => InputAcidDur);
            }
        }

        private double _inputStarchTemp;
        public double InputStarchTemp
        {
            get
            {
                return
                    _inputStarchTemp;
            }
            set
            {
                _inputStarchTemp = value;
                NotifyOfPropertyChange(() => InputStarchTemp);
            }
        }

        private double _inputStarchDur;
        public double InputStarchDur
        {
            get
            {
                return
                    _inputStarchDur;
            }
            set
            {
                _inputStarchDur = value;
                NotifyOfPropertyChange(() => InputStarchDur);
            }
        }

        private double _inputSpargeTemp;
        public double InputSpargeTemp
        {
            get
            {
                return _inputSpargeTemp;
            }
            set
            {
                _inputSpargeTemp = value;
                NotifyOfPropertyChange(() => InputSpargeTemp);
            }
        }

        private double _inputSpargeDur;
        public double InputSpargeDur
        {
            get { return _inputSpargeDur; }
            set
            {
                _inputSpargeDur = value;
                NotifyOfPropertyChange(() => InputSpargeDur);
            }
        }

        private int _totalBoilTime;
        public int TotalBoilTime
        {
            get
            {
                return _totalBoilTime;
            }
            set
            {
                _totalBoilTime = value;
                NotifyOfPropertyChange(() => TotalBoilTime);
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

        public static SessionInfo SessionInfoList { get; set; } = new SessionInfo();
        public static ObservableCollection<MashStep> MashList { get; set; } = new ObservableCollection<MashStep>();

        public static BindingList<StringListItem> BrewMethods { get; set; } = new BindingList<StringListItem>();
        public static ObservableCollection<BeerStyle> StyleList { get; set; } = new ObservableCollection<BeerStyle>();

        public StringListItem SelectedBrewMethod
        {
            get 
            { 
                return _selectedBrewMethod;
            }
            set 
            { 
                _selectedBrewMethod = value;
                NotifyOfPropertyChange(() => SelectedBrewMethod);
            }
        }
        public BeerStyle SelectedBeerStyle
        {
            get 
            { 
                return _selectedBeerStyle; 
            }
            set 
            {
                _selectedBeerStyle = value;
                NotifyOfPropertyChange(() => SelectedBeerStyle);
            }
        }

        private string _grainText;
        public string GrainText
        {
            get { return _grainText; }
            set
            {
                _grainText = value;
                NotifyOfPropertyChange(() => GrainText);
            }
        }

        public string SessionName
        {
            get 
            { 
                return _sessionName; }
            set 
            {
                _sessionName = value;
                NotifyOfPropertyChange(() => SessionName);
            }
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

        private double _spargeWaterAmount;
        public double SpargeWaterAmount
        {
            get { return _spargeWaterAmount; }
            set 
            { 
                _spargeWaterAmount = value;
                NotifyOfPropertyChange(() => SpargeWaterAmount);
            }
        }

        private List<Hops> _hopsDB;
        public List<Hops> HopsDB
        {
            get { return _hopsDB; }
            set
            {
                _hopsDB = value;
                NotifyOfPropertyChange(() => HopsDB);
            }
        }

        private ObservableCollection<Hops> _addedHops;
        public ObservableCollection<Hops> AddedHops
        {
            get { return _addedHops; }
            set
            {
                _addedHops = value;
                NotifyOfPropertyChange(() => AddedHops);
            }
        }

        private double _hopsAmount;
        public double HopsAmount
        {
            get { return _hopsAmount; }
            set
            {
                _hopsAmount = value;
                NotifyOfPropertyChange(() => HopsAmount);
            }
        }

        private double _hopsBoilTime;
        public double HopsBoilTime
        {
            get { return _hopsBoilTime; }
            set
            {
                _hopsBoilTime = value;
                NotifyOfPropertyChange(() => HopsBoilTime);
            }
        }

        private Hops _selectedHops;
        public Hops SelectedHops
        {
            get { return _selectedHops; }
            set
            {
                _selectedHops = value;
                NotifyOfPropertyChange(() => SelectedHops);
            }
        }

        private List<Grain> _grainDB;
        public List<Grain> GrainDB
        {
            get { return _grainDB; }
            set
            {
                _grainDB = value;
                NotifyOfPropertyChange(() => GrainDB);
            }
        }

        private ObservableCollection<Grain> _addedGrains;
        public ObservableCollection<Grain> AddedGrains
        {
            get { return _addedGrains; }
            set 
            {
                _addedGrains = value;
                NotifyOfPropertyChange(() => AddedGrains);
            }
        }

        private double _grainAmount;
        public double GrainAmount
        {
            get { return _grainAmount; }
            set 
            {
                _grainAmount = value;
                NotifyOfPropertyChange(() => GrainAmount);
            }
        }

        public double grainBill { get; set; }

        private Grain _selectedGrain;
        public Grain SelectedGrain
        {
            get { return _selectedGrain; }
            set
            {
                _selectedGrain = value;
                NotifyOfPropertyChange(() => SelectedGrain);
            }
        }

        private bool _sessionNameBorder;
        public bool SessionNameBorder
        {
            get { return _sessionNameBorder; }
            set 
            {
                _sessionNameBorder = value;
                NotifyOfPropertyChange(() => SessionNameBorder);
            }
        }

        private double _cdTargetTemp;
        public double CDTargetTemp
        {
            get { return _cdTargetTemp; }
            set { 
                _cdTargetTemp = value;
                NotifyOfPropertyChange(() => CDTargetTemp); 
            }
        }

        private bool _runCooldown;
        public bool RunCooldown
        {
            get { return _runCooldown; }
            set { 
                _runCooldown = value;
                NotifyOfPropertyChange(() => RunCooldown);
            }
        }

        private bool _canStart;
        public bool CanStart
        {
            get { return _canStart; }
            set
            {
                _canStart = value;
                NotifyOfPropertyChange(() => CanStart);
            }
        }
        #endregion

        private IEventAggregator _events;

        private BreweryRecipe breweryRecipe = new BreweryRecipe();

        // Let the app know which window is active
        protected override void OnActivate()
        {
            _events.PublishOnUIThread(new ActiveWindowEvent { activeWindow = "SessionVM" });
            base.OnActivate();
        }

        public SessionSettingsViewModel(IEventAggregator events)
        {
            _events = events;
            _events.Subscribe(this);

            CanStart = false;

            #region Start values

            BrewMethods.Add(new StringListItem() { StringItem = "All grain" });
            BrewMethods.Add(new StringListItem() { StringItem = "Extract" });
            BrewMethods.Add(new StringListItem() { StringItem = "BIAB" });

            StyleList = new ObservableCollection<BeerStyle>(FileInteraction.StylesFromDB());

            SessionName = "New session";
            BatchSize = Properties.Settings.Default.BatchSize;
            SelectedBrewMethod = BrewMethods[0];
            SelectedBeerStyle = StyleList[0];

            InputAcidTemp = 43.0;
            InputAcidDur = 15.0;

            InputProteinTemp = 52.0;
            InputProteinDur = 20.0;

            StarchChecked = true;
            InputStarchTemp = 67.0;
            InputStarchDur = 45.0;

            HopsDB = FileInteraction.HopsFromDB();
            AddedHops = new ObservableCollection<Hops>();
            SelectedHops = HopsDB[0];

            GrainDB = FileInteraction.GrainsFromDB();
            AddedGrains = new ObservableCollection<Grain>();
            SelectedGrain = GrainDB[0];

            CDTargetTemp = 20.0;

            SessionNameBorder = false;
            #endregion
        }

        //CHECKBOX BOOLEAN
        #region Checkbox booleans

        private bool _proteinChecked;
        public bool ProteinChecked
        {
            get 
            { 
                return _proteinChecked; 
            }
            set 
            { 
                _proteinChecked = value;
                NotifyOfPropertyChange(() => ProteinChecked);
            }
        }

        private bool _acidChecked;
        public bool AcidChecked
        {
            get 
            { 
                return _acidChecked;
            }
            set
            {
                _acidChecked = value;
                NotifyOfPropertyChange(() => AcidChecked);
            }
        }

        private bool _starchChecked;
        public bool StarchChecked
        {
            get
            {
                return _starchChecked;
            }
            set
            {
                _starchChecked = value;
                NotifyOfPropertyChange(() => StarchChecked);
            }
        }

        private bool _spargeChecked;
        public bool SpargeChecked
        {
            get { return _spargeChecked; }
            set 
            { 
                _spargeChecked = value;
                NotifyOfPropertyChange(() => SpargeChecked);
            }
        }


        #endregion

        //GUI METHODS
        #region GUI METHODS
        public void StartButton()
        {
            //if (CanStart)
            if(true)
            {
                breweryRecipe = UIToRecipe();

                _events.PublishOnUIThread(new SessionRunningEvent { SessionRunning = true, BreweryRecipe=breweryRecipe});
            }
            else
            {
                MessageBox.Show("Please connect to the Brewery in order to start the session.");
            }
        }

        public void MouseEntered()
        {
            SessionNameBorder = true;
        }

        public void AddGrainItem()
        {
            if(GrainAmount > 0)
            {
                AddedGrains.Add(new Grain { amount = GrainAmount, name = SelectedGrain.name });
            }
            else
            {
                MessageBox.Show("Grain amount cannot be 0.");
            }
        }

        public void RemoveGrains()
        {
            foreach(Grain grain in AddedGrains.ToList())
            {
                if (grain.remove)
                {
                    AddedGrains.Remove(grain);
                }
            }
        }

        public void AddHopsItem()
        {
            if (HopsAmount > 0)
            {
                AddedHops.Add(new Hops { Amount = HopsAmount, Name = SelectedHops.Name, BoilTime=TimeSpan.FromMinutes(HopsBoilTime)});
            }
            else
            {
                MessageBox.Show("Hops amount cannot be 0.");
            }
        }

        public void RemoveHops()
        {
            foreach (Hops hops in AddedHops.ToList())
            {
                if (hops.Remove)
                {
                    AddedHops.Remove(hops);
                }
            }
        }

        public void CalculateSpargeWaterAmount()
        {
            grainBill = 0;

            foreach(Grain grain in AddedGrains)
            {
                grainBill += grain.amount;
            }
            SpargeWaterAmount = Calculations.SpargeWater(grainBill);
        }
        #endregion

        public void UpdateSessionInfoList()
        {
            SessionInfoList.BatchSize = BatchSize;
            SessionInfoList.style = SelectedBeerStyle;
            SessionInfoList.BrewMethod = SelectedBrewMethod.StringItem;
            SessionInfoList.sessionName = SessionName;
        }

        public void UpdateMashList()
        {
            MashList.Clear();
            if (AcidChecked == true)
            {
                MashList.Add(new MashStep() { stepName = "Acid rest", stepDuration = TimeSpan.FromMinutes(InputAcidDur), stepTemp = InputAcidTemp });
            }
            if (ProteinChecked == true)
            {
                MashList.Add(new MashStep() { stepName = "Protein rest", stepDuration = TimeSpan.FromMinutes(InputProteinDur), stepTemp = InputProteinTemp });
            }
            if (StarchChecked == true)
            {
                MashList.Add(new MashStep() { stepName = "Starch rest", stepDuration = TimeSpan.FromMinutes(InputStarchDur), stepTemp = InputStarchTemp });
            }
            if (SpargeChecked == true)
            {
                MashList.Add(new MashStep() { stepName = "Sparge out", stepDuration = TimeSpan.FromMinutes(InputAcidDur), stepTemp = InputSpargeTemp });
            }
        }

        public BreweryRecipe UIToRecipe()
        {
            UpdateMashList();
            UpdateSessionInfoList();

            BreweryRecipe br = new BreweryRecipe();

            br.sessionInfo = SessionInfoList;
            br.mashSteps = MashList;
            br.grainList = AddedGrains;
            SpargeStep spargeStep = new SpargeStep();
            spargeStep.spargeTemp = SpargeTemp;
            spargeStep.spargeWaterAmount = SpargeWaterAmount;
            br.spargeStep = spargeStep;
            br.hopsList = AddedHops;
            foreach(MashStep step in br.mashSteps)
            {
                step.TimerText = step.stepDuration.ToString("hh\\:mm\\:ss");
            }
            br.hopsList = AddedHops;

            if (RunCooldown)
            {
                br.cooldownTargetTemp = CDTargetTemp;
            }

            return br;
        }

        public void RecipeToUI()
        {
            #region Session info

            SessionName = breweryRecipe.sessionInfo.sessionName;
            BatchSize = breweryRecipe.sessionInfo.BatchSize;

            bool newMethod = true;
            foreach(StringListItem item in BrewMethods)
            {
                if(item.StringItem == breweryRecipe.sessionInfo.BrewMethod)
                {
                    SelectedBrewMethod = item;
                    newMethod = false;
                }
            }
            if (newMethod)
            {
                BrewMethods.Add(new StringListItem() { StringItem = breweryRecipe.sessionInfo.BrewMethod });
            }

            bool newStyle = true;
            foreach(BeerStyle beerStyle in StyleList)
            {
                if(beerStyle.Name == breweryRecipe.sessionInfo.style.Name)
                {
                    SelectedBeerStyle = beerStyle;
                    newStyle = false;
                }
            }
            if (newStyle)
            {
                StyleList.Add(breweryRecipe.sessionInfo.style);
            }

            SelectedBeerStyle = breweryRecipe.sessionInfo.style;

            #endregion

            #region Grains
            AddedGrains = breweryRecipe.grainList;
            #endregion

            #region Mash steps
            foreach(MashStep mashStep in breweryRecipe.mashSteps)
            {
                if(mashStep.stepName == "Acid rest")
                {
                    AcidChecked = true;
                    InputAcidDur = Convert.ToDouble(mashStep.stepDuration.TotalMinutes);
                    InputAcidTemp = mashStep.stepTemp;
                }
                if(mashStep.stepName == "Protein rest")
                {
                    ProteinChecked = true;
                    InputProteinDur = Convert.ToDouble(mashStep.stepDuration.TotalMinutes);
                    InputProteinTemp = mashStep.stepTemp;
                }
                if (mashStep.stepName == "Starch rest")
                {
                    StarchChecked = true;
                    InputStarchDur = Convert.ToDouble(mashStep.stepDuration.TotalMinutes);
                    InputStarchTemp = mashStep.stepTemp;
                }
                if (mashStep.stepName == "Sparge out")
                {
                    SpargeChecked = true;
                    InputSpargeDur = Convert.ToDouble(mashStep.stepDuration.TotalMinutes);
                    InputSpargeTemp = mashStep.stepTemp;
                }
            }

            #endregion

            #region Sparge step
            SpargeTemp = breweryRecipe.spargeStep.spargeTemp;
            SpargeWaterAmount = breweryRecipe.spargeStep.spargeWaterAmount;
            #endregion

            #region Hops list
            AddedHops.Clear();
            foreach(Hops hops in breweryRecipe.hopsList)
            {
                AddedHops.Add(hops);
            }

            #endregion
        }

        #region Event handlers
        public void Handle(SessionRunningEvent message)
        {

        }

        public void Handle(ConnectionEvent message)
        {
            if (message.ConnectionStatus == MyEnums.ConnectionStatus.Connected)
            {
                CanStart = true;
            }
            else
            {
                CanStart = false;
            }
        }

        public void Handle(SettingsUpdatedEvent message)
        {
            BatchSize = message.brewerySettings.BatchSize;
        }

        public void Handle(DatabaseUpdatedEvent message)
        {
            switch (message.dataType)
            {
                case "Hops":
                    HopsDB = FileInteraction.HopsFromDB();
                    break;
            }
        }

        public void Handle(RecipeOpened recipe)
        {
            breweryRecipe = recipe.openedRecipe;
            RecipeToUI();
        }

        public void Handle(SaveRecipeEvent recipe)
        {
            BreweryRecipe br = new BreweryRecipe();
            br = UIToRecipe();
            _events.PublishOnUIThread(new RecipeToSaveEvent { breweryRecipe = br });
        }

        #endregion
    }
}
