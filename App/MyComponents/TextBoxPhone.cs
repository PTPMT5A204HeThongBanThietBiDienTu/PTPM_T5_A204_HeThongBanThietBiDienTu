using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyComponents
{
    public class TextBoxPhone : TextBox
    {
        public TextBoxPhone()
        {
            this.KeyPress += TextBoxPhone_KeyPress;
            this.LostFocus += TextBoxPhone_LostFocus;
        }

        private void TextBoxPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void TextBoxPhone_LostFocus(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;
            if (txt.Text.Length != 10)
            {
                MessageBox.Show("Số điện thoại không hợp lệ.");
                this.Focus();
            }
        }
    }
}
