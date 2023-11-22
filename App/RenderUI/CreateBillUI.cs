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
    public class CreateBillUI
    {
        Control ctr;
        User currentUser;
        BLL_DAL_Bill bdb = new BLL_DAL_Bill();
        BLL_DAL_BillProduct bdbP = new BLL_DAL_BillProduct();
        BLL_DAL_User bdu = new BLL_DAL_User();
        string billId;

        public CreateBillUI(Control ctr, User currentUser, string billId)
        {
            this.ctr = ctr;
            this.currentUser = currentUser;
            this.billId = billId;
        }

        public void loadBillInfo()
        {
            TextBox txtBillId = (TextBox)ctr.Controls.Find("txtBillId", false)[0];
            TextBox txtUsername = (TextBox)ctr.Controls.Find("txtUsername", false)[0];
            TextBox txtTime = (TextBox)ctr.Controls.Find("txtTime", false)[0];
            TextBox txtDate = (TextBox)ctr.Controls.Find("txtDate", false)[0];
            TextBox txtTotal = (TextBox)ctr.Controls.Find("txtTotal", false)[0];

            txtTotal.Font = new Font("Arial", 16, FontStyle.Bold);

            txtBillId.Text = billId.ToUpper();
            User user = bdu.getInfo(currentUser.email);
            txtUsername.Text = user.name;
            Bill bill = bdb.getById(billId);
            txtTime.Text = DateTime.Parse(bill.createdAt.ToString()).ToLongTimeString();
            txtDate.Text = DateTime.Parse(bill.createdAt.ToString()).ToShortDateString();
            txtTotal.Text = bill.total?.ToString("#,##") + " VNĐ" ?? "";
        }

        public void renderDataGridView(string name, int leftPos, int topPos)
        {
            DataGridView dtgv = new DataGridView();
            dtgv.Name = name;
            dtgv.Left = leftPos;
            dtgv.Top = topPos;

            dtgv.Width = 770;
            dtgv.Height = 200;
            dtgv.Font = new Font("Arial", 15);
            dtgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            ctr.Controls.Add(dtgv);

            loadData(dtgv);
        }

        private void loadData(DataGridView dtgv)
        {
            List<Object> billPs = bdbP.getAllByBillId(billId);
            dtgv.DataSource = billPs;

            if (billPs.Count != 0)
            {
                dtgv.ColumnHeadersHeight = 50;

                dtgv.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dtgv.Columns[0].HeaderCell.Style.Font = new Font("Arial", 16, FontStyle.Bold);
                dtgv.Columns[0].Width = 180;
                dtgv.Columns[0].HeaderText = "Mã sản phẩm";

                dtgv.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dtgv.Columns[1].HeaderCell.Style.Font = new Font("Arial", 16, FontStyle.Bold);
                dtgv.Columns[1].Width = 330;
                dtgv.Columns[1].HeaderText = "Tên sản phẩm";

                dtgv.Columns[2].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dtgv.Columns[2].HeaderCell.Style.Font = new Font("Arial", 16, FontStyle.Bold);
                dtgv.Columns[2].HeaderText = "Giá";

                dtgv.Columns[3].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dtgv.Columns[3].HeaderCell.Style.Font = new Font("Arial", 16, FontStyle.Bold);
                dtgv.Columns[3].Width = 116;
                dtgv.Columns[3].HeaderText = "Số lượng";
            }
        }
    }
}
