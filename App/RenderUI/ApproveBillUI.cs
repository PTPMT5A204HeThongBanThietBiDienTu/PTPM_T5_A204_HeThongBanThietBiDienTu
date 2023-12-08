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
    public class ApproveBillUI
    {
        BLL_DAL_Bill bdb = new BLL_DAL_Bill();
        BLL_DAL_BillProduct bdbP = new BLL_DAL_BillProduct();
        BLL_DAL_Product bdp = new BLL_DAL_Product();
        Control ctr;

        public ApproveBillUI(Control ctr)
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
            btn.Width = 50 + text.Length * 10;
            btn.Height = 40;
            btn.Click += btn_Click;
            ctr.Controls.Add(btn);
        }

        private void btn_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            
            switch (btn.Name)
            {
                case "btnPaid":
                    {
                        handlePayment();
                    }break;
                case "btnCancel":
                    {
                        handleCancel();
                    }
                    break;
            }
        }

        private void handlePayment()
        {
            DataGridView dtgvBill = (DataGridView)ctr.Controls.Find("dtgvBill", false)[0];
            DataGridView dtgvBillProduct = (DataGridView)ctr.Controls.Find("dtgvBillProduct", false)[0];
            TextBox txt = (TextBox)ctr.Controls.Find("txtBillId", false)[0];

            if (txt.Text == string.Empty)
            {
                MessageBox.Show("Vui lòng chọn hóa đơn cần thanh toán");
                return;
            }

            DialogResult dialogResult = MessageBox.Show("Bạn có chắc là muốn thanh toán hóa đơn này ?", "Thông báo", MessageBoxButtons.YesNo);
            if(dialogResult == DialogResult.Yes)
            {
                int n = dtgvBillProduct.Rows.Count;
                for(int i = 0; i < n; i++)
                {
                    string proId = dtgvBillProduct.Rows[i].Cells[0].Value.ToString();
                    int quantity = int.Parse(dtgvBillProduct.Rows[i].Cells[3].Value.ToString());

                    bdp.updateDecreaseQuantity(proId, quantity);
                }

                string billId = dtgvBill.CurrentRow.Cells[0].Value.ToString();
                string status = "paid";

                int result = bdb.updateStatus(billId, status);
                if (result == 1)
                {
                    MessageBox.Show("Thanh toán thành công");
                    loadDataBill(dtgvBill);

                    txt.Text = string.Empty;
                    dtgvBillProduct.Visible = false;
                }
                else
                    MessageBox.Show("Thanh toán thất bại");
            }
        }

        private void handleCancel()
        {
            DataGridView dtgvBill = (DataGridView)ctr.Controls.Find("dtgvBill", false)[0];
            DataGridView dtgvBillProduct = (DataGridView)ctr.Controls.Find("dtgvBillProduct", false)[0];
            TextBox txt = (TextBox)ctr.Controls.Find("txtBillId", false)[0];

            if (txt.Text == string.Empty)
            {
                MessageBox.Show("Vui lòng chọn hóa đơn cần hủy");
                return;
            }

            DialogResult dialogResult = MessageBox.Show("Bạn có chắc là muốn hủy hóa đơn này ?", "Thông báo", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                string billId = dtgvBill.CurrentRow.Cells[0].Value.ToString();
                int result = bdbP.deleteAll(billId);

                if (result == 1)
                {
                    bdb.delete(billId);
                    MessageBox.Show("Hủy hóa đơn thành công");
                    loadDataBill(dtgvBill);

                    txt.Text = string.Empty;
                    dtgvBillProduct.Visible = false;
                }
                else
                    MessageBox.Show("Hủy thất bại");
            }
        }

        public void renderDTGVBill(string name, int leftPos, int topPos)
        {
            DataGridView dtgv = new DataGridView();
            dtgv.Name = name;
            dtgv.Top = topPos;
            dtgv.Left = leftPos;
            dtgv.Width = 935;
            dtgv.Height = 300;
            dtgv.Font = new Font("Arial", 15);
            dtgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dtgv.MultiSelect = false;
            dtgv.CellMouseClick += dtgvBill_CellMouseClick;
            ctr.Controls.Add(dtgv);

            loadDataBill(dtgv);
        }

        private void dtgvBill_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dtgvBill = (DataGridView)sender;
            TextBox txt = (TextBox)ctr.Controls.Find("txtBillId", false)[0];
            DataGridView dtgvBillProduct = (DataGridView)ctr.Controls.Find("dtgvBillProduct", false)[0];

            string billId = dtgvBill.CurrentRow.Cells[0].Value.ToString();
            txt.Text = billId;
            dtgvBillProduct.Visible = true;
            loadDataBillProduct(dtgvBillProduct, billId);
        }

        private void loadDataBill(DataGridView dtgv)
        {
            List<Object> bills = bdb.getAllUnpaid();
            if (bills.Count == 0)
            {
                renderLabelReport("lblReport", 500, 350);
                ctr.Controls.RemoveByKey("dtgvBill");
                ctr.Controls.RemoveByKey("dtgvBillProduct");
            }
            else
            {
                dtgv.DataSource = bills;

                dtgv.ColumnHeadersHeight = 50;

                dtgv.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dtgv.Columns[0].HeaderCell.Style.Font = new Font("Arial", 16, FontStyle.Bold);
                dtgv.Columns[0].Width = 190;
                dtgv.Columns[0].HeaderText = "Mã hóa đơn";

                dtgv.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dtgv.Columns[1].HeaderCell.Style.Font = new Font("Arial", 16, FontStyle.Bold);
                dtgv.Columns[1].Width = 138;
                dtgv.Columns[1].HeaderText = "Tổng cộng";

                dtgv.Columns[2].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dtgv.Columns[2].HeaderCell.Style.Font = new Font("Arial", 16, FontStyle.Bold);
                dtgv.Columns[2].Width = 130;
                dtgv.Columns[2].HeaderText = "Trạng thái";

                dtgv.Columns[3].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dtgv.Columns[3].HeaderCell.Style.Font = new Font("Arial", 16, FontStyle.Bold);
                dtgv.Columns[3].Width = 130;
                dtgv.Columns[3].HeaderText = "Ngày lập";

                dtgv.Columns[4].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dtgv.Columns[4].HeaderCell.Style.Font = new Font("Arial", 16, FontStyle.Bold);
                dtgv.Columns[4].Width = 180;
                dtgv.Columns[4].HeaderText = "Khách hàng";

                dtgv.Columns[5].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dtgv.Columns[5].HeaderCell.Style.Font = new Font("Arial", 16, FontStyle.Bold);
                dtgv.Columns[5].Width = 125;
                dtgv.Columns[5].HeaderText = "SĐT";
            }
        }

        public void renderDTGVBillProduct(string name, int leftPos, int topPos)
        {
            DataGridView dtgv = new DataGridView();
            dtgv.Name = name;
            dtgv.Top = topPos;
            dtgv.Left = leftPos;
            dtgv.Width = 935;
            dtgv.Height = 250;
            dtgv.Font = new Font("Arial", 15);
            dtgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dtgv.MultiSelect = false;
            dtgv.Visible = false;
            ctr.Controls.Add(dtgv);
        }

        private void loadDataBillProduct(DataGridView dtgv, string billId)
        {
            dtgv.DataSource = bdbP.getAllByBillId(billId);

            dtgv.ColumnHeadersHeight = 50;

            dtgv.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dtgv.Columns[0].HeaderCell.Style.Font = new Font("Arial", 16, FontStyle.Bold);
            dtgv.Columns[0].Width = 190;
            dtgv.Columns[0].HeaderText = "Mã sản phẩm";

            dtgv.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dtgv.Columns[1].HeaderCell.Style.Font = new Font("Arial", 16, FontStyle.Bold);
            dtgv.Columns[1].Width = 351;
            dtgv.Columns[1].HeaderText = "Tên sản phẩm";

            dtgv.Columns[2].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dtgv.Columns[2].HeaderCell.Style.Font = new Font("Arial", 16, FontStyle.Bold);
            dtgv.Columns[2].Width = 200;
            dtgv.Columns[2].HeaderText = "Giá sản phẩm";

            dtgv.Columns[3].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dtgv.Columns[3].HeaderCell.Style.Font = new Font("Arial", 16, FontStyle.Bold);
            dtgv.Columns[3].Width = 150;
            dtgv.Columns[3].HeaderText = "Số lượng";
        }

        public void renderLabelReport(string name, int leftPos, int topPos)
        {
            Label lbl = new Label();
            lbl.Name = name;
            lbl.Text = "Không có hóa đơn nào cần duyệt";
            lbl.Left = leftPos;
            lbl.Top = topPos;

            lbl.Font = new Font("Arial", 25, FontStyle.Bold);
            lbl.Width = 650;
            lbl.Height = 50;
            ctr.Controls.Add(lbl);
        }
    }
}
