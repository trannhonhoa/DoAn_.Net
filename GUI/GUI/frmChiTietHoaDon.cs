using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL;
using DTO;
namespace QuanLyCuaHangDienThoai
{
    public partial class frmChiTietHoaDon : Form
    {
        public frmChiTietHoaDon()
        {
            InitializeComponent();
        }
        BLL_CTHDB bllcthd = new BLL_CTHDB();
        BLL_SanPham bllsp = new BLL_SanPham();
        public string SoHD { get; set; }
        private void frmChiTietHoaDon_Load(object sender, EventArgs e)
        {
            LoadDefault();
        }
        private void DisableElemnts()
        {
            btnThemMoi.Enabled = true;
            btnLuu.Enabled = false;
            btnHuy.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            cmbSoHD.Enabled = false;
            cmbMaSP.Enabled = false;
            txtSoLuong.Enabled = false;
            txtDonGia.Enabled = false;
        }
        private void EnbleElements()
        {
            btnThemMoi.Enabled = false;
            btnLuu.Enabled = true;
            btnHuy.Enabled = true;
            cmbSoHD.Enabled = true;
            cmbMaSP.Enabled = true;
            txtSoLuong.Enabled = true;
            txtDonGia.Enabled = true;
        }
        
        private void Resettext()
        {
            txtSoLuong.ResetText();
            cmbSoHD.SelectedIndex = 0;
            cmbMaSP.SelectedIndex = 0;
            txtDonGia.ResetText();
            txtThanhTien.ResetText();
        }
        private void LoadDefault()
        {
            DisableElemnts();
            cmbMaSP.DataSource = bllsp.GetData("");
            cmbMaSP.DisplayMember = "tensp";
            cmbMaSP.ValueMember = "masp";
        }

        private void AfterClickCell()
        {

            btnThemMoi.Enabled = false;
            btnLuu.Enabled = false;
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnHuy.Enabled = true;
        }
        private void LoadCTHDB()
        {
            dgCTHDB.DataSource = bllcthd.GetData("where mahd = N'"+SoHD+"' ");
            dgCTHDB.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgCTHDB.Columns[0].HeaderText = "Số HDB";
            dgCTHDB.Columns[0].Frozen = true;
            dgCTHDB.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgCTHDB.Columns[0].Width = 100;
            dgCTHDB.Columns[1].HeaderText = "Mã Hàng";
            dgCTHDB.Columns[1].Width = 100;
            dgCTHDB.Columns[2].HeaderText = "Số Lượng";
            dgCTHDB.Columns[2].Width = 80;
            dgCTHDB.Columns[4].HeaderText = "Thành Tiền";
            dgCTHDB.Columns[4].Width = 80;
        }

    }
}

