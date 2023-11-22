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
    public class BrandUI
    {
        Control ctr;
        BLL_DAL_Brand bdb = new BLL_DAL_Brand();
        BLL_DAL_Product bdp = new BLL_DAL_Product();

        public BrandUI(Control ctr)
        {
            this.ctr = ctr;
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
                case "btnAdd":
                    {
                        handleInsert();
                    }
                    break;
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

            dtgv.Width = 650;
            dtgv.Height = 300;
            dtgv.Font = new Font("Arial", 15);
            dtgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dtgv.MultiSelect = false;
            dtgv.CellMouseClick += dtgv_CellMouseClick;
            ctr.Controls.Add(dtgv);

            loadData(dtgv);
        }

        private void dtgv_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dtgv = (DataGridView)sender;


            TextBox txtId = (TextBox)ctr.Controls.Find("txtBrandId", false)[0];
            txtId.Text = dtgv.CurrentRow.Cells[0].Value.ToString();

            TextBox txtName = (TextBox)ctr.Controls.Find("txtBrandName", false)[0];
            txtName.Text = dtgv.CurrentRow.Cells[1].Value.ToString();
        }

        private void loadData(DataGridView dtgv)
        {
            dtgv.DataSource = bdb.getAll();

            dtgv.ColumnHeadersHeight = 50;

            dtgv.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dtgv.Columns[0].HeaderCell.Style.Font = new Font("Arial", 16, FontStyle.Bold);
            dtgv.Columns[0].Width = 280;
            dtgv.Columns[0].HeaderText = "Mã thương hiệu";
            
            dtgv.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dtgv.Columns[1].HeaderCell.Style.Font = new Font("Arial", 16, FontStyle.Bold);
            dtgv.Columns[1].Width = 326;
            dtgv.Columns[1].HeaderText = "Tên thương hiệu";
        }

        private void handleInsert()
        {
            TextBox txtId = (TextBox)ctr.Controls.Find("txtBrandId", false)[0];
            TextBox txtName = (TextBox)ctr.Controls.Find("txtBrandName", false)[0];
            DataGridView dtgv = (DataGridView)ctr.Controls.Find("dtgvBrand", false)[0];

            if (txtName.Text == string.Empty)
            {
                MessageBox.Show("Vui lòng nhập tên thương hiệu");
                txtName.Focus();
                return;
            }

            if (bdb.is_ExistsName(txtName.Text))
            {
                MessageBox.Show("Tên thương hiệu đã tồn tại");
                txtName.Focus();
                return;
            }

            Brand brand = new Brand();
            brand.id = Guid.NewGuid().ToString();
            brand.name = txtName.Text;

            int result = bdb.insert(brand);
            if (result == 0)
                MessageBox.Show("Thêm thất bại");
            else
            {
                MessageBox.Show("Thêm thành công");
                loadData(dtgv);

                txtId.Text = string.Empty;
                txtName.Text = string.Empty;
            }
        }

        private void handleUpdate()
        {
            TextBox txtId = (TextBox)ctr.Controls.Find("txtBrandId", false)[0];
            TextBox txtName = (TextBox)ctr.Controls.Find("txtBrandName", false)[0];
            DataGridView dtgv = (DataGridView)ctr.Controls.Find("dtgvBrand", false)[0];

            if (txtName.Text == string.Empty)
            {
                MessageBox.Show("Vui lòng nhập tên thương hiệu");
                txtName.Focus();
                return;
            }

            if (txtId.Text == string.Empty)
            {
                MessageBox.Show("Vui lòng chọn thương hiệu cần chỉnh sửa");
                return;
            }

            string brandName = dtgv.CurrentRow.Cells[1].Value.ToString();
            if (txtName.Text != brandName)
            {
                if (bdb.is_ExistsName(txtName.Text))
                {
                    MessageBox.Show("Tên thương hiệu đã tồn tại");
                    return;
                }
            }

            Brand brand = new Brand();
            brand.id = txtId.Text;
            brand.name = txtName.Text;

            int result = bdb.update(brand);
            if (result == 0)
                MessageBox.Show("Sửa thất bại");
            else
            {
                MessageBox.Show("Sửa thành công");
                loadData(dtgv);

                txtId.Text = string.Empty;
                txtName.Text = string.Empty;
            }
        }

        private void handleDelete()
        {
            TextBox txtId = (TextBox)ctr.Controls.Find("txtBrandId", false)[0];
            TextBox txtName = (TextBox)ctr.Controls.Find("txtBrandName", false)[0];
            DataGridView dtgv = (DataGridView)ctr.Controls.Find("dtgvBrand", false)[0];

            if (txtId.Text == string.Empty)
            {
                MessageBox.Show("Vui lòng chọn thương hiệu cần xóa");
                return;
            }

            DialogResult dialogResult = MessageBox.Show("Bạn có chắc là muốn xóa thương biệu này ?", "Thông báo", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                List<Product> products = bdp.getAllByBrandId(txtId.Text);
                if (products.Count != 0)
                {
                    MessageBox.Show("Không thể xóa thương hiệu này");
                    return;
                }

                int result = bdb.delete(txtId.Text);
                if (result == 0)
                    MessageBox.Show("Xóa thất bại");
                else
                {
                    MessageBox.Show("Xóa thành công");
                    loadData(dtgv);
                     
                    txtId.Text = string.Empty;
                    txtName.Text = string.Empty;
                }
            }
        }
    }
}
