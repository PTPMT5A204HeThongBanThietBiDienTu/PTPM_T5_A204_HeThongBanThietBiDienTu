
namespace App
{
    partial class frmCart
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
            this.dtgvCart = new System.Windows.Forms.DataGridView();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.txtProName = new System.Windows.Forms.TextBox();
            this.txtProId = new System.Windows.Forms.TextBox();
            this.lblProName = new System.Windows.Forms.Label();
            this.lblBrandId = new System.Windows.Forms.Label();
            this.lblSLuong = new System.Windows.Forms.Label();
            this.txtQuantity = new MyComponents.TextBoxNumber();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvCart)).BeginInit();
            this.SuspendLayout();
            // 
            // dtgvCart
            // 
            this.dtgvCart.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgvCart.Location = new System.Drawing.Point(20, 276);
            this.dtgvCart.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.dtgvCart.Name = "dtgvCart";
            this.dtgvCart.RowHeadersWidth = 62;
            this.dtgvCart.RowTemplate.Height = 28;
            this.dtgvCart.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtgvCart.Size = new System.Drawing.Size(611, 210);
            this.dtgvCart.TabIndex = 23;
            this.dtgvCart.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dtgvCart_CellMouseClick);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdate.Location = new System.Drawing.Point(424, 211);
            this.btnUpdate.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(119, 35);
            this.btnUpdate.TabIndex = 22;
            this.btnUpdate.Text = "Sửa";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.Location = new System.Drawing.Point(258, 211);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(119, 35);
            this.btnDelete.TabIndex = 21;
            this.btnDelete.Text = "Xóa";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.Location = new System.Drawing.Point(90, 211);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(119, 35);
            this.btnAdd.TabIndex = 20;
            this.btnAdd.Text = "Thêm";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // txtProName
            // 
            this.txtProName.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtProName.Location = new System.Drawing.Point(193, 75);
            this.txtProName.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.txtProName.Name = "txtProName";
            this.txtProName.ReadOnly = true;
            this.txtProName.Size = new System.Drawing.Size(387, 39);
            this.txtProName.TabIndex = 19;
            // 
            // txtProId
            // 
            this.txtProId.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtProId.Location = new System.Drawing.Point(193, 15);
            this.txtProId.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.txtProId.Name = "txtProId";
            this.txtProId.ReadOnly = true;
            this.txtProId.Size = new System.Drawing.Size(387, 39);
            this.txtProId.TabIndex = 18;
            // 
            // lblProName
            // 
            this.lblProName.AutoSize = true;
            this.lblProName.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProName.Location = new System.Drawing.Point(55, 78);
            this.lblProName.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblProName.Name = "lblProName";
            this.lblProName.Size = new System.Drawing.Size(195, 32);
            this.lblProName.TabIndex = 17;
            this.lblProName.Text = "Tên sản phẩm";
            // 
            // lblBrandId
            // 
            this.lblBrandId.AutoSize = true;
            this.lblBrandId.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBrandId.Location = new System.Drawing.Point(55, 22);
            this.lblBrandId.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblBrandId.Name = "lblBrandId";
            this.lblBrandId.Size = new System.Drawing.Size(185, 32);
            this.lblBrandId.TabIndex = 16;
            this.lblBrandId.Text = "Mã sản phẩm";
            // 
            // lblSLuong
            // 
            this.lblSLuong.AutoSize = true;
            this.lblSLuong.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSLuong.Location = new System.Drawing.Point(55, 139);
            this.lblSLuong.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblSLuong.Name = "lblSLuong";
            this.lblSLuong.Size = new System.Drawing.Size(128, 32);
            this.lblSLuong.TabIndex = 24;
            this.lblSLuong.Text = "Số lượng";
            // 
            // txtQuantity
            // 
            this.txtQuantity.Location = new System.Drawing.Point(191, 139);
            this.txtQuantity.Name = "txtQuantity";
            this.txtQuantity.Size = new System.Drawing.Size(387, 35);
            this.txtQuantity.TabIndex = 25;
            // 
            // frmCart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 29F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(655, 511);
            this.Controls.Add(this.txtQuantity);
            this.Controls.Add(this.lblSLuong);
            this.Controls.Add(this.dtgvCart);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.txtProName);
            this.Controls.Add(this.txtProId);
            this.Controls.Add(this.lblProName);
            this.Controls.Add(this.lblBrandId);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.Name = "frmCart";
            this.Text = "Giỏ hàng";
            this.Load += new System.EventHandler(this.frmCart_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtgvCart)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dtgvCart;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.TextBox txtProName;
        private System.Windows.Forms.TextBox txtProId;
        private System.Windows.Forms.Label lblProName;
        private System.Windows.Forms.Label lblBrandId;
        private System.Windows.Forms.Label lblSLuong;
        private MyComponents.TextBoxNumber txtQuantity;
    }
}