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
    public partial class frmCategory : Form
    {
        BLL_DAL_Category bdc = new BLL_DAL_Category();
        BLL_DAL_Product bdp = new BLL_DAL_Product();

        public frmCategory()
        {
            InitializeComponent();

            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void frmCategory_Load(object sender, EventArgs e)
        {
            loadDataGridView();

            txtCatId.Text = dtgvCategory.CurrentRow.Cells[0].Value.ToString();
            txtCatName.Text = dtgvCategory.CurrentRow.Cells[1].Value.ToString();
        }

        private void loadDataGridView()
        {
            dtgvCategory.DataSource = bdc.getAll();

            dtgvCategory.Columns[0].Width = 291;
            dtgvCategory.Columns[0].HeaderText = "Mã danh mục";

            dtgvCategory.Columns[1].Width = 150;
            dtgvCategory.Columns[1].HeaderText = "Tên danh mục";
        }

        private void dtgvCategory_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            txtCatId.Text = dtgvCategory.CurrentRow.Cells[0].Value.ToString();
            txtCatName.Text = dtgvCategory.CurrentRow.Cells[1].Value.ToString();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtCatName.Text == string.Empty)
            {
                MessageBox.Show("Vui lòng nhập tên danh mục");
                txtCatName.Focus();
                return;
            }

            if (bdc.is_ExistsName(txtCatName.Text))
            {
                MessageBox.Show("Tên danh mục đã tồn tại");
                txtCatName.Focus();
                return;
            }

            Category category = new Category();
            category.id = Guid.NewGuid().ToString();
            category.name = txtCatName.Text;

            int result = bdc.insert(category);
            if (result == 0)
                MessageBox.Show("Thêm thất bại");
            else
            {
                MessageBox.Show("Thêm thành công");
                loadDataGridView();

                txtCatId.Text = string.Empty;
                txtCatName.Text = string.Empty;
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (txtCatName.Text == string.Empty)
            {
                MessageBox.Show("Vui lòng nhập tên danh mục");
                txtCatName.Focus();
                return;
            }

            if (txtCatId.Text == string.Empty)
            {
                MessageBox.Show("Vui lòng chọn danh mục cần chỉnh sửa");
                return;
            }

            string categoryName = dtgvCategory.CurrentRow.Cells[1].Value.ToString();
            if (txtCatName.Text != categoryName)
            {
                if (bdc.is_ExistsName(txtCatName.Text))
                {
                    MessageBox.Show("Tên danh mục đã tồn tại");
                    return;
                }
            }

            Category category = new Category();
            category.id = txtCatId.Text;
            category.name = txtCatName.Text;

            int result = bdc.update(category);
            if (result == 0)
                MessageBox.Show("Sửa thất bại");
            else
            {
                MessageBox.Show("Sửa thành công");
                loadDataGridView();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (txtCatId.Text == string.Empty)
            {
                MessageBox.Show("Vui lòng chọn danh mục cần xóa");
                return;
            }

            DialogResult dialogResult = MessageBox.Show("Bạn có chắc là muốn xóa danh mục này ?", "Thông báo", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                List<Product> products = bdp.getAllByCatId(txtCatId.Text);
                if (products.Count != 0)
                {
                    MessageBox.Show("Vui lòng xóa các sản phẩm thuộc danh mục này trước.");
                    return;
                }

                int result = bdc.delete(txtCatId.Text);
                if (result == 0)
                    MessageBox.Show("Xóa thất bại");
                else
                {
                    MessageBox.Show("Xóa thành công");
                    loadDataGridView();

                    txtCatId.Text = string.Empty;
                    txtCatName.Text = string.Empty;
                }
            }
        }

        private void frmCategory_FormClosed(object sender, FormClosedEventArgs e)
        {
            frmMain frm = new frmMain();
            this.Hide();
            frm.ShowDialog();
        }
    }
}
