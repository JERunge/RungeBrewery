using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace BrewUI.Items
{
    public class BoilStep : Conductor<object>
    {
        public TimeSpan boilTime { get; set; }
        public ObservableCollection<Hops>  hopsList { get; set; }

        private bool _added;
        public bool added
        {
            get { return _added; }
            set 
            {
                _added = value;
                NotifyOfPropertyChange(() => added);
            }
        }

    }
}
