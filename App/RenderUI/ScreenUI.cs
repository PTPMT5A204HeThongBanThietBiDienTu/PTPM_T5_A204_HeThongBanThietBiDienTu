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
    public class ScreenUI
    {
        Control ctr;
        BLL_DAL_Screen bds = new BLL_DAL_Screen();
        BLL_DAL_Role bdr = new BLL_DAL_Role();
        BLL_DAL_Permission bdp = new BLL_DAL_Permission();

        public ScreenUI(Control ctr)
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


            TextBox txtId = (TextBox)ctr.Controls.Find("txtScreenId", false)[0];
            txtId.Text = dtgv.CurrentRow.Cells[0].Value.ToString();

            TextBox txtName = (TextBox)ctr.Controls.Find("txtScreenName", false)[0];
            txtName.Text = dtgv.CurrentRow.Cells[1].Value.ToString();
        }

        private void loadData(DataGridView dtgv)
        {
            dtgv.DataSource = bds.getAll();

            dtgv.ColumnHeadersHeight = 50;

            dtgv.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dtgv.Columns[0].HeaderCell.Style.Font = new Font("Arial", 16, FontStyle.Bold);
            dtgv.Columns[0].Width = 280;
            dtgv.Columns[0].HeaderText = "Mã màn hình";

            dtgv.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dtgv.Columns[1].HeaderCell.Style.Font = new Font("Arial", 16, FontStyle.Bold);
            dtgv.Columns[1].Width = 326;
            dtgv.Columns[1].HeaderText = "Tên màn hình";
        }

        private void handleInsert()
        {
            TextBox txtId = (TextBox)ctr.Controls.Find("txtScreenId", false)[0];
            TextBox txtName = (TextBox)ctr.Controls.Find("txtScreenName", false)[0];
            DataGridView dtgv = (DataGridView)ctr.Controls.Find("dtgvScreen", false)[0];

            if (txtName.Text == string.Empty)
            {
                MessageBox.Show("Vui lòng nhập tên màn hình");
                txtName.Focus();
                return;
            }

            if (bds.is_ExistsName(txtName.Text))
            {
                MessageBox.Show("Tên màn hình đã tồn tại");
                txtName.Focus();
                return;
            }

            
            BLL_DAL.Screen screen = new BLL_DAL.Screen();
            screen.id = createScreenId();
            screen.screenName = txtName.Text;

            int result = bds.insert(screen);
            if (result == 0)
                MessageBox.Show("Thêm thất bại");
            else
            {
                List<Role> roles = bdr.getAll();
                foreach(Role role in roles)
                {
                    bool isPermission = false;
                    if (role.roleName == "admin")
                        isPermission = true;
                    insertPer(role.id, screen.id, isPermission);
                }

                MessageBox.Show("Thêm thành công");
                loadData(dtgv);

                txtId.Text = string.Empty;
                txtName.Text = string.Empty;
            }
        }

        private string createScreenId()
        {
            int count = 1;
            string id = "SR";

            while (true)
            {
                id = "SR" + string.Format("{0:D3}", count);
                BLL_DAL.Screen scr = bds.getById(id);
                if (scr == null)
                    break;
                count++;
            }

            return id;
        }

        private void insertPer(string roleId, string screenId, bool isPermission)
        {
            Permission per = new Permission();
            per.id = Guid.NewGuid().ToString();
            per.roleId = roleId;
            per.screenId = screenId;
            per.is_Permission = isPermission;
            bdp.insert(per);
        }

        private void handleUpdate()
        {
            TextBox txtId = (TextBox)ctr.Controls.Find("txtScreenId", false)[0];
            TextBox txtName = (TextBox)ctr.Controls.Find("txtScreenName", false)[0];
            DataGridView dtgv = (DataGridView)ctr.Controls.Find("dtgvScreen", false)[0];

            if (txtName.Text == string.Empty)
            {
                MessageBox.Show("Vui lòng nhập tên màn hình");
                txtName.Focus();
                return;
            }

            if (txtId.Text == string.Empty)
            {
                MessageBox.Show("Vui lòng chọn màn hình cần chỉnh sửa");
                return;
            }

            string screenName = dtgv.CurrentRow.Cells[1].Value.ToString();
            if (txtName.Text != screenName)
            {
                if (bds.is_ExistsName(txtName.Text))
                {
                    MessageBox.Show("Tên màn hình đã tồn tại");
                    return;
                }
            }

            BLL_DAL.Screen screen = new BLL_DAL.Screen();
            screen.id = txtId.Text;
            screen.screenName = txtName.Text;

            int result = bds.update(screen);
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
            TextBox txtId = (TextBox)ctr.Controls.Find("txtScreenId", false)[0];
            TextBox txtName = (TextBox)ctr.Controls.Find("txtScreenName", false)[0];
            DataGridView dtgv = (DataGridView)ctr.Controls.Find("dtgvScreen", false)[0];

            if (txtId.Text == string.Empty)
            {
                MessageBox.Show("Vui lòng chọn màn hình cần xóa");
                return;
            }

            DialogResult dialogResult = MessageBox.Show("Bạn có chắc là muốn xóa màn hình này ?", "Thông báo", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                List<Role> roles = bdr.getAll();
                foreach(Role role in roles)
                    bdp.deleteByRoleIdAndScreenId(role.id, txtId.Text);

                int result = bds.delete(txtId.Text);
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
