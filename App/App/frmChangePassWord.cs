using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL_DAL;

namespace App
{
    public partial class frmChangePassWord : Form
    {
        BLL_DAL_User bdu = new BLL_DAL_User();

        public frmChangePassWord()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(txtPassOld.Text == string.Empty)
            {
                MessageBox.Show("Vui lòng nhập mật khẩu cũ");
                txtPassOld.Focus();
                return;
            }

            if (txtPassNew.Text == string.Empty)
            {
                MessageBox.Show("Vui lòng nhập mật khẩu mới");
                txtPassNew.Focus();
                return;
            }
            else
            {
                if(txtPassNew.Text.Length < 5)
                {
                    MessageBox.Show("Mật khẩu mới phải chứa ít nhất 5 kí tự");
                    txtPassNew.Focus();
                    return;
                }
            }

            int result = bdu.updatePassword(Program.currentUser.id, txtPassOld.Text, txtPassNew.Text);
            if(result == 1) 
            { 
                MessageBox.Show("Lưu thành công");
                txtPassOld.Text = string.Empty;
                txtPassNew.Text = string.Empty;
                this.Close();
            }
            else
            {
                if (result == 2)
                {
                    MessageBox.Show("Mật khẩu cũ không đúng");
                    txtPassOld.Focus();
                }
                else
                    MessageBox.Show("Lưu thất bại");
            }    
        }
    }
}
