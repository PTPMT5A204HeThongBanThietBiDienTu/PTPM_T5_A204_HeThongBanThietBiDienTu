using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using MyComponents;
using BLL_DAL;

namespace RenderUI
{
    public class InfomationUI
    {
        BLL_DAL_User bdu = new BLL_DAL_User();
        Control ctr;
        User currentUser;

        public InfomationUI(Control ctr, User currentUser)
        {
            this.ctr = ctr;
            this.currentUser = currentUser;
        }

        public void renderTextBoxEmail(string name, int leftPos, int topPos)
        {
            TextBoxEmail txt = new TextBoxEmail();
            txt.Name = name;
            txt.Left = leftPos;
            txt.Top = topPos;
            txt.Width = 410;
            txt.Font = new Font("Arial", 16);
            txt.Anchor = AnchorStyles.None;
            ctr.Controls.Add(txt);
        }

        public void renderTextBoxAddress(string name, int leftPos, int topPos)
        {
            TextBox txt = new TextBox();
            txt.Name = name;
            txt.Left = leftPos;
            txt.Top = topPos;
            txt.Width = 410;
            txt.Height = 80;
            txt.Font = new Font("Arial", 16);
            txt.Multiline = true;
            ctr.Controls.Add(txt);
        }

        public void renderTextBoxPhone(string name, int leftPos, int topPos)
        {
            TextBox txt = new TextBox();
            txt.Name = name;
            txt.Left = leftPos;
            txt.Top = topPos;
            txt.KeyPress += txtPhone_KeyPress;
            txt.Width = 410;
            txt.Font = new Font("Arial", 16);
            ctr.Controls.Add(txt);
        }

        private void txtPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        public void renderButton(string name, int leftPos, int topPos)
        {
            Button btn = new Button();
            btn.Text = "Lưu thông tin";
            btn.Name = name;
            btn.Left = leftPos;
            btn.Top = topPos;
            btn.Font = new Font("Arial", 16);
            btn.Width = 200;
            btn.Height = 40;
            btn.Click += btn_Click;
            ctr.Controls.Add(btn);
        }

        private void btn_Click(object sender, EventArgs e)
        {
            TextBox txtUsername = (TextBox)ctr.Controls.Find("txtUsername", false)[0];
            TextBox txtEmail = (TextBox)ctr.Controls.Find("txtEmail", false)[0];
            TextBox txtAddress = (TextBox)ctr.Controls.Find("txtAddress", false)[0];
            TextBox txtPhone = (TextBox)ctr.Controls.Find("txtPhone", false)[0];

            if(txtUsername.Text == string.Empty)
            {
                MessageBox.Show("Vui lòng nhập họ tên");
                txtUsername.Focus();
                return;
            }

            if (txtEmail.Text == string.Empty)
            {
                MessageBox.Show("Vui lòng nhập email");
                txtEmail.Focus();
                return;
            }

            if (txtAddress.Text == string.Empty)
            {
                MessageBox.Show("Vui lòng nhập địa chỉ");
                txtAddress.Focus();
                return;
            }

            if (txtPhone.Text == string.Empty)
            {
                MessageBox.Show("Vui lòng nhập số điện thoại");
                txtPhone.Focus();
                return;
            }
            else
            {
                if(txtPhone.Text.Length != 10)
                {
                    MessageBox.Show("Số điện thoại không hợp lệ");
                    txtPhone.Focus();
                    return;
                }
            }

            if(txtEmail.Text != currentUser.email)
            {
                if (bdu.is_ExistsEmail(txtEmail.Text))
                {
                    MessageBox.Show("Email đã tồn tại");
                    txtEmail.Focus();
                    return;
                }
            }

            if(txtPhone.Text != currentUser.phone)
            {
                if (bdu.is_ExistsPhone(txtPhone.Text))
                {
                    MessageBox.Show("Số điện thoại đã tồn tại");
                    txtPhone.Focus();
                    return;
                }
            }

            User user = new User();
            user.id = currentUser.id;
            user.name = txtUsername.Text;
            user.email = txtEmail.Text;
            user.address = txtAddress.Text;
            user.phone = txtPhone.Text;

            int result = bdu.updateInfo(user);
            if (result == 1)
            {
                this.currentUser = bdu.getInfo(txtEmail.Text);
                MessageBox.Show("Lưu thông tin thành công");
                loadInfo();
            }
            else
                MessageBox.Show("Lưu thất bại");
        }

        public void loadInfo()
        {
            TextBox txtUsername = (TextBox)ctr.Controls.Find("txtUsername", false)[0];
            TextBox txtEmail = (TextBox)ctr.Controls.Find("txtEmail", false)[0];
            TextBox txtAddress = (TextBox)ctr.Controls.Find("txtAddress", false)[0];
            TextBox txtPhone = (TextBox)ctr.Controls.Find("txtPhone", false)[0];

            txtUsername.Text = currentUser.name;
            txtEmail.Text = currentUser.email;
            txtAddress.Text = currentUser.address;
            txtPhone.Text = currentUser.phone;
        }
    }
}
