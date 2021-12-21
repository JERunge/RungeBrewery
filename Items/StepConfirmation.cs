using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrewUI.Models
{
    public class StepConfirmation : Conductor<object>
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set {
                _name = value;
                NotifyOfPropertyChange(() => Name);
            }
        }

        private string _content;
        public string Content
        {
            get { return _content; }
            set { 
                _content = value;
                NotifyOfPropertyChange(() => Content);
            }
        }

        private bool _checked;
        public bool Checked
        {
            get { return _checked; }
            set {
                _checked = value;
                NotifyOfPropertyChange(() => Checked);
            }
        }
    }
}
