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
        public string userName { get; set; }
        public string per { get; set; }
        BLL_SanPham bllsp = new BLL_SanPham();
        BLL_NhaCungCap bllncc = new BLL_NhaCungCap();
        BLL_NhomSanPham bllnsp = new BLL_NhomSanPham();
        BLL_CTHDB bllcthd = new BLL_CTHDB();
        SanPham sp = new SanPham();
        CTHDB cthd = new CTHDB();
        Writelog wlg = new Writelog();
        List<SanPham> spAction = new List<SanPham>();
        bool undoingDelete = false; bool undoingUpdate = false;
        public frmDienThoai(string userName, string per)
        {
            this.userName = userName;
            this.per = per;
            InitializeComponent();
           
            
        }
        private void frmDienThoai_Load(object sender, EventArgs e)
        {
            LoadSanPham();
            LoadDanhMuc();
            LoadDefault();
            wlg.WriteLog(userName, "SanPham", "Xem", "dgv_SanPham" );
            if (int.Parse(this.per) == 2)
            {
                btnThemMoi.Enabled = false;
            }
        }
       
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
            radTKTenSP.Checked = true;
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
            if (dgSanPham.Rows.Count <= 0) return;
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

            dgSanPham.Columns["giaban"].HeaderText = "Giá Bán";
            dgSanPham.Columns["giaban"].Width = 120;
            dgSanPham.Columns["giaban"].Visible = true;

            dgSanPham.Columns["gianhap"].HeaderText = "Giá Nhập";
            dgSanPham.Columns["gianhap"].Width = 120;
            dgSanPham.Columns["gianhap"].Visible = true;


            dgSanPham.Columns["mancc"].HeaderText = "Mã NCC";
            dgSanPham.Columns["mancc"].Width = 100;
            dgSanPham.Columns["mancc"].Visible = true;
           
            if(spAction.Count > 0)
            {
                btnUndo.Enabled = true;
            }
            else
            {
                btnUndo.Enabled = false;
            }
            

        }
        bool flagCheck;
        int pos = -1;
        private void btnThemMoi_Click(object sender, EventArgs e)
        {
              
            EnbleElements();
            txtMaSanPham.Focus();
            flagCheck = true;
            undoingDelete = false;
                
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
            else if (!int.TryParse(txtGiaNhap.Text, out gianhap))
            {
                MessageBox.Show("Giá nhập phải là số"); return;
            }
            SanPham sp = new SanPham();
            sp.MaSP = txtMaSanPham.Text;
            sp.TenSP = txtTenSanPham.Text;
            sp.DonViTinh = txtDonViTinh.Text;
            sp.SLTon = soluong;
            sp.GiaBan = giaban;
            sp.GiaNhap = gianhap;
            sp.MaNhom = cmbNhom.SelectedValue.ToString();
            sp.MaNCC = cmbNCC.SelectedValue.ToString();
           
            if (flagCheck == true && !undoingDelete)
            {
                if (bllsp.GetData("where masp = '"+sp.MaSP+"'") != null)
                {
                    MessageBox.Show("Sản phẩm đã tồn tại!", "Thông báo");
                    
                    return;
                }
                else
                {
                    try
                    {
                        sp.action = 1;
                        spAction.Add(sp);
                        bllsp.AddData(sp);
                        MessageBox.Show("Thêm thành công", "Thông báo");
                        wlg.WriteLog(userName, "SanPham", "Them", sp.TenSP);
                        btnHuy.PerformClick();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Lỗi! Không thêm được", "Thông báo");
                    }
                }
                
            }
            else if (undoingDelete)
            {
                sp.action = 1;
                //spAction.Add(sp);
                bllsp.AddData(sp);
                btnHuy.PerformClick();
            }
            else if(!flagCheck && !undoingUpdate) 
            {
                try
                {
                    if (pos != -1)
                    {
                        DataRow row = (dgSanPham.Rows[pos].DataBoundItem as DataRowView).Row;
                        SanPham sp1 = new SanPham();
                        sp1.MaSP = row[0].ToString();
                        sp1.TenSP = row[2].ToString();
                        sp1.MaNhom = row[1].ToString();
                        sp1.DonViTinh = row[3].ToString();
                        sp1.SLTon = int.Parse(row[4].ToString());
                        sp1.GiaBan = int.Parse(row[5].ToString());
                        sp1.GiaNhap = int.Parse(row[6].ToString());
                        sp1.MaNCC = row[7].ToString();
                        sp1.action = 3;
                        spAction.Add(sp1);
                    }
                    bllsp.EditData(sp);
                    wlg.WriteLog(userName, "SanPham", "Sua", sp.TenSP);
                    MessageBox.Show("Sửa Thành Công", "Thông báo");
                    btnHuy.PerformClick();
                    
                }
                catch (Exception)
                {

                    MessageBox.Show("Lỗi không sửa được", "Thống báo");
                }
            }
            else if (undoingUpdate)
            {
                bllsp.EditData(sp);
                btnHuy.PerformClick();
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
            if (int.Parse(this.per) == 2)
            {
                btnThemMoi.Enabled = false;
            }
            
        }

        private void dgSanPham_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            pos = e.RowIndex;
            if (pos == -1 || pos == dgSanPham.Rows.Count - 1) { return; }
            AfterClickCell();
            if (int.Parse(this.per) == 2)
            {
                btnXoa.Enabled = false;
                btnSua.Enabled = false;
            }
            DataRow row = (dgSanPham.Rows[pos].DataBoundItem as DataRowView).Row;
            txtMaSanPham.Text = row[0].ToString();
            cmbNhom.SelectedValue = row[1].ToString();
            txtTenSanPham.Text = row[2].ToString();
            txtDonViTinh.Text = row[3].ToString();
            txtSoLuong.Text = row[4].ToString();
            txtGiaBan.Text = row[5].ToString();
            txtGiaNhap.Text = row[6].ToString();
            cmbNCC.SelectedValue = row[7].ToString();
            txtMaSanPham.Enabled = false;
            
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
            undoingUpdate = false;
            txtMaSanPham.Enabled = false;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {

            AfterClickCell();
            SanPham sp = new SanPham();
            if(pos != -1){
                DialogResult ret = MessageBox.Show("Bạn có chắc muốn xóa ?", "Cho suy nghĩ lại lần nữa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (ret == DialogResult.Yes)
                {
                    DataRow row = (dgSanPham.Rows[pos].DataBoundItem as DataRowView).Row;

                    sp.MaSP = row[0].ToString();
                    sp.TenSP = row[2].ToString();
                    sp.MaNhom = row[1].ToString();
                    sp.DonViTinh = row[3].ToString();
                    sp.SLTon = int.Parse(row[4].ToString());
                    sp.GiaBan = int.Parse(row[5].ToString());
                    sp.GiaNhap = int.Parse(row[6].ToString());
                    sp.MaNCC = row[7].ToString();
                    try
                    {
                        sp.action = 2;
                        spAction.Add(sp);
                        bllsp.DeleteData(sp);
                        MessageBox.Show("Xóa Thành Công", "Thông báo");
                        wlg.WriteLog(userName, "SanPham", "Xoa", sp.MaSP);
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

            else if (txtMaSanPham.Text != "")
            {
                sp.MaSP = txtMaSanPham.Text;
                bllsp.DeleteData(sp);
                LoadSanPham();
                Resettext();
                DisableElemnts();
            }
            
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            if (txtTimKiem.Text == "")
            {
                MessageBox.Show("Hãy nhập tên sản phẩm cần tìm", "Thông báo"); return;
            }
            DataTable dt = null;
            try
            {
                if (radTKTenSP.Checked == true)
                {
                    sp.TenSP = txtTimKiem.Text.Trim();
                    dt = bllsp.GetData("where tensp like '%" + sp.TenSP + "%'");
                }
                else if (radTKloaiSP.Checked == true)
                {
                    sp.TenSP = txtTimKiem.Text.Trim();
                    dt = bllsp.GetData("where masp like '%" + sp.TenSP + "%'");
                }
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

        private void btnThemAnh_Click(object sender, EventArgs e)
        {
            
        }

        private void btnXoaAnh_Click(object sender, EventArgs e)
        {
            
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void backgroundWorkerSanPham_DoWork(object sender, DoWorkEventArgs e)
        {
           
           

               
               
          
        }

        private void backgroundWorkerSanPham_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            
        }

        private void backgroundWorkerSanPham_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
           
           
            
        }

        private void btnUndo_Click(object sender, EventArgs e)
        {
            if (spAction.Count > 0)
            {
                pos = -1;
                if (spAction[spAction.Count - 1].action == 1)
                {
                    txtMaSanPham.Text = spAction[spAction.Count - 1].MaSP;
                    spAction.Remove(spAction[spAction.Count - 1]);
                    btnXoa.Enabled = true;
                    btnXoa.PerformClick();
                }
                else if (spAction[spAction.Count - 1].action == 2)
                {
                    undoingDelete = true;
                    undoingUpdate = false;
                    var sp = spAction[spAction.Count - 1];
                    txtMaSanPham.Text = sp.MaSP;
                    txtTenSanPham.Text = sp.TenSP;
                    cmbNhom.SelectedItem = sp.MaNhom;
                    txtDonViTinh.Text = sp.DonViTinh;
                    txtGiaBan.Text = sp.GiaBan.ToString();
                    txtGiaNhap.Text = sp.GiaNhap.ToString();
                    cmbNCC.SelectedItem = sp.MaNCC;
                    txtSoLuong.Text = sp.SLTon.ToString();
                    spAction.Remove(spAction[spAction.Count - 1]);
                    btnLuu.Enabled = true;
                    btnLuu.PerformClick();
                }
                else if (spAction[spAction.Count - 1].action == 3)
                {
                    undoingDelete = false;
                    undoingUpdate = true;
                    var sp = spAction[spAction.Count - 1];
                    txtMaSanPham.Text = sp.MaSP;
                    txtTenSanPham.Text = sp.TenSP;
                    cmbNhom.SelectedItem = sp.MaNhom;
                    txtDonViTinh.Text = sp.DonViTinh;
                    txtGiaBan.Text = sp.GiaBan.ToString();
                    txtGiaNhap.Text = sp.GiaNhap.ToString();
                    cmbNCC.SelectedItem = sp.MaNCC;
                    txtSoLuong.Text = sp.SLTon.ToString();
                    spAction.Remove(spAction[spAction.Count - 1]);
                    btnLuu.Enabled = true;
                    btnLuu.PerformClick();
                }
            }
        }
        

        
    }
}
