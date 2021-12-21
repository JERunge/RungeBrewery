using BrewUI.EventModels;
using BrewUI.Items;
using BrewUI.Models;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Windows;
using Microsoft.Win32;
using System.IO;
using System.Configuration;
using System.Data;
using System.Linq;
using BrewUI.Data;
using static BrewUI.Models.MyEnums;

namespace BrewUI.ViewModels
{
    public class BrewerySettingsViewModel : Screen, IHandle<DatabaseUpdatedEvent>, IHandle<ConnectionEvent>, IHandle<SessionRunningEvent>
    {
        #region Variables
        private double prevBatchSize { get; set; }
        private double prevHeaterEffect { get; set; }
        private double prevEquipmentLoss { get; set; }
        private double prevMashThickness { get; set; }
        private double prevGrainAbsorption { get; set; }
        private double prevPumpOnDuration { get; set; }
        private double prevPumpOffDuration { get; set; }
        private double prevFermenterLoss { get; set; }
        private double prevKettleLoss { get; set; }
        private double prevEvaporationRate { get; set; }
        private double prevCoolingShrinkage { get; set; }

        private IEventAggregator _events;

        private ConnectionStatus connectionStatus;

        private bool sessionRunning;

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

        private double _heaterEffect;
        public double HeaterEffect
        {
            get { return _heaterEffect; }
            set
            { 
                _heaterEffect = value;
                NotifyOfPropertyChange(() => HeaterEffect);
            }
        }

        private double _fermenterLoss;
        public double FermenterLoss
        {
            get { return _fermenterLoss; }
            set {
                _fermenterLoss = value;
                NotifyOfPropertyChange(() => FermenterLoss);
            }
        }

        private double _kettleLoss;
        public double KettleLoss
        {
            get { return _kettleLoss; }
            set {
                _kettleLoss = value;
                NotifyOfPropertyChange(() => KettleLoss);
            }
        }

        private double _equipmentLoss;
        public double EquipmentLoss
        {
            get { return _equipmentLoss; }
            set 
            { 
                _equipmentLoss = value;
                NotifyOfPropertyChange(() => EquipmentLoss);
            }
        }

        private double _mashThickness;
        public double MashThickness
        {
            get { return _mashThickness; }
            set
            {
                _mashThickness = value;
                NotifyOfPropertyChange(() => MashThickness);
            }
        }

        private double _grainAbsorption;
        public double GrainAbsorption
        {
            get { return _grainAbsorption; }
            set
            {
                _grainAbsorption = value;
                NotifyOfPropertyChange(() => GrainAbsorption);
            }
        }

        private double _coolingShrinkage;
        public double CoolingShrinkage
        {
            get { return _coolingShrinkage; }
            set {
                _coolingShrinkage = value;
                NotifyOfPropertyChange(() => CoolingShrinkage);
            }
        }

        private double _evaporationRate;
        public double EvaporationRate
        {
            get { return _evaporationRate; }
            set { 
                _evaporationRate = value;
                NotifyOfPropertyChange(() => EvaporationRate);
            }
        }

        private double _pumpOnDuration;
        public double PumpOnDuration
        {
            get { return _pumpOnDuration; }
            set
            { 
                _pumpOnDuration = value;
                NotifyOfPropertyChange(() => PumpOnDuration);
            }
        }

        private double _pumpOffDuration;
        public double PumpOffDuration
        {
            get { return _pumpOffDuration; }
            set 
            {
                _pumpOffDuration = value;
                NotifyOfPropertyChange(() => PumpOffDuration);
            }
        }

        private List<Hops> _hopList;
        public List<Hops> HopList
        {
            get { return _hopList; }
            set
            {
                _hopList = value;
                NotifyOfPropertyChange(() => HopList);
            }
        }

        private List<Grain> _grainList;
        public List<Grain> GrainList
        {
            get { return _grainList; }
            set
            { 
                _grainList = value;
                NotifyOfPropertyChange(() => GrainList);
            }
        }

        private List<Yeast> _yeastList;
        public List<Yeast> YeastList
        {
            get { return _yeastList; }
            set 
            {
                _yeastList = value;
                NotifyOfPropertyChange(() => YeastList);
            }
        }


        private List<BeerStyle> _styleList;
        public List<BeerStyle> StyleList
        {
            get { return _styleList; }
            set 
            {
                _styleList = value;
                NotifyOfPropertyChange(() => StyleList);
            }
        }

        private bool _bluetoothEnabled;
        public bool BluetoothEnabled
        {
            get { return _bluetoothEnabled; }
            set 
            { 
                _bluetoothEnabled = value;
                NotifyOfPropertyChange(() => BluetoothEnabled);
            }
        }

        private bool _wifiEnabled;
        public bool WifiEnabled
        {
            get { return _wifiEnabled; }
            set { 
                _wifiEnabled = value;
                NotifyOfPropertyChange(() => WifiEnabled);
            }
        }

        private string _breweryIP;
        public string breweryIP
        {
            get { return _breweryIP; }
            set 
            {
                _breweryIP = value;
                NotifyOfPropertyChange(() => breweryIP);
            }
        }

        #endregion

        public BrewerySettingsViewModel(IEventAggregator events)
        {
            // Subscribe to events
            _events = events;
            _events.Subscribe(this);

            // Equipment
            BatchSize = Properties.Settings.Default.BatchSize;
            HeaterEffect = Properties.Settings.Default.HeaterEffect;
            FermenterLoss = Properties.Settings.Default.FermentertLoss;
            KettleLoss = Properties.Settings.Default.KettleLoss;

            // General
            MashThickness = Properties.Settings.Default.MashThickness;
            GrainAbsorption = Properties.Settings.Default.GrainAbsorption;
            CoolingShrinkage = Properties.Settings.Default.CooldownShrinkage;
            EvaporationRate = Properties.Settings.Default.EvaporationRate;
            PumpOnDuration = Properties.Settings.Default.PumpOnDuration;

            // Equipment
            prevBatchSize = Properties.Settings.Default.BatchSize;
            prevHeaterEffect = Properties.Settings.Default.HeaterEffect;
            prevFermenterLoss = Properties.Settings.Default.FermentertLoss;
            prevKettleLoss = Properties.Settings.Default.KettleLoss;

            prevMashThickness = Properties.Settings.Default.MashThickness;
            prevGrainAbsorption = Properties.Settings.Default.GrainAbsorption;
            prevCoolingShrinkage = Properties.Settings.Default.CooldownShrinkage;
            prevEvaporationRate = Properties.Settings.Default.EvaporationRate;
            prevPumpOnDuration = Properties.Settings.Default.PumpOnDuration;

            HopList = new List<Hops>(FileInteraction.HopsFromDB());
            GrainList = new List<Grain>(FileInteraction.GrainsFromDB());
            YeastList = new List<Yeast>(FileInteraction.YeastsFromDB());
            StyleList = new List<BeerStyle>(FileInteraction.StylesFromDB());

            if (Properties.Settings.Default.ConnectionType == "Bluetooth")
            {
                BluetoothEnabled = true;
                WifiEnabled = false;
            }
            else
            {
                BluetoothEnabled = false;
                WifiEnabled = true;
            }

            breweryIP = Properties.Settings.Default.breweryIP;
        }

        #region UI Methods
        public void CancelSettings()
        {
            // Equipment
            BatchSize = prevBatchSize;
            HeaterEffect = prevHeaterEffect;
            FermenterLoss = prevFermenterLoss;
            KettleLoss = prevKettleLoss;

            // General
            MashThickness = prevMashThickness;
            GrainAbsorption = prevGrainAbsorption;
            CoolingShrinkage = prevCoolingShrinkage;
            EvaporationRate = prevEvaporationRate;
            PumpOnDuration = prevPumpOnDuration;
            PumpOffDuration = prevPumpOffDuration;

            this.TryClose();
        }

        public void ResetSettings()
        {
            MessageBoxResult answer = MessageBox.Show("This will delete all settings and restore them to default. Continue?", "Caution", MessageBoxButton.YesNo);
            if (answer == MessageBoxResult.Yes)
            {
                // Equipment
                BatchSize = Properties.Settings.Default.STD_BatchSize;
                HeaterEffect = Properties.Settings.Default.STD_HeaterEffect;
                FermenterLoss = Properties.Settings.Default.STD_FermenterLoss;
                KettleLoss = Properties.Settings.Default.STD_KettleLoss;

                // General
                MashThickness = Properties.Settings.Default.STD_MashThickness;
                GrainAbsorption = Properties.Settings.Default.STD_GrainAbsorption;
                CoolingShrinkage = Properties.Settings.Default.STD_CooldownShrinkage;
                EvaporationRate = Properties.Settings.Default.STD_EvaporationRate;
                PumpOnDuration = Properties.Settings.Default.STD_PumpOnDuration;

                // Equipment
                Properties.Settings.Default.BatchSize = BatchSize;
                Properties.Settings.Default.HeaterEffect = HeaterEffect;
                Properties.Settings.Default.FermentertLoss = FermenterLoss;
                Properties.Settings.Default.KettleLoss = KettleLoss;

                // General
                Properties.Settings.Default.MashThickness = MashThickness;
                Properties.Settings.Default.GrainAbsorption = GrainAbsorption;
                Properties.Settings.Default.CooldownShrinkage = CoolingShrinkage;
                Properties.Settings.Default.EvaporationRate = EvaporationRate;
                Properties.Settings.Default.PumpOnDuration = PumpOnDuration;
                Properties.Settings.Default.Save();
                Properties.Settings.Default.Reload();

                // Equipment
                prevBatchSize = Properties.Settings.Default.BatchSize;
                prevHeaterEffect = Properties.Settings.Default.HeaterEffect;
                prevFermenterLoss = Properties.Settings.Default.FermentertLoss;
                prevKettleLoss = Properties.Settings.Default.KettleLoss;

                // General
                prevMashThickness = Properties.Settings.Default.MashThickness;
                prevGrainAbsorption = Properties.Settings.Default.GrainAbsorption;
                prevCoolingShrinkage = Properties.Settings.Default.CooldownShrinkage;
                prevEvaporationRate = Properties.Settings.Default.EvaporationRate;
                prevPumpOnDuration = Properties.Settings.Default.PumpOnDuration;

                MessageBox.Show("All settings have been reset to their default values.");

                this.TryClose();
            }
        }

        public void ApplySettings()
        {
            // Equipment
            Properties.Settings.Default.BatchSize = BatchSize;
            Properties.Settings.Default.HeaterEffect = HeaterEffect;
            Properties.Settings.Default.FermentertLoss = FermenterLoss;
            Properties.Settings.Default.KettleLoss = KettleLoss;

            // General
            Properties.Settings.Default.MashThickness = MashThickness;
            Properties.Settings.Default.GrainAbsorption = GrainAbsorption;
            Properties.Settings.Default.CooldownShrinkage = CoolingShrinkage;
            Properties.Settings.Default.EvaporationRate = EvaporationRate;
            Properties.Settings.Default.PumpOnDuration = PumpOnDuration;

            if (sessionRunning)
            {
                MessageBox.Show("Cannot update connection settings during ongoing session.", "Caution");
            }
            else
            {
                if (!BluetoothEnabled && Properties.Settings.Default.ConnectionType == "Bluetooth" || BluetoothEnabled && Properties.Settings.Default.ConnectionType == "Wifi") // User has updated connection settings
                {
                    if (BluetoothEnabled)
                    {
                        Properties.Settings.Default.ConnectionType = "Bluetooth";
                    }
                    else
                    {
                        Properties.Settings.Default.ConnectionType = "Wifi";
                    }

                    // Check if this is changed while the system is connected. If so, offer to update the setting in arduino as well and restart it.
                    if (connectionStatus == ConnectionStatus.Connected)
                    {
                        MessageBox.Show("The software is currently connected to the brewery. Please reconnect manually after changes are applied.");
                        _events.PublishOnUIThread(new SerialToSendEvent { arduinoMessage = new ArduinoMessage { AIndex = 'C', AMessage = Properties.Settings.Default.ConnectionType } });
                        _events.PublishOnUIThread(new ConnectionEvent { ConnectionStatus = ConnectionStatus.Disconnected });
                    }
                }
            }
            
            if(breweryIP != Properties.Settings.Default.breweryIP)
            {
                Properties.Settings.Default.breweryIP = breweryIP;
            }
            
            Properties.Settings.Default.Save();
            Properties.Settings.Default.Reload();
            MessageBox.Show("Settings saved!");
            _events.PublishOnUIThread(new SettingsUpdatedEvent { brewerySettings = new BrewerySettings()});
            this.TryClose();
        }

        public void ImportIngredients()
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "Beersmith (*.bsmx)|*.bsmx";
            openDialog.FilterIndex = 1;

            if (openDialog.ShowDialog() == true)
            {
                string importText = File.ReadAllText(openDialog.FileName);
                string DBText;

                try
                {
                    if (importText.Contains("<Hops>"))
                    {
                        FileInteraction.HopsToDB(new List<Hops>(FileInteraction.ImportHopsList(importText)));
                        _events.PublishOnUIThread(new DatabaseUpdatedEvent { dataType = "Hops" });
                    }

                    else if (importText.Contains("<Grain>"))
                    {
                        FileInteraction.GrainsToDB(new List<Grain>(FileInteraction.ImportGrainList(importText)));
                        _events.PublishOnUIThread(new DatabaseUpdatedEvent { dataType = "Grains" });
                    }

                    else if (importText.Contains("<Yeast>"))
                    {
                        FileInteraction.YeastToDB(new List<Yeast>(FileInteraction.ImportYeastList(importText)));
                        _events.PublishOnUIThread(new DatabaseUpdatedEvent { dataType = "Yeasts" });
                    }
                }
                catch(Exception e)
                {
                    MessageBox.Show(e.Message, "Error");
                    return;
                }
            }
        }

        public void ImportEquipment()
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "Beersmith (*.bsmx)|*.bsmx";
            openDialog.FilterIndex = 1;

            if(openDialog.ShowDialog() == true)
            {
                string importText = File.ReadAllText(openDialog.FileName);
                string DBText;

                try
                {
                    if (importText.Contains("<Style>"))
                    {
                        FileInteraction.StylesToDB(new List<BeerStyle>(FileInteraction.ImportStyleList(importText)));
                        _events.PublishOnUIThread(new DatabaseUpdatedEvent { dataType = "Styles" });
                    }
                }
                catch(Exception e)
                {
                    MessageBox.Show(e.Message, "Error");
                    return;
                }
            }
        }
        #endregion

        #region Methods

        #endregion

        #region Event handlers
        public void Handle(DatabaseUpdatedEvent message)
        {
            switch (message.dataType)
            {
                case "Hops":
                    HopList = FileInteraction.HopsFromDB();
                    break;
                case "Grains":
                    GrainList = FileInteraction.GrainsFromDB();
                    break;
                case "Yeasts":
                    YeastList = FileInteraction.YeastsFromDB();
                    break;
                case "Styles":
                    StyleList = FileInteraction.StylesFromDB();
                    break;
            }
        }

        public void Handle(ConnectionEvent message)
        {
            connectionStatus = message.ConnectionStatus;
        }

        public void Handle(SessionRunningEvent message)
        {
            sessionRunning = message.SessionRunning;
        }
        #endregion
    }
}
