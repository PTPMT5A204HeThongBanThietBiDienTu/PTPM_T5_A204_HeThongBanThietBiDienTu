using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace App
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();

            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            Program.currentUser = null;
            frmLogin frm = new frmLogin();
            this.Hide();
            frm.ShowDialog();
        }

        private void logout_Click(object sender, EventArgs e)
        {
            Program.currentUser = null;
            frmLogin frm = new frmLogin();
            this.Hide();
            frm.ShowDialog();
        }

        private void SR002_Click(object sender, EventArgs e)
        {
            frmBrand frm = new frmBrand();
            this.Hide();
            frm.ShowDialog();
        }

        private void SR003_Click(object sender, EventArgs e)
        {
            frmCategory frm = new frmCategory();
            this.Hide();
            frm.ShowDialog();
        }

        private void SR005_Click(object sender, EventArgs e)
        {
            frmProductSale frm = new frmProductSale();
            this.Hide();
            frm.ShowDialog();
        }
    }
}
