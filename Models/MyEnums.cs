using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrewUI.Models
{
    public static class MyEnums
    {
        public enum ConnectionStatus
        {
            Disconnected,
            Searching,
            Connecting,
            Connected,
            Reconnecting
        }

        public enum Sound
        {
            Finished,
            Error
        }
    }
}
