using System;
using System.Windows.Threading;
using Caliburn.Micro;

namespace BrewUI.Items
{
    public class SpargeStep : Conductor<object>
    {
        public double spargeTemp { get; set; }
        public TimeSpan spargeDur { get; set; }

        public DateTime startTime { get; set; }

        public DateTime endTime { get; set; }

        private string _status;
        public string Status
        {
            get { return _status; }
            set
            {
                _status = value;
                NotifyOfPropertyChange(() => Status);
            }
        }
    }
}
