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
    public partial class frmCTHDB : Form
    {
        public frmCTHDB()
        {
            InitializeComponent();
        }

        private void btquaylai_Click(object sender, EventArgs e)
        {
            
        }
        BLL_CTHDB bllcthdb = new BLL_CTHDB();
        BLL_SanPham bllmasp = new BLL_SanPham();

        CTHDB cthdn = new CTHDB();
        bool flagCheck;
        int dong = 0;   

        private string sohdn;
        public string SOHDB
        {
            get
            {
                return sohdn;
            }
            set
            {
                sohdn = value;
            }
        }

        private void DisableElemnts()
        {
            btnLuu.Enabled = false;
            btnHuy.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            cmbSoHD.Enabled = false;
            txtSL.Enabled = false;
          
            cmbMaSP.Enabled = false;
            txtDonGia.Enabled = false;
            txtThanhTien.Enabled = false;
        }
        private void EnbleElements()
        {
            btnLuu.Enabled = true;
            btnHuy.Enabled = true;
            txtSL.Enabled = true;
           
            cmbMaSP.Enabled = true;
            txtDonGia.Enabled = true;
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
            cmbSoHD.SelectedIndex = 0;
            txtSL.ResetText();
           
            cmbMaSP.SelectedIndex  = 0;
            txtDonGia.ResetText();
            txtThanhTien.ResetText();
        }
        private void frmCTHDNH_Load(object sender, EventArgs e)
        {
            LoadDefault();
            cmbSoHD.Text = sohdn.ToString();

        }
        private void LoadDefault()
        {
            DisableElemnts();
            LoadChiTiet();
            cmbMaSP.DataSource = bllmasp.GetData("");

            cmbMaSP.DisplayMember = "masp";
            cmbMaSP.ValueMember = "masp";
        }
        
            
        
        public void LoadChiTiet()
        {

            dgCTHDN.DataSource = bllcthdb.GetData("where mahd = N'" + sohdn + "'");
            
            dgCTHDN.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgCTHDN.Columns[0].HeaderText = "Số HD";
            dgCTHDN.Columns[0].Frozen = true;
            dgCTHDN.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgCTHDN.Columns[0].Width = 100;
            dgCTHDN.Columns[1].HeaderText = "Mã SP";
            dgCTHDN.Columns[1].Width = 100;
            dgCTHDN.Columns[2].HeaderText = "Số Lượng";
            dgCTHDN.Columns[2].Width = 80;
            dgCTHDN.Columns[3].HeaderText = "Đơn Giá";
            dgCTHDN.Columns[3].Width = 80;
            dgCTHDN.Columns[4].HeaderText = "Thành Tiền";
            dgCTHDN.Columns[4].Width = 80;
        }
        

       
        
        

        

        private void txtsl_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtgg_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtdg_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void btnThemMoi_Click(object sender, EventArgs e)
        {
            flagCheck = true;
            EnbleElements();
            txtThanhTien.Enabled = false;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (cmbMaSP.Text != "")
            {
                if (flagCheck == true)
                {
                    try
                    {
                        float tt = (float.Parse(txtSL.Text) * float.Parse(txtDonGia.Text));

                        cthdn.MaHD = cmbSoHD.Text;
                        cthdn.MaSP = cmbMaSP.Text;
                        cthdn.SLBan = int.Parse(txtSL.Text);
                        cthdn.DonGiaBan = int.Parse(txtDonGia.Text);
                        cthdn.ThanhTienBan = int.Parse(tt.ToString());

                        bllcthdb.AddData(cthdn);
                        DisableElemnts();
                        MessageBox.Show("Đã Lưu Thành Công", "Chú Ý", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString(), "Chú Ý", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                    try
                    {
                        float tt = (float.Parse(txtSL.Text) * float.Parse(txtDonGia.Text));
                        cthdn.MaHD = cmbSoHD.Text;
                        cthdn.MaSP = cmbMaSP.Text;
                        cthdn.SLBan = int.Parse(txtSL.Text);
                        cthdn.DonGiaBan = int.Parse(txtDonGia.Text);
                        cthdn.ThanhTienBan = int.Parse(tt.ToString());

                        bllcthdb.AddData(cthdn);
                        DisableElemnts();
                        MessageBox.Show("Đã Sửa Thành Công Thành Công", "Chú Ý", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Đã có sản phẩm!", "Chú Ý", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                LoadChiTiet();
                DisableElemnts();
                float t1 = (float.Parse(txtSL.Text) * float.Parse(txtDonGia.Text));
                txtThanhTien.Text = t1.ToString();
            }
            else
            {
                MessageBox.Show("Mã Không được để trống", "Chú Ý", MessageBoxButtons.OK);
                cmbMaSP.Focus();
            }
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        

        
    }
}
