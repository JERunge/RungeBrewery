using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrewUI.Items
{
    public class MashProfile
    {
        public string Name { get; set; }
        public List<MashStep> MashSteps { get; set; }
    }
}
