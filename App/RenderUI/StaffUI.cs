using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MyComponents;
using System.Drawing;
using BLL_DAL;

namespace RenderUI
{
    public class StaffUI
    {
        BLL_DAL_Role bdr = new BLL_DAL_Role();
        BLL_DAL_User bdu = new BLL_DAL_User();
        BLL_DAL_Bill bdb = new BLL_DAL_Bill();
        BLL_DAL_Cart bdc = new BLL_DAL_Cart();
        Control ctr;
        User currentUser;

        public StaffUI(Control ctr,User currentUser)
        {
            this.ctr = ctr;
            this.currentUser = currentUser;
        }

        public void renderTextBoxPhone(string name, int leftPos, int topPos)
        {
            TextBox txt = new TextBox();
            txt.Name = name;
            txt.Left = leftPos;
            txt.Top = topPos;
            txt.KeyPress += txtPhone_KeyPress;
            txt.Width = 200;
            txt.Font = new Font("Arial", 16);
            ctr.Controls.Add(txt);
        }

        private void txtPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        public void renderTextBoxEmail(string name, int leftPos, int topPos)
        {
            TextBoxEmail txt = new TextBoxEmail();
            txt.Name = name;
            txt.Left = leftPos;
            txt.Top = topPos;

            txt.Width = 300;
            txt.Font = new Font("Arial", 16);
            ctr.Controls.Add(txt);
        }

        public void renderCBoRole(string name, int leftPos, int topPos)
        {
            ComboBox cbo = new ComboBox();
            cbo.Name = name;
            cbo.Left = leftPos;
            cbo.Top = topPos;

            cbo.Width = 300;
            cbo.Font = new Font("Arial", 16);
            cbo.DataSource = bdr.getAllForPermission();
            cbo.DisplayMember = "roleName";
            cbo.ValueMember = "id";
            ctr.Controls.Add(cbo);
        }

        public void renderCheckBox(string name, int leftPos, int topPos)
        {
            CheckBox cb = new CheckBox();
            cb.Name = name;
            cb.Left = leftPos;
            cb.Top = topPos;
            cb.Text = "Hoạt động";
            cb.Font = new Font("Arial", 16);
            cb.Width = 200;
            cb.Height = 40;
            ctr.Controls.Add(cb);
        }

        public void renderDataGridView(string name, int leftPos, int topPos)
        {
            DataGridView dtgv = new DataGridView();
            dtgv.Name = name;
            dtgv.Top = topPos;
            dtgv.Left = leftPos;
            dtgv.Width = 920;
            dtgv.Height = 250;
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
            TextBox txtName = (TextBox)ctr.Controls.Find("txtName", false)[0];
            TextBox txtPhone = (TextBox)ctr.Controls.Find("txtPhone", false)[0];
            TextBoxEmail txtEmail = (TextBoxEmail)ctr.Controls.Find("txtEmail", false)[0];
            TextBox txtAddress = (TextBox)ctr.Controls.Find("txtAddress", false)[0];
            TextBox txtPassword = (TextBox)ctr.Controls.Find("txtPassword", false)[0];
            ComboBox cboRole = (ComboBox)ctr.Controls.Find("cboRole", false)[0];
            CheckBox cbActive = (CheckBox)ctr.Controls.Find("cbActive", false)[0];

            txtName.Text = dtgv.CurrentRow.Cells[1].Value.ToString();
            txtEmail.Text = dtgv.CurrentRow.Cells[2].Value.ToString();

            txtAddress.Text = dtgv.CurrentRow.Cells[3].Value.ToString();
            txtPhone.Text = dtgv.CurrentRow.Cells[4].Value.ToString();

            int index = cboRole.FindString(dtgv.CurrentRow.Cells[6].Value.ToString());
            cboRole.SelectedIndex = index;

            cbActive.Checked = bool.Parse(dtgv.CurrentRow.Cells[7].Value.ToString());

            if(cboRole.Text == "admin" && currentUser.id != "6271e405-a68c-438d-8d72-9e3610d0d4d5")
            {
                if (currentUser.email == txtEmail.Text)
                {
                    txtName.ReadOnly = false;
                    txtPhone.ReadOnly = false;
                    txtEmail.ReadOnly = false;
                    txtPassword.ReadOnly = false;
                    txtAddress.ReadOnly = false;
                    cboRole.Enabled = true;
                    cbActive.Enabled = true;
                }
                else
                {
                    txtName.ReadOnly = true;
                    txtPhone.ReadOnly = true;
                    txtEmail.ReadOnly = true;
                    txtPassword.ReadOnly = true;
                    txtAddress.ReadOnly = true;
                    cboRole.Enabled = false;
                    cbActive.Enabled = false;
                }
            }
            else
            {
                txtName.ReadOnly = false;
                txtPhone.ReadOnly = false;
                txtAddress.ReadOnly = false;
                txtEmail.ReadOnly = false;
                txtPassword.ReadOnly = false;

                if (txtEmail.Text == "adminapp@gmail.com")
                {
                    cboRole.Enabled = false;
                    cbActive.Enabled = false;
                }
                else
                {
                    cboRole.Enabled = true;
                    cbActive.Enabled = true;
                }
            }
        }

        private void loadData(DataGridView dtgv, List<Object> users = null)
        {
            if (users != null)
                dtgv.DataSource = users;
            else
                dtgv.DataSource = bdu.getAll();

            dtgv.ColumnHeadersHeight = 50;

            dtgv.Columns[0].Visible = false;

            dtgv.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dtgv.Columns[1].HeaderCell.Style.Font = new Font("Arial", 16, FontStyle.Bold);
            dtgv.Columns[1].Width = 200;
            dtgv.Columns[1].HeaderText = "Họ tên";

            dtgv.Columns[2].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dtgv.Columns[2].HeaderCell.Style.Font = new Font("Arial", 16, FontStyle.Bold);
            dtgv.Columns[2].Width = 200;
            dtgv.Columns[2].HeaderText = "Email";

            dtgv.Columns[3].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dtgv.Columns[3].HeaderCell.Style.Font = new Font("Arial", 16, FontStyle.Bold);
            dtgv.Columns[3].Width = 150;
            dtgv.Columns[3].HeaderText = "Địa chỉ";

            dtgv.Columns[4].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dtgv.Columns[4].HeaderCell.Style.Font = new Font("Arial", 16, FontStyle.Bold);
            dtgv.Columns[4].Width = 150;
            dtgv.Columns[4].HeaderText = "SĐT";

            dtgv.Columns[5].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dtgv.Columns[5].HeaderCell.Style.Font = new Font("Arial", 16, FontStyle.Bold);
            dtgv.Columns[5].Width = 150;
            dtgv.Columns[5].HeaderText = "Mật khẩu";

            dtgv.Columns[6].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dtgv.Columns[6].HeaderCell.Style.Font = new Font("Arial", 16, FontStyle.Bold);
            dtgv.Columns[6].Width = 150;
            dtgv.Columns[6].HeaderText = "Nhóm quyền";

            dtgv.Columns[7].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dtgv.Columns[7].HeaderCell.Style.Font = new Font("Arial", 16, FontStyle.Bold);
            dtgv.Columns[7].Width = 150;
            dtgv.Columns[7].HeaderText = "Hoạt động";
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
                case "btnSearch":
                    {
                        handleSearch();
                    }
                    break;
                case "btnRefresh":
                    {
                        handleRefresh();
                    }
                    break;
            }
        }

        private void handleInsert()
        {
            TextBox txtName = (TextBox)ctr.Controls.Find("txtName", false)[0];
            TextBox txtPhone = (TextBox)ctr.Controls.Find("txtPhone", false)[0];
            TextBoxEmail txtEmail = (TextBoxEmail)ctr.Controls.Find("txtEmail", false)[0];
            TextBox txtAddress = (TextBox)ctr.Controls.Find("txtAddress", false)[0];
            ComboBox cboRole = (ComboBox)ctr.Controls.Find("cboRole", false)[0];
            CheckBox cbActive = (CheckBox)ctr.Controls.Find("cbActive", false)[0];
            TextBox txtPassword = (TextBox)ctr.Controls.Find("txtPassword", false)[0];
            DataGridView dtgv = (DataGridView)ctr.Controls.Find("dtgvStaff", false)[0];

            if (txtName.Text == string.Empty)
            {
                MessageBox.Show("Vui lòng nhập tên nhân viên");
                txtName.Focus();
                return;
            }

            if (txtPhone.Text == string.Empty)
            {
                MessageBox.Show("Vui lòng nhập số điện thoại");
                txtPhone.Focus();
                return;
            }
            else
            {
                if(txtPhone.Text.Length != 10)
                {
                    MessageBox.Show("Số điện thoại không hợp lệ");
                    txtPhone.Focus();
                    return;
                }
            }

            if (txtEmail.Text == string.Empty)
            {
                MessageBox.Show("Vui lòng nhập email");
                txtEmail.Focus();
                return;
            }

            if (txtPassword.Text == string.Empty)
            {
                MessageBox.Show("Vui lòng nhập mật khẩu");
                txtPassword.Focus();
                return;
            }
            else
            {
                if (txtPassword.Text.Length < 5)
                {
                    MessageBox.Show("Mật khẩu phải có tối thiểu 5 kí tự");
                    txtPhone.Focus();
                    return;
                }
            }

            if (txtAddress.Text == string.Empty)
            {
                MessageBox.Show("Vui lòng nhập địa chỉ");
                txtAddress.Focus();
                return;
            }

            if (bdu.is_ExistsPhone(txtPhone.Text))
            {
                MessageBox.Show("Số điện thoại đã tồn tại");
                txtPhone.Focus();
                return;
            }

            if (bdu.is_ExistsEmail(txtEmail.Text))
            {
                MessageBox.Show("Email đã tồn tại");
                txtEmail.Focus();
                return;
            }

            User user = new User();
            user.id = Guid.NewGuid().ToString();
            user.name = txtName.Text;
            user.phone = txtPhone.Text;
            user.email = txtEmail.Text;
            user.password = txtPassword.Text;
            user.address = txtAddress.Text;
            user.roleId = cboRole.SelectedValue.ToString();
            user.is_Active = cbActive.Checked;

            int result = bdu.insert(user);
            if (result == 0)
                MessageBox.Show("Thêm thất bại");
            else
            {
                MessageBox.Show("Thêm thành công");
                loadData(dtgv);

                handleRefresh();
            }
        }

        private void handleUpdate()
        {
            TextBox txtName = (TextBox)ctr.Controls.Find("txtName", false)[0];
            TextBox txtPhone = (TextBox)ctr.Controls.Find("txtPhone", false)[0];
            TextBoxEmail txtEmail = (TextBoxEmail)ctr.Controls.Find("txtEmail", false)[0];
            TextBox txtAddress = (TextBox)ctr.Controls.Find("txtAddress", false)[0];
            ComboBox cboRole = (ComboBox)ctr.Controls.Find("cboRole", false)[0];
            CheckBox cbActive = (CheckBox)ctr.Controls.Find("cbActive", false)[0];
            TextBox txtPassword = (TextBox)ctr.Controls.Find("txtPassword", false)[0];
            DataGridView dtgv = (DataGridView)ctr.Controls.Find("dtgvStaff", false)[0];

            if (txtName.Text == string.Empty)
            {
                MessageBox.Show("Vui lòng nhập tên nhân viên");
                txtName.Focus();
                return;
            }

            if (txtPhone.Text == string.Empty)
            {
                MessageBox.Show("Vui lòng nhập số điện thoại");
                txtPhone.Focus();
                return;
            }
            else
            {
                if (txtPhone.Text.Length != 10)
                {
                    MessageBox.Show("Số điện thoại không hợp lệ");
                    txtPhone.Focus();
                    return;
                }
            }

            if (txtEmail.Text == string.Empty)
            {
                MessageBox.Show("Vui lòng nhập email");
                txtEmail.Focus();
                return;
            }

            if (txtPassword.Text != string.Empty)
            {
                if (txtPassword.Text.Length < 5)
                {
                    MessageBox.Show("Mật khẩu phải có tối thiểu 5 kí tự");
                    txtPhone.Focus();
                    return;
                }
            }

            if (txtAddress.Text == string.Empty)
            {
                MessageBox.Show("Vui lòng nhập địa chỉ");
                txtAddress.Focus();
                return;
            }

            if(txtPhone.Text != dtgv.CurrentRow.Cells[4].Value.ToString())
            {
                if (bdu.is_ExistsPhone(txtPhone.Text))
                {
                    MessageBox.Show("Số điện thoại đã tồn tại");
                    txtPhone.Focus();
                    return;
                }
            }

            if (txtEmail.Text != dtgv.CurrentRow.Cells[2].Value.ToString())
            {
                if (bdu.is_ExistsEmail(txtEmail.Text))
                {
                    MessageBox.Show("Email đã tồn tại");
                    txtEmail.Focus();
                    return;
                }
            }

            User user = new User();
            user.id = dtgv.CurrentRow.Cells[0].Value.ToString();
            user.name = txtName.Text;
            user.phone = txtPhone.Text;
            user.email = txtEmail.Text;
            user.password = txtPassword.Text;
            user.address = txtAddress.Text;
            user.roleId = cboRole.SelectedValue.ToString();
            user.is_Active = cbActive.Checked;

            int result = bdu.update(user);
            if (result == 0)
                MessageBox.Show("Sửa thất bại");
            else
            {
                MessageBox.Show("Sửa thành công");
                loadData(dtgv);

                handleRefresh();
            }
        }

        private void handleDelete()
        {
            DataGridView dtgv = (DataGridView)ctr.Controls.Find("dtgvStaff", false)[0];
            TextBox txtName = (TextBox)ctr.Controls.Find("txtName", false)[0];
            TextBox txtPhone = (TextBox)ctr.Controls.Find("txtPhone", false)[0];
            TextBoxEmail txtEmail = (TextBoxEmail)ctr.Controls.Find("txtEmail", false)[0];
            TextBox txtAddress = (TextBox)ctr.Controls.Find("txtAddress", false)[0];
            CheckBox cbActive = (CheckBox)ctr.Controls.Find("cbActive", false)[0];


            if (dtgv.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn nhân viên cần xóa");
                return;
            }

            string id = dtgv.CurrentRow.Cells[0].Value.ToString();
            DialogResult dialogResult = MessageBox.Show("Bạn có chắc là muốn xóa nhân viên này ?", "Thông báo", MessageBoxButtons.YesNo);
            if(dialogResult == DialogResult.Yes)
            {
                if(dtgv.CurrentRow.Cells[0].Value.ToString() == "6271e405-a68c-438d-8d72-9e3610d0d4d5")
                {
                    MessageBox.Show("Bạn không được xóa tài khoản này");
                    return;
                }

                updateUserId(id);

                int result = bdu.delete(id);
                if (result == 1)
                {
                    MessageBox.Show("Xóa thành công");
                    loadData(dtgv);

                    handleRefresh();
                }
                else
                    MessageBox.Show("Xóa thất bại");
            }
        }

        private void updateUserId(string userId)
        {
            List<Cart> carts = bdc.getAll();
            foreach(var cart in carts)
            {
                if (cart.userId == userId)
                    bdc.updateUserId(cart.id, currentUser.id);
            }

            List<Bill> bills = bdb.getAllByUserId(userId);
            foreach (var bill in bills)
                bdb.updateUserId(bill.id, currentUser.id);
        }

        private void handleSearch()
        {
            TextBox txtName = (TextBox)ctr.Controls.Find("txtName", false)[0];
            DataGridView dtgv = (DataGridView)ctr.Controls.Find("dtgvStaff", false)[0];

            if (txtName.Text ==string.Empty)
            {
                MessageBox.Show("Vui lòng nhập tên nhân viên cần tìm");
                return;
            }

            List<Object> users = bdu.getAllByName(txtName.Text);
            if (users.Count == 0)
            {
                MessageBox.Show("Không tìm thấy nhân viên '" + txtName.Text + "'");
                loadData(dtgv);
            }    
            else
            {
                loadData(dtgv, users);
                handleRefresh();
            }    
        }

        private void handleRefresh()
        {
            TextBox txtName = (TextBox)ctr.Controls.Find("txtName", false)[0];
            TextBox txtPhone = (TextBox)ctr.Controls.Find("txtPhone", false)[0];
            TextBoxEmail txtEmail = (TextBoxEmail)ctr.Controls.Find("txtEmail", false)[0];
            TextBox txtAddress = (TextBox)ctr.Controls.Find("txtAddress", false)[0];
            CheckBox cbActive = (CheckBox)ctr.Controls.Find("cbActive", false)[0];
            TextBox txtPassword = (TextBox)ctr.Controls.Find("txtPassword", false)[0];
            ComboBox cboRole = (ComboBox)ctr.Controls.Find("cboRole", false)[0];

            txtName.Text = string.Empty;
            txtPhone.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtPassword.Text = string.Empty;
            txtAddress.Text = string.Empty;
            cbActive.Checked = false;

            txtName.ReadOnly = false;
            txtPhone.ReadOnly = false;
            txtEmail.ReadOnly = false;
            txtPassword.ReadOnly = false;
            txtAddress.ReadOnly = false;
            cbActive.Enabled = true;
            cboRole.Enabled = true;
        }
    }
}
