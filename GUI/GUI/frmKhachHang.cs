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
namespace QuanLyCuaHangDienThoai
{
    public partial class frmKhachHang : Form
    {
        public frmKhachHang()
        {
            InitializeComponent();
        }

        BLL_KhachHang bllkh = new BLL_KhachHang();
        KhachHang kh = new KhachHang();
        bool flagCheck; int pos = -1;
        private void frmKhachHang_Load(object sender, EventArgs e)
        {
            LoadKhacHang();
            LoadDefault();
        }
        private void DisableElemnts()
        {
            btnThemMoi.Enabled = true;
            btnLuu.Enabled = false;
            btnHuy.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            txtMaKH.Enabled = false;
            txtTenKH.Enabled = false;
            txtDiaChi.Enabled = false;
            txtDienThoai.Enabled = false;
            
        }
        private void EnbleElements()
        {
            btnThemMoi.Enabled = false;
            btnLuu.Enabled = true;
            btnHuy.Enabled = true;
            txtMaKH.Enabled = true;
            txtTenKH.Enabled = true;
            txtDiaChi.Enabled = true;
            txtDienThoai.Enabled = true;
        }
        private void Resettext()
        {
            txtMaKH.ResetText();
            txtTenKH.ResetText();
            txtDiaChi.ResetText();
            txtDienThoai.ResetText();
        }
        private void AfterClickCell()
        {

            btnThemMoi.Enabled = false;
            btnLuu.Enabled = false;
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnHuy.Enabled = true;
        }
        private void LoadDefault()
        {
            DisableElemnts();
            
        }
        private void LoadKhacHang()
        {
            dgKhachHang.DataSource = bllkh.GetData("");
            if (dgKhachHang.Rows.Count > 0)
            {
                dgKhachHang.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgKhachHang.Columns[0].HeaderText = "Mã KH";
                dgKhachHang.Columns[0].Frozen = true;
                dgKhachHang.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgKhachHang.Columns[0].Width = 100;
                dgKhachHang.Columns[1].HeaderText = "Tên KH";
                dgKhachHang.Columns[1].Width = 300;
                dgKhachHang.Columns[2].HeaderText = "Địa Chỉ";
                dgKhachHang.Columns[2].Width = 300;
                dgKhachHang.Columns[3].HeaderText = "Điện Thoại";
                dgKhachHang.Columns[3].Width = 300;
            }
            
        }

        private void dgKhachHang_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            pos = e.RowIndex;
            if (pos == -1) { return; }
            AfterClickCell();
            DataRow row = (dgKhachHang.Rows[pos].DataBoundItem as DataRowView).Row;
            txtMaKH.Text = row[0].ToString();
            txtTenKH.Text = row[1].ToString();
            txtDiaChi.Text = row[2].ToString();
            txtDienThoai.Text = row[3].ToString();
        }

        private void btnThemMoi_Click(object sender, EventArgs e)
        {
            EnbleElements();
            txtMaKH.Focus();
            flagCheck = true;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (txtMaKH.Text == "")
            {
                MessageBox.Show("Hãy nhập mã khách hàng", "Thông báo"); return;
            }
            if (txtTenKH.Text == "")
            {
                MessageBox.Show("Hãy nhập tên khách hàng", "Thông báo"); return;
            } if (txtDiaChi.Text == "")
            {
                MessageBox.Show("Hãy nhập địa chỉ khách hàng", "Thông báo"); return;
            } if (txtDienThoai.Text == "")
            {
                MessageBox.Show("Hãy nhập tên khách hàng", "Thông báo"); return;
            }
            kh.MaKH = txtMaKH.Text;
            kh.TenKH = txtTenKH.Text;
            kh.DiaChiKH = txtDiaChi.Text;
            kh.SDTKH = txtDienThoai.Text;
            if (flagCheck)
            {

                if (bllkh.GetData("where makh = '"+kh.MaKH+"'") != null)
                {
                    MessageBox.Show("Khách Hàng đã tồn tại", "Thông báo"); return;
                }
                else
                {
                    try
                    {
                        bllkh.AddData(kh);
                        MessageBox.Show("Thêm thành công ", "Thông báo");
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
                    bllkh.EditData(kh);
                    MessageBox.Show("Sửa thành công ", "Thông báo");
                }
                catch (Exception)
                {

                    MessageBox.Show("Lỗi! Không sửa được", "Thông báo");
                }
                
            }
            pos = -1;
            LoadKhacHang();
            Resettext();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            btnLuu.Enabled = true;
            flagCheck = false;

            EnbleElements();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            Resettext();
            DisableElemnts();
            pos = -1;
            btnThemMoi.Enabled = true;
            LoadKhacHang();
        }
        private void btxoa_Click(object sender, EventArgs e)
        {
            AfterClickCell();
            DataRow row = (dgKhachHang.Rows[pos].DataBoundItem as DataRowView).Row;
            kh.MaKH = row[0].ToString();
            try
            {

                bllkh.DeleteData(kh);
                MessageBox.Show("Xóa Thành Công", "Thông báo");
                LoadKhacHang();
                pos = -1;
                Resettext();
                DisableElemnts();
            }
            catch (Exception)
            {

                MessageBox.Show("Lỗi! Không thể xóa", "Thông báo");
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
                kh.TenKH = txtTimKiem.Text.Trim();
                DataTable dt = bllkh.GetData("where tenkh like N'%" + kh.TenKH + "%'");
                if (dt == null)
                {
                    MessageBox.Show("Không tìm thấy khách hàng", "Thông báo"); return;
                }
                dgKhachHang.DataSource = dt;
            }
            catch (Exception)
            {

                throw;
            }
            btnHuy.Enabled = true;
        }
    }
}
