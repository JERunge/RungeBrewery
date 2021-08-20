using BrewUI.Items;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrewUI.EventModels
{
    public class AddingGrainsEvent
    {
        public ObservableCollection<Grain> grainList { get; set; }
        public bool grainsAdded { get; set; }
    }
}
