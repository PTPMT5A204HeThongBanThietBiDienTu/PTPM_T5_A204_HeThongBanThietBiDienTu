using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyComponents
{
    public class TextBoxNumber : TextBox
    {
        public TextBoxNumber()
        {
            this.KeyPress += TextBoxNumber_KeyPress;
        }

        private void TextBoxNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }
    }
}
