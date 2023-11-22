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
    public class CategoryUI
    {
        Control ctr;
        BLL_DAL_Category bdc = new BLL_DAL_Category();
        BLL_DAL_Product bdp = new BLL_DAL_Product();

        public CategoryUI(Control ctr)
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


            TextBox txtId = (TextBox)ctr.Controls.Find("txtCatId", false)[0];
            txtId.Text = dtgv.CurrentRow.Cells[0].Value.ToString();

            TextBox txtName = (TextBox)ctr.Controls.Find("txtCatName", false)[0];
            txtName.Text = dtgv.CurrentRow.Cells[1].Value.ToString();
        }

        private void loadData(DataGridView dtgv)
        {
            dtgv.DataSource = bdc.getAll();

            dtgv.ColumnHeadersHeight = 50;

            dtgv.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dtgv.Columns[0].HeaderCell.Style.Font = new Font("Arial", 16, FontStyle.Bold);
            dtgv.Columns[0].Width = 280;
            dtgv.Columns[0].HeaderText = "Mã danh mục";

            dtgv.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dtgv.Columns[1].HeaderCell.Style.Font = new Font("Arial", 16, FontStyle.Bold);
            dtgv.Columns[1].Width = 326;
            dtgv.Columns[1].HeaderText = "Tên danh mục";
        }

        private void handleInsert()
        {
            TextBox txtId = (TextBox)ctr.Controls.Find("txtCatId", false)[0];
            TextBox txtName = (TextBox)ctr.Controls.Find("txtCatName", false)[0];
            DataGridView dtgv = (DataGridView)ctr.Controls.Find("dtgvCategory", false)[0];

            if (txtName.Text == string.Empty)
            {
                MessageBox.Show("Vui lòng nhập tên danh mục");
                txtName.Focus();
                return;
            }

            if (bdc.is_ExistsName(txtName.Text))
            {
                MessageBox.Show("Tên danh mục đã tồn tại");
                txtName.Focus();
                return;
            }

            Category category = new Category();
            category.id = Guid.NewGuid().ToString();
            category.name = txtName.Text;

            int result = bdc.insert(category);
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
            TextBox txtId = (TextBox)ctr.Controls.Find("txtCatId", false)[0];
            TextBox txtName = (TextBox)ctr.Controls.Find("txtCatName", false)[0];
            DataGridView dtgv = (DataGridView)ctr.Controls.Find("dtgvCategory", false)[0];

            if (txtName.Text == string.Empty)
            {
                MessageBox.Show("Vui lòng nhập tên danh mục");
                txtName.Focus();
                return;
            }

            if (txtId.Text == string.Empty)
            {
                MessageBox.Show("Vui lòng chọn danh mục cần chỉnh sửa");
                return;
            }

            string categoryName = dtgv.CurrentRow.Cells[1].Value.ToString();
            if (txtName.Text != categoryName)
            {
                if (bdc.is_ExistsName(txtName.Text))
                {
                    MessageBox.Show("Tên danh mục đã tồn tại");
                    return;
                }
            }

            Category category = new Category();
            category.id = txtId.Text;
            category.name = txtName.Text;

            int result = bdc.update(category);
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
            TextBox txtId = (TextBox)ctr.Controls.Find("txtCatId", false)[0];
            TextBox txtName = (TextBox)ctr.Controls.Find("txtCatName", false)[0];
            DataGridView dtgv = (DataGridView)ctr.Controls.Find("dtgvCategory", false)[0];

            if (txtId.Text == string.Empty)
            {
                MessageBox.Show("Vui lòng chọn danh mục cần xóa");
                return;
            }

            DialogResult dialogResult = MessageBox.Show("Bạn có chắc là muốn xóa danh mục này ?", "Thông báo", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                List<Product> products = bdp.getAllByCatId(txtId.Text);
                if (products.Count != 0)
                {
                    MessageBox.Show("Không thể xóa danh mục này");
                    return;
                }

                int result = bdc.delete(txtId.Text);
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
