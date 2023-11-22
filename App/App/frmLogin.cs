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
    public partial class frmLogin : Form
    {
        BLL_DAL_User bdu = new BLL_DAL_User();
        public frmLogin()
        {
            InitializeComponent();

            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            handleLogin();
        }

        private void handleLogin()
        {
            if (txtPassword.Text == string.Empty)
            {
                MessageBox.Show("Vui lòng nhập mật khẩu");
                txtPassword.Focus();
                return;
            }
            else
            {
                if (txtPassword.Text.Length < 5)
                {
                    MessageBox.Show("Mật khẩu phải chứa ít nhất 5 kí tự");
                    txtPassword.Focus();
                    return;
                }
            }

            int result = bdu.checkEmailAndPassword(txtEmail.Text, txtPassword.Text);
            if (result == -1)
            {
                MessageBox.Show("Email không tồn tại");
                txtEmail.Focus();
                return;
            }

            if (result == 0)
            {
                MessageBox.Show("Mật khẩu không chính xác");
                txtPassword.Focus();
                return;
            }

            if (result == 2)
            {
                MessageBox.Show("Tài khoản đã bị khóa");
                txtEmail.Focus();
                return;
            }

            Program.currentUser = bdu.getInfo(txtEmail.Text);
            frmDashboard frm = new frmDashboard();

            this.Hide();
            frm.Show();
        }

        private void frmLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(0);
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                handleLogin();
        }
    }
}
