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
    public partial class frmCustomer : Form
    {
        BLL_DAL_Customer bdc = new BLL_DAL_Customer();

        public frmCustomer()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(txtCusName.Text == string.Empty)
            {
                MessageBox.Show("Vui lòng nhập tên khách hàng");
                txtCusName.Focus();
                return;
            }

            if(txtPhone.Text == string.Empty)
            {
                MessageBox.Show("Vui lòng nhập số điện thoại");
                txtPhone.Focus();
                return;
            }

            Customer cus = new Customer();
            cus.id = Guid.NewGuid().ToString();
            cus.name = txtCusName.Text;
            cus.phone = txtPhone.Text;
            cus.address = txtAddress.Text;

            int result = bdc.insert(cus);
            if (result == 1)
            {
                Program.cusId = cus.id;
                this.Close();
            }
            else
                MessageBox.Show("Lưu không thành công");
        }
    }
}
