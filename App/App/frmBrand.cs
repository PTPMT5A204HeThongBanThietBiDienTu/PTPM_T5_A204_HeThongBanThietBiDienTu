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
    public partial class frmBrand : Form
    {
        BLL_DAL_Brand bdb = new BLL_DAL_Brand();
        BLL_DAL_Product bdp = new BLL_DAL_Product();

        public frmBrand()
        {
            InitializeComponent();

            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void frmBrand_Load(object sender, EventArgs e)
        {
            loadDataGridView();

            txtBrandId.Text = dtgvBrand.CurrentRow.Cells[0].Value.ToString();
            txtBrandName.Text = dtgvBrand.CurrentRow.Cells[1].Value.ToString();
        }

        private void loadDataGridView()
        {
            dtgvBrand.DataSource = bdb.getAll();

            dtgvBrand.Columns[0].Width = 291;
            dtgvBrand.Columns[0].HeaderText = "Mã thương hiệu";

            dtgvBrand.Columns[1].Width = 150;
            dtgvBrand.Columns[1].HeaderText = "Tên thương hiệu";
        }

        private void dtgvBrand_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            txtBrandId.Text = dtgvBrand.CurrentRow.Cells[0].Value.ToString();
            txtBrandName.Text = dtgvBrand.CurrentRow.Cells[1].Value.ToString();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtBrandName.Text == string.Empty)
            {
                MessageBox.Show("Vui lòng nhập tên thương hiệu");
                txtBrandName.Focus();
                return;
            }

            if (bdb.is_ExistsName(txtBrandName.Text))
            {
                MessageBox.Show("Tên thương hiệu đã tồn tại");
                txtBrandName.Focus();
                return;
            }

            Brand brand = new Brand();
            brand.id = Guid.NewGuid().ToString();
            brand.name = txtBrandName.Text;

            int result = bdb.insert(brand);
            if (result == 0)
                MessageBox.Show("Thêm thất bại");
            else
            {
                MessageBox.Show("Thêm thành công");
                loadDataGridView();

                txtBrandId.Text = string.Empty;
                txtBrandName.Text = string.Empty;
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (txtBrandName.Text == string.Empty)
            {
                MessageBox.Show("Vui lòng nhập tên thương hiệu");
                txtBrandName.Focus();
                return;
            }

            if (txtBrandId.Text == string.Empty)
            {
                MessageBox.Show("Vui lòng chọn thương hiệu cần chỉnh sửa");
                return;
            }

            string brandName = dtgvBrand.CurrentRow.Cells[1].Value.ToString();
            if (txtBrandName.Text != brandName)
            {
                if (bdb.is_ExistsName(txtBrandName.Text))
                {
                    MessageBox.Show("Tên thương hiệu đã tồn tại");
                    return;
                }
            }

            Brand brand = new Brand();
            brand.id = txtBrandId.Text;
            brand.name = txtBrandName.Text;

            int result = bdb.update(brand);
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
            if (txtBrandId.Text == string.Empty)
            {
                MessageBox.Show("Vui lòng chọn thương hiệu cần xóa");
                return;
            }

            DialogResult dialogResult = MessageBox.Show("Bạn có chắc là muốn xóa thương biệu này ?", "Thông báo", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                List<Product> products = bdp.getAllByBrandId(txtBrandId.Text);
                if (products.Count != 0)
                {
                    MessageBox.Show("Vui lòng xóa các sản phẩm thuộc thương hiệu này trước.");
                    return;
                }

                int result = bdb.delete(txtBrandId.Text);
                if (result == 0)
                    MessageBox.Show("Xóa thất bại");
                else
                {
                    MessageBox.Show("Xóa thành công");
                    loadDataGridView();

                    txtBrandId.Text = string.Empty;
                    txtBrandName.Text = string.Empty;
                }
            }
        }

        private void frmBrand_FormClosed(object sender, FormClosedEventArgs e)
        {
            frmMain frm = new frmMain();
            this.Hide();
            frm.ShowDialog();
        }
    }
}
