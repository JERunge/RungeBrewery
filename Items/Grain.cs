using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Caliburn.Micro;

namespace BrewUI.Items
{
    public class Grain : Conductor<object>
    {
        public bool remove { get; set; }
        public string name { get; set; }
        public string origin { get; set; }
        public string notes { get; set; }
        public double amount { get; set; }
        
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
