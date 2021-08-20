using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrewUI.Items
{
    public class Hops
    {
        public bool Remove { get; set; }
        public string Name { get; set; }
        public string Origin { get; set; }
        public string Alpha { get; set; }
        public string Beta { get; set; }
        public string Notes { get; set; }
        public double Amount { get; set; }
        public TimeSpan BoilTime { get; set; }
    }
}
