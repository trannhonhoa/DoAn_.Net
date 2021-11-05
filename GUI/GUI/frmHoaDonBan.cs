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
    public partial class frmHoaDonBan : Form
    {
        public frmHoaDonBan()
        {
            InitializeComponent();
        }
        BLL_HoaDonBan bllhdb = new BLL_HoaDonBan();
        BLL_KhachHang bllkh = new BLL_KhachHang();
        BLL_NhanVien bllnv = new BLL_NhanVien();
        BLL_CTHDB bllcthd = new BLL_CTHDB();
        HoaDonBan hdb = new HoaDonBan();
        CTHDB cthd = new CTHDB();
        bool flagCheck; int pos = -1;
        private void frmHoaDonBan_Load(object sender, EventArgs e)
        {
            LoadHoaDon();
            LoadDefault();
        }
        public void LoadHoaDon()
        {
           

            dgHoaDon.DataSource = bllhdb.GetData("");
            dgHoaDon.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            if (dgHoaDon.Rows.Count > 0)
            {
                dgHoaDon.Columns["MaHD"].HeaderText = "Số HDB";
                dgHoaDon.Columns["MaHD"].Frozen = true;
                dgHoaDon.Columns["MaHD"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgHoaDon.Columns["MaHD"].Width = 100;
                dgHoaDon.Columns["MaNV"].HeaderText = "Nhân Viên";
                dgHoaDon.Columns["MaNV"].Width = 200;
                dgHoaDon.Columns["MaKH"].HeaderText = "Khách Hàng";
                dgHoaDon.Columns["MaKH"].Width = 100;
                dgHoaDon.Columns["NgayBan"].HeaderText = "Ngày Bán";
                dgHoaDon.Columns["NgayBan"].Width = 100;
                dgHoaDon.Columns["TongTienBan"].HeaderText = "Tổng Tiền";
                dgHoaDon.Columns["TongTienBan"].Width = 100;
            }
            

        }
        private void DisableElemnts()
        {
            btnThemMoi.Enabled = true;
            btnLuu.Enabled = false;
            btnHuy.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            txtSoHD.Enabled = false;
            cmbNhanVien.Enabled = false;
            cmbKhachHang.Enabled = false;
            dtpNgayBan.Enabled = false;
            txttt.Enabled = false;
        }
        private void EnbleElements()
        {
            btnThemMoi.Enabled = false;
            btnLuu.Enabled = true;
            btnHuy.Enabled = true;
            txtSoHD.Enabled = true;
            cmbNhanVien.Enabled = true;
            cmbKhachHang.Enabled = true;
            dtpNgayBan.Enabled = true;
            txttt.Enabled = true;
        }
        private void Resettext()
        {
            txtSoHD.ResetText();
            cmbKhachHang.SelectedIndex = 0;
            cmbNhanVien.SelectedIndex = 0;
            dtpNgayBan.Text = DateTime.Now.ToShortDateString();
            txttt.ResetText();
        }
        private void LoadDefault()
        {
            DisableElemnts();
            cmbKhachHang.DataSource = bllkh.GetData("");
            cmbKhachHang.DisplayMember = "tenkh";
            cmbKhachHang.ValueMember = "makh";
            cmbNhanVien.DataSource = bllnv.GetData("");
            cmbNhanVien.DisplayMember = "tennv";
            cmbNhanVien.ValueMember = "manv";
            cmbNhanVien.SelectedIndex = 0;
            cmbKhachHang.SelectedIndex = 0;
            dtpNgayBan.Text = DateTime.Now.ToShortDateString();
            

        }

        private void AfterClickCell()
        {

            btnThemMoi.Enabled = false;
            btnLuu.Enabled = false;
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnHuy.Enabled = true;
        }

        private void btnThemMoi_Click(object sender, EventArgs e)
        {
            EnbleElements();
            txtSoHD.Focus();
            flagCheck = true;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (txtSoHD.Text == "")
            {
                MessageBox.Show("Mã hóa đơn Không được để trống", "Thông báo"); return;
            }
            hdb.MaHD = txtSoHD.Text;
            hdb.MaNV = cmbNhanVien.SelectedValue.ToString();
            hdb.MaKH = cmbKhachHang.SelectedValue.ToString();
            hdb.NgayBan = dtpNgayBan.Value;
            hdb.TongTienBan = 0;
            if (flagCheck)
            {
                try
                {
                    bllhdb.AddData(hdb);
                    MessageBox.Show("Thêm thành công", "Thông báo");
                }
                catch (Exception)
                {

                    MessageBox.Show("Lỗi! Không thêm được"); throw;
                }
            }
            else
            {
                try
                {
                    bllhdb.EditData(hdb);
                    MessageBox.Show("Sửa thành công", "Thông báo");
                }
                catch (Exception)
                {

                    MessageBox.Show("Lỗi! Không sửa được");
                }
            }
            pos = -1;
            Resettext();
            LoadHoaDon();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            btnLuu.Enabled = true;
            flagCheck = false;

            EnbleElements();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (pos == -1) return;
            DataRow row = (dgHoaDon.Rows[pos].DataBoundItem as DataRowView).Row;
           
            hdb.MaHD = row[0].ToString();
            try
            {
                cthd.MaHD = hdb.MaHD;
                bllcthd.DeleteAllData(cthd);
                bllhdb.DeleteData(hdb);
                MessageBox.Show("Xóa Thành Công", "Thông báo");
                LoadHoaDon();
                pos = -1;
                Resettext();
                DisableElemnts();
            }
            catch (Exception)
            {

                MessageBox.Show("Lỗi! Không thể xóa", "Thông báo");
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            Resettext();
            DisableElemnts();
            pos = -1;
            btnThemMoi.Enabled = true;
            LoadHoaDon();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dgHoaDon_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            pos = e.RowIndex;
            if (pos == -1) { return; }
            AfterClickCell();
            DataRow row = (dgHoaDon.Rows[pos].DataBoundItem as DataRowView).Row;
            txtSoHD.Text = row[0].ToString();
            cmbNhanVien.SelectedValue = row[1].ToString();
            cmbKhachHang.SelectedValue = row[2].ToString();
            dtpNgayBan.Value = DateTime.Parse(row[3].ToString());
            txttt.Text = row[4].ToString();
        }

        private void dgHoaDon_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int dong = e.RowIndex;
            frmChiTietHoaDon fr = new frmChiTietHoaDon();
            fr.SoHD = dgHoaDon.Rows[dong].Cells[0].Value.ToString();
            this.Close();
            fr.Show();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            if (txtTimKiem.Text == "")
            {
                MessageBox.Show("Hãy nhập tên sản phẩm cần tìm", "Thông báo"); return;
            }
            try
            {
                hdb.MaHD = txtTimKiem.Text.Trim();
                DataTable dt = bllhdb.GetData("where mahd like '%" + hdb.MaHD + "%'");
                if (dt == null)
                {
                    MessageBox.Show("Không tìm thấy hóa đơn", "Thông báo"); return;
                }
                dgHoaDon.DataSource = dt;
            }
            catch (Exception)
            {

                throw;
            }
            btnHuy.Enabled = true;
        }
        
    }
}
