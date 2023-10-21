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
    public partial class frmPermission : Form
    {
        BLL_DAL_Permission bdp = new BLL_DAL_Permission();
        public frmPermission()
        {
            InitializeComponent();
        }

        private void frmPermission_Load(object sender, EventArgs e)
        {
            dtgvPermission.DataSource = bdp.getAll();
            dtgvPermission.Columns[4].Visible = false;
        }
    }
}
