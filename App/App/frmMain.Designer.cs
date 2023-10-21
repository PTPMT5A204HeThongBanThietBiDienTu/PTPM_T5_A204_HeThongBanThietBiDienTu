
namespace App
{
    partial class frmMain
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.SR001 = new System.Windows.Forms.ToolStripMenuItem();
            this.productToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SR004 = new System.Windows.Forms.ToolStripMenuItem();
            this.SR005 = new System.Windows.Forms.ToolStripMenuItem();
            this.SR002 = new System.Windows.Forms.ToolStripMenuItem();
            this.SR003 = new System.Windows.Forms.ToolStripMenuItem();
            this.logout = new System.Windows.Forms.ToolStripMenuItem();
            this.SR006 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SR001,
            this.productToolStripMenuItem,
            this.SR002,
            this.SR003,
            this.SR006,
            this.logout});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(14, 4, 0, 4);
            this.menuStrip1.Size = new System.Drawing.Size(949, 44);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // SR001
            // 
            this.SR001.Name = "SR001";
            this.SR001.Size = new System.Drawing.Size(158, 36);
            this.SR001.Text = "Phân quyền";
            // 
            // productToolStripMenuItem
            // 
            this.productToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SR004,
            this.SR005});
            this.productToolStripMenuItem.Name = "productToolStripMenuItem";
            this.productToolStripMenuItem.Size = new System.Drawing.Size(138, 36);
            this.productToolStripMenuItem.Text = "Sản phẩm";
            // 
            // SR004
            // 
            this.SR004.Name = "SR004";
            this.SR004.Size = new System.Drawing.Size(270, 40);
            this.SR004.Text = "Nhập hàng";
            // 
            // SR005
            // 
            this.SR005.Name = "SR005";
            this.SR005.Size = new System.Drawing.Size(270, 40);
            this.SR005.Text = "Bán hàng";
            this.SR005.Click += new System.EventHandler(this.SR005_Click);
            // 
            // SR002
            // 
            this.SR002.Name = "SR002";
            this.SR002.Size = new System.Drawing.Size(168, 36);
            this.SR002.Text = "Thương hiệu";
            this.SR002.Click += new System.EventHandler(this.SR002_Click);
            // 
            // SR003
            // 
            this.SR003.Name = "SR003";
            this.SR003.Size = new System.Drawing.Size(141, 36);
            this.SR003.Text = "Danh mục";
            this.SR003.Click += new System.EventHandler(this.SR003_Click);
            // 
            // logout
            // 
            this.logout.Name = "logout";
            this.logout.Size = new System.Drawing.Size(140, 36);
            this.logout.Text = "Đăng xuất";
            this.logout.Click += new System.EventHandler(this.logout_Click);
            // 
            // SR006
            // 
            this.SR006.Name = "SR006";
            this.SR006.Size = new System.Drawing.Size(160, 36);
            this.SR006.Text = "Người dùng";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 29F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(949, 472);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.Name = "frmMain";
            this.Text = "Trang chủ";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmMain_FormClosed);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem SR001;
        private System.Windows.Forms.ToolStripMenuItem productToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SR004;
        private System.Windows.Forms.ToolStripMenuItem SR005;
        private System.Windows.Forms.ToolStripMenuItem SR002;
        private System.Windows.Forms.ToolStripMenuItem SR003;
        private System.Windows.Forms.ToolStripMenuItem logout;
        private System.Windows.Forms.ToolStripMenuItem SR006;
    }
}