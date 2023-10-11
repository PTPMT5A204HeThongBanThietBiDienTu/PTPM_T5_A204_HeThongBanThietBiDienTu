using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyComponents
{
    public class TextBoxEmail:TextBox
    {
        public TextBoxEmail()
        {
            this.LostFocus += TextBoxEmail_LostFocus;
        }

        private void TextBoxEmail_LostFocus(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;
            if (txt.Text == string.Empty)
            {
                MessageBox.Show("Email không được bỏ trống");
                this.Focus();
            }
            else
            {
                string str = txt.Text;
                if (str.StartsWith(".com") || str.StartsWith("@") || !str.EndsWith(".com") || str.EndsWith("@.com") || !str.Contains("@"))
                    MessageBox.Show("Email không hợp lệ");
            }
        }
    }
}
