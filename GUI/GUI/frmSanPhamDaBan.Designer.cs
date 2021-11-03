namespace QuanLyCuaHangDienThoai
{
    partial class frmSanPhamDaBan
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
            this.reportSanPhamDaBan = new Microsoft.Reporting.WinForms.ReportViewer();
            this.SuspendLayout();
            // 
            // reportSanPhamDaBan
            // 
            this.reportSanPhamDaBan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reportSanPhamDaBan.Location = new System.Drawing.Point(0, 0);
            this.reportSanPhamDaBan.Name = "reportSanPhamDaBan";
            this.reportSanPhamDaBan.Size = new System.Drawing.Size(605, 373);
            this.reportSanPhamDaBan.TabIndex = 0;
            // 
            // frmSanPhamDaBan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(605, 373);
            this.Controls.Add(this.reportSanPhamDaBan);
            this.Name = "frmSanPhamDaBan";
            this.Text = "frmSanPhamDaBan";
            this.Load += new System.EventHandler(this.frmSanPhamDaBan_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportSanPhamDaBan;
    }
}