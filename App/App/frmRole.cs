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
    public partial class frmRole : Form
    {
        BLL_DAL_Role bdr = new BLL_DAL_Role();
        BLL_DAL_User bdu = new BLL_DAL_User();
        BLL_DAL_Permission bdp = new BLL_DAL_Permission();

        public frmRole()
        {
            InitializeComponent();

            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void frmRole_Load(object sender, EventArgs e)
        {
            loadDataGridView();

            txtRoleId.Text = dtgvRole.CurrentRow.Cells[0].Value.ToString();
            txtRoleName.Text = dtgvRole.CurrentRow.Cells[1].Value.ToString();
        }

        private void loadDataGridView()
        {
            dtgvRole.DataSource = bdr.getAll();

            dtgvRole.Columns[0].Width = 291;
            dtgvRole.Columns[0].HeaderText = "Mã nhóm quyền";

            dtgvRole.Columns[1].Width = 150;
            dtgvRole.Columns[1].HeaderText = "Tên nhóm quyền";
        }

        private void dtgvRole_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            txtRoleId.Text = dtgvRole.CurrentRow.Cells[0].Value.ToString();
            txtRoleName.Text = dtgvRole.CurrentRow.Cells[1].Value.ToString();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtRoleName.Text == string.Empty)
            {
                MessageBox.Show("Vui lòng nhập tên nhóm quyền");
                txtRoleName.Focus();
                return;
            }

            if(bdr.is_ExistsName(txtRoleName.Text))
            {
                MessageBox.Show("Tên nhóm quyền đã tồn tại");
                txtRoleName.Focus();
                return;
            }

            Role role = new Role();
            role.id = Guid.NewGuid().ToString();
            role.name = txtRoleName.Text;

            int result = bdr.insert(role);
            if (result == 0)
                MessageBox.Show("Thêm thất bại");
            else
            {
                MessageBox.Show("Thêm thành công");
                loadDataGridView();

                txtRoleId.Text = string.Empty;
                txtRoleName.Text = string.Empty;
            }    
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if(txtRoleName.Text == string.Empty)
            {
                MessageBox.Show("Vui lòng nhập tên nhóm quyền");
                txtRoleName.Focus();
                return;
            }

            if(txtRoleId.Text == string.Empty)
            {
                MessageBox.Show("Vui lòng chọn nhóm quyền cần chỉnh sửa");
                return;
            }

            string roleName = dtgvRole.CurrentRow.Cells[1].Value.ToString();
            if(txtRoleName.Text != roleName)
            {
                if (bdr.is_ExistsName(txtRoleName.Text))
                {
                    MessageBox.Show("Tên nhóm quyền đã tồn tại");
                    return;
                }
            }

            Role role = new Role();
            role.id = txtRoleId.Text;
            role.name = txtRoleName.Text;

            int result = bdr.update(role);
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
            if(txtRoleId.Text == string.Empty)
            {
                MessageBox.Show("Vui lòng chọn nhóm quyền cần xóa");
                return;
            }

            DialogResult dialogResult = MessageBox.Show("Bạn có chắc là muốn xóa nhóm quyền này ?", "Thông báo", MessageBoxButtons.YesNo);
            if(dialogResult == DialogResult.Yes)
            {
                List<User> users = bdu.getAllByRoleId(txtRoleId.Text);
                if(users.Count != 0)
                {
                    MessageBox.Show("Vui lòng xóa các người dùng thuộc nhóm quyền này trước.");
                    return;
                }

                List<Permission> pers = bdp.getAllByRoleId(txtRoleId.Text);
                if (pers.Count != 0)
                {
                    MessageBox.Show("Vui lòng xóa các quyền của nhóm quyền này trước.");
                    return;
                }

                int result = bdr.delete(txtRoleId.Text);
                if (result == 0)
                    MessageBox.Show("Xóa thất bại");
                else
                {
                    MessageBox.Show("Xóa thành công");
                    loadDataGridView();

                    txtRoleId.Text = string.Empty;
                    txtRoleName.Text = string.Empty;
                }
            }
        }
    }
}
