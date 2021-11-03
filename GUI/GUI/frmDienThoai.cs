using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DTO;
using BLL;
using System.IO;
namespace QuanLyCuaHangDienThoai
{
    public partial class frmDienThoai : Form
    {
        public frmDienThoai()
        {
            InitializeComponent();
        }
        
        private void frmDienThoai_Load(object sender, EventArgs e)
        {
            LoadSanPham();
            LoadDanhMuc();
            LoadDefault();
        }
        BLL_SanPham bllsp = new BLL_SanPham();
        BLL_NhaCungCap bllncc = new BLL_NhaCungCap();
        BLL_NhomSanPham bllnsp = new BLL_NhomSanPham();
        BLL_CTHDB bllcthd = new BLL_CTHDB();
        SanPham sp = new SanPham();
        CTHDB cthd = new CTHDB();
        private void DisableElemnts(){
            btnThemMoi.Enabled = true;
            btnLuu.Enabled = false;
            btnHuy.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            txtDonViTinh.Enabled = false;
            txtGiaBan.Enabled = false;
            txtGiaNhap.Enabled = false;
            txtMaSanPham.Enabled = false;
            txtSoLuong.Enabled = false;
            txtTenSanPham.Enabled = false;
            cmbNCC.Enabled = false;
            cmbNhom.Enabled = false;
        }
        private void EnbleElements()
        {
            btnThemMoi.Enabled = false;
            btnLuu.Enabled = true;
            btnHuy.Enabled = true;
            txtDonViTinh.Enabled = true;
            txtGiaBan.Enabled = true;
            txtGiaNhap.Enabled = true;
            txtMaSanPham.Enabled = true;
            txtSoLuong.Enabled = true;
            txtTenSanPham.Enabled = true;
            cmbNCC.Enabled = true;
            cmbNhom.Enabled = true;
        }
        private void Resettext()
        {
            txtMaSanPham.ResetText();
            txtTenSanPham.ResetText();
            txtDonViTinh.ResetText();
            txtSoLuong.ResetText();
            txtGiaBan.ResetText();
            txtGiaNhap.ResetText();
            cmbNCC.SelectedIndex = 0;
            cmbNhom.SelectedIndex = 0;
        }
        private void LoadDefault(){
            DisableElemnts();
            cmbNhom.SelectedIndex = 0;
            cmbNCC.SelectedIndex = 0;
        }
        private void LoadDanhMuc(){
            cmbNCC.DataSource = bllncc.GetData("");
            cmbNCC.DisplayMember = "Tenncc";
            cmbNCC.ValueMember = "mancc";

            cmbNhom.DataSource = bllnsp.GetData("");
            cmbNhom.DisplayMember = "tennhom";
            cmbNhom.ValueMember = "manhom";
        }
        private void LoadSanPham()
        {
            dgSanPham.DataSource = bllsp.GetData("");
            
            dgSanPham.Columns["masp"].HeaderText = "Mã Sản Phẩm";
            dgSanPham.Columns["masp"].Width = 100;
            dgSanPham.Columns["masp"].Visible = true;

            dgSanPham.Columns["manhom"].HeaderText = "Mã Nhóm";
            dgSanPham.Columns["manhom"].Width = 125;
            dgSanPham.Columns["manhom"].Visible = true;

            dgSanPham.Columns["tensp"].HeaderText = "Tên Sản Phẩm";
            dgSanPham.Columns["tensp"].Width = 200;
            dgSanPham.Columns["tensp"].Visible = true;

            dgSanPham.Columns["donvitinh"].HeaderText = "Đơn Vị Tính";
            dgSanPham.Columns["donvitinh"].Width = 120;
            dgSanPham.Columns["donvitinh"].Visible = true;

            dgSanPham.Columns["slton"].HeaderText = "Số Lượng";
            dgSanPham.Columns["slton"].Width = 100;
            dgSanPham.Columns["slton"].Visible = true;

            dgSanPham.Columns["gianhap"].HeaderText = "Giá Nhập";
            dgSanPham.Columns["gianhap"].Width = 120;
            dgSanPham.Columns["gianhap"].Visible = true;

            dgSanPham.Columns["giaban"].HeaderText = "Giá Bán";
            dgSanPham.Columns["giaban"].Width = 120;
            dgSanPham.Columns["giaban"].Visible = true;

            dgSanPham.Columns["mancc"].HeaderText = "Mã NCC";
            dgSanPham.Columns["mancc"].Width = 100;
            dgSanPham.Columns["mancc"].Visible = true;

            

        }
        bool flagCheck;
        int pos = -1;
        private void btnThemMoi_Click(object sender, EventArgs e)
        {
            EnbleElements();
            txtMaSanPham.Focus();
            flagCheck = true;
        }
       
        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (txtMaSanPham.Text == "")
            {
                MessageBox.Show("Mã Sản Phẩm Không được để trống", "Thông báo"); return;
            }
            if (txtTenSanPham.Text == "")
            {
                MessageBox.Show("Tên Sản Phẩm Không được để trống", "Thông báo"); return;
            }
            if (txtDonViTinh.Text == "")
            {
                MessageBox.Show("Đơn vị tính Không được để trống", "Thông báo"); return;
            }
            int soluong, giaban, gianhap;
            if (txtSoLuong.Text == "")
            {

                MessageBox.Show("Số lượng không được để trống", "Thông báo"); return;
            }
            else if (!int.TryParse(txtSoLuong.Text, out soluong))
            {
                MessageBox.Show("Số lượng phải là số"); return;
            }
            if (txtGiaBan.Text == "")
            {

                MessageBox.Show("Giá nhập không được để trống", "Thông báo"); return;
            }
            else if (!int.TryParse(txtGiaBan.Text, out giaban))
            {
                MessageBox.Show("Giá bán phải là số"); return;
            }

            if (txtGiaNhap.Text == "")
            {

                MessageBox.Show("Giá nhập không được để trống", "Thông báo"); return;
            }
            else if (!int.TryParse(txtSoLuong.Text, out gianhap))
            {
                MessageBox.Show("Giá nhập phải là số"); return;
            }
            sp.MaSP = txtMaSanPham.Text;
            sp.TenSP = txtTenSanPham.Text;
            sp.DonViTinh = txtDonViTinh.Text;
            sp.SLTon = soluong;
            sp.GiaBan = giaban;
            sp.GiaNhap = gianhap;
            sp.MaNhom = cmbNhom.SelectedValue.ToString();
            sp.MaNCC = cmbNCC.SelectedValue.ToString();

            if (flagCheck)
            {
                if (bllsp.GetData("where masp = '"+sp.MaSP+"'") != null)
                {
                    MessageBox.Show("Sản phẩm đã tồn tại", "Thông báo"); return;
                }
                else
                {
                    try
                    {

                        bllsp.AddData(sp);
                        MessageBox.Show("Thêm thành công", "Thông báo");
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
                    bllsp.EditData(sp);
                    MessageBox.Show("Sửa Thành Công", "Thông báo");
                    
                    
                }
                catch (Exception)
                {

                    MessageBox.Show("Lỗi không sửa được", "Thống báo");
                }
            }
            pos = -1;
            Resettext();
            LoadSanPham();
           
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            Resettext();
            DisableElemnts();
            pos = -1;
            btnThemMoi.Enabled = true;
            LoadSanPham();
            
        }

        private void dgSanPham_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            pos = e.RowIndex;
            if (pos == -1) { return; }
            AfterClickCell();
            DataRow row = (dgSanPham.Rows[pos].DataBoundItem as DataRowView).Row;
            txtMaSanPham.Text = row[0].ToString();
            cmbNhom.SelectedValue = row[1].ToString();
            txtTenSanPham.Text = row[2].ToString();
            txtDonViTinh.Text = row[3].ToString();
            txtSoLuong.Text = row[4].ToString();
            txtGiaNhap.Text = row[5].ToString();
            txtGiaBan.Text = row[6].ToString();
            cmbNCC.SelectedValue = row[7].ToString();
            
        }
       
        private void dgSanPham_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            

        }
        private void AfterClickCell()
        {

            btnThemMoi.Enabled = false;
            btnLuu.Enabled = false;
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnHuy.Enabled = true;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            btnLuu.Enabled = true;
            flagCheck = false;
            
            EnbleElements();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (pos == -1) { return; } 
            DialogResult ret = MessageBox.Show("Bạn có chắc muốn xóa ?","Cho suy nghĩ lại lần nữa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(ret == DialogResult.Yes){
                AfterClickCell();
                DataRow row = (dgSanPham.Rows[pos].DataBoundItem as DataRowView).Row;
                SanPham sp = new SanPham();
                sp.MaSP = row[0].ToString();
                try
                {
                    cthd.MaSP = sp.MaSP;
                    bllcthd.DeleteAllData(cthd);
                    bllsp.DeleteData(sp);
                    MessageBox.Show("Xóa Thành Công", "Thông báo");
                    LoadSanPham();
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
                MessageBox.Show("Hãy nhập tên sản phẩm cần tìm", "Thông báo"); return;
            }
            try
            {
                sp.TenSP = txtTimKiem.Text.Trim();
                DataTable dt = bllsp.GetData("where tensp like '" + sp.TenSP + "%'");
                if(dt == null){
                    MessageBox.Show("Không tìm thấy sản phẩm", "Thông báo"); return;
                }
                dgSanPham.DataSource = dt;
            }
            catch (Exception)
            {
                
                throw;
            }
            btnHuy.Enabled = true;
           
        }

        private void gb1_Enter(object sender, EventArgs e)
        {

        }
        

        
    }
}
