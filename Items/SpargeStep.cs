using System;
using System.Windows.Threading;
using Caliburn.Micro;

namespace BrewUI.Items
{
    public class SpargeStep : Conductor<object>
    {
        public double spargeTemp { get; set; }
        public double spargeWaterAmount { get; set; }
        public int spargeDur { get; set; }
    }
}
