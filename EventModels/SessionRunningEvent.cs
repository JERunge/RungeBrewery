using BrewUI.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrewUI.EventModels
{
    public class SessionRunningEvent
    {
        public bool SessionRunning { get; set; }
        public BreweryRecipe BreweryRecipe { get; set; }
    }
}
