using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RenderUI;
using BLL_DAL;
using Aspose.Words;
using Aspose.Words.Tables;

namespace App
{
    public partial class frmDashboard : Form
    {
        BLL_DAL_Permission bdp = new BLL_DAL_Permission();
        BLL_DAL_Cart bdc = new BLL_DAL_Cart();
        BLL_DAL_BillProduct bdbP = new BLL_DAL_BillProduct();
        BLL_DAL_Bill bdb = new BLL_DAL_Bill();
        BLL_DAL_Order bdo = new BLL_DAL_Order();
        BLL_DAL_OrderDetails bdod = new BLL_DAL_OrderDetails();

        List<object> pers;
        string billId;
        BLL_DAL_User bdu = new BLL_DAL_User();

        public frmDashboard()
        {
            InitializeComponent();
            subMenuProduct.Renderer = new MyRenderer();
            renderItemList();
        }

        private class MyRenderer : ToolStripProfessionalRenderer
        {
            public MyRenderer() : base(new MyColors()) { }
        }

        private class MyColors : ProfessionalColorTable
        {
            public override Color MenuItemSelected
            {
                get { return Color.Black; }
            }

            public override Color MenuItemBorder
            {
                get { return Color.White; }
            }
        }

        private void renderItemList()
        {
            pers = bdp.getAllByRoleId(Program.currentUser.roleId);

            int topPos = 100;
            bool showLabelProduct = false;
            foreach (var per in pers)
            {
                string scrId = per.GetType().GetProperty("screenId").GetValue(per).ToString().Trim();
                string scrName = per.GetType().GetProperty("screenName").GetValue(per).ToString().Trim();
                if (scrId != "SR004" && scrId != "SR005")
                {
                    renderItem(scrName, scrId, topPos);
                    topPos += 49;
                }
                else
                {
                    showLabelProduct = true;
                    if (scrId == "SR004")
                        SR004.Visible = true;
                    if (scrId == "SR005")
                        SR005.Visible = true;
                }
            }

            if (showLabelProduct)
                renderItem("Sản phẩm", "lblProduct", topPos);
        }

        private void renderItem(string text, string key, int top)
        {
            Label lbl = new Label();
            lbl.Width = 272;
            lbl.Height = 50;
            lbl.Left = -1;
            lbl.Text = text;
            lbl.Top = top;
            lbl.TextAlign = ContentAlignment.MiddleCenter;
            lbl.Name = key;
            lbl.BackColor = Color.Black;
            lbl.ForeColor = Color.White;
            lbl.Font = new System.Drawing.Font("Arial", 14);
            lbl.Click += lbl_Click;
            lbl.BorderStyle = BorderStyle.Fixed3D;
            panel1.Controls.Add(lbl);
            this.Controls.Add(panel1);
        }

        private void lbl_Click(object sender, EventArgs e)
        {
            Label lbl = (Label)sender;
            activeLabel(lbl.Name);

            switch (lbl.Name)
            {
                case "SR001":
                    {
                        removeAllForm();
                        renderFormCategory();
                    }
                    break;
                case "SR002":
                    {
                        removeAllForm();
                        renderFormBrand();
                    }
                    break;
                case "SR003":
                    {
                        removeAllForm();
                        renderFormRole();
                    }
                    break;
                case "lblProduct":
                    {
                        subMenuProduct.Size = new Size(300, 200);
                        subMenuProduct.Show(new Point(lbl.Right, lbl.Bottom - 50));
                    }
                    break;
                case "SR006":
                    {
                        removeAllForm();
                        renderFormScreen();
                    }
                    break;
                case "SR007":
                    {
                        removeAllForm();
                        renderFormStaff();
                    }
                    break;
                case "SR008":
                    {
                        removeAllForm();
                        renderFormPermission();
                    }
                    break;
                case "SR009":
                    {
                        removeAllForm();
                        renderFormApproveBill();
                    }
                    break;
                case "SR010":
                    {
                        removeAllForm();
                        renderFormInfomation();
                    }
                    break;
                case "SR011":
                    {
                        removeAllForm();
                        renderFormSale();
                    }
                    break;
            }
        }

        private void removeAllForm()
        {
            removeFormBrand();
            removeFormCategory();
            removeFormRole();
            removeFormScreen();
            removeFormProductSale();
            removeFormCart();
            removeFormCreateBill();
            removeFormPermission();
            removeFormStaff();
            removeFormApproveBill();
            removeFormInfomation();
            removeFormSale();
            removeFormOrder();
            removeFormReceipt();
        }

        private void activeLabel(string lblName)
        {
            foreach (var per in pers)
            {
                string scrId = per.GetType().GetProperty("screenId").GetValue(per).ToString().Trim();
                if (scrId != "SR004" && scrId != "SR005")
                {
                    Label label = (Label)this.Controls.Find(scrId, true)[0];
                    if (label.Name == lblName)
                    {
                        label.BackColor = Color.White;
                        label.ForeColor = Color.Black;
                    }
                    else
                    {
                        label.BackColor = Color.Black;
                        label.ForeColor = Color.White;
                    }
                }
            }
        }

        /*********************************************************************/
        /*                                CATEGORY                           */
        /*********************************************************************/
        private void renderFormCategory()
        {
            AppUI appUI = new AppUI(this);

            appUI.renderLabel("Mã danh mục", "lblCatId", 460, 80);
            appUI.renderTextBox("txtCatId", 410, 680, 75, true);

            appUI.renderLabel("Tên danh mục", "lblCatName", 460, 150);
            appUI.renderTextBox("txtCatName", 410, 680, 145);

            CategoryUI catUI = new CategoryUI(this);

            catUI.renderButton("Thêm", "btnAdd", 500, 220);
            catUI.renderButton("Xóa", "btnDelete", 730, 220);
            catUI.renderButton("Sửa", "btnUpdate", 950, 220);

            catUI.renderDataGridView("dtgvCategory", 455, 300);
        }

        private void removeFormCategory()
        {
            this.Controls.RemoveByKey("lblCatId");
            this.Controls.RemoveByKey("lblCatName");
            this.Controls.RemoveByKey("txtCatId");
            this.Controls.RemoveByKey("txtCatName");
            this.Controls.RemoveByKey("btnAdd");
            this.Controls.RemoveByKey("btnUpdate");
            this.Controls.RemoveByKey("btnDelete");
            this.Controls.RemoveByKey("dtgvCategory");
        }

        /*********************************************************************/
        /*                                 BRAND                             */
        /*********************************************************************/
        private void renderFormBrand()
        {
            removeFormCategory();
            AppUI appUI = new AppUI(this);

            appUI.renderLabel("Mã thương hiệu", "lblBrandId", 460, 80);
            appUI.renderTextBox("txtBrandId", 410, 680, 75, true);

            appUI.renderLabel("Tên thương hiệu", "lblBrandName", 460, 150);
            appUI.renderTextBox("txtBrandName", 410, 680, 145);

            BrandUI braUI = new BrandUI(this);

            braUI.renderButton("Thêm", "btnAdd", 500, 220);
            braUI.renderButton("Xóa", "btnDelete", 730, 220);
            braUI.renderButton("Sửa", "btnUpdate", 950, 220);

            braUI.renderDataGridView("dtgvBrand", 455, 300);
        }

        private void removeFormBrand()
        {
            this.Controls.RemoveByKey("lblBrandId");
            this.Controls.RemoveByKey("lblBrandName");
            this.Controls.RemoveByKey("txtBrandId");
            this.Controls.RemoveByKey("txtBrandName");
            this.Controls.RemoveByKey("btnAdd");
            this.Controls.RemoveByKey("btnUpdate");
            this.Controls.RemoveByKey("btnDelete");
            this.Controls.RemoveByKey("dtgvBrand");
        }

        /*********************************************************************/
        /*                                  ROLE                             */
        /*********************************************************************/
        private void renderFormRole()
        {
            AppUI appUI = new AppUI(this);

            appUI.renderLabel("Mã nhóm quyền", "lblRoleId", 460, 80);
            appUI.renderTextBox("txtRoleId", 410, 680, 75, true);

            appUI.renderLabel("Tên nhóm quyền", "lblRoleName", 460, 150);
            appUI.renderTextBox("txtRoleName", 410, 680, 145);

            RoleUI roleUI = new RoleUI(this);

            roleUI.renderButton("Thêm", "btnAdd", 500, 220);
            roleUI.renderButton("Xóa", "btnDelete", 730, 220);
            roleUI.renderButton("Sửa", "btnUpdate", 950, 220);

            roleUI.renderDataGridView("dtgvRole", 455, 300);
        }

        private void removeFormRole()
        {
            this.Controls.RemoveByKey("lblRoleId");
            this.Controls.RemoveByKey("lblRoleName");
            this.Controls.RemoveByKey("txtRoleId");
            this.Controls.RemoveByKey("txtRoleName");
            this.Controls.RemoveByKey("btnAdd");
            this.Controls.RemoveByKey("btnUpdate");
            this.Controls.RemoveByKey("btnDelete");
            this.Controls.RemoveByKey("dtgvRole");
        }

        /*********************************************************************/
        /*                                  SCREEN                           */
        /*********************************************************************/
        private void renderFormScreen()
        {
            AppUI appUI = new AppUI(this);

            appUI.renderLabel("Mã màn hình", "lblScreenId", 460, 80);
            appUI.renderTextBox("txtScreenId", 410, 680, 75, true);

            appUI.renderLabel("Tên màn hình", "lblScreenName", 460, 150);
            appUI.renderTextBox("txtScreenName", 410, 680, 145);

            ScreenUI scrUI = new ScreenUI(this);

            scrUI.renderButton("Thêm", "btnAdd", 500, 220);
            scrUI.renderButton("Xóa", "btnDelete", 730, 220);
            scrUI.renderButton("Sửa", "btnUpdate", 950, 220);

            scrUI.renderDataGridView("dtgvScreen", 455, 300);
        }

        private void removeFormScreen()
        {
            this.Controls.RemoveByKey("lblScreenId");
            this.Controls.RemoveByKey("lblScreenName");
            this.Controls.RemoveByKey("txtScreenId");
            this.Controls.RemoveByKey("txtScreenName");
            this.Controls.RemoveByKey("btnAdd");
            this.Controls.RemoveByKey("btnUpdate");
            this.Controls.RemoveByKey("btnDelete");
            this.Controls.RemoveByKey("dtgvScreen");
        }

        /*********************************************************************/
        /*                               PRODUCT SALE                        */
        /*********************************************************************/
        private void renderFormProductSale()
        {
            AppUI appUI = new AppUI(this);

            appUI.renderLabel("Danh mục", "lblCategory", 350, 150);
            appUI.renderLabel("Thương hiệu", "lblBrand", 840, 150);

            ProductSaleUI prSUI = new ProductSaleUI(this, Program.currentUser.id);
            prSUI.renderCBoCategory("cboCategory", 200, 480, 145);

            prSUI.renderCBoBrand("cboBrand", 200, 1000, 145);

            prSUI.renderButtonNext("btnNext", 790, 600);

            prSUI.renderButtonPrevious("btnPrevious", 690, 600);

            prSUI.renderLabelReport("lblReport", 450, 400);

            prSUI.loadData();

            btnCart.Visible = true;
            btnCart.Location = new Point(1052, 77);
        }

        private void removeFormProductSale()
        {
            this.Controls.RemoveByKey("lblCategory");
            this.Controls.RemoveByKey("lblBrand");
            this.Controls.RemoveByKey("lblReport");
            this.Controls.RemoveByKey("cboCategory");
            this.Controls.RemoveByKey("cboBrand");
            this.Controls.RemoveByKey("btnNext");
            this.Controls.RemoveByKey("btnPreVious");
            btnCart.Visible = false;

            ProductSaleUI prSUI = new ProductSaleUI(this, Program.currentUser.id);
            prSUI.clearPanelProduct();
        }

        private void btnCart_Click(object sender, EventArgs e)
        {
            List<Object> carts = bdc.getAllByUserId(Program.currentUser.id);
            if (carts.Count == 0)
            {
                MessageBox.Show("Chưa có sản phẩm nào trong giỏ hàng. Vui lòng chọn sản phẩm");
                return;
            }

            removeAllForm();
            renderFormCart();
        }

        /*********************************************************************/
        /*                                  CART                             */
        /*********************************************************************/
        private void renderFormCart()
        {
            AppUI appUI = new AppUI(this);

            appUI.renderLabel("Mã sản phẩm", "lblProId", 460, 80);
            appUI.renderTextBox("txtProId", 410, 680, 75, true);

            appUI.renderLabel("Tên sản phẩm", "lblProName", 460, 150);
            appUI.renderTextBox("txtProName", 410, 680, 145, true);

            appUI.renderLabel("Số lượng", "lblQuantity", 460, 220);
            appUI.renderTextBox("txtQuantity", 410, 680, 215);

            CartUI cUI = new CartUI(this, Program.currentUser.id);

            btnAddCart.Visible = true;
            btnAddCart.Location = new Point(480, 280);

            cUI.renderButton("Xóa", "btnDelete", 780, 280);
            cUI.renderButton("Sửa", "btnUpdate", 970, 280);

            cUI.renderDataGridView("dtgvCart", 400, 350);

            btnPayment.Visible = true;
            btnPayment.Location = new Point(500, 630);

            btnCancelCart.Visible = true;
            btnCancelCart.Location = new Point(950, 630);
        }

        private void removeFormCart()
        {
            this.Controls.RemoveByKey("lblProId");
            this.Controls.RemoveByKey("lblProName");
            this.Controls.RemoveByKey("lblQuantity");
            this.Controls.RemoveByKey("txtProId");
            this.Controls.RemoveByKey("txtProName");
            this.Controls.RemoveByKey("txtQuantity");
            this.Controls.RemoveByKey("btnDelete");
            this.Controls.RemoveByKey("btnUpdate");
            this.Controls.RemoveByKey("dtgvCart");
            btnAddCart.Visible = false;
            btnPayment.Visible = false;
            btnCancelCart.Visible = false;
        }

        private void btnAddCart_Click(object sender, EventArgs e)
        {
            removeAllForm();
            renderFormProductSale();
        }

        private void btnPayment_Click(object sender, EventArgs e)
        {
            DataGridView dtgvCart = (DataGridView)this.Controls.Find("dtgvCart", false)[0];
            int n = dtgvCart.Rows.Count;
            if (n == 0)
            {
                DialogResult dialogResult = MessageBox.Show("Không có sản phẩm nào để thanh toán. Bạn có muốn chọn sản phẩm ?", "Thông báo", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    removeAllForm();
                    renderFormProductSale();
                }
            }
            else
            {
                CartUI cUI = new CartUI(this, Program.currentUser.id);
                string billId = cUI.handlePayment(dtgvCart, n);
                this.billId = billId;

                removeAllForm();
                renderFormCreateBill();
            }
        }

        private void btnCancelCart_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Bạn có chắc là muốn hủy giỏ hàng ?", "Thông báo", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                int result = bdc.deleteAll(Program.currentUser.id);
                if (result == 1)
                {
                    MessageBox.Show("Hủy thành công");
                    removeAllForm();
                    renderFormProductSale();
                }
                else
                    MessageBox.Show("Hủy thất bại");
            }
        }

        /*********************************************************************/
        /*                              CREATE BILL                          */
        /*********************************************************************/
        private void renderFormCreateBill()
        {
            AppUI appUI = new AppUI(this);

            appUI.renderLabel("Mã hóa đơn", "lblBillId", 490, 70);
            appUI.renderTextBox("txtBillId", 350, 680, 65, true);

            appUI.renderLabel("Tên nhân viên", "lblUsername", 490, 140);
            appUI.renderTextBox("txtUsername", 350, 680, 135, true);

            appUI.renderLabel("Ngày lập", "lblDate", 490, 210);
            appUI.renderTextBox("txtDate", 350, 680, 205, true);

            appUI.renderLabel("Thời gian lập", "lblTime", 490, 280);
            appUI.renderTextBox("txtTime", 350, 680, 275, true);

            appUI.renderLabel("Tổng cộng", "lblTotal", 490, 570);
            appUI.renderTextBox("txtTotal", 350, 680, 570, true);

            CreateBillUI bUI = new CreateBillUI(this, Program.currentUser, this.billId);

            bUI.loadBillInfo();
            bUI.renderDataGridView("dtgvBill", 400, 340);

            btnExportBill.Visible = true;
            btnExportBill.Location = new Point(500, 650);

            btnCancelBill.Visible = true;
            btnCancelBill.Location = new Point(800, 650);
        }

        private void removeFormCreateBill()
        {
            this.Controls.RemoveByKey("lblBillId");
            this.Controls.RemoveByKey("lblUsername");
            this.Controls.RemoveByKey("lblDate");
            this.Controls.RemoveByKey("lblTime");
            this.Controls.RemoveByKey("lblTotal");
            this.Controls.RemoveByKey("txtBillId");
            this.Controls.RemoveByKey("txtUsername");
            this.Controls.RemoveByKey("txtDate");
            this.Controls.RemoveByKey("txtTime");
            this.Controls.RemoveByKey("txtTotal");
            this.Controls.RemoveByKey("dtgvBill");

            btnExportBill.Visible = false;
            btnCancelBill.Visible = false;
        }

        private void btnExportBill_Click(object sender, EventArgs e)
        {

            WordExport we = new WordExport();

            TextBox txtBillId = (TextBox)this.Controls.Find("txtBillId", false)[0];
            TextBox txtUsername = (TextBox)this.Controls.Find("txtUsername", false)[0];
            TextBox txtTime = (TextBox)this.Controls.Find("txtTime", false)[0];
            TextBox txtDate = (TextBox)this.Controls.Find("txtDate", false)[0];
            TextBox txtTotal = (TextBox)this.Controls.Find("txtTotal", false)[0];
            DataGridView dtgvBill = (DataGridView)this.Controls.Find("dtgvBill", false)[0];

            string id = txtBillId.Text;
            string name = txtUsername.Text;
            string createTime = txtDate.Text + " " + txtTime.Text;
            string total = txtTotal.Text;

            we.PutValue(id, name, createTime, total);

            int rowNumber = dtgvBill.Rows.Count;

            Table productsTable = we.doc.GetChild(NodeType.Table, 0, true) as Table;
            we.InsertRows(productsTable, 1, 1, rowNumber);

            for (int i = 0; i < rowNumber; i++)
            {
                we.PutValueToRow(productsTable, i + 1, 0, i + 1 + "");
                we.PutValueToRow(productsTable, i + 1, 1, dtgvBill.Rows[i].Cells[1].Value.ToString());
                we.PutValueToRow(productsTable, i + 1, 2, dtgvBill.Rows[i].Cells[3].Value.ToString());
                we.PutValueToRow(productsTable, i + 1, 3, dtgvBill.Rows[i].Cells[2].Value.ToString());
            }

            Bill bill = bdb.getById(this.billId);
            frmCustomer frm = new frmCustomer();
            frm.ShowDialog();

            bool result = we.SaveAndOpenFile(DateTime.Parse(bill.createdAt.ToString()));
            bdb.updateCusId(billId, Program.cusId);
            bdc.deleteAll(Program.currentUser.id);
            removeAllForm();
            renderFormProductSale();
        }

        private void btnCancelBill_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Bạn có chắc là muốn hủy hóa đơn này ?", "Thông báo", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                int result = bdbP.deleteAll(this.billId);
                if (result == 1)
                {
                    bdb.delete(this.billId);
                    MessageBox.Show("Hủy hóa đơn thành công");
                    removeAllForm();
                    renderFormCart();
                }
                else
                    MessageBox.Show("Hủy thất bại");
            }
        }

        /*********************************************************************/
        /*                              APPROVE BILL                          */
        /*********************************************************************/
        private void renderFormApproveBill()
        {
            AppUI appUI = new AppUI(this);

            appUI.renderLabel("Mã hóa đơn", "lblBillId", 300, 60);
            appUI.renderTextBox("txtBillId", 410, 450, 55, true);

            ApproveBillUI abUI = new ApproveBillUI(this);

            abUI.renderButton("Đã thanh toán", "btnPaid", 920, 50);
            abUI.renderButton("Hủy", "btnCancel", 1160, 50);

            List<Object> bills = bdb.getAllUnpaid();
            if (bills.Count == 0)
                abUI.renderLabelReport("lblReport", 500, 350);
            else
            {
                abUI.renderDTGVBill("dtgvBill", 305, 115);
                abUI.renderDTGVBillProduct("dtgvBillProduct", 305, 440);
            }
        }

        private void removeFormApproveBill()
        {
            this.Controls.RemoveByKey("lblBillId");
            this.Controls.RemoveByKey("txtBillId");
            this.Controls.RemoveByKey("btnPaid");
            this.Controls.RemoveByKey("btnCancel");
            this.Controls.RemoveByKey("lblReport");
            this.Controls.RemoveByKey("dtgvBill");
            this.Controls.RemoveByKey("dtgvBillProduct");
        }

        /*********************************************************************/
        /*                                 STAFF                             */
        /*********************************************************************/
        private void renderFormStaff()
        {
            AppUI appUI = new AppUI(this);

            appUI.renderLabel("Tên nhân viên", "lblName", 310, 60);
            appUI.renderTextBox("txtName", 300, 500, 55);

            appUI.renderLabel("Số điện thoại", "lblPhone", 850, 60);

            appUI.renderLabel("Email", "lblEmail", 310, 130);

            appUI.renderLabel("Mật khẩu", "lblPassword", 850, 130);
            appUI.renderTextBox("txtPassword", 200, 1030, 125);

            appUI.renderLabel("Địa chỉ", "lblAddress", 310, 200);
            appUI.renderTextBox("txtAddress", 730, 500, 195);

            appUI.renderLabel("Nhóm quyền", "lblRole", 310, 270);

            StaffUI sUI = new StaffUI(this, Program.currentUser);

            sUI.renderTextBoxPhone("txtPhone", 1030, 55);
            sUI.renderTextBoxEmail("txtEmail", 500, 125);
            sUI.renderCBoRole("cboRole", 500, 265);
            sUI.renderCheckBox("cbActive", 860, 265);
            sUI.renderDataGridView("dtgvStaff", 315, 430);
            sUI.renderButton("Thêm", "btnAdd", 390, 340);
            sUI.renderButton("Xóa", "btnDelete", 620, 340);
            sUI.renderButton("Sửa", "btnUpdate", 840, 340);
            sUI.renderButton("Tìm", "btnSearch", 1050, 340);
            sUI.renderButton("Làm mới", "btnRefresh", 1090, 265);
        }

        private void removeFormStaff()
        {
            this.Controls.RemoveByKey("lblName");
            this.Controls.RemoveByKey("lblPhone");
            this.Controls.RemoveByKey("lblEmail");
            this.Controls.RemoveByKey("lblPassword");
            this.Controls.RemoveByKey("lblAddress");
            this.Controls.RemoveByKey("lblRole");
            this.Controls.RemoveByKey("txtName");
            this.Controls.RemoveByKey("txtPassword");
            this.Controls.RemoveByKey("txtAddress");
            this.Controls.RemoveByKey("txtPhone");
            this.Controls.RemoveByKey("txtEmail");
            this.Controls.RemoveByKey("cboRole");
            this.Controls.RemoveByKey("cbActive");
            this.Controls.RemoveByKey("dtgvStaff");
            this.Controls.RemoveByKey("btnAdd");
            this.Controls.RemoveByKey("btnDelete");
            this.Controls.RemoveByKey("btnUpdate");
            this.Controls.RemoveByKey("btnSearch");
            this.Controls.RemoveByKey("btnRefresh");
        }

        /*********************************************************************/
        /*                              PERMISSIONS                          */
        /*********************************************************************/
        private void renderFormPermission()
        {
            AppUI appUI = new AppUI(this);

            appUI.renderLabel("Nhóm quyền", "lblRole", 380, 130);

            PermissionUI pUI = new PermissionUI(this);
            pUI.renderCBoRole("cboRole", 200, 550, 125);
            pUI.renderGroupBox("gbPermission", 380, 200);
            pUI.renderButtonSave("btnSave", 1030, 120);
        }

        private void removeFormPermission()
        {
            this.Controls.RemoveByKey("lblRole");
            this.Controls.RemoveByKey("cboRole");
            this.Controls.RemoveByKey("gbPermission");
            this.Controls.RemoveByKey("btnSave");
        }

        /*********************************************************************/
        /*                              INFORMATION                          */
        /*********************************************************************/
        private void renderFormInfomation()
        {
            AppUI appUI = new AppUI(this);

            appUI.renderLabel("Họ tên", "lblUsername", 480, 150);
            appUI.renderTextBox("txtUsername", 410, 660, 145);

            appUI.renderLabel("Email", "lblEmail", 480, 220);
            appUI.renderLabel("Địa chỉ", "lblAddress", 480, 300);
            appUI.renderLabel("Số điện thoại", "lblPhone", 480, 410);

            InfomationUI iUI = new InfomationUI(this, Program.currentUser);

            iUI.renderTextBoxEmail("txtEmail", 660, 215);
            iUI.renderTextBoxAddress("txtAddress", 660, 290);
            iUI.renderTextBoxPhone("txtPhone", 660, 405);
            iUI.renderButton("btnSave", 480, 500);
            iUI.loadInfo();

            btnChangePassword.Visible = true;
            btnChangePassword.Location = new Point(830, 500);
        }

        private void removeFormInfomation()
        {
            this.Controls.RemoveByKey("lblUsername");
            this.Controls.RemoveByKey("lblEmail");
            this.Controls.RemoveByKey("lblAddress");
            this.Controls.RemoveByKey("lblPhone");
            this.Controls.RemoveByKey("txtUsername");
            this.Controls.RemoveByKey("txtPhone");
            this.Controls.RemoveByKey("txtEmail");
            this.Controls.RemoveByKey("txtAddress");
            this.Controls.RemoveByKey("btnSave");

            btnChangePassword.Visible = false;
        }

        private void btnChangePassword_Click(object sender, EventArgs e)
        {
            frmChangePassWord frm = new frmChangePassWord();
            frm.ShowDialog();
        }

        /*********************************************************************/
        /*                                 SALE                              */
        /*********************************************************************/
        private void renderFormSale()
        {
            AppUI appUI = new AppUI(this);

            appUI.renderLabel("Tìm kiếm hóa đơn:", "lblSearch", 300, 60);

            SaleUI abUI = new SaleUI(this);
            abUI.renderTextBoxSearch("txtSearch", 500, 55);
            List<Object> bills = bdb.getAllPaid();
            if (bills.Count == 0)
                abUI.renderLabelReport("lblReport", 500, 350);
            else
            {

                appUI.renderLabel("Danh sách hóa đơn", "DSHD", 305, 110);
                abUI.renderDataGridView("dtgvBill", 305, 135);
                appUI.renderLabel("Danh sách sản phẩm", "DSSP", 305, 340);
                abUI.renderDTGVBillProduct("dtgvBillProduct", 305, 375);
                appUI.renderLabel("Định dạng doanh thu:", "dinhdang", 305, 580);
                appUI.renderComboBox("typeOfDateExport", new string[] { "Ngày", "Nhiều ngày", "Tháng" }, 555, 580);
                ComboBox cb = (ComboBox)this.Controls.Find("typeOfDateExport", false)[0];
                cb.SelectedIndexChanged += cbo_Changed;
                appUI.renderLabel("Ngày thống kế:", "ngaythongke", 308, 623);
                abUI.renderDatePicker("startDate", 200, 488, 620);
                abUI.renderButtonExport("Export", 305, 660);

            }
        }
        public void cbo_Changed(object sender, EventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            AppUI appUI = new AppUI(this);
            SaleUI abUI = new SaleUI(this);
            if (cb.SelectedItem.ToString() == "Ngày")
            {
                appUI.renderLabel("Ngày thống kế:", "ngaythongke", 308, 623);
                abUI.renderDatePicker("startDate", 200, 488, 620);
                this.Controls.RemoveByKey("ngaybatdau");
                this.Controls.RemoveByKey("ngayketthuc");
                this.Controls.RemoveByKey("begin");
                this.Controls.RemoveByKey("end");
                this.Controls.RemoveByKey("thangthongke");
                this.Controls.RemoveByKey("Month");
            }
            else if (cb.SelectedItem.ToString() == "Nhiều ngày")
            {
                appUI.renderLabel("Ngày bắt đầu:", "ngaybatdau", 308, 623);
                abUI.renderDatePickerStart("begin", 200, 488, 620);
                appUI.renderLabel("Ngày kết thúc:", "ngayketthuc", 708, 623);
                abUI.renderDatePickerEnd("end", 200, 888, 620);
                this.Controls.RemoveByKey("ngaythongke");
                this.Controls.RemoveByKey("startDate");
                this.Controls.RemoveByKey("thangthongke");
                this.Controls.RemoveByKey("Month");
            }
            else
            {
                appUI.renderLabel("Tháng thống kế:", "thangthongke", 308, 623);
                abUI.renderDatePickerOnlyMonthYear("Month", 200, 492, 620);
                this.Controls.RemoveByKey("ngaybatdau");
                this.Controls.RemoveByKey("ngayketthuc");
                this.Controls.RemoveByKey("begin");
                this.Controls.RemoveByKey("end");
                this.Controls.RemoveByKey("ngaythongke");
                this.Controls.RemoveByKey("startDate");

            }
        }
        private void removeFormSale()
        {
            this.Controls.RemoveByKey("lblReport");
            this.Controls.RemoveByKey("DSHD");
            this.Controls.RemoveByKey("DSSP");
            this.Controls.RemoveByKey("dtgvBill");
            this.Controls.RemoveByKey("dtgvBillProduct");
            this.Controls.RemoveByKey("ngaythongke");
            this.Controls.RemoveByKey("startDate");
            this.Controls.RemoveByKey("Export");
            this.Controls.RemoveByKey("ngaybatdau");
            this.Controls.RemoveByKey("ngayketthuc");
            this.Controls.RemoveByKey("begin");
            this.Controls.RemoveByKey("end");
            this.Controls.RemoveByKey("thangthongke");
            this.Controls.RemoveByKey("Month");
            this.Controls.RemoveByKey("ngaythongke");
            this.Controls.RemoveByKey("startDate");
            this.Controls.RemoveByKey("dinhdang");
            this.Controls.RemoveByKey("typeOfDateExport");
            this.Controls.RemoveByKey("lblSearch");
            this.Controls.RemoveByKey("txtSearch");
        }

        /*********************************************************************/
        /*                                 ORDER                             */
        /*********************************************************************/
        private void renderFormOrder()
        {
            AppUI appUI = new AppUI(this);
            string idorder = bdo.getIdOrderInPending();

            appUI.renderLabel("Tìm kiếm hóa đơn:", "lblSearch", 300, 60);

            OrderUI abUI = new OrderUI(this, Program.currentUser);

            abUI.renderTextBoxSearch("txtSearch", 500, 55);
            BLL_DAL_Product bdpt = new BLL_DAL_Product();
            List<Object> products = bdpt.getAllDataGridView();
            if (products.Count == 0)
                abUI.renderLabelReport("lblReport", 500, 400);
            else
            {

                appUI.renderLabel("Danh sách sản phẩm", "DSSP", 305, 110);
                abUI.renderDataGridView("dtgvProduct", 305, 135);
            }
            appUI.renderLabel("Tạo đơn đặt hàng:", "lbltao", 300, 320);
            appUI.renderTextBox("txtIdOrder", 300, 500, 320, true);

            appUI.renderLabel("Mã sản phẩm:", "lblMa", 300, 365);
            appUI.renderTextBox("txtMa", 200, 470, 360, true);
            appUI.renderLabel("Tên sản phẩm:", "lblTen", 680, 365);
            appUI.renderTextBox("txtTen", 200, 850, 360, true);

            appUI.renderLabel("Hãng:", "lblHang", 300, 405);
            abUI.renderCBoBrand("cboBrand", 200, 470, 400);
            appUI.renderLabel("Loai:", "lbLoai", 680, 405);
            abUI.renderCBoCategory("cboCategory", 200, 850, 400);


            appUI.renderLabel("Giá:", "lblGia", 300, 445);
            appUI.renderTextBoxNumberOnly("txtGia", 200, 470, 440, true);
            appUI.renderLabel("Số lượng:", "lblSL", 680, 445);
            appUI.renderTextBoxNumberOnly("txtSoLuong", 200, 850, 440, true);

            TextBox IdOrder = (TextBox)this.Controls.Find("txtIdOrder", false)[0];
            if (idorder != "")
            {
                IdOrder.Text = idorder;
                abUI.renderDetailsDataGridView("dtgvOrderDetails", 300, 520);
            }
            else
            {
                IdOrder.Text = Guid.NewGuid().ToString();
            }


            Receipt r = new Receipt();

            abUI.renderButtonAddToOrder("btnThemSP", "Add", 300, 480);
            abUI.renderButtonFinishOrder("btnHoanThanhOrder", "Check out", 400, 480);
            appUI.renderButton("btnReceipt", "Nhận hàng", 580, 480);
            Button btn = (Button)this.Controls.Find("btnReceipt", false)[0];
            btn.Click += ButtonReceipt_Click;
        }
        private void ButtonReceipt_Click(object sender, EventArgs e)
        {
            removeAllForm();
            renderFormReceipt();
        }
        private void removeFormOrder()
        {
            this.Controls.RemoveByKey("DSSP");
            this.Controls.RemoveByKey("dtgvProduct");
            this.Controls.RemoveByKey("lblReport");
            this.Controls.RemoveByKey("lblSearch");

            this.Controls.RemoveByKey("lbltao");
            this.Controls.RemoveByKey("lblMa");
            this.Controls.RemoveByKey("txtMa");
            this.Controls.RemoveByKey("lblTen");
            this.Controls.RemoveByKey("txtTen");
            this.Controls.RemoveByKey("lblHang");
            this.Controls.RemoveByKey("cboBrand");
            this.Controls.RemoveByKey("lbLoai");
            this.Controls.RemoveByKey("cboCategory");
            this.Controls.RemoveByKey("lblGia");
            this.Controls.RemoveByKey("txtGia");
            this.Controls.RemoveByKey("lblSL");
            this.Controls.RemoveByKey("txtSoLuong");
            this.Controls.RemoveByKey("btnHoanThanhOrder");
            this.Controls.RemoveByKey("dtgvOrderDetails");
            this.Controls.RemoveByKey("btnThemSP");
            this.Controls.RemoveByKey("txtIdOrder");
            this.Controls.RemoveByKey("btnReceipt");
        }
        /*********************************************************************/
        /*                                 RECEIPT                           */
        /*********************************************************************/
        private void renderFormReceipt()
        {
            AppUI appUI = new AppUI(this);
            string idorder = bdo.getIdOrderInPending();

            appUI.renderLabel("Danh sách giao dịch:", "lblOrder", 300, 60);

            ReceiptUI abUI = new ReceiptUI(this, Program.currentUser);
            List<Object> orders = bdo.getAllOrders();
            if (orders.Count == 0)
                abUI.renderLabelReport("lblReport", 500, 400);
            else
            {
                abUI.renderDataGridView("dtgvOrders", 305, 105);
                appUI.renderLabel("Danh sách chi tiết:", "lblDetails", 300, 310);
                abUI.renderDetailsDataGridView("dtgvOrderDetails", 305, 335);
                appUI.renderButton("btnConfirm", "Confirm", 305, 538);
                appUI.renderButton("btnCancel", "Cancel", 455, 538);
                Button btn1 = (Button)this.Controls.Find("btnConfirm", false)[0];
                Button btn2 = (Button)this.Controls.Find("btnCancel", false)[0];
                btn1.Click += btnConfirm_Click;
                btn2.Click += btnCancel_Click;
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            DataGridView dtgvOrders = (DataGridView)this.Controls.Find("dtgvOrders", false)[0];
            ReceiptUI rui = new ReceiptUI(this, Program.currentUser);
            if (dtgvOrders.SelectedRows != null)
            {
                bdo.update(dtgvOrders.CurrentRow.Cells[0].Value.ToString(), "canceled");
                rui.loadData(dtgvOrders);
                this.Controls.RemoveByKey("dtgvOrderDetails");
            }
            else
                MessageBox.Show("Vui lòng chọn 1 đơn hàng để xác nhận!");

        }
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            DataGridView dtgvOrders = (DataGridView)this.Controls.Find("dtgvOrders", false)[0];
            DataGridView dtgvOrderDetails = (DataGridView)this.Controls.Find("dtgvOrderDetails", false)[0];
            ReceiptUI rui = new ReceiptUI(this, Program.currentUser);
            if (dtgvOrders.SelectedRows !=null)
            {
                Receipt rc = new Receipt();
                rc.id = Guid.NewGuid().ToString();
                rc.userId = Program.currentUser.id;
                rc.orderId = dtgvOrders.CurrentRow.Cells[0].Value.ToString();
                rc.createdAt = DateTime.Now.Date;
                BLL_DAL_Product bdpr = new BLL_DAL_Product();
                for (int i = 0; i < dtgvOrderDetails.Rows.Count; i++)
                {
                    int quantity = (int)dtgvOrderDetails.Rows[i].Cells[3].Value;
                    double price = bdpr.getPriceOfProduct(dtgvOrderDetails.Rows[i].Cells[1].Value.ToString());
                    rc.total += quantity * price; 
                }
                BLL_DAL_Receipt bdrc = new BLL_DAL_Receipt();
                BLL_DAL_ReceiptDetails bdrcd = new BLL_DAL_ReceiptDetails();
                bdrc.insert(rc);
                for (int i = 0; i < dtgvOrderDetails.Rows.Count; i++)
                {
                    ReceiptDetail rd = new ReceiptDetail();
                    rd.id = Guid.NewGuid().ToString();
                    rd.receiptId = rc.id;
                    rd.proId = dtgvOrderDetails.Rows[i].Cells[1].Value.ToString();
                    rd.price= bdpr.getPriceOfProduct(dtgvOrderDetails.Rows[i].Cells[1].Value.ToString());
                    rd.quantity = (int)dtgvOrderDetails.Rows[i].Cells[3].Value;
                    bdrcd.insert(rd);
                    bdpr.updateIncreaseQuantity(rd.proId,rd.quantity);
                }
                bdo.update(rc.orderId, "finished");
                rui.loadData(dtgvOrders);
                this.Controls.RemoveByKey("dtgvOrderDetails");
            }
            else
            {
                MessageBox.Show("Vui lòng chọn 1 đơn hàng để xác nhận!");
            }

        }
        private void removeFormReceipt()
        {
            this.Controls.RemoveByKey("lblOrder");
            this.Controls.RemoveByKey("lblReport");
            this.Controls.RemoveByKey("dtgvOrders");
            this.Controls.RemoveByKey("lblDetails");

            this.Controls.RemoveByKey("lblIDReceipt");
            this.Controls.RemoveByKey("ReceiptId");
            this.Controls.RemoveByKey("btnConfirm");
            this.Controls.RemoveByKey("btnCancel");
        }

        /*********************************************************************/
        private void pnlLogout_MouseClick(object sender, MouseEventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn là muốn thoát ?", "Thông báo", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                bdc.deleteAll(Program.currentUser.id);
                frmLogin frm = new frmLogin();
                this.Close();
                frm.Show();
            }
        }

        private void SR004_Click(object sender, EventArgs e)
        {
            removeAllForm();
            renderFormProductSale();
        }

        private void SR005_Click(object sender, EventArgs e)
        {
            removeAllForm();
            renderFormOrder();

        }
    }
}
