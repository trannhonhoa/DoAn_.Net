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
        BLL_HoaDonBan bllhdb = new BLL_HoaDonBan();
        HoaDonBan hdb = new HoaDonBan();
        CTHDB cthdb = new CTHDB();
        SanPham sp = new SanPham();
        public string SoHD { get; set; }
       
        bool flagCheck;
        int pos = -1;
        private void frmChiTietHoaDon_Load(object sender, EventArgs e)
        {
            LoadDefault();
            LoadCTHDB();
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
            txtThanhTien.Enabled = false;
        }
        private void EnbleElements()
        {
            btnThemMoi.Enabled = false;
            btnLuu.Enabled = true;
            btnHuy.Enabled = true;
            cmbSoHD.Enabled = false;
            cmbMaSP.Enabled = true;
            txtSoLuong.Enabled = true;
            
        }
        
        private void Resettext()
        {
            txtSoLuong.ResetText();
            cmbSoHD.Text = SoHD;
            cmbMaSP.SelectedIndex = 0;
            txtDonGia.ResetText();
            txtThanhTien.ResetText();
        }
        private void LoadDefault()
        {
            DisableElemnts();
            cmbMaSP.DataSource = bllsp.GetData("where SLTon > 0");
            cmbMaSP.DisplayMember = "tensp";
            cmbMaSP.ValueMember = "masp";

            cmbSoHD.Text = SoHD;
            cmbMaSP.SelectedIndex = 0;
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
            if (dgCTHDB.Rows.Count <= 0) return;
            dgCTHDB.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgCTHDB.Columns[0].HeaderText = "Số HDB";
            dgCTHDB.Columns[0].Frozen = true;
            dgCTHDB.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgCTHDB.Columns[0].Width = 150;
            dgCTHDB.Columns[1].HeaderText = "Mã Sản Phẩm";
            dgCTHDB.Columns[1].Width = 150;
            dgCTHDB.Columns[2].HeaderText = "Số Lượng";
            dgCTHDB.Columns[2].Width = 150;
            dgCTHDB.Columns[3].HeaderText = "Giá Bán";
            dgCTHDB.Columns[3].Width = 150;
            dgCTHDB.Columns[4].HeaderText = "Thành Tiền";
            dgCTHDB.Columns[4].Width = 150;
        }

        private void btnThemMoi_Click(object sender, EventArgs e)
        {
            EnbleElements();
            flagCheck = true;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            btnLuu.Enabled = true;
            flagCheck = false;

            EnbleElements();
            
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            DialogResult ret = MessageBox.Show("Bạn có chắc muốn xóa", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (ret == DialogResult.OK)
            {
            if (pos == -1) return;
            DataRow row = (dgCTHDB.Rows[pos].DataBoundItem as DataRowView).Row;

            cthdb.MaSP = row[1].ToString();
            try
            {
                
                bllcthd.DeleteData(cthdb);
                MessageBox.Show("Xóa Thành Công", "Thông báo");
                LoadCTHDB();
                pos = -1;
                Resettext();
                DisableElemnts();
            }
            catch (Exception)
            {

                MessageBox.Show("Lỗi! Không thể xóa", "Thông báo");
            }
            hdb.MaHD = SoHD;
            bllhdb.TongTien(hdb);
            }
        }

        private void dgCTHDB_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            pos = e.RowIndex;
            if (pos == -1 || pos == dgCTHDB.Rows.Count - 1) { return; }
            AfterClickCell();
            DataRow row = (dgCTHDB.Rows[pos].DataBoundItem as DataRowView).Row;
            cmbSoHD.Text = row[0].ToString();
            cmbMaSP.SelectedValue = row[1].ToString();
            txtSoLuong.Text = row[2].ToString();
            txtDonGia.Text = row[3].ToString();
            txtThanhTien.Text = row[4].ToString();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (txtSoLuong.Text == "" || int.Parse(txtSoLuong.Text) <= 0)
            {
                MessageBox.Show("Hãy nhập số lượng", "Thông báo"); return;
            }
            int tongtien = int.Parse(txtSoLuong.Text) * int.Parse(txtDonGia.Text);
            cthdb.MaHD = cmbSoHD.Text;
            cthdb.MaSP = cmbMaSP.SelectedValue.ToString();
            cthdb.SLBan = int.Parse(txtSoLuong.Text);
            cthdb.DonGiaBan = int.Parse(txtDonGia.Text);
            cthdb.ThanhTienBan = tongtien;
            int SLTon = int.Parse(bllsp.GetSL("where masp = '" + cthdb.MaSP + "'"));
            if (cthdb.SLBan > SLTon)
            {
                MessageBox.Show("Không đủ số lượng bán", "Thông báo"); return;
            }
            if (flagCheck)
            {
                try
                {
                    if (bllcthd.CheckValue(cthdb) != null)
                    {
                        MessageBox.Show("Sản phẩm đã tồn tại", "Thông báo"); return;
                    }
                    bllcthd.AddData(cthdb);
                    MessageBox.Show("Thêm thành công", "Thông báo");

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
                    bllcthd.EditData(cthdb);
                    MessageBox.Show("Sửa thành công", "Thông báo");

                }
                catch (Exception)
                {

                    MessageBox.Show("Lỗi! Không sửa được", "Thông báo");
                }
            }
            //sp.SLTon = cthdb.SLBan;
            //sp.MaSP = cthdb.MaSP;
            //bllsp.UpdateSL(sp);

            hdb.MaHD = cthdb.MaHD;
            bllhdb.TongTien(hdb);
            pos = -1;
            Resettext();
            LoadDefault();
            LoadCTHDB();
        }

        private void cmbMaSP_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtDonGia.Text = bllsp.GetGiaBan("where masp = '"+cmbMaSP.SelectedValue.ToString()+"'");
            txtSoLuong.Text = bllsp.GetSL("where masp = '" + cmbMaSP.SelectedValue.ToString() + "'");
        }

        private void btnQuayLai_Click(object sender, EventArgs e)
        {
            
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            Resettext();
            DisableElemnts();
            pos = -1;
            btnThemMoi.Enabled = true;
        }

        private void btnQuayLai_Click_1(object sender, EventArgs e)
        {
            frmHoaDonBan fr = new frmHoaDonBan();
            this.Close();
            fr.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmReportHDB fr = new frmReportHDB();
            fr.MaHD = cmbSoHD.Text;
            fr.Show();
        }

    }
}

