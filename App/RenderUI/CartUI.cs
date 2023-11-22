using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using BLL_DAL;

namespace RenderUI
{
    public class CartUI
    {
        Control ctr;
        string userId;
        BLL_DAL_Cart bdc = new BLL_DAL_Cart();
        BLL_DAL_Product bdp = new BLL_DAL_Product();
        BLL_DAL_Bill bdb = new BLL_DAL_Bill();
        BLL_DAL_BillProduct bdbP = new BLL_DAL_BillProduct();

        public CartUI(Control ctr, string userId)
        {
            this.ctr = ctr;
            this.userId = userId;
        }

        public void renderButton(string text, string name, int leftPos, int topPos)
        {
            Button btn = new Button();
            btn.Text = text;
            btn.Name = name;
            btn.Left = leftPos;
            btn.Top = topPos;

            btn.Font = new Font("Arial", 16);
            btn.Width = 70 + text.Length * 10;
            btn.Height = 40;
            btn.Click += btn_Click;
            ctr.Controls.Add(btn);
        }

        private void btn_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            switch (btn.Name)
            {
                case "btnDelete":
                    {
                        handleDelete();
                    }
                    break;
                case "btnUpdate":
                    {
                        handleUpdate();
                    }
                    break;
            }
        }

        public void renderDataGridView(string name, int leftPos, int topPos)
        {
            DataGridView dtgv = new DataGridView();
            dtgv.Name = name;
            dtgv.Left = leftPos;
            dtgv.Top = topPos;

            dtgv.Width = 770;
            dtgv.Height = 250;
            dtgv.Font = new Font("Arial", 15);
            dtgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dtgv.CellMouseClick += dtgv_CellMouseClick;
            ctr.Controls.Add(dtgv);

            loadData(dtgv);
        }

        private void dtgv_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dtgv = (DataGridView)sender;

            TextBox txtId = (TextBox)ctr.Controls.Find("txtProId", false)[0];
            txtId.Text = dtgv.CurrentRow.Cells[0].Value.ToString();

            TextBox txtName = (TextBox)ctr.Controls.Find("txtProName", false)[0];
            txtName.Text = dtgv.CurrentRow.Cells[1].Value.ToString();

            TextBox txtQuantity = (TextBox)ctr.Controls.Find("txtQuantity", false)[0];
            txtQuantity.Text = dtgv.CurrentRow.Cells[2].Value.ToString();
        }

        private void loadData(DataGridView dtgv)
        {
            List<Object> carts = bdc.getAllByUserId(userId);
            dtgv.DataSource = carts;

            if(carts.Count != 0)
            {
                dtgv.ColumnHeadersHeight = 50;

                dtgv.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dtgv.Columns[0].HeaderCell.Style.Font = new Font("Arial", 16, FontStyle.Bold);
                dtgv.Columns[0].Width = 250;
                dtgv.Columns[0].HeaderText = "Mã sản phẩm";

                dtgv.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dtgv.Columns[1].HeaderCell.Style.Font = new Font("Arial", 16, FontStyle.Bold);
                dtgv.Columns[1].Width = 326;
                dtgv.Columns[1].HeaderText = "Tên sản phẩm";

                dtgv.Columns[2].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dtgv.Columns[2].HeaderCell.Style.Font = new Font("Arial", 16, FontStyle.Bold);
                dtgv.Columns[2].Width = 150;
                dtgv.Columns[2].HeaderText = "Số lượng";
            }
        }

        private void handleUpdate()
        {
            TextBox txtProId = (TextBox)ctr.Controls.Find("txtProId", false)[0];
            TextBox txtProName = (TextBox)ctr.Controls.Find("txtProName", false)[0];
            TextBox txtQuantity = (TextBox)ctr.Controls.Find("txtQuantity", false)[0];
            DataGridView dtgv = (DataGridView)ctr.Controls.Find("dtgvCart", false)[0];

            if(txtProId.Text == string.Empty)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm cần sửa");
                return;
            }

            if (txtQuantity.Text == string.Empty)
            {
                MessageBox.Show("Vui lòng nhập số lượng sản phẩm");
                return;
            }

            int quantity = int.Parse(txtQuantity.Text);
            if (quantity < 1)
            {
                MessageBox.Show("Số lượng sản phẩm tối thiểu là 1");
                return;
            }

            int numberInStock = bdp.getQuantityOfProduct(txtProId.Text);
            if (quantity > numberInStock)
            {
                MessageBox.Show("Số lượng vượt quá số lượng hàng trong kho");
                return;
            }

            Cart cart = new Cart();
            cart.id = Guid.NewGuid().ToString();
            cart.proId = txtProId.Text;
            cart.userId = userId;
            cart.quantity = quantity;

            int result = bdc.update(cart);
            if (result == 1)
            {
                MessageBox.Show("Sửa thành công");
                loadData(dtgv);

                txtProId.Text = string.Empty;
                txtProName.Text = string.Empty;
                txtQuantity.Text = string.Empty;
            }
            else
                MessageBox.Show("Sửa thất bại");
        }

        private void handleDelete()
        {
            TextBox txtProId = (TextBox)ctr.Controls.Find("txtProId", false)[0];
            TextBox txtProName = (TextBox)ctr.Controls.Find("txtProName", false)[0];
            TextBox txtQuantity = (TextBox)ctr.Controls.Find("txtQuantity", false)[0];
            DataGridView dtgv = (DataGridView)ctr.Controls.Find("dtgvCart", false)[0];

            int n = dtgv.SelectedRows.Count;
            if (n == 0)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm cần xóa");
                return;
            }

            DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn là muốn xóa ?", "Thông báo", MessageBoxButtons.YesNo);
            if(dialogResult == DialogResult.Yes)
            {
                int result = 0;
                for (int i = 0; i < n; i++)
                {
                    int index = dtgv.SelectedRows[i].Index;
                    string proId = dtgv.Rows[index].Cells[0].Value.ToString();
                    result = bdc.delete(proId, userId);
                }

                if (result == 1)
                {
                    MessageBox.Show("Xóa thành công");
                    loadData(dtgv);

                    txtProId.Text = string.Empty;
                    txtProName.Text = string.Empty;
                    txtQuantity.Text = string.Empty;
                }
                else
                    MessageBox.Show("Xóa thất bại");
            }
        }

        public string handlePayment(DataGridView dtgv, int rowNumber)
        {
            Bill bill = new Bill();
            bill.id = Guid.NewGuid().ToString().Substring(24);
            bill.total = 0;
            bill.userId = userId;
            bill.status = "unpaid";
            bill.createdAt = DateTime.Now;
            bdb.insert(bill);

            int result = 0;
            double total = 0;
            
            for (int i = 0; i < rowNumber; i++)
            {
                string proId = dtgv.Rows[i].Cells[0].Value.ToString();
                int quantity = int.Parse(dtgv.Rows[i].Cells[2].Value.ToString());
                Product product = bdp.getByProId(proId);

                BillProduct billProduct = new BillProduct();
                billProduct.id = Guid.NewGuid().ToString();
                billProduct.proId = proId;
                billProduct.price = product.price;
                billProduct.quantity = quantity;
                billProduct.billId = bill.id;

                total += (product.price * quantity);
                result = bdbP.insert(billProduct);
            }

            if (result == 1)
                bdb.updateTotal(bill.id, total);
            return bill.id;
        }
    }
}
