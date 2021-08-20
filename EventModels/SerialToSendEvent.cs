using BrewUI.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrewUI.EventModels
{
    public class SerialToSendEvent
    {
        public ArduinoMessage arduinoMessage { get; set; }
    }
}
