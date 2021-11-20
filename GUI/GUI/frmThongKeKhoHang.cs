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
    public partial class frmThongKeKhoHang : Form
    {
        public frmThongKeKhoHang()
        {
            InitializeComponent();
        }

        private void frmThongKeKhoHang_Load(object sender, EventArgs e)
        {

            SqlConnection conn = new SqlConnection("Data Source=ADMIN\\SQLEXPRESS;Initial Catalog=CuaHangDienThoai; User = sa; password=trannhonhoa; Integrated Security=True");

            SqlDataAdapter adapter = new SqlDataAdapter("select MaSp, MaNhom, TenSP,MaNCC, SLTon, GiaBan, GiaNhap from SANPHAM", conn);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            this.reportThongKeKhoHang.LocalReport.ReportEmbeddedResource = "QuanLyCuaHangDienThoai.ReportKhoHang.rdlc";
            ReportDataSource rds = new ReportDataSource();
            rds.Name = "DataSet1";
            rds.Value = ds.Tables[0];
            reportThongKeKhoHang.LocalReport.DataSources.Add(rds);
            this.reportThongKeKhoHang.RefreshReport();
        }
    }
}
