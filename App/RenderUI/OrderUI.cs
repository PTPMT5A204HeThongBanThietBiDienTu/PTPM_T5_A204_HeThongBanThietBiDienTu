using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using BLL_DAL;
using System.IO;

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
        public string imagePath;
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

            dtgv.Width = 980;
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

            dtgvOrderDetail.Width = 980;
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
                renderLabelReport("lblReport", 500,620);
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
                dtgv.Columns[0].Visible = false;

                dtgv.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dtgv.Columns[1].HeaderCell.Style.Font = new Font("Arial", 16, FontStyle.Bold);
                dtgv.Columns[1].Width = 325;
                dtgv.Columns[1].HeaderText = "Mã sản phẩm";


                dtgv.Columns[2].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dtgv.Columns[2].HeaderCell.Style.Font = new Font("Arial", 16, FontStyle.Bold);
                dtgv.Columns[2].Width = 325;
                dtgv.Columns[2].HeaderText = "Tên sản phẩm";

                dtgv.Columns[3].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dtgv.Columns[3].HeaderCell.Style.Font = new Font("Arial", 16, FontStyle.Bold);
                dtgv.Columns[3].Width = 300;
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
                dtgv.Columns[0].Width = 250;
                dtgv.Columns[0].HeaderText = "Mã sản phẩm";
                dtgv.Columns[0].Visible = false;


                dtgv.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dtgv.Columns[1].HeaderCell.Style.Font = new Font("Arial", 16, FontStyle.Bold);
                dtgv.Columns[1].Width = 300;
                dtgv.Columns[1].HeaderText = "Tên sản phẩm";

                dtgv.Columns[2].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dtgv.Columns[2].HeaderCell.Style.Font = new Font("Arial", 16, FontStyle.Bold);
                dtgv.Columns[2].Width = 150;
                dtgv.Columns[2].HeaderText = "Loại" ;

                dtgv.Columns[3].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dtgv.Columns[3].HeaderCell.Style.Font = new Font("Arial", 16, FontStyle.Bold);
                dtgv.Columns[3].Width = 150;
                dtgv.Columns[3].HeaderText = "Hãng";

                dtgv.Columns[4].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dtgv.Columns[4].HeaderCell.Style.Font = new Font("Arial", 16, FontStyle.Bold);
                dtgv.Columns[4].Width = 150;
                dtgv.Columns[4].HeaderText = "Số lượng";

                dtgv.Columns[5].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dtgv.Columns[5].HeaderCell.Style.Font = new Font("Arial", 16, FontStyle.Bold);
                dtgv.Columns[5].Width = 200;
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
            cbo.DropDownStyle = ComboBoxStyle.DropDownList;
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
            cbo.DropDownStyle = ComboBoxStyle.DropDownList;
            ctr.Controls.Add(cbo);
        }
        public void renderButtonAddNewProduct(string name, string text, int leftPos, int topPos)
        {
            Button btn = new Button();
            btn.Text = text;
            btn.Name = name;
            btn.Click += btnAddNewProduct_Click;
            btn.Top = topPos;
            btn.Left = leftPos;
            btn.Height = 30;
            btn.Width = 200;
            ctr.Controls.Add(btn);
        }
        public void renderButtonInsertProduct(string name, string text, int leftPos, int topPos)
        {
            Button btn = new Button();
            btn.Text = text;
            btn.Name = name;
            btn.Click += btnInsert_Click;
            btn.Top = topPos;
            btn.Left = leftPos;
            btn.Height = 30;
            btn.Width = 80;
            btn.Visible = false;
            ctr.Controls.Add(btn);
        }
        public void renderButtonCancelProduct(string name, string text, int leftPos, int topPos)
        {
            Button btn = new Button();
            btn.Text = text;
            btn.Name = name;
            btn.Click += btnCancel_Click;
            btn.Top = topPos;
            btn.Left = leftPos;
            btn.Height = 30;
            btn.Width = 80;
            btn.Visible = false;
            ctr.Controls.Add(btn);
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có muốn hủy thêm sản phẩm này!", "Xác nhận hủy thêm sản phẩm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                TextBox txtTen = (TextBox)ctr.Controls.Find("txtTen", false)[0];
                ComboBox cboBra = (ComboBox)ctr.Controls.Find("cboBrand", false)[0];
                ComboBox cboCat = (ComboBox)ctr.Controls.Find("cboCategory", false)[0];
                TextBox txtGia = (TextBox)ctr.Controls.Find("txtGia", false)[0];
                TextBox txtSL = (TextBox)ctr.Controls.Find("txtSoLuong", false)[0];
                DataGridView ordergv = (DataGridView)ctr.Controls.Find("dtgvOrderDetails", false)[0];
                DataGridView productgv = (DataGridView)ctr.Controls.Find("dtgvProduct", false)[0];
                PictureBox pb = (PictureBox)ctr.Controls.Find("pictureBox1", false)[0];
                Label lblImg = (Label)ctr.Controls.Find("lblImg", false)[0];
                Button btnThemSP = (Button)ctr.Controls.Find("btnThemSP", false)[0];
                Button btnHoanThanhOrder = (Button)ctr.Controls.Find("btnHoanThanhOrder", false)[0];
                Button btnReceipt = (Button)ctr.Controls.Find("btnReceipt", false)[0];
                Button btnAddProduct = (Button)ctr.Controls.Find("btnAddProduct", false)[0];
                Button btnInsert = (Button)ctr.Controls.Find("btnInsert", false)[0];
                Button btnCancel = (Button)ctr.Controls.Find("btnCancel", false)[0];
                txtTen.ReadOnly = true;
                cboBra.Enabled = false;
                cboCat.Enabled = false;
                txtGia.ReadOnly = true;
                txtTen.Text = "";
                cboBra.SelectedIndex = 0;
                cboCat.SelectedIndex = 0;
                txtGia.Text = "";
                txtSL.Text = "";
                lblImg.Visible = false;
                btnThemSP.Visible = true;
                btnHoanThanhOrder.Visible = true;
                btnReceipt.Visible = true;
                btnAddProduct.Visible = true;
                productgv.Enabled = true;
                txtSL.ReadOnly = true;
                btnInsert.Visible = false;
                btnCancel.Visible = false;
                if (ordergv.Visible == false)
                    ordergv.Visible = true;
                pb.Visible = false;
            }
        }
        private void btnInsert_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có muốn thêm sản phẩm này!", "Xác nhận thêm sản phẩm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                string err = "";
                Product pd = new Product();
                TextBox txtMa = (TextBox)ctr.Controls.Find("txtMa", false)[0];
                TextBox txtTen = (TextBox)ctr.Controls.Find("txtTen", false)[0];
                ComboBox cboBra = (ComboBox)ctr.Controls.Find("cboBrand", false)[0];
                ComboBox cboCat = (ComboBox)ctr.Controls.Find("cboCategory", false)[0];
                TextBox txtGia = (TextBox)ctr.Controls.Find("txtGia", false)[0];
                TextBox txtSL = (TextBox)ctr.Controls.Find("txtSoLuong", false)[0];
                PictureBox pb = (PictureBox)ctr.Controls.Find("pictureBox1", false)[0];
                if (txtTen.Text.Trim().Length == 0)
                    err += "Vui lòng nhập tên sản phẩm!\n";
                if (cboBra.SelectedItem == null)
                    err += "Vui lòng chọn hãng!\n";
                if (cboCat.SelectedItem == null)
                    err += "Vui lòng chọn loại!\n";
                if (txtGia.Text.Trim().Length == 0)
                    err += "Vui lòng nhập giá!\n";
                if (txtSL.Text.Trim().Length == 0)
                    err += "Vui lòng nhập số lượng!\n";
                if (pb.Image == null)
                    err += "Vui lòng chọn ảnh sản phẩm!\n";

                if (err.Length == 0)
                {
                    try
                    {
                        Product pt = new Product();
                        pt.id = txtMa.Text;
                        pt.name = txtTen.Text;
                        pt.price = double.Parse(txtGia.Text);
                        pt.quantity = int.Parse(txtSL.Text);
                        pt.catId = cboCat.SelectedValue.ToString();
                        pt.braId = cboBra.SelectedValue.ToString();
                        pt.img = imagePath;
                        txtMa.Text = Guid.NewGuid().ToString();
                        txtTen.Text = "";
                        cboBra.SelectedIndex = 0;
                        cboCat.SelectedIndex = 0;
                        txtGia.Text = "";
                        txtSL.Text = "";
                        pb.Image = null;
                        bdp.insert(pt);
                        DataGridView dtgvProduct = (DataGridView)ctr.Controls.Find("dtgvProduct", false)[0];
                        products = bdp.getAllDataGridView();
                        reloadProductDataGridView(dtgvProduct);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else
                    MessageBox.Show(err);
            }

        }
        private void btnAddNewProduct_Click(object sender, EventArgs e)
        {
            TextBox txtMa = (TextBox)ctr.Controls.Find("txtMa", false)[0];
            TextBox txtTen = (TextBox)ctr.Controls.Find("txtTen", false)[0];
            ComboBox cboBra = (ComboBox)ctr.Controls.Find("cboBrand", false)[0];
            ComboBox cboCat = (ComboBox)ctr.Controls.Find("cboCategory", false)[0];
            TextBox txtGia = (TextBox)ctr.Controls.Find("txtGia", false)[0];
            TextBox txtSL = (TextBox)ctr.Controls.Find("txtSoLuong", false)[0];
            DataGridView ordergv =(DataGridView)ctr.Controls.Find("dtgvOrderDetails", false)[0];
            DataGridView productgv = (DataGridView)ctr.Controls.Find("dtgvProduct", false)[0];
            PictureBox pb = (PictureBox)ctr.Controls.Find("pictureBox1", false)[0];
            Label lblImg = (Label)ctr.Controls.Find("lblImg", false)[0];
            Button btnThemSP= (Button)ctr.Controls.Find("btnThemSP", false)[0];
            Button btnHoanThanhOrder= (Button)ctr.Controls.Find("btnHoanThanhOrder", false)[0];
            Button btnReceipt= (Button)ctr.Controls.Find("btnReceipt", false)[0];
            Button btnAddProduct= (Button)ctr.Controls.Find("btnAddProduct", false)[0];
            Button btnInsert= (Button)ctr.Controls.Find("btnInsert", false)[0];
            Button btnCancel = (Button)ctr.Controls.Find("btnCancel", false)[0];
            txtMa.Text = Guid.NewGuid().ToString();
            txtTen.ReadOnly = false;
            cboBra.Enabled = true;
            cboCat.Enabled = true;
            txtGia.ReadOnly = false;
            txtTen.Text = "";
            cboBra.SelectedIndex = 0;
            cboCat.SelectedIndex = 0;
            txtGia.Text = "";
            txtSL.Text = "";
            lblImg.Visible = true;
            btnThemSP.Visible = false;
            btnHoanThanhOrder.Visible = false;
            btnReceipt.Visible = false;
            btnAddProduct.Visible = false;
            productgv.Enabled = false;
            txtSL.ReadOnly = false;
            btnInsert.Visible = true;
            btnCancel.Visible = true;
            if (ordergv.Visible == true)
                ordergv.Visible = false;
            pb.Visible = true;
            


        }
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
        public void renderPictureBox(string name, int leftPos, int topPos)
        {
            PictureBox pb = new PictureBox();
            pb.Name = name;

            pb.Location = new System.Drawing.Point(leftPos, topPos);
            pb.Size = new System.Drawing.Size(200, 200);
            pb.TabStop = false;
            pb.SizeMode = PictureBoxSizeMode.Zoom ;
            pb.BorderStyle = BorderStyle.Fixed3D;
            pb.Click += btnUpload_Click;
            pb.MouseHover += pictureBox_Hover;
            pb.MouseLeave += pictureBox_Leave;
            pb.Visible = false;
            ctr.Controls.Add(pb);
        }
        public void pictureBox_Hover(object sender, EventArgs e)
        {
            PictureBox pb = (PictureBox)sender;
            pb.BackColor = Color.LightGray;

        }
        public void pictureBox_Leave(object sender, EventArgs e)
        {
            PictureBox pb = (PictureBox)sender;
            pb.BackColor = Color.White;
        }
        private async void btnUpload_Click(object sender, EventArgs e)
        {
            BLL_DAL_Image bdi = new BLL_DAL_Image();
            PictureBox pictureBox1 = (PictureBox)ctr.Controls.Find("pictureBox1", false)[0];
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "Please select an image";
            dialog.Multiselect = false;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                foreach (string fileSelected in dialog.FileNames)
                {
                    FileInfo file = new FileInfo(fileSelected);
                    System.Drawing.Image img = await bdi.uploadFile(file);
                    pictureBox1.Image = img;
                    imagePath = bdi.imagePath;
                }
            }
        }
        public void reloadProductDataGridView(DataGridView dtgvProduct)
        {
            dtgvProduct.DataSource = products;
            if (products.Count != 0)
            {
                dtgvProduct.ColumnHeadersHeight = 50;
                dtgvProduct.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dtgvProduct.Columns[0].HeaderCell.Style.Font = new Font("Arial", 16, FontStyle.Bold);
                dtgvProduct.Columns[0].Width = 190;
                dtgvProduct.Columns[0].HeaderText = "Mã sản phẩm";
                dtgvProduct.Columns[0].Visible = false;

                dtgvProduct.ColumnHeadersHeight = 50;
                dtgvProduct.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dtgvProduct.Columns[1].HeaderCell.Style.Font = new Font("Arial", 16, FontStyle.Bold);
                dtgvProduct.Columns[1].Width = 300;
                dtgvProduct.Columns[1].HeaderText = "Tên sản phẩm";


                dtgvProduct.Columns[2].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dtgvProduct.Columns[2].HeaderCell.Style.Font = new Font("Arial", 16, FontStyle.Bold);
                dtgvProduct.Columns[2].Width = 150;
                dtgvProduct.Columns[2].HeaderText = "Loại";

                dtgvProduct.Columns[3].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dtgvProduct.Columns[3].HeaderCell.Style.Font = new Font("Arial", 16, FontStyle.Bold);
                dtgvProduct.Columns[3].Width = 150;
                dtgvProduct.Columns[3].HeaderText = "Hãng";

                dtgvProduct.Columns[4].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dtgvProduct.Columns[4].HeaderCell.Style.Font = new Font("Arial", 16, FontStyle.Bold);
                dtgvProduct.Columns[4].Width = 150;
                dtgvProduct.Columns[4].HeaderText = "Số lượng";

                dtgvProduct.Columns[5].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dtgvProduct.Columns[5].HeaderCell.Style.Font = new Font("Arial", 16, FontStyle.Bold);
                dtgvProduct.Columns[5].Width = 200;
                dtgvProduct.Columns[5].HeaderText = "Giá";
            }
        }
        public void searchTextBoxChanged(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;
            DataGridView dtgvProduct = (DataGridView)ctr.Controls.Find("dtgvProduct", false)[0];
            products = bdp.getProductsBySearching(txt.Text.Trim());
            reloadProductDataGridView(dtgvProduct);
        }
    }
}
