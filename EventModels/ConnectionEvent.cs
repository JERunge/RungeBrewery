using BrewUI.Models;
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
        private MyEnums.ConnectionStatus _connectionStatus;
        public MyEnums.ConnectionStatus ConnectionStatus
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
