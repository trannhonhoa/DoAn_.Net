using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyCuaHangDienThoai
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {

        }

        private void sanPhamToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDienThoai dt = new frmDienThoai();
            dt.Show();

        }

        private void kháchHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmKhachHang kh = new frmKhachHang();
            kh.Show();
        }

        private void hóaĐơnBánHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmHoaDonBan fr = new frmHoaDonBan();
            fr.Show();
        }

        private void khoHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmThongKeKhoHang fr = new frmThongKeKhoHang();
            fr.Show();
        }

        private void sảnPhẩmĐãBánToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSanPhamDaBan fr = new frmSanPhamDaBan();
            fr.Show();
        }

        private void đăngNhậpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLogin fr = new frmLogin();
            this.Hide();
            fr.Show();
        }
    }
}
