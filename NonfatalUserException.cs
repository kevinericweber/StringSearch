using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StringSearch
{
    public class NonfatalUserException : Exception
    {
        private Control controlToSetFocusTo;
        public NonfatalUserException(string message, Control controlToSetFocusTo) : base(message)
        {
            this.controlToSetFocusTo = controlToSetFocusTo;
        }
        public void SetFocusOnRelevantControl()
        {
            this.controlToSetFocusTo.Focus();
        }
    }
}
