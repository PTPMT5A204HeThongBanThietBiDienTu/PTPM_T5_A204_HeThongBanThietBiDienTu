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
    public class PermissionUI
    {
        BLL_DAL_Permission bdp = new BLL_DAL_Permission();
        BLL_DAL_Role bdr = new BLL_DAL_Role();
        Control ctr;

        public PermissionUI(Control ctr)
        {
            this.ctr = ctr;
        }

        public void renderCBoRole(string name, int width, int leftPos, int topPos)
        {
            ComboBox cbo = new ComboBox();
            cbo.Name = name;
            cbo.Left = leftPos;
            cbo.Top = topPos;

            cbo.Width = width;
            cbo.Font = new Font("Arial", 16);
            cbo.DataSource = bdr.getAllForPermission();
            cbo.DisplayMember = "roleName";
            cbo.ValueMember = "id";
            cbo.SelectedValueChanged += cbo_SelectedValueChanged;
            ctr.Controls.Add(cbo);
        }

        private void cbo_SelectedValueChanged(object sender, EventArgs e)
        {
            ComboBox cbo = (ComboBox)sender;
            string roleId = cbo.SelectedValue.ToString();

            DataGridView dtgv = (DataGridView)ctr.Controls.Find("dtgvPermission", true)[0];
            loadData(dtgv, roleId);
        }

        public void renderGroupBox(string name, int leftPos, int topPos)
        {
            GroupBox gb = new GroupBox();
            gb.Name = name;
            gb.Text = "Danh sách quyền chức năng";
            gb.Left = leftPos;
            gb.Top = topPos;
            gb.Size = new Size(800, 400);
            gb.Font = new Font("Arial", 15);
            gb.ControlAdded += gb_ControlAdded;
            ctr.Controls.Add(gb);
            renderDataGridView(gb);
        }

        private void gb_ControlAdded(object sender, ControlEventArgs e)
        {
            ComboBox cbo = (ComboBox)ctr.Controls.Find("cboRole", false)[0];
            string roleId = cbo.SelectedValue.ToString();

            DataGridView dtgv = (DataGridView)ctr.Controls.Find("dtgvPermission", true)[0];
            loadData(dtgv, roleId);
        }

        public void renderDataGridView(Control ctr)
        {
            DataGridView dtgv = new DataGridView();
            dtgv.Name = "dtgvPermission";
            dtgv.Top = 55;
            dtgv.Left = 50;
            dtgv.Width = 700;
            dtgv.Height = 300;
            dtgv.Font = new Font("Arial", 15);
            dtgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dtgv.MultiSelect = false;
            ctr.Controls.Add(dtgv);
        }

        private void loadData(DataGridView dtgv, string roleId)
        {
            dtgv.DataSource = bdp.getAll(roleId);

            dtgv.ColumnHeadersHeight = 50;

            dtgv.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dtgv.Columns[0].HeaderCell.Style.Font = new Font("Arial", 16, FontStyle.Bold);
            dtgv.Columns[0].Width = 260;
            dtgv.Columns[0].HeaderText = "Mã màn hình";

            dtgv.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dtgv.Columns[1].HeaderCell.Style.Font = new Font("Arial", 16, FontStyle.Bold);
            dtgv.Columns[1].Width = 247;
            dtgv.Columns[1].HeaderText = "Tên màn hình";

            dtgv.Columns[2].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dtgv.Columns[2].HeaderCell.Style.Font = new Font("Arial", 16, FontStyle.Bold);
            dtgv.Columns[2].Width = 150;
            dtgv.Columns[2].HeaderText = "Có quyền";
        }

        public void renderButtonSave(string name, int leftPos, int topPos)
        {
            Button btn = new Button();
            btn.Text = "Lưu";
            btn.Name = name;
            btn.Click += btn_Click;
            btn.Top = topPos;
            btn.Left = leftPos;
            btn.Width = 150;
            btn.Height = 40;
            btn.Font = new Font("Arial", 14, FontStyle.Bold);
            ctr.Controls.Add(btn);
        }

        private void btn_Click(object sender, EventArgs e)
        {
            ComboBox cbo = (ComboBox)ctr.Controls.Find("cboRole", false)[0];
            DataGridView dtgv = (DataGridView)ctr.Controls.Find("dtgvPermission", true)[0];

            string roleId = cbo.SelectedValue.ToString();
            int rowNumber = dtgv.Rows.Count, result = 0;

            for(int i = 0; i < rowNumber; i++)
            {
                string sceenId = dtgv.Rows[i].Cells[0].Value.ToString();
                bool is_Per = bool.Parse(dtgv.Rows[i].Cells[2].Value.ToString());
                result = bdp.update(roleId, sceenId, is_Per);
            }

            if (result == 1)
            {
                MessageBox.Show("Lưu thành công");
                loadData(dtgv, roleId);
            }
            else
                MessageBox.Show("Lưu thất bại");
        }
    }
}
