using BrewUI.Items;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrewUI.Data
{
    public class SessionInfo : Conductor<object>
    {

        public string sessionName { get; set; }

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

        public string BrewMethod { get; set; }
        public BeerStyle style { get; set; }

    }
}
