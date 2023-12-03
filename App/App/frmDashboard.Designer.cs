
namespace App
{
    partial class frmDashboard
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDashboard));
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.subMenuProduct = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.SR004 = new System.Windows.Forms.ToolStripMenuItem();
            this.SR005 = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlLogout = new System.Windows.Forms.Panel();
            this.btnCart = new System.Windows.Forms.Button();
            this.btnAddCart = new System.Windows.Forms.Button();
            this.btnPayment = new System.Windows.Forms.Button();
            this.btnCancelCart = new System.Windows.Forms.Button();
            this.btnExportBill = new System.Windows.Forms.Button();
            this.btnCancelBill = new System.Windows.Forms.Button();
            this.btnChangePassword = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.subMenuProduct.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Black;
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(270, 729);
            this.panel1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel2.BackgroundImage")));
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel2.Location = new System.Drawing.Point(-8, -3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(285, 81);
            this.panel2.TabIndex = 1;
            // 
            // subMenuProduct
            // 
            this.subMenuProduct.BackColor = System.Drawing.Color.Black;
            this.subMenuProduct.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.subMenuProduct.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.subMenuProduct.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SR004,
            this.SR005});
            this.subMenuProduct.Name = "contextMenuStrip1";
            this.subMenuProduct.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.subMenuProduct.ShowImageMargin = false;
            this.subMenuProduct.Size = new System.Drawing.Size(195, 112);
            this.subMenuProduct.Text = "Bán hàng";
            // 
            // SR004
            // 
            this.SR004.ForeColor = System.Drawing.Color.White;
            this.SR004.Margin = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.SR004.Name = "SR004";
            this.SR004.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.SR004.Size = new System.Drawing.Size(199, 44);
            this.SR004.Text = "Bán hàng";
            this.SR004.Visible = false;
            this.SR004.Click += new System.EventHandler(this.SR004_Click);
            // 
            // SR005
            // 
            this.SR005.ForeColor = System.Drawing.Color.White;
            this.SR005.Margin = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.SR005.Name = "SR005";
            this.SR005.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.SR005.Size = new System.Drawing.Size(199, 44);
            this.SR005.Text = "Mua hàng";
            this.SR005.Visible = false;
            this.SR005.Click += new System.EventHandler(this.SR005_Click);
            // 
            // pnlLogout
            // 
            this.pnlLogout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlLogout.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnlLogout.BackgroundImage")));
            this.pnlLogout.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlLogout.Location = new System.Drawing.Point(1256, -3);
            this.pnlLogout.Name = "pnlLogout";
            this.pnlLogout.Size = new System.Drawing.Size(27, 29);
            this.pnlLogout.TabIndex = 3;
            this.pnlLogout.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pnlLogout_MouseClick);
            // 
            // btnCart
            // 
            this.btnCart.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCart.Location = new System.Drawing.Point(1134, 684);
            this.btnCart.Name = "btnCart";
            this.btnCart.Size = new System.Drawing.Size(147, 43);
            this.btnCart.TabIndex = 5;
            this.btnCart.Text = "Giỏ hàng";
            this.btnCart.UseVisualStyleBackColor = true;
            this.btnCart.Visible = false;
            this.btnCart.Click += new System.EventHandler(this.btnCart_Click);
            // 
            // btnAddCart
            // 
            this.btnAddCart.Font = new System.Drawing.Font("Arial", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddCart.Location = new System.Drawing.Point(931, 684);
            this.btnAddCart.Name = "btnAddCart";
            this.btnAddCart.Size = new System.Drawing.Size(197, 39);
            this.btnAddCart.TabIndex = 6;
            this.btnAddCart.Text = "Chọn sản phẩm";
            this.btnAddCart.UseVisualStyleBackColor = true;
            this.btnAddCart.Visible = false;
            this.btnAddCart.Click += new System.EventHandler(this.btnAddCart_Click);
            // 
            // btnPayment
            // 
            this.btnPayment.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.btnPayment.Font = new System.Drawing.Font("Arial", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPayment.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btnPayment.Location = new System.Drawing.Point(592, 680);
            this.btnPayment.Name = "btnPayment";
            this.btnPayment.Size = new System.Drawing.Size(205, 43);
            this.btnPayment.TabIndex = 7;
            this.btnPayment.Text = "Thanh toán";
            this.btnPayment.UseVisualStyleBackColor = false;
            this.btnPayment.Visible = false;
            this.btnPayment.Click += new System.EventHandler(this.btnPayment_Click);
            // 
            // btnCancelCart
            // 
            this.btnCancelCart.BackColor = System.Drawing.Color.Red;
            this.btnCancelCart.Font = new System.Drawing.Font("Arial", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelCart.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btnCancelCart.Location = new System.Drawing.Point(814, 688);
            this.btnCancelCart.Name = "btnCancelCart";
            this.btnCancelCart.Size = new System.Drawing.Size(111, 39);
            this.btnCancelCart.TabIndex = 8;
            this.btnCancelCart.Text = "Hủy";
            this.btnCancelCart.UseVisualStyleBackColor = false;
            this.btnCancelCart.Visible = false;
            this.btnCancelCart.Click += new System.EventHandler(this.btnCancelCart_Click);
            // 
            // btnExportBill
            // 
            this.btnExportBill.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.btnExportBill.Font = new System.Drawing.Font("Arial", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExportBill.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btnExportBill.Location = new System.Drawing.Point(757, 635);
            this.btnExportBill.Name = "btnExportBill";
            this.btnExportBill.Size = new System.Drawing.Size(254, 43);
            this.btnExportBill.TabIndex = 42;
            this.btnExportBill.Text = "Xuất hóa đơn";
            this.btnExportBill.UseVisualStyleBackColor = false;
            this.btnExportBill.Visible = false;
            this.btnExportBill.Click += new System.EventHandler(this.btnExportBill_Click);
            // 
            // btnCancelBill
            // 
            this.btnCancelBill.BackColor = System.Drawing.Color.Red;
            this.btnCancelBill.Font = new System.Drawing.Font("Arial", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelBill.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btnCancelBill.Location = new System.Drawing.Point(1034, 635);
            this.btnCancelBill.Name = "btnCancelBill";
            this.btnCancelBill.Size = new System.Drawing.Size(236, 43);
            this.btnCancelBill.TabIndex = 43;
            this.btnCancelBill.Text = "Hủy hóa đơn";
            this.btnCancelBill.UseVisualStyleBackColor = false;
            this.btnCancelBill.Visible = false;
            this.btnCancelBill.Click += new System.EventHandler(this.btnCancelBill_Click);
            // 
            // btnChangePassword
            // 
            this.btnChangePassword.Font = new System.Drawing.Font("Arial", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnChangePassword.Location = new System.Drawing.Point(512, 632);
            this.btnChangePassword.Name = "btnChangePassword";
            this.btnChangePassword.Size = new System.Drawing.Size(239, 42);
            this.btnChangePassword.TabIndex = 46;
            this.btnChangePassword.Text = "Đổi mật khẩu";
            this.btnChangePassword.UseVisualStyleBackColor = true;
            this.btnChangePassword.Visible = false;
            this.btnChangePassword.Click += new System.EventHandler(this.btnChangePassword_Click);
            // 
            // frmDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(1283, 729);
            this.Controls.Add(this.btnChangePassword);
            this.Controls.Add(this.btnCancelBill);
            this.Controls.Add(this.btnExportBill);
            this.Controls.Add(this.btnCancelCart);
            this.Controls.Add(this.btnPayment);
            this.Controls.Add(this.btnAddCart);
            this.Controls.Add(this.btnCart);
            this.Controls.Add(this.pnlLogout);
            this.Controls.Add(this.panel1);
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.HelpButton = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmDashboard";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CellPhoneS";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.panel1.ResumeLayout(false);
            this.subMenuProduct.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ContextMenuStrip subMenuProduct;
        private System.Windows.Forms.ToolStripMenuItem SR004;
        private System.Windows.Forms.ToolStripMenuItem SR005;
        private System.Windows.Forms.Panel pnlLogout;
        private System.Windows.Forms.Button btnCart;
        private System.Windows.Forms.Button btnAddCart;
        private System.Windows.Forms.Button btnPayment;
        private System.Windows.Forms.Button btnCancelCart;
        private System.Windows.Forms.Button btnExportBill;
        private System.Windows.Forms.Button btnCancelBill;
        private System.Windows.Forms.Button btnChangePassword;
    }
}