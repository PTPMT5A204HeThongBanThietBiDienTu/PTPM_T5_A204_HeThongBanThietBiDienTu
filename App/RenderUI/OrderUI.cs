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
    public class OrderUI
    {
        Control ctr;
        User user;
        BLL_DAL_Product bdp = new BLL_DAL_Product();
        BLL_DAL_Category bdc = new BLL_DAL_Category();
        BLL_DAL_Brand bdd = new BLL_DAL_Brand();
        BLL_DAL_Order bdo = new BLL_DAL_Order();
        BLL_DAL_OrderDetails bdod = new BLL_DAL_OrderDetails();
        public static List<Object> products = new List<Object>() { };
        public static List<Object> orderdetails = new List<Object>() { };
        DataGridView dtgvOrderDetail = new DataGridView();
        public OrderUI(Control ctr, User CurrentUser)
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

            loadDetailData(dtgvOrderDetail);
        }
        private void loadDetailData(DataGridView dtgv)
        {
            TextBox IdOrder = (TextBox)ctr.Controls.Find("txtIdOrder", false)[0];
            orderdetails = bdod.getAllDetailsById(IdOrder.Text);
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
                dtgv.Columns[0].HeaderText = "Mã Order";

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
        private void dtgv_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dtgv = (DataGridView)sender;
            TextBox txtMa = (TextBox)ctr.Controls.Find("txtMa", false)[0];
            TextBox txtTen = (TextBox)ctr.Controls.Find("txtTen", false)[0];
            TextBox txtGia = (TextBox)ctr.Controls.Find("txtGia", false)[0];
            TextBox txtSL = (TextBox)ctr.Controls.Find("txtSoLuong", false)[0];
            TextBox txtSoLuong = (TextBox)ctr.Controls.Find("txtSoLuong", false)[0];
            ComboBox cboCat = (ComboBox)ctr.Controls.Find("cboCategory", false)[0];
            ComboBox cboBra = (ComboBox)ctr.Controls.Find("cboBrand", false)[0];

            txtMa.Text = dtgv.CurrentRow.Cells[0].Value.ToString();
            txtTen.Text = dtgv.CurrentRow.Cells[1].Value.ToString();
            txtGia.Text= dtgv.CurrentRow.Cells[5].Value.ToString();
            int indexBrand = cboBra.FindString(dtgv.CurrentRow.Cells[3].Value.ToString());
            int indexCategory = cboCat.FindString(dtgv.CurrentRow.Cells[2].Value.ToString());
            cboBra.SelectedIndex = indexBrand;
            cboCat.SelectedIndex = indexCategory;
            txtTen.ReadOnly = true;
            cboBra.Enabled = false;
            cboCat.Enabled = false;
            txtGia.ReadOnly = true;
            txtSL.ReadOnly = false;
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
        private void loadData(DataGridView dtgv)
        {
            products = bdp.getAllDataGridView();
            if (products.Count == 0)
            {
                renderLabelReport("lblReport", 500, 350);
                ctr.Controls.RemoveByKey("dtgvProduct");
            }
            else
            {
                dtgv.DataSource = products;

                dtgv.ColumnHeadersHeight = 50;

                dtgv.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dtgv.Columns[0].HeaderCell.Style.Font = new Font("Arial", 16, FontStyle.Bold);
                dtgv.Columns[0].Width = 190;
                dtgv.Columns[0].HeaderText = "Mã sản phẩm";


                dtgv.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dtgv.Columns[1].HeaderCell.Style.Font = new Font("Arial", 16, FontStyle.Bold);
                dtgv.Columns[1].Width = 138;
                dtgv.Columns[1].HeaderText = "Tên sản phẩm";

                dtgv.Columns[2].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dtgv.Columns[2].HeaderCell.Style.Font = new Font("Arial", 16, FontStyle.Bold);
                dtgv.Columns[2].Width = 130;
                dtgv.Columns[2].HeaderText = "Loại" ;

                dtgv.Columns[3].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dtgv.Columns[3].HeaderCell.Style.Font = new Font("Arial", 16, FontStyle.Bold);
                dtgv.Columns[3].Width = 130;
                dtgv.Columns[3].HeaderText = "Hãng";

                dtgv.Columns[4].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dtgv.Columns[4].HeaderCell.Style.Font = new Font("Arial", 16, FontStyle.Bold);
                dtgv.Columns[4].Width = 180;
                dtgv.Columns[4].HeaderText = "Số lượng";

                dtgv.Columns[5].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dtgv.Columns[5].HeaderCell.Style.Font = new Font("Arial", 16, FontStyle.Bold);
                dtgv.Columns[5].Width = 180;
                dtgv.Columns[5].HeaderText = "Giá";
            }
        }
        public void renderTextBoxSearch(string name, int leftPos, int topPos)
        {
            TextBox txt = new TextBox();
            txt.Name = name;
            txt.Left = leftPos;
            txt.Top = topPos;
            txt.Width = 410;
            txt.Font = new Font("Arial", 16);
            txt.TextChanged += searchTextBoxChanged;
            ctr.Controls.Add(txt);
        }
        public void renderCBoBrand(string name, int width, int leftPos, int topPos)
        {
            ComboBox cbo = new ComboBox();
            cbo.Name = name;
            cbo.Left = leftPos;
            cbo.Top = topPos;

            cbo.Width = width;
            cbo.Font = new Font("Arial", 16);
            cbo.DataSource = bdd.getAll();
            cbo.DisplayMember = "name";
            cbo.ValueMember = "id";
            cbo.Enabled = false;
            ctr.Controls.Add(cbo);
        }
        public void renderCBoCategory(string name, int width, int leftPos, int topPos)
        {
            ComboBox cbo = new ComboBox();
            cbo.Name = name;
            cbo.Left = leftPos;
            cbo.Top = topPos;

            cbo.Width = width;
            cbo.Font = new Font("Arial", 16);
            cbo.DataSource = bdc.getAll();
            cbo.DisplayMember = "name";
            cbo.ValueMember = "id";
            cbo.Enabled = false;
            ctr.Controls.Add(cbo);
        }
        //public void renderButtonAddNewProduct(string name, string text, int leftPos, int topPos)
        //{
        //    Button btn = new Button();
        //    btn.Text = text;
        //    btn.Name = name;
        //    btn.Click += btnAddNewProduct_Click;
        //    btn.Top = topPos;
        //    btn.Left = leftPos;
        //    btn.Height = 30;
        //    ctr.Controls.Add(btn);
        //}
        //private void btnAddNewProduct_Click(object sender, EventArgs e)
        //{
        //    TextBox txtMa=(TextBox)ctr.Controls.Find("txtMa", false)[0];
        //    TextBox txtTen = (TextBox)ctr.Controls.Find("txtTen", false)[0];
        //    ComboBox cboBra = (ComboBox)ctr.Controls.Find("cboBrand", false)[0];
        //    ComboBox cboCat = (ComboBox)ctr.Controls.Find("cboCategory", false)[0];
        //    TextBox txtGia = (TextBox)ctr.Controls.Find("txtGia", false)[0];
        //    TextBox txtSL = (TextBox)ctr.Controls.Find("txtSoLuong", false)[0];
        //    txtMa.Text = Guid.NewGuid().ToString();
        //    txtTen.ReadOnly = false;
        //    cboBra.Enabled = true;
        //    cboCat.Enabled = true;
        //    txtGia.ReadOnly = false;
        //    txtTen.Text = "";
        //    cboBra.SelectedIndex = 0;
        //    cboCat.SelectedIndex = 0;
        //    txtGia.Text = "";
        //    txtSL.Text = "";
        //}
        public void renderButtonAddToOrder(string name, string text, int leftPos, int topPos)
        {
            Button btn = new Button();
            btn.Text = text;
            btn.Name = name;
            btn.Click += ButtonAddToOrder_Click;
            btn.Top = topPos;
            btn.Left = leftPos;
            btn.Height = 30;
            ctr.Controls.Add(btn);
        }
        private void ButtonAddToOrder_Click(object sender, EventArgs e)
        {
            TextBox txtMa = (TextBox)ctr.Controls.Find("txtMa", false)[0];
            TextBox txtTen = (TextBox)ctr.Controls.Find("txtTen", false)[0];
            ComboBox cboBra = (ComboBox)ctr.Controls.Find("cboBrand", false)[0];
            ComboBox cboCat = (ComboBox)ctr.Controls.Find("cboCategory", false)[0];
            TextBox txtGia = (TextBox)ctr.Controls.Find("txtGia", false)[0];
            TextBox txtSL = (TextBox)ctr.Controls.Find("txtSoLuong", false)[0];
            TextBox IdOrder = (TextBox)ctr.Controls.Find("txtIdOrder", false)[0];
            Order o = new Order();
            
            o.id = IdOrder.Text;
            o.createdAt = DateTime.Now;
            o.status = "pending";
            o.userId = user.id;
            OrderDetail detail = new OrderDetail();
            detail.id= Guid.NewGuid().ToString();
            detail.orderId = o.id;
            detail.proId = txtMa.Text;
            try
            {
                detail.quantity = int.Parse(txtSL.Text.Trim());
            }
            catch
            {
                MessageBox.Show("Vui lòng nhập số lượng sản phẩm!");
                return;
            }
            bdo.insert(o);
            bdod.insert(detail);
            txtTen.Text = "";
            cboBra.SelectedIndex = 0;
            cboCat.SelectedIndex = 0;
            txtGia.Text = "";
            txtSL.Text = "";
            txtMa.Text = "";
            txtSL.ReadOnly = true;
                renderDetailsDataGridView("dtgvOrderDetails", 300, 520);
                loadDetailData(dtgvOrderDetail);

        }

        public void renderButtonFinishOrder(string name, string text, int leftPos, int topPos)
        {
            Button btn = new Button();
            btn.Text = text;
            btn.Name = name;
            btn.Click += ButtonFinishOrder_Click;
            btn.Top = topPos;
            btn.Left = leftPos;
            btn.Height = 30;
            btn.Width = 150;
            ctr.Controls.Add(btn);
        }
        private void ButtonFinishOrder_Click(object sender, EventArgs e)
        {
            try
            {
                if(dtgvOrderDetail.RowCount==0)
                {
                    MessageBox.Show("Vui lòng chọn ít nhất 1 sản phẩm để hoàn tất đơn đặt hàng!");
                    return;
                }    
                TextBox IdOrder = (TextBox)ctr.Controls.Find("txtIdOrder", false)[0];
                bdo.update(IdOrder.Text.Trim(), "proccessing");
                IdOrder.Text= Guid.NewGuid().ToString();
                loadDetailData(dtgvOrderDetail);
                MessageBox.Show("Đã hoàn thành việc đặt hàng!");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
           
        }
        public void searchTextBoxChanged(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;
            DataGridView dtgvProduct = (DataGridView)ctr.Controls.Find("dtgvProduct", false)[0];
            products = bdp.getProductsBySearching(txt.Text.Trim());
            dtgvProduct.DataSource = products;
            if (products.Count != 0)
            {
                dtgvProduct.ColumnHeadersHeight = 50;
                dtgvProduct.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dtgvProduct.Columns[0].HeaderCell.Style.Font = new Font("Arial", 16, FontStyle.Bold);
                dtgvProduct.Columns[0].Width = 190;
                dtgvProduct.Columns[0].HeaderText = "Mã sản phẩm";

                dtgvProduct.ColumnHeadersHeight = 50;
                dtgvProduct.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dtgvProduct.Columns[1].HeaderCell.Style.Font = new Font("Arial", 16, FontStyle.Bold);
                dtgvProduct.Columns[1].Width = 190;
                dtgvProduct.Columns[1].HeaderText = "Tên sản phẩm";


                dtgvProduct.Columns[2].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dtgvProduct.Columns[2].HeaderCell.Style.Font = new Font("Arial", 16, FontStyle.Bold);
                dtgvProduct.Columns[2].Width = 138;
                dtgvProduct.Columns[2].HeaderText = "Loại";

                dtgvProduct.Columns[3].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dtgvProduct.Columns[3].HeaderCell.Style.Font = new Font("Arial", 16, FontStyle.Bold);
                dtgvProduct.Columns[3].Width = 130;
                dtgvProduct.Columns[3].HeaderText = "Hãng";

                dtgvProduct.Columns[4].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dtgvProduct.Columns[4].HeaderCell.Style.Font = new Font("Arial", 16, FontStyle.Bold);
                dtgvProduct.Columns[4].Width = 130;
                dtgvProduct.Columns[4].HeaderText = "Số lượng";

                dtgvProduct.Columns[5].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dtgvProduct.Columns[5].HeaderCell.Style.Font = new Font("Arial", 16, FontStyle.Bold);
                dtgvProduct.Columns[5].Width = 180;
                dtgvProduct.Columns[5].HeaderText = "Giá";
            }

        }
    }
}
