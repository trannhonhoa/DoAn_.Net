using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using Microsoft.Reporting.WinForms;

namespace QuanLyCuaHangDienThoai
{
    public partial class frmSanPhamDaBan : Form
    {
        public frmSanPhamDaBan()
        {
            InitializeComponent();
        }

        private void frmSanPhamDaBan_Load(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection("Server=DESKTOP-P9L00KA; Database=CuaHangDienThoai; User Id=sa; pwd=trannhonhoa");

            SqlDataAdapter adapter = new SqlDataAdapter("select ct.MaHD, MaSP, SLBan, DonGiaBan, ThanhTienBan from HOADONBAN as hdb, CHITIETHDBAN as ct where hdb.MaHD = ct.MaHD", conn);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            this.reportSanPhamDaBan.LocalReport.ReportEmbeddedResource = "QuanLyCuaHangDienThoai.ReportSanPhamDaBan.rdlc";
            ReportDataSource rds = new ReportDataSource();
            rds.Name = "DataSet1";
            rds.Value = ds.Tables[0];
            reportSanPhamDaBan.LocalReport.DataSources.Add(rds);
            this.reportSanPhamDaBan.RefreshReport();
        }
    }
}
