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
    public class ReceiptUI
    {
        Control ctr;
        User user;
        BLL_DAL_Order bdo = new BLL_DAL_Order();
        BLL_DAL_OrderDetails bdod = new BLL_DAL_OrderDetails();
        public static List<Object> orders = new List<Object>() { };
        public static List<Object> orderdetails = new List<Object>() { };
        DataGridView dtgvOrderDetail = new DataGridView();
        public ReceiptUI(Control ctr, User CurrentUser)
        {
            this.ctr = ctr;
            this.user = CurrentUser;
        }
        public void renderDataGridView(string name, int leftPos, int topPos)
        {
            DataGridView dtgv = new DataGridView();
            dtgv.Name = name;
            dtgv.Left = leftPos;
            dtgv.Top = topPos;

            dtgv.Width = ctr.Width - 600;
            dtgv.Height = 180;
            dtgv.Font = new Font("Arial", 15);
            dtgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dtgv.MultiSelect = false;
            dtgv.CellMouseClick += dtgv_CellMouseClick;
            ctr.Controls.Add(dtgv);

            loadData(dtgv);
        }
        public void loadData(DataGridView dtgv)
        {
            orders = bdo.getAllOrders();
            if (orders.Count == 0)
            {
                renderLabelReport("lblReport", 500, 350);
                ctr.Controls.RemoveByKey("dtgvOrders");
            }
            else
            {
                dtgv.DataSource = orders;

                dtgv.ColumnHeadersHeight = 50;

                dtgv.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dtgv.Columns[0].HeaderCell.Style.Font = new Font("Arial", 16, FontStyle.Bold);
                dtgv.Columns[0].Width = 210;
                dtgv.Columns[0].HeaderText = "Mã giao dịch";

                dtgv.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dtgv.Columns[1].HeaderCell.Style.Font = new Font("Arial", 16, FontStyle.Bold);
                dtgv.Columns[1].Width = 210;
                dtgv.Columns[1].HeaderText = "Mã nhân viên";

                dtgv.Columns[2].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dtgv.Columns[2].HeaderCell.Style.Font = new Font("Arial", 16, FontStyle.Bold);
                dtgv.Columns[2].Width = 210;
                dtgv.Columns[2].HeaderText = "Tên nhân viên";

                dtgv.Columns[3].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dtgv.Columns[3].HeaderCell.Style.Font = new Font("Arial", 16, FontStyle.Bold);
                dtgv.Columns[3].Width = 210;
                dtgv.Columns[3].HeaderText = "Ngày tạo";
            }
        }
        public void renderLabelReport(string name, int leftPos, int topPos)
        {
            Label lbl = new Label();
            lbl.Name = name;
            lbl.Text = "Không có sản phẩm nào!";
            lbl.Left = leftPos;
            lbl.Top = topPos;

            lbl.Font = new Font("Arial", 25, FontStyle.Bold);
            lbl.Width = 650;
            lbl.Height = 50;
            ctr.Controls.Add(lbl);
        }
        private void dtgv_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dtgvOrders = (DataGridView)sender;
            
            try
            {
                dtgvOrderDetail = (DataGridView)ctr.Controls.Find("dtgvOrderDetails", false)[0];
            }
            catch
            {
                renderDetailsDataGridView("dtgvOrderDetails", 305, 335);
                dtgvOrderDetail = (DataGridView)ctr.Controls.Find("dtgvOrderDetails", false)[0];
            }

            string orderId = dtgvOrders.CurrentRow.Cells[0].Value.ToString();
            
            loadDetailData(dtgvOrderDetail, orderId);
        }
        public void renderDetailsDataGridView(string name, int leftPos, int topPos)
        {

            dtgvOrderDetail.Name = name;
            dtgvOrderDetail.Left = leftPos;
            dtgvOrderDetail.Top = topPos;

            dtgvOrderDetail.Width = ctr.Width - 600;
            dtgvOrderDetail.Height = 180;
            dtgvOrderDetail.Font = new Font("Arial", 15);
            dtgvOrderDetail.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dtgvOrderDetail.MultiSelect = false;
            ctr.Controls.Add(dtgvOrderDetail);
        }
        public void loadDetailData(DataGridView dtgv,string orderid)
        {
            orderdetails = bdod.getAllDetailsById(orderid);
            if (orderdetails.Count == 0)
            {
                renderLabelReport("lblReport", 500, 520);
                ctr.Controls.RemoveByKey("dtgvOrderDetails");
            }
            else
            {
                ctr.Controls.RemoveByKey("lblReport");
                dtgv.DataSource = orderdetails;

                dtgv.ColumnHeadersHeight = 50;

                dtgv.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dtgv.Columns[0].HeaderCell.Style.Font = new Font("Arial", 16, FontStyle.Bold);
                dtgv.Columns[0].Width = 220;
                dtgv.Columns[0].HeaderText = "Mã giao dịch";

                dtgv.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dtgv.Columns[1].HeaderCell.Style.Font = new Font("Arial", 16, FontStyle.Bold);
                dtgv.Columns[1].Width = 220;
                dtgv.Columns[1].HeaderText = "Mã sản phẩm";


                dtgv.Columns[2].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dtgv.Columns[2].HeaderCell.Style.Font = new Font("Arial", 16, FontStyle.Bold);
                dtgv.Columns[2].Width = 220;
                dtgv.Columns[2].HeaderText = "Tên sản phẩm";

                dtgv.Columns[3].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dtgv.Columns[3].HeaderCell.Style.Font = new Font("Arial", 16, FontStyle.Bold);
                dtgv.Columns[3].Width = 220;
                dtgv.Columns[3].HeaderText = "Số lượng";
            }
        }
    }
}
