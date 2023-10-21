using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL_DAL;
using System.Net.Http;
using System.IO;

namespace App
{
    public partial class frmProductSale : Form
    {
        BLL_DAL_Product bdp = new BLL_DAL_Product();
        BLL_DAL_Category bdc = new BLL_DAL_Category();
        BLL_DAL_Brand bdb = new BLL_DAL_Brand();
        BLL_DAL_Cart bdCart = new BLL_DAL_Cart();
        HttpClient client = new HttpClient();
        List<Product> products;

        int page = 1;
        public frmProductSale()
        {
            InitializeComponent();

            this.StartPosition = FormStartPosition.CenterScreen;
            this.WindowState = FormWindowState.Maximized;
        }

        private void frmProduct_Load(object sender, EventArgs e)
        {
            loadCBoBrand();
            loadCBoCategory();
        }

        private void loadCBoCategory()
        {
            cboCategory.DataSource = bdc.getAll();
            cboCategory.DisplayMember = "name";
            cboCategory.ValueMember = "id";
        }

        private void loadCBoBrand()
        {
            cboBrand.DataSource = bdb.getAll();
            cboBrand.DisplayMember = "name";
            cboBrand.ValueMember = "id";
        }

        private void renderProductCard(string catId, string braId, int page)
        {
            clearPanel();
            products = bdp.getAllByCatIdAndBraId(catId, braId, page);

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

                int topPos = 150, leftPos = 100;
                foreach (Product product in products)
                {
                    Panel panel = new Panel();
                    panel.Top = topPos;
                    panel.Left = leftPos;
                    panel.Height = 320;
                    panel.BorderStyle = BorderStyle.FixedSingle;

                    renderPictureBox(panel, product.img);

                    renderLabelProName(panel, product.name);

                    renderLabelProPrice(panel, product.price);

                    renderButtonBuy(panel, product.id);

                    this.Controls.Add(panel);
                    leftPos += 220;
                }
            }  
        }

        private void renderButtonBuy(Control ctr, string name)
        {
            Button btn = new Button();
            btn.Text = "Mua";
            btn.Name = name;
            btn.Click += Btn_Click;
            btn.Top = 280;
            btn.Left = 60;
            btn.Height = 30;
            ctr.Controls.Add(btn);
        }

        private void Btn_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            Cart cart = new Cart();
            cart.id = Guid.NewGuid().ToString();
            cart.proId = btn.Name;
            cart.userId = Program.currentUser.id;
            cart.quantity = 1;

            int result = 0;
            if (bdCart.isExistsProduct(cart))
                result = bdCart.updateAdd(cart);
            else
                result = bdCart.insert(cart);

            if (result == 0)
                MessageBox.Show("Thất bại");
            else
                MessageBox.Show("Thành công");
        }

        private void renderLabelProPrice(Control ctr, double price)
        {
            Label label = new Label();
            label.Text = "Giá: " + price.ToString("#,##") + " vnđ";
            label.Width = 200;
            label.ForeColor = Color.Red;
            label.Top = 255;
            ctr.Controls.Add(label);
        }

        private void renderLabelProName(Control ctr, string proName)
        {
            Label label = new Label();
            label.Text = proName;
            label.Font = new Font(Font, FontStyle.Bold);
            label.Width = 200;
            label.Height = 50;
            label.Top = 205;
            ctr.Controls.Add(label);
        }

        private async void renderPictureBox(Control ctr, string image)
        {
            var imageDes = await client.GetByteArrayAsync($"http://localhost:1234{image}");
            MemoryStream ms = new MemoryStream(imageDes);
            System.Drawing.Image img = System.Drawing.Image.FromStream(ms);
            PictureBox pictureBox = new PictureBox();
            pictureBox.Image = img;
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox.Size = new Size(200, 200);
            ctr.Controls.Add(pictureBox);
        }

        private int countPanel()
        {
            int count = 0;
            foreach(Control ctr in this.Controls)
            {
                if (ctr.GetType() == typeof(Panel))
                    count++;
            }
            return count;
        }

        private void clearPanel()
        {
            while(countPanel() != 0)
            {
                foreach (Control ctr in this.Controls)
                {
                    if (ctr.GetType() == typeof(Panel))
                        this.Controls.Remove(ctr);
                }
            }
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            page--;
            if (page < 1)
            {
                this.page = 1;
                btnPrevious.Visible = false;
            }
            string catId = cboCategory.SelectedValue.ToString();
            string braId = cboBrand.SelectedValue.ToString();
            renderProductCard(catId, braId, page);
            btnNext.Visible = true;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            page++;
            int maxPage = (products.Count / 5) + 1;
            if (page > maxPage)
            {
                this.page = maxPage;
                btnNext.Visible = false;
            }
            string catId = cboCategory.SelectedValue.ToString();
            string braId = cboBrand.SelectedValue.ToString();
            renderProductCard(catId, braId, page);
            btnPrevious.Visible = true;
        }

        private void cboCategory_SelectedValueChanged(object sender, EventArgs e)
        {
            string catId = cboCategory.SelectedValue.ToString();
            string braId = cboBrand.SelectedValue.ToString();
            renderProductCard(catId, braId, page);
        }

        private void cboBrand_SelectedIndexChanged(object sender, EventArgs e)
        {
            string catId = cboCategory.SelectedValue != null ? cboCategory.SelectedValue.ToString() : "";
            string braId = cboBrand.SelectedValue.ToString();
            renderProductCard(catId, braId, page);
        }

        private void btnCart_Click(object sender, EventArgs e)
        {
            frmCart frm = new frmCart();
            this.Hide();
            frm.ShowDialog();
        }

        private void frmProductSale_FormClosed(object sender, FormClosedEventArgs e)
        {
            frmMain frm = new frmMain();
            this.Hide();
            frm.ShowDialog();
        }
    }
}
