using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace RenderUI
{
    public class AppUI
    {
        Control ctr;

        public AppUI(Control ctr)
        {
            this.ctr = ctr;
        }

        public void renderLabel(string text, string name, int leftPos, int topPos)
        {
            Label lbl = new Label();
            lbl.Name = name;
            lbl.Text = text;
            lbl.Left = leftPos;
            lbl.Top = topPos;

            lbl.Font = new Font("Arial", 16);
            lbl.Width = 50 + text.Length * 9;
            ctr.Controls.Add(lbl);
        }

        public void renderTextBox(string name, int width, int leftPos, int topPos, bool readOnly = false)
        {
            TextBox txt = new TextBox();
            txt.Name = name;
            txt.Left = leftPos;
            txt.Top = topPos;

            txt.Width = width;
            txt.Font = new Font("Arial", 16);
            txt.ReadOnly = readOnly;
            ctr.Controls.Add(txt);
        }
    }
}
