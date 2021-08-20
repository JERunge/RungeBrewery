using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using BrewUI.EventModels;
using BrewUI.Items;
using Caliburn.Micro;

namespace BrewUI.ViewModels
{
    public class AddGrainsViewModel : Screen, IHandle<AddingGrainsEvent>
    {
        private IEventAggregator _events;

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

        private bool _grainsAdded;
        public bool GrainsAdded
        {
            get { return _grainsAdded; }
            set
            { 
                _grainsAdded = value;
                NotifyOfPropertyChange(() => GrainsAdded);
            }
        }

        public DispatcherTimer timer { get; set; }

        public AddGrainsViewModel(IEventAggregator events)
        {
            // Subscribe to events
            _events = events;
            _events.Subscribe(this);

            GrainList = new ObservableCollection<Grain>();
            GrainsAdded = false;

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(200);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            GrainsAdded = true;
            foreach(Grain grain in GrainList)
            {
                if(grain.added == false)
                {
                    GrainsAdded = false;
                }
            }
        }

        public void Continue()
        {
            timer.Stop();
            _events.PublishOnUIThread(new AddingGrainsEvent { grainsAdded = true });
            this.TryClose();
        }

        public void Handle(AddingGrainsEvent message)
        {
            if(message.grainsAdded != true)
            {
                GrainList = message.grainList;
            }
            else
            {
                GrainList.Clear();
            }
        }

    }
}
