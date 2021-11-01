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
    public partial class frmHDB : Form
    {
        public frmHDB()
        {
            InitializeComponent();
        }
        BLL_HDB bllhdb = new BLL_HDB();
        BLL_NhanVien bllnv = new BLL_NhanVien();
        BLL_KhachHang bllkh = new BLL_KhachHang();
        HDB hdn = new HDB();
        bool flagCheck; int pos = -1;
        private void frmPhieuNhap_Load(object sender, EventArgs e)
        {
            LoadDefault();
            LoadHoaDon();
        }
        private void DisableElemnts()
        {
            btnLuu.Enabled = false;
            btnHuy.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            txtMaHD.Enabled = false;
            cmbKH.Enabled = false;
            cmbNhanVien.Enabled = false;
            dtpNgayNhap.Enabled = false;
            txtThanhTien.Enabled = false;
        }
        private void EnbleElements()
        {
            btnLuu.Enabled = true;
            btnHuy.Enabled = true;
            txtMaHD.Enabled = true;
            cmbKH.Enabled = true;
            cmbNhanVien.Enabled = true;
            dtpNgayNhap.Enabled = true;
            txtThanhTien.Enabled = true;
            
        }
        private void AfterClickCell()
        {

            btnThemMoi.Enabled = false;
            btnLuu.Enabled = false;
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
        }
        private void Resettext()
        {
            txtMaHD.ResetText();
            txtThanhTien.ResetText();
            cmbKH.SelectedIndex = 0;
            cmbNhanVien.SelectedIndex = 0;
            dtpNgayNhap.Text = DateTime.Now.ToShortTimeString();
        }
        private void LoadDefault()
        {
            DisableElemnts();
            cmbKH.DataSource = bllkh.GetData("");
            cmbKH.DisplayMember = "Tenkh";
            cmbKH.ValueMember = "makh";
            cmbNhanVien.DataSource = bllnv.GetData("");
            cmbNhanVien.DisplayMember = "Tennv";
            cmbNhanVien.ValueMember = "Manv";
            
        }
        private void LoadHoaDon()
        {
            dgHoaDonNhap.DataSource = bllhdb.GetData("");
            dgHoaDonNhap.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgHoaDonNhap.Columns["MaHD"].HeaderText = "Mã HĐ";
            dgHoaDonNhap.Columns["MaHD"].Frozen = true;
            dgHoaDonNhap.Columns["MaHD"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgHoaDonNhap.Columns["MaHD"].Width = 120;
            dgHoaDonNhap.Columns["MaNV"].HeaderText = "Nhân Viên";
            dgHoaDonNhap.Columns["MaNV"].Width = 120;
            dgHoaDonNhap.Columns["MaKH"].HeaderText = "Khách Hàng";
            dgHoaDonNhap.Columns["MaKH"].Width = 120;
            dgHoaDonNhap.Columns["NgayBan"].HeaderText = "Ngày Nhập";
            dgHoaDonNhap.Columns["NgayBan"].Width = 200;  
            dgHoaDonNhap.Columns["TongTienBan"].HeaderText = "Tổng Tiền";
            dgHoaDonNhap.Columns["TongTienBan"].Width = 120;
        }
        

        private void btnThemMoi_Click(object sender, EventArgs e)
        {
            EnbleElements();
            txtMaHD.Focus();
            flagCheck = true;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (txtMaHD.Text == "")
            {
                MessageBox.Show("Mã hóa đơn không được trống !"); return;
            }
            hdn.MaHD = txtMaHD.Text;
            hdn.MaKH = cmbKH.SelectedValue.ToString();
            hdn.MaNV = cmbNhanVien.SelectedValue.ToString();
            hdn.NgayBan = dtpNgayNhap.Value;
           
            if (flagCheck)
            {
                try
                {
                    bllhdb.AddData(hdn);
                    MessageBox.Show("Thêm Thành Công", "Thêm Thất Bại");
                    
                }
                catch (Exception)
                {

                    MessageBox.Show("Lỗi! Không thêm được", "Thông báo");
                }
            }
            else
            {
                try
                {
                    bllhdb.EditData(hdn);
                    MessageBox.Show("Sửa Thành Công", "Thông báo");
                }
                catch (Exception)
                {

                    MessageBox.Show("Lỗi! Không Sửa được", "Thông báo");
                }
            }
            pos = -1;
            LoadHoaDon();
        }

        private void dgHoaDonNhap_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            pos = e.RowIndex;
            if (pos == -1) return;
            AfterClickCell();
            txtMaHD.Text = dgHoaDonNhap.Rows[pos].Cells[0].Value.ToString();
            cmbKH.Text = dgHoaDonNhap.Rows[pos].Cells[1].Value.ToString();
            cmbNhanVien.Text = dgHoaDonNhap.Rows[pos].Cells[2].Value.ToString();
            dtpNgayNhap.Text = dgHoaDonNhap.Rows[pos].Cells[3].Value.ToString();
            txtThanhTien.Text = dgHoaDonNhap.Rows[pos].Cells[4].Value.ToString();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            flagCheck = false;
            EnbleElements();

        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (pos == -1) return;
            AfterClickCell();
            hdn.MaHD = dgHoaDonNhap.Rows[pos].Cells[0].Value.ToString();
            try
            {
                DialogResult ret = MessageBox.Show("Bạn có chắc muốn xóa ?", "Thông báo");
                if (ret == DialogResult.OK)
                {
                    bllhdb.DeleteData(hdn);
                }
                MessageBox.Show("Xóa Thành Công", "Thông báo");
                LoadHoaDon();
            }
            catch (Exception)
            {

                MessageBox.Show("Lỗi! Không xóa được", "Thông báo");
            }
        }

        private void dgHoaDonNhap_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            frmCTHDB fr = new frmCTHDB();
            fr.SOHDB = txtMaHD.Text;
            this.Hide();
            fr.Show();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void cmbNCC_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void lbncc_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
    }
}
