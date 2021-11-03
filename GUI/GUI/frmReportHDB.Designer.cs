namespace QuanLyCuaHangDienThoai
{
    partial class frmReportHDB
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
            this.reportHoaDonBan = new Microsoft.Reporting.WinForms.ReportViewer();
            this.SuspendLayout();
            // 
            // reportHoaDonBan
            // 
            this.reportHoaDonBan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reportHoaDonBan.Location = new System.Drawing.Point(0, 0);
            this.reportHoaDonBan.Name = "reportHoaDonBan";
            this.reportHoaDonBan.Size = new System.Drawing.Size(426, 381);
            this.reportHoaDonBan.TabIndex = 0;
            this.reportHoaDonBan.Load += new System.EventHandler(this.reportHoaDonBan_Load);
            // 
            // frmReportHDB
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(426, 381);
            this.Controls.Add(this.reportHoaDonBan);
            this.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmReportHDB";
            this.Text = "reportHoaDon";
            this.Load += new System.EventHandler(this.frmReportHDB_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportHoaDonBan;
    }
}