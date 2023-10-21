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
    public partial class frmCart : Form
    {
        BLL_DAL_Cart bdc = new BLL_DAL_Cart();
        BLL_DAL_Product bdp = new BLL_DAL_Product();

        public frmCart()
        {
            InitializeComponent();

            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void frmCart_Load(object sender, EventArgs e)
        {
            loadDataGridView();
        }

        private void loadDataGridView()
        {
            List<Object> carts = bdc.getAllByUserId(Program.currentUser.id);
            dtgvCart.DataSource = carts;

            if (carts.Count != 0)
            {
                dtgvCart.Columns[0].Width = 190;
                dtgvCart.Columns[0].HeaderText = "Mã sản phẩm";

                dtgvCart.Columns[1].Width = 255;
                dtgvCart.Columns[1].HeaderText = "Tên sản phẩm";

                dtgvCart.Columns[2].HeaderText = "Số lượng";

                txtProId.Text = dtgvCart.CurrentRow.Cells[0].Value.ToString();
                txtProName.Text = dtgvCart.CurrentRow.Cells[1].Value.ToString();
                txtQuantity.Text = dtgvCart.CurrentRow.Cells[2].Value.ToString();
            }
        }

        private void dtgvCart_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            txtProId.Text = dtgvCart.CurrentRow.Cells[0].Value.ToString();
            txtProName.Text = dtgvCart.CurrentRow.Cells[1].Value.ToString();
            txtQuantity.Text = dtgvCart.CurrentRow.Cells[2].Value.ToString();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmProductSale frm = new frmProductSale();
            this.Hide();
            frm.ShowDialog();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if(txtQuantity.Text == string.Empty)
            {
                MessageBox.Show("Vui lòng nhập số lượng sản phẩm");
                return;
            }

            int quantity = int.Parse(txtQuantity.Text);
            if(quantity < 1)
            {
                MessageBox.Show("Số lượng sản phẩm tối thiểu là 1");
                return;
            }

            int numberInStock = bdp.getQuantityOfProduct(txtProId.Text);
            if(quantity > numberInStock)
            {
                MessageBox.Show("Số lượng vượt quá số lượng hàng trong kho");
                return;
            }

            Cart cart = new Cart();
            cart.id = Guid.NewGuid().ToString();
            cart.proId = txtProId.Text;
            cart.userId = Program.currentUser.id;
            cart.quantity = quantity;

            int result = bdc.update(cart);
            if (result == 1)
            {
                MessageBox.Show("Sửa thành công");
                loadDataGridView();
            }
            else
                MessageBox.Show("Sửa thất bại");
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int n = dtgvCart.SelectedRows.Count;
            for(int i = 0; i < n; i++)
            {
                int index = dtgvCart.SelectedRows[i].Index;
                Console.WriteLine(dtgvCart.Rows[index].Cells[1].Value.ToString());
            }
        }
    }
}
