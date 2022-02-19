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
    public partial class frmNhanVien : Form
    {

        public frmNhanVien()
        {
            InitializeComponent();
        }
        BLL_NhanVien bllnv = new BLL_NhanVien();
        BLL_Login blllg = new BLL_Login();
        NhanVien nv = new NhanVien();
        Login lg = new Login();
        private void frmNhanVien_Load(object sender, EventArgs e)
        {
            LoadNhanVien();
            LoadDefault();
        }
        private void DisableElemnts()
        {
            btnThemMoi.Enabled = true;
            btnLuu.Enabled = false;
            btnHuy.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            txtMaNV.Enabled = false;
            txtTenNV.Enabled = false;
            txtDiaChi.Enabled = false;
            txtPhone.Enabled = false;
            cmbGioiTinh.Enabled = false;
            dtpNgaySinh.Enabled = false;

            txtusername.Enabled = false;
            txtpassword.Enabled = false;
            cmbLoaiNV.Enabled = false;
        }
        private void EnbleElements()
        {
            btnThemMoi.Enabled = false;
            btnLuu.Enabled = true;
            btnHuy.Enabled = true;
            txtMaNV.Enabled = true;
            txtTenNV.Enabled = true;
            txtDiaChi.Enabled = true;
            txtPhone.Enabled = true;
            dtpNgaySinh.Enabled = true;
            cmbGioiTinh.Enabled = true;
            txtusername.Enabled = true;
            txtpassword.Enabled = true;
            cmbLoaiNV.Enabled = true;
        }
        private void Resettext()
        {
            txtMaNV.ResetText();
            txtTenNV.ResetText();
            txtDiaChi.ResetText();
            txtPhone.ResetText();
            cmbGioiTinh.SelectedIndex = 0;
            txtusername.ResetText();
            txtpassword.ResetText();
            cmbLoaiNV.SelectedIndex = 0;
           
        }
        private void LoadDefault()
        {
            DisableElemnts();
            cmbGioiTinh.SelectedIndex = 0;
            cmbLoaiNV.SelectedIndex = 0;
        }
        private void AfterClickCell()
        {

            btnThemMoi.Enabled = false;
            btnLuu.Enabled = false;
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnHuy.Enabled = true;
        }
        private void LoadNhanVien()
        {
            dgNhanVien.DataSource = bllnv.GetData(" where NhanVien.manv = login.manv");
            if (dgNhanVien.Rows.Count <= 0) return;
            dgNhanVien.Columns["manv"].HeaderText = "Mã NV";
            dgNhanVien.Columns["manv"].Width = 100;
            dgNhanVien.Columns["manv"].Visible = true;
            dgNhanVien.Columns["manv"].Frozen = true;

            dgNhanVien.Columns["tennv"].HeaderText = "Họ Tên";
            dgNhanVien.Columns["tennv"].Width = 125;
            dgNhanVien.Columns["tennv"].Visible = true;

            dgNhanVien.Columns["diachinv"].HeaderText = "Địa chỉ";
            dgNhanVien.Columns["diachinv"].Width = 200;
            dgNhanVien.Columns["diachinv"].Visible = true;

            dgNhanVien.Columns["sdt"].HeaderText = "SDT";
            dgNhanVien.Columns["sdt"].Width = 120;
            dgNhanVien.Columns["sdt"].Visible = true;

            dgNhanVien.Columns["gioitinh"].HeaderText = "Giới Tính";
            dgNhanVien.Columns["gioitinh"].Width = 100;
            dgNhanVien.Columns["gioitinh"].Visible = true;

            dgNhanVien.Columns["ngaysinh"].HeaderText = "Ngày Sinh";
            dgNhanVien.Columns["ngaysinh"].Width = 120;
            dgNhanVien.Columns["ngaysinh"].Visible = true;

            dgNhanVien.Columns["username"].HeaderText = "Username";
            dgNhanVien.Columns["username"].Width = 120;
            dgNhanVien.Columns["username"].Visible = true;

            dgNhanVien.Columns["password"].HeaderText = "Password";
            dgNhanVien.Columns["password"].Width = 120;
            dgNhanVien.Columns["password"].Visible = true;

            dgNhanVien.Columns["per"].HeaderText = "Quyền";
            dgNhanVien.Columns["per"].Width = 120;
            dgNhanVien.Columns["per"].Visible = true;

            dgNhanVien.Columns["manv1"].Visible = false;

        }
        bool flagCheck;
        int pos = -1;

        private void btnThemMoi_Click(object sender, EventArgs e)
        {
            EnbleElements();
            txtMaNV.Focus();
            flagCheck = true;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (txtMaNV.Text == "")
            {
                MessageBox.Show("Mã nhân viên Không được để trống", "Thông báo"); return;
            }
            if (txtTenNV.Text == "")
            {
                MessageBox.Show("Tên nhân viên Không được để trống", "Thông báo"); return;
            }
            if (txtPhone.Text == "")
            {
                MessageBox.Show("Số điện thoại không được để trống", "Thông báo"); return;
            }
            if (txtusername.Text == "")
            {
                MessageBox.Show("Username Không được để trống", "Thông báo"); return;
            }
            if (txtPhone.Text == "")
            {
                MessageBox.Show("Password không được để trống", "Thông báo"); return;
            }
            nv.MaNV = txtMaNV.Text;
            nv.TenNV = txtTenNV.Text;
            nv.SDTNV = txtPhone.Text;
            nv.GioiTinh = cmbGioiTinh.SelectedItem.ToString();
            nv.NgaySinh = dtpNgaySinh.Value.ToShortDateString();
            nv.DiaChiNV = txtDiaChi.Text;
            lg.username = txtusername.Text;
            lg.password = txtpassword.Text;
            lg.per = cmbLoaiNV.SelectedIndex;
            lg.manv = txtMaNV.Text;
            
            if (flagCheck)
            {
                if (bllnv.GetData("where NhanVien.manv = '" + nv.MaNV + "'") != null)
                {
                    MessageBox.Show("Nhân viên đã tồn tại", "Thông báo"); return;
                }
                else
                {
                    try
                    {
                       
                        bllnv.AddData(nv);  
                        MessageBox.Show("Thêm thành công", "Thông báo");
                        blllg.AddData(lg);
                        btnHuy.PerformClick();
                    }
                    catch (Exception)
                    {
                       
                        MessageBox.Show("Lỗi! Không thêm được", "Thông báo");
                    }
                }

            }
            else
            {
                try
                {
                  
                    bllnv.EditData(nv);
                    MessageBox.Show("Sửa Thành Công", "Thông báo");
                    blllg.EditData(lg);
                    btnHuy.PerformClick();

                }
                catch (Exception)
                {

                    MessageBox.Show("Lỗi không sửa được", "Thống báo");
                }
            }
            pos = -1;
            Resettext();
            LoadNhanVien();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            Resettext();
            DisableElemnts();
            pos = -1;
            btnThemMoi.Enabled = true;
            LoadNhanVien();
        }

        private void dgNhanVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            pos = e.RowIndex;
            if (pos == -1 || pos == dgNhanVien.Rows.Count - 1) { return; }
            AfterClickCell();
            DataRow row = (dgNhanVien.Rows[pos].DataBoundItem as DataRowView).Row;
            txtMaNV.Text = row[0].ToString();
            txtTenNV.Text = row[1].ToString();
            txtDiaChi.Text = row[2].ToString();
            txtPhone.Text = row[3].ToString();
            cmbGioiTinh.SelectedItem = row[4].ToString();
            dtpNgaySinh.Value = DateTime.Parse(row[5].ToString());

            txtusername.Text = row[6].ToString();
            txtpassword.Text = row[7].ToString();
            cmbLoaiNV.SelectedIndex = int.Parse(row[8].ToString());
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            btnLuu.Enabled = true;
            flagCheck = false;
            EnbleElements();
            txtMaNV.Enabled = false;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (pos == -1) { return; }
            DialogResult ret = MessageBox.Show("Bạn có chắc muốn xóa ?", "Cho suy nghĩ lại lần nữa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (ret == DialogResult.Yes)
            {
                AfterClickCell();
                DataRow row = (dgNhanVien.Rows[pos].DataBoundItem as DataRowView).Row;
                NhanVien sp = new NhanVien();
                sp.MaNV = row[0].ToString();
                lg.manv = sp.MaNV;
                try
                {
                    bllnv.DeleteData(nv);
                    blllg.DeleteData(lg);
                    MessageBox.Show("Xóa Thành Công", "Thông báo");
                    LoadNhanVien();
                    pos = -1;
                    Resettext();
                    DisableElemnts();
                }
                catch (Exception)
                {

                    MessageBox.Show("Lỗi! Không thể xóa", "Thông báo");
                }
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            if (txtTimKiem.Text == "")
            {
                MessageBox.Show("Hãy nhập tên nhân viên cần tìm", "Thông báo"); return;
            }
            DataTable dt = null;
            try
            {
                nv.TenNV = txtTimKiem.Text.Trim();
                if (radTenNV.Checked)
                {
                    dt = bllnv.GetData("where tennv like '%" + nv.TenNV + "%' and nhanvien.manv = login.manv");
                }
                else
                {
                    dt = bllnv.GetData("where manv like '%" + nv.MaNV + "%' and nhanvien.manv = login.manv");
                }
              
                if (dt == null)
                {
                    MessageBox.Show("Không tìm thấy nhân viên", "Thông báo"); return;
                }
                dgNhanVien.DataSource = dt;
            }
            catch (Exception)
            {

                throw;
            }
            btnHuy.Enabled = true;
        }
    }
}
