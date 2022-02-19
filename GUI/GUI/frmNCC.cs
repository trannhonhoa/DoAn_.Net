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
    public partial class frmNCC : Form
    {
        public frmNCC()
        {
            InitializeComponent();
        }
        BLL_NhaCungCap bllncc = new BLL_NhaCungCap();
        NhaCungCap ncc = new NhaCungCap();
        private void frmNCC_Load(object sender, EventArgs e)
        {
            LoadNCC();
            LoadDefault();
        }
        private void DisableElemnts()
        {
            btnThemMoi.Enabled = true;
            btnLuu.Enabled = false;
            btnHuy.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            txtMancc.Enabled = false;
            txtTenncc.Enabled = false;
            txtDiaChi.Enabled = false;
            txtPhone.Enabled = false;
        }
        private void EnbleElements()
        {
            btnThemMoi.Enabled = false;
            btnLuu.Enabled = true;
            btnHuy.Enabled = true;
            txtMancc.Enabled = true;
            txtTenncc.Enabled = true;
            txtDiaChi.Enabled = true;
            txtPhone.Enabled = true;
        }
        private void Resettext()
        {
            txtMancc.ResetText();
            txtTenncc.ResetText();
            txtDiaChi.ResetText();
            txtPhone.ResetText();
        }
        private void LoadDefault()
        {
            DisableElemnts();
        }
        private void AfterClickCell()
        {

            btnThemMoi.Enabled = false;
            btnLuu.Enabled = false;
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnHuy.Enabled = true;
        }
        private void LoadNCC()
        {
            dgNCC.DataSource = bllncc.GetData("");
            if (dgNCC.Rows.Count <= 0) return;
            dgNCC.Columns["mancc"].HeaderText = "Mã NCC";
            dgNCC.Columns["mancc"].Width = 100;
            dgNCC.Columns["mancc"].Visible = true;

            dgNCC.Columns["tenncc"].HeaderText = "Tên NCC";
            dgNCC.Columns["tenncc"].Width = 125;
            dgNCC.Columns["tenncc"].Visible = true;

            dgNCC.Columns["diachincc"].HeaderText = "Địa chỉ";
            dgNCC.Columns["diachincc"].Width = 200;
            dgNCC.Columns["diachincc"].Visible = true;

            dgNCC.Columns["sdtncc"].HeaderText = "SDT";
            dgNCC.Columns["sdtncc"].Width = 120;
            dgNCC.Columns["sdtncc"].Visible = true;
        }
        bool flagCheck;
        int pos = -1;

        private void btnThemMoi_Click(object sender, EventArgs e)
        {
            EnbleElements();
            txtMancc.Focus();
            flagCheck = true;
        }

        private void dgNCC_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            pos = e.RowIndex;
            if (pos == -1 || pos == dgNCC.Rows.Count - 1) { return; }
            AfterClickCell();
            DataRow row = (dgNCC.Rows[pos].DataBoundItem as DataRowView).Row;
            txtMancc.Text = row[0].ToString();
            txtTenncc.Text = row[1].ToString();
            txtDiaChi.Text = row[2].ToString();
            txtPhone.Text = row[3].ToString();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            btnLuu.Enabled = true;
            flagCheck = false;
            EnbleElements();
            txtMancc.Enabled = false;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (pos == -1 || pos == dgNCC.Rows.Count - 1) { return; }
            DialogResult ret = MessageBox.Show("Bạn có chắc muốn xóa ?", "Cho suy nghĩ lại lần nữa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (ret == DialogResult.Yes)
            {
                AfterClickCell();
                DataRow row = (dgNCC.Rows[pos].DataBoundItem as DataRowView).Row;
                ncc.MaNCC = row[0].ToString();
                try
                {
                    //cthd.MaSP = sp.MaSP;
                    //bllcthd.DeleteAllData(cthd);
                    bllncc.DeleteData(ncc);
                    MessageBox.Show("Xóa Thành Công", "Thông báo");
                    LoadNCC();
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
            try
            {
                ncc.TenNCC = txtTimKiem.Text.Trim();
                DataTable dt = bllncc.GetData("where tenncc like '%" + ncc.TenNCC + "%'");
                if (dt == null)
                {
                    MessageBox.Show("Không tìm thấy nhân viên", "Thông báo"); return;
                }
                dgNCC.DataSource = dt;
            }
            catch (Exception)
            {

                throw;
            }
            btnHuy.Enabled = true;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (txtMancc.Text == "")
            {
                MessageBox.Show("Mã nhân viên Không được để trống", "Thông báo"); return;
            }
            if (txtTenncc.Text == "")
            {
                MessageBox.Show("Tên nhân viên Không được để trống", "Thông báo"); return;
            }
            if (txtPhone.Text == "")
            {
                MessageBox.Show("Số điện thoại không được để trống", "Thông báo"); return;
            }
            if (txtDiaChi.Text == "")
            {
                MessageBox.Show("Địa chỉ không được để trống", "Thông báo"); return;
            }
            ncc.MaNCC = txtMancc.Text;
            ncc.TenNCC = txtTenncc.Text;
            ncc.SDTNCC = txtPhone.Text;
            ncc.DiaChiNCC = txtDiaChi.Text;
            if (flagCheck)
            {
                if (bllncc.GetData(" where mancc = '" + ncc.MaNCC + "'") != null)
                {
                    MessageBox.Show("Nhân viên đã tồn tại", "Thông báo"); return;
                }
                else
                {
                    try
                    {

                        bllncc.AddData(ncc);
                        MessageBox.Show("Thêm thành công", "Thông báo");
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
                    bllncc.EditData(ncc);
                    MessageBox.Show("Sửa Thành Công", "Thông báo");
                    btnHuy.PerformClick();

                }
                catch (Exception)
                {

                    MessageBox.Show("Lỗi không sửa được", "Thống báo");
                }
            }
            pos = -1;
            Resettext();
            LoadNCC();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            Resettext();
            DisableElemnts();
            pos = -1;
            btnThemMoi.Enabled = true;
            LoadNCC();
        }
    }
}
