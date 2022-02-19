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
    public partial class frmReportHDB : Form
    {
        public frmReportHDB()
        {
            InitializeComponent();
        }

        public string MaHD { get; set; }
       
        private void reportHoaDonBan_Load(object sender, EventArgs e)
        {

        }

        private void frmReportHDB_Load(object sender, EventArgs e)
        {

            SqlConnection conn = new SqlConnection("Data Source=ADMIN\\SQLEXPRESS;Initial Catalog=CuaHangDienThoai; User = sa; password=trannhonhoa; Integrated Security=True");
            SqlDataAdapter adapter = new SqlDataAdapter("select sp.tensp, ct.MaSp, SLBan, DonGiaBan, ThanhTienBan from CHITIETHDBAN as ct, SanPham as sp where ct.masp = sp.masp and mahd = '"+MaHD+"'", conn);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "HD");
            this.reportHoaDonBan.LocalReport.ReportEmbeddedResource = "QuanLyCuaHangDienThoai.ReportHDB.rdlc";
            ReportDataSource rds = new ReportDataSource();
            rds.Name = "DataSet1";
            rds.Value = ds.Tables["HD"];
            reportHoaDonBan.LocalReport.DataSources.Add(rds);
            this.reportHoaDonBan.RefreshReport();
        }
    }
}
