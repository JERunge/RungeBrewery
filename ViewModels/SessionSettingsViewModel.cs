﻿using BrewUI.Data;
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
        private BrewMethod _selectedBrewMethod;
        private BeerStyle _selectedBeerStyle;

        #endregion

        #region Public variables
        private double _inputMashTemp;
        public double InputMashTemp
        {
            get { return _inputMashTemp; }
            set
            { 
                _inputMashTemp = value;
                NotifyOfPropertyChange(() => InputMashTemp);
            }
        }

        private int _inputMashDur;
        public int InputMashDur
        {
            get { return _inputMashDur; }
            set 
            { 
                _inputMashDur = value;
                NotifyOfPropertyChange(() => InputMashDur);
            }
        }

        private string _inputMashName;
        public string InputMashName
        {
            get { return _inputMashName; }
            set
            { 
                _inputMashName = value;
                NotifyOfPropertyChange(() => InputMashName);
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

        public static List<BrewMethod> BrewMethods { get; set; } = new List<BrewMethod>();
        public static ObservableCollection<BeerStyle> StyleList { get; set; } = new ObservableCollection<BeerStyle>();

        public BrewMethod SelectedBrewMethod
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

        private int _spargeDur;
        public int SpargeDur
        {
            get { return _spargeDur; }
            set
            {
                _spargeDur = value;
                NotifyOfPropertyChange(() => SpargeDur);
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

        private string _selectedHopsName;
        public string SelectedHopsName
        {
            get { return _selectedHopsName; }
            set { _selectedHopsName = value;
                NotifyOfPropertyChange(() => SelectedHopsName);
                UpdateSelectedHops();
            }
        }

        private List<string> _hopsSearchList;
        public List<string> HopsSearchList
        {
            get { return _hopsSearchList; }
            set { 
                _hopsSearchList = value;
                NotifyOfPropertyChange(() => HopsSearchList);
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

        private string _selectedGrainName;
        public string SelectedGrainName
        {
            get { return _selectedGrainName; }
            set { 
                _selectedGrainName = value;
                NotifyOfPropertyChange(() => SelectedGrainName) ;
                UpdateSelectedGrain();
            }
        }

        private List<string> _grainSearchList;
        public List<string> GrainSearchList
        {
            get { return _grainSearchList; }
            set
            {
                _grainSearchList = value;
                NotifyOfPropertyChange(() => GrainSearchList);
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

            BrewMethods = SQLiteAcces.LoadBrewMethods();

            StyleList = new ObservableCollection<BeerStyle>(FileInteraction.StylesFromDB());

            SessionName = "New session";
            BatchSize = Properties.Settings.Default.BatchSize;
            SelectedBrewMethod = BrewMethods[0];
            SelectedBeerStyle = StyleList[0];

            HopsDB = FileInteraction.HopsFromDB();
            AddedHops = new ObservableCollection<Hops>();
            HopsSearchList = new List<string>();
            foreach(Hops hops in HopsDB)
            {
                HopsSearchList.Add(hops.Name);
            }
            SelectedHops = new Hops();

            GrainDB = FileInteraction.GrainsFromDB();
            AddedGrains = new ObservableCollection<Grain>();
            GrainSearchList = new List<string>();
            foreach(Grain grain in GrainDB)
            {
                GrainSearchList.Add(grain.name);
            }
            SelectedGrain = new Grain();

            CDTargetTemp = 20.0;

            SessionNameBorder = false;

            if(Properties.Settings.Default.LastFile.Length > 1)
            {
                _events.PublishOnUIThread(new RecipeOpened { openedRecipe = FileInteraction.OpenRecipe(Properties.Settings.Default.LastFile) });
            }
            #endregion
        }


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

        public void AddMashStep()
        {
            MashStep ms = new MashStep() { stepName = InputMashName, stepTemp = InputMashTemp, stepDuration = TimeSpan.FromMinutes(InputMashDur)};
            MashList.Add(ms);
        }

        public void RemoveMashSteps()
        {
            foreach(MashStep ms in MashList.ToList())
            {
                if (ms.remove)
                {
                    MashList.Remove(ms);
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

        private void UpdateSelectedGrain()
        {
            if(SelectedGrainName != "")
            {
                foreach (Grain grain in GrainDB)
                {
                    if (grain.name == SelectedGrainName)
                    {
                        SelectedGrain = grain;
                        return;
                    }
                }
            }
        }

        private void UpdateSelectedHops()
        {
            if(SelectedHopsName != "")
            {
                foreach(Hops hops in HopsDB)
                {
                    if(hops.Name == SelectedHopsName)
                    {
                        SelectedHops = hops;
                        return;
                    }
                }
            }
        }
        #endregion

        public void UpdateSessionInfoList()
        {
            SessionInfoList.BatchSize = BatchSize;
            SessionInfoList.style = SelectedBeerStyle;
            SessionInfoList.BrewMethod = SelectedBrewMethod.Name;
            SessionInfoList.sessionName = SessionName;
        }

        public BreweryRecipe UIToRecipe()
        {
            UpdateSessionInfoList();

            BreweryRecipe br = new BreweryRecipe();

            br.sessionInfo = SessionInfoList;
            br.mashSteps = MashList;
            br.grainList = AddedGrains;
            SpargeStep spargeStep = new SpargeStep();
            spargeStep.spargeTemp = SpargeTemp;
            spargeStep.spargeDur = SpargeDur;
            br.spargeStep = spargeStep;
            br.hopsList = AddedHops;
            foreach(MashStep step in br.mashSteps)
            {
                step.TimerText = step.stepDuration.ToString("hh\\:mm\\:ss");
            }
            br.hopsList = AddedHops;

            br.cooldownTargetTemp = CDTargetTemp;
            br.runCooldown = RunCooldown;

            return br;
        }

        public void RecipeToUI()
        {
            #region Session info

            SessionName = breweryRecipe.sessionInfo.sessionName;
            BatchSize = breweryRecipe.sessionInfo.BatchSize;

            bool newMethod = true;
            foreach(BrewMethod item in BrewMethods)
            {
                if(item.Name == breweryRecipe.sessionInfo.BrewMethod)
                {
                    SelectedBrewMethod = item;
                    newMethod = false;
                }
            }
            if (newMethod)
            {
                BrewMethods.Add(new BrewMethod() { Name = breweryRecipe.sessionInfo.BrewMethod });
            }

            bool newStyle = true;
            int i = 0;
            foreach(BeerStyle beerStyle in StyleList)
            {
                if (beerStyle.Name == breweryRecipe.sessionInfo.style.Name)
                {
                    newStyle = false;
                    break;
                }
                i++;
            }
            if (newStyle)
            {
                StyleList.Add(breweryRecipe.sessionInfo.style);
                SelectedBeerStyle = breweryRecipe.sessionInfo.style;
            }
            else
            {
                SelectedBeerStyle = StyleList[i];
            }
            

            

            #endregion

            #region Grains
            AddedGrains = breweryRecipe.grainList;
            #endregion

            #region Mash steps
            MashList.Clear();
            foreach(MashStep mashStep in breweryRecipe.mashSteps)
            {
                MashList.Add(mashStep);
            }

            #endregion

            #region Sparge step
            SpargeTemp = breweryRecipe.spargeStep.spargeTemp;
            SpargeWaterAmount = breweryRecipe.spargeStep.spargeWaterAmount;
            SpargeDur = breweryRecipe.spargeStep.spargeDur;
            #endregion

            #region Hops list
            AddedHops.Clear();
            foreach(Hops hops in breweryRecipe.hopsList)
            {
                AddedHops.Add(hops);
            }

            #endregion

            #region Cooldown

            CDTargetTemp = breweryRecipe.cooldownTargetTemp;

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
