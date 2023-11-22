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
    public class RoleUI
    {
        Control ctr;
        BLL_DAL_Role bdr = new BLL_DAL_Role();
        BLL_DAL_User bdu = new BLL_DAL_User();
        BLL_DAL_Permission bdp = new BLL_DAL_Permission();

        public RoleUI(Control ctr)
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


            TextBox txtId = (TextBox)ctr.Controls.Find("txtRoleId", false)[0];
            txtId.Text = dtgv.CurrentRow.Cells[0].Value.ToString();

            TextBox txtName = (TextBox)ctr.Controls.Find("txtRoleName", false)[0];
            txtName.Text = dtgv.CurrentRow.Cells[1].Value.ToString();
        }

        private void loadData(DataGridView dtgv)
        {
            dtgv.DataSource = bdr.getAll();

            dtgv.ColumnHeadersHeight = 50;

            dtgv.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dtgv.Columns[0].HeaderCell.Style.Font = new Font("Arial", 16, FontStyle.Bold);
            dtgv.Columns[0].Width = 280;
            dtgv.Columns[0].HeaderText = "Mã nhóm quyền";

            dtgv.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dtgv.Columns[1].HeaderCell.Style.Font = new Font("Arial", 16, FontStyle.Bold);
            dtgv.Columns[1].Width = 326;
            dtgv.Columns[1].HeaderText = "Tên nhóm quyền";
        }

        private void handleInsert()
        {
            TextBox txtId = (TextBox)ctr.Controls.Find("txtRoleId", false)[0];
            TextBox txtName = (TextBox)ctr.Controls.Find("txtRoleName", false)[0];
            DataGridView dtgv = (DataGridView)ctr.Controls.Find("dtgvRole", false)[0];

            if (txtName.Text == string.Empty)
            {
                MessageBox.Show("Vui lòng nhập tên nhóm quyền");
                txtName.Focus();
                return;
            }

            if (bdr.is_ExistsName(txtName.Text))
            {
                MessageBox.Show("Tên nhóm quyền đã tồn tại");
                txtName.Focus();
                return;
            }

            Role role = new Role();
            role.id = Guid.NewGuid().ToString();
            role.roleName = txtName.Text;

            int result = bdr.insert(role);
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
            TextBox txtId = (TextBox)ctr.Controls.Find("txtRoleId", false)[0];
            TextBox txtName = (TextBox)ctr.Controls.Find("txtRoleName", false)[0];
            DataGridView dtgv = (DataGridView)ctr.Controls.Find("dtgvRole", false)[0];

            if (txtName.Text == string.Empty)
            {
                MessageBox.Show("Vui lòng nhập tên nhóm quyền");
                txtName.Focus();
                return;
            }

            if (txtId.Text == string.Empty)
            {
                MessageBox.Show("Vui lòng chọn nhóm quyền cần chỉnh sửa");
                return;
            }

            string roleName = dtgv.CurrentRow.Cells[1].Value.ToString();
            if (txtName.Text != roleName)
            {
                if (bdr.is_ExistsName(txtName.Text))
                {
                    MessageBox.Show("Tên nhóm quyền đã tồn tại");
                    return;
                }
            }

            Role role = new Role();
            role.id = txtId.Text;
            role.roleName = txtName.Text;

            int result = bdr.update(role);
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
            TextBox txtId = (TextBox)ctr.Controls.Find("txtRoleId", false)[0];
            TextBox txtName = (TextBox)ctr.Controls.Find("txtRoleName", false)[0];
            DataGridView dtgv = (DataGridView)ctr.Controls.Find("dtgvRole", false)[0];

            if (txtId.Text == string.Empty)
            {
                MessageBox.Show("Vui lòng chọn nhóm quyền cần xóa");
                return;
            }

            DialogResult dialogResult = MessageBox.Show("Bạn có chắc là muốn xóa nhóm quyền này ?", "Thông báo", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                List<User> users = bdu.getAllByRoleId(txtId.Text);
                if (users.Count != 0)
                {
                    MessageBox.Show("Không thể xóa nhóm quyền này");
                    return;
                }

                List<object> pers = bdp.getAllByRoleId(txtId.Text);
                if (pers.Count != 0)
                {
                    MessageBox.Show("Không thể xóa nhóm quyền này");
                    return;
                }

                int result = bdr.delete(txtId.Text);
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
