using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrewUI.Items
{
    public class Equipment
    {
        public string name { get; set; } // Name of equipment
        public double batchVolume { get; set; } // Batch volume excluding fermenter loss
        public double fermenterLoss { get; set; } // Amount of vort left in fermenter
        public double bottlingVolume { get; set; } // Amount of resulting beer
        public double boilerLoss { get; set; } // How much vort is left in boiler
        public double coolingShrinkage { get; set; } // How much liters the vort shrinks due to cooldown
        public double postBoilVolume { get; set; }  // The amount of vort we should have after boil, before cooldown
        public double evaporationRatePerHour { get; set; } // At what rate does water evaporate during boil, per hour
        public double boilVolume { get; set; } // How much vort we should have at the start of the boil
        public double breweryEfficiency { get; set; } // Brewhouse efficiency including all losses
        public double mashTunVolume { get; set; }
    }
}
