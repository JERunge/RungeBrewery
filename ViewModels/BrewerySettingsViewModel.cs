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

namespace BrewUI.ViewModels
{
    public class BrewerySettingsViewModel : Screen, IHandle<DatabaseUpdatedEvent>
    {
        #region Variables
        private double prevBatchSize { get; set; }
        private double prevHeaterEffect { get; set; }
        private double prevTapTemp { get; set; }
        private double prevEquipmentLoss { get; set; }
        private double prevMashThickness { get; set; }
        private double prevGrainAbsorption { get; set; }
        private double prevPumpOnDuration { get; set; }
        private double prevPumpOffDuration { get; set; }
        private readonly string pathHopsDB;
        private IEventAggregator _events;

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

        private double _tapTemp;
        public double TapTemp
        {
            get { return _tapTemp; }
            set
            {
                _tapTemp = value;
                NotifyOfPropertyChange(() => TapTemp);
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

        #endregion

        public BrewerySettingsViewModel(IEventAggregator events)
        {
            // Subscribe to events
            _events = events;
            _events.Subscribe(this);

            BatchSize = Properties.Settings.Default.BatchSize;
            HeaterEffect = Properties.Settings.Default.HeaterEffect;
            TapTemp = Properties.Settings.Default.TapTemp;
            EquipmentLoss = Properties.Settings.Default.EquipmentLoss;
            MashThickness = Properties.Settings.Default.MashThickness;
            GrainAbsorption = Properties.Settings.Default.GrainAbsorption;
            PumpOnDuration = Properties.Settings.Default.PumpOnDuration;
            PumpOffDuration = Properties.Settings.Default.PumpOffDuration;

            prevBatchSize = Properties.Settings.Default.BatchSize;
            prevHeaterEffect = Properties.Settings.Default.HeaterEffect;
            prevTapTemp = Properties.Settings.Default.TapTemp;
            prevEquipmentLoss = Properties.Settings.Default.EquipmentLoss;
            prevMashThickness = Properties.Settings.Default.MashThickness;
            prevGrainAbsorption = Properties.Settings.Default.GrainAbsorption;
            prevPumpOffDuration = Properties.Settings.Default.PumpOffDuration;
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
        }

        #region UI Methods
        public void CancelSettings()
        {
            BatchSize = prevBatchSize;
            HeaterEffect = prevHeaterEffect;
            TapTemp = prevTapTemp;
            EquipmentLoss = prevEquipmentLoss;
            MashThickness = prevMashThickness;
            GrainAbsorption = prevGrainAbsorption;
            PumpOnDuration = prevPumpOnDuration;
            PumpOffDuration = prevPumpOffDuration;
            this.TryClose();
        }

        public void ResetSettings()
        {
            MessageBoxResult answer = MessageBox.Show("This will delete all settings and restore them to default. Continue?", "Caution", MessageBoxButton.YesNo);
            if (answer == MessageBoxResult.Yes)
            {
                BatchSize = Properties.Settings.Default.STD_BatchSize;
                HeaterEffect = Properties.Settings.Default.STD_HeaterEffect;
                TapTemp = Properties.Settings.Default.STD_TapTemp;
                EquipmentLoss = Properties.Settings.Default.STD_EquipmentLoss;
                MashThickness = Properties.Settings.Default.STD_MashThickness;
                GrainAbsorption = Properties.Settings.Default.STD_GrainAbsorption;
                PumpOnDuration = Properties.Settings.Default.STD_PumpOnDuration;
                PumpOffDuration = Properties.Settings.Default.STD_PumpOffDuration;

                Properties.Settings.Default.BatchSize = BatchSize;
                Properties.Settings.Default.HeaterEffect = HeaterEffect;
                Properties.Settings.Default.TapTemp = TapTemp;
                Properties.Settings.Default.EquipmentLoss = EquipmentLoss;
                Properties.Settings.Default.MashThickness = MashThickness;
                Properties.Settings.Default.GrainAbsorption = GrainAbsorption;
                Properties.Settings.Default.PumpOnDuration = PumpOnDuration;
                Properties.Settings.Default.PumpOffDuration = PumpOffDuration;
                Properties.Settings.Default.Save();

                prevBatchSize = Properties.Settings.Default.BatchSize;
                prevHeaterEffect = Properties.Settings.Default.HeaterEffect;
                prevTapTemp = Properties.Settings.Default.TapTemp;
                prevEquipmentLoss = Properties.Settings.Default.EquipmentLoss;
                prevMashThickness = Properties.Settings.Default.MashThickness;
                prevGrainAbsorption = Properties.Settings.Default.GrainAbsorption;
                prevPumpOnDuration = Properties.Settings.Default.PumpOnDuration;
                prevPumpOffDuration = Properties.Settings.Default.PumpOffDuration;

                MessageBox.Show("All settings have been reset to their default values.");

                this.TryClose();
            }
        }

        public void ApplySettings()
        {
            Properties.Settings.Default.BatchSize = BatchSize;
            Properties.Settings.Default.HeaterEffect = HeaterEffect;
            Properties.Settings.Default.TapTemp = TapTemp;
            Properties.Settings.Default.EquipmentLoss = EquipmentLoss;
            Properties.Settings.Default.MashThickness = MashThickness;
            Properties.Settings.Default.GrainAbsorption = GrainAbsorption;
            Properties.Settings.Default.PumpOnDuration = PumpOnDuration;
            Properties.Settings.Default.PumpOffDuration = PumpOffDuration;
            
            if (BluetoothEnabled)
            {
                Properties.Settings.Default.ConnectionType = "Bluetooth";
            }
            else
            {
                Properties.Settings.Default.ConnectionType = "Wifi";
            }
            Properties.Settings.Default.Save();
            MessageBox.Show("Settings saved.");
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
        #endregion
    }
}
