using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrewUI.EventModels
{
    public class ConnectionEvent : Conductor<object>
    {
        private string _connectionStatus;

        public string ConnectionStatus
        {
            get { return _connectionStatus; }
            set 
            {
                _connectionStatus = value;
                NotifyOfPropertyChange(() => ConnectionStatus);
            }
        }

    }
}
