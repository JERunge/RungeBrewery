using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrewUI.EventModels
{
    public class DebugDataUpdatedEvent
    {
        public string index { get; set; }
        public string stringValue { get; set; }
        public bool boolValue { get; set; }
    }
}
