using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using BLL_DAL;
using System.Net.Http;
using System.IO;

namespace RenderUI
{
    public class ProductSaleUI
    {
        Control ctr;
        string userId;

        BLL_DAL_Category bdc = new BLL_DAL_Category();
        BLL_DAL_Brand bdb = new BLL_DAL_Brand();
        BLL_DAL_Product bdp = new BLL_DAL_Product();
        BLL_DAL_Cart bdCart = new BLL_DAL_Cart();

        List<Product> products;
        HttpClient client = new HttpClient();
        int page = 1, maxPage = 0;

        public ProductSaleUI(Control ctr, string userId)
        {
            this.ctr = ctr;
            this.userId = userId;
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
            cbo.SelectedValueChanged += cboCategory_SelectedValueChanged;
            ctr.Controls.Add(cbo);
        }

        private void cboCategory_SelectedValueChanged(object sender, EventArgs e)
        {
            ComboBox cboCategory = (ComboBox)sender;
            ComboBox cboBrand = (ComboBox)ctr.Controls.Find("cboBrand", false)[0];

            string catId = cboCategory.SelectedValue.ToString();
            string braId = cboBrand.SelectedValue.ToString();

            renderProductList(catId, braId, page);
        }

        public void renderCBoBrand(string name, int width, int leftPos, int topPos)
        {
            ComboBox cbo = new ComboBox();
            cbo.Name = name;
            cbo.Left = leftPos;
            cbo.Top = topPos;

            cbo.Width = width;
            cbo.Font = new Font("Arial", 16);
            cbo.DataSource = bdb.getAll();
            cbo.DisplayMember = "name";
            cbo.ValueMember = "id";
            cbo.SelectedIndexChanged += cboBrand_SelectedIndexChanged;
            ctr.Controls.Add(cbo);
        }

        private void cboBrand_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cboCategory = (ComboBox)ctr.Controls.Find("cboCategory", false)[0];
            ComboBox cboBrand = (ComboBox)sender;

            string catId = cboCategory.SelectedValue != null ? cboCategory.SelectedValue.ToString() : "";
            string braId = cboBrand.SelectedValue.ToString();

            renderProductList(catId, braId, page);
        }

        public void renderProductList(string catId, string braId, int page)
        {
            clearPanelProduct();
            products = bdp.getAllByCatIdAndBraId(catId, braId, page);

            Label lblReport = (Label)ctr.Controls.Find("lblReport", false)[0];
            Button btnNext = (Button)ctr.Controls.Find("btnNext", false)[0];
            Button btnPrevious = (Button)ctr.Controls.Find("btnPrevious", false)[0];

            if (products.Count == 0)
            {
                lblReport.Visible = true;
                btnNext.Visible = false;
                btnPrevious.Visible = false;
            }
            else
            {
                lblReport.Visible = false;
                btnNext.Visible = true;
                btnPrevious.Visible = true;

                int topPos = 230, leftPos = 350;
                foreach (Product product in products)
                {
                    Panel panel = new Panel();
                    panel.Name = "pnlProduct" + product.id;
                    panel.Top = topPos;
                    panel.Left = leftPos;
                    panel.Height = 350;
                    panel.BorderStyle = BorderStyle.FixedSingle;

                    renderPictureBox(panel, product.img);

                    renderLabelProName(panel, product.name);

                    renderLabelProPrice(panel, product.price);

                    renderButtonBuy(panel, product.id);

                    renderLabelQuantity(panel, product.quantity);

                    ctr.Controls.Add(panel);
                    leftPos += 220;
                }
            }
        }

        public void clearPanelProduct()
        {
            while (countPanelProduct() != 0)
            {
                foreach (Control c in ctr.Controls)
                {
                    if (c.GetType() == typeof(Panel))
                    {
                        Panel pnl = (Panel)c;
                        if (c.Name.StartsWith("pnlProduct"))
                            ctr.Controls.Remove(c);
                    }
                }
            }
        }

        private int countPanelProduct()
        {
            int count = 0;
            foreach (Control c in ctr.Controls)
            {
                if (c.GetType() == typeof(Panel))
                {
                    Panel pnl = (Panel)c;
                    if (c.Name.StartsWith("pnlProduct"))
                        count++;
                }
            }
            return count;
        }

        private async void renderPictureBox(Control ctr, string image)
        {
            var imageDes = await client.GetByteArrayAsync($"http://nrodark.click:7777{image}");
            MemoryStream ms = new MemoryStream(imageDes);
            System.Drawing.Image img = System.Drawing.Image.FromStream(ms);
            PictureBox pictureBox = new PictureBox();
            pictureBox.Image = img;
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox.Size = new Size(200, 200);
            ctr.Controls.Add(pictureBox);
        }

        private void renderLabelProName(Control ctr, string proName)
        {
            Label label = new Label();
            label.Text = proName;
            label.Font = new Font("Arial", 12, FontStyle.Bold);
            label.Width = 200;
            label.Height = 50;
            label.Top = 205;
            label.Left = 5;
            ctr.Controls.Add(label);
        }

        private void renderLabelProPrice(Control ctr, double price)
        {
            Label label = new Label();
            label.Text = "Giá: " + price.ToString("#,##") + " vnđ";
            label.Width = 200;
            label.ForeColor = Color.Red;
            label.Top = 255;
            label.Left = 5;
            label.Font = new Font("Arial", 12);
            ctr.Controls.Add(label);
        }

        private void renderButtonBuy(Control ctr, string name)
        {
            Button btn = new Button();
            btn.Text = "Chọn";
            btn.Name = name;
            btn.Click += btnBuy_Click;
            btn.Top = 305;
            btn.Left = 60;
            btn.Height = 30;
            ctr.Controls.Add(btn);
        }

        private void btnBuy_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            Cart cart = new Cart();
            cart.id = Guid.NewGuid().ToString();
            cart.proId = btn.Name;
            cart.userId = userId;
            cart.quantity = 1;

            int result = 0;
            if (bdCart.isExistsProduct(cart))
                result = bdCart.updateAdd(cart);
            else
                result = bdCart.insert(cart);

            if (result == 0)
                MessageBox.Show("Thất bại");
            else
            {
                bdp.updateDecreaseQuantity(btn.Name);
                MessageBox.Show("Thành công");

                Product product = bdp.getByProId(btn.Name);
                if(product.quantity <= 3 && product.quantity > 0)
                    MessageBox.Show("Sản phẩm sắp hết. Vui lòng nhập hàng");

                ComboBox cboBrand = (ComboBox)ctr.Controls.Find("cboBrand", false)[0];
                ComboBox cboCategory = (ComboBox)ctr.Controls.Find("cboCategory", false)[0];

                string catId = cboCategory.SelectedValue.ToString();
                string braId = cboBrand.SelectedValue.ToString();

                renderProductList(catId, braId, page);
            }
        }

        private void renderLabelQuantity(Control ctr, int quantity)
        {
            Label label = new Label();
            label.Text = "Số lượng: " + quantity.ToString();
            label.Width = 200;
            label.Top = 280;
            label.Left = 5;
            label.Font = new Font("Arial", 12);
            ctr.Controls.Add(label);
        }

        public void renderLabelReport(string name, int leftPos, int topPos)
        {
            Label lbl = new Label();
            lbl.Name = name;
            lbl.Text = "Không tìm thấy sản phẩm nào phù hợp";
            lbl.Left = leftPos;
            lbl.Top = topPos;

            lbl.Font = new Font("Arial", 25, FontStyle.Bold);
            lbl.Width = 650;
            lbl.Height = 50;
            ctr.Controls.Add(lbl);
        }

        public void renderButtonNext(string name, int leftPos, int topPos)
        {
            Button btn = new Button();
            btn.Text = ">>";
            btn.Name = name;
            btn.Click += btnNext_Click;
            btn.Top = topPos;
            btn.Left = leftPos;
            btn.Height = 40;
            btn.Font = new Font("Arial", 14, FontStyle.Bold);
            btn.Visible = false;
            ctr.Controls.Add(btn);
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            Button btnPrevious = (Button)ctr.Controls.Find("btnPrevious", false)[0];
            ComboBox cboCategory = (ComboBox)ctr.Controls.Find("cboCategory", false)[0];
            ComboBox cboBrand = (ComboBox)ctr.Controls.Find("cboBrand", false)[0];
            string catId = cboCategory.SelectedValue.ToString();
            string braId = cboBrand.SelectedValue.ToString();

            page++;
            int proNumber = bdp.countProductByCatIdAndBraId(catId, braId);
            if (proNumber % 2 == 0)
                maxPage = proNumber / 4;
            else
                maxPage = proNumber / 4 + 1;

            if (page > maxPage)
                this.page = maxPage;

            renderProductList(catId, braId, page);
            btnPrevious.Visible = true;
        }

        public void renderButtonPrevious(string name, int leftPos, int topPos)
        {
            Button btn = new Button();
            btn.Text = "<<";
            btn.Name = name;
            btn.Click += btnPrevious_Click;
            btn.Top = topPos;
            btn.Left = leftPos;
            btn.Height = 40;
            btn.Font = new Font("Arial", 14, FontStyle.Bold);
            btn.Visible = false;
            ctr.Controls.Add(btn);
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            Button btnNext = (Button)ctr.Controls.Find("btnNext", false)[0];
            ComboBox cboCategory = (ComboBox)ctr.Controls.Find("cboCategory", false)[0];
            ComboBox cboBrand = (ComboBox)ctr.Controls.Find("cboBrand", false)[0];
            
            page--;
            if (page < 1)
                this.page = 1;

            string catId = cboCategory.SelectedValue.ToString();
            string braId = cboBrand.SelectedValue.ToString();
            renderProductList(catId, braId, page);
            btnNext.Visible = true;
        }
    }
}
