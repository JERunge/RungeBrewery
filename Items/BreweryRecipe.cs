using BrewUI.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrewUI.Items
{
    public class BreweryRecipe
    {
        public SessionInfo sessionInfo { get; set; }
        public ObservableCollection<Grain> grainList { get; set; }
        public ObservableCollection<MashStep> mashSteps { get; set; }
        public SpargeStep spargeStep { get; set; }
        public ObservableCollection<Hops> hopsList { get; set; }
    }
}
