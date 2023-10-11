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
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if(txtPassword.Text == string.Empty)
            {
                MessageBox.Show("Vui lòng nhập mật khẩu");
                txtPassword.Focus();
                return;
            }

            int result = bdu.checkEmailAndPassword(txtEmail.Text, txtPassword.Text);
            if (result == -1)
            {
                MessageBox.Show("Email không tồn tại");
                txtEmail.Focus();
                return;
            }  

            if(result == 0)
            {
                MessageBox.Show("Mật khẩu không chính xác");
                txtPassword.Focus();
                return;
            }

            Program.currentUser = bdu.getInfo(txtEmail.Text);
            frmMain frm = new frmMain();
            this.Hide();
            frm.ShowDialog();
        }

        private void frmLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
