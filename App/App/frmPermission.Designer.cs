
namespace App
{
    partial class frmPermission
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
            this.dtgvPermission = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvPermission)).BeginInit();
            this.SuspendLayout();
            // 
            // dtgvPermission
            // 
            this.dtgvPermission.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgvPermission.Location = new System.Drawing.Point(217, 96);
            this.dtgvPermission.Name = "dtgvPermission";
            this.dtgvPermission.RowHeadersWidth = 62;
            this.dtgvPermission.RowTemplate.Height = 28;
            this.dtgvPermission.Size = new System.Drawing.Size(414, 238);
            this.dtgvPermission.TabIndex = 1;
            // 
            // frmPermission
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dtgvPermission);
            this.Name = "frmPermission";
            this.Text = "frmPermission";
            this.Load += new System.EventHandler(this.frmPermission_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtgvPermission)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dtgvPermission;
    }
}