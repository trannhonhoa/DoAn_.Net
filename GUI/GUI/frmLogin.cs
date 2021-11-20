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
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }
        BLL_Login blllg = new BLL_Login();
        Login lg = new Login();
        private void frmLogin_Load(object sender, EventArgs e)
        {
            
        }

        private void btnlogin_Click(object sender, EventArgs e)
        {
            if(txtusername.Text == ""){
                MessageBox.Show("Hãy nhập tên đăng nhập", "Thông báo"); return;
            }
            if(txtpass.Text == ""){
                MessageBox.Show("Hãy nhập mật khẩu", "Thông báo"); return;
            }
            
            try
            {
                lg.username = txtusername.Text;
                lg.password = txtpass.Text;
                DataTable dt = blllg.GetData("where username = '"+lg.username+"' and password = '"+lg.password+"' ");
		        if( dt != null){
                    MessageBox.Show("Đăng nhập thành công", "Thông báo");
                    frmMain fr = new frmMain();
                    this.Hide();
                    fr.Per = dt.Rows[0][2].ToString();
                    fr.Show();
                }
                else
                {
                    MessageBox.Show("Sai thông tin đăng nhập", "Thông báo");
                }
	        }
	        catch (Exception)
	        {
		
		        throw;
	        }
        }

        private void lbclear_Click(object sender, EventArgs e)
        {
            txtpass.ResetText();
            txtusername.ResetText();
        }
    }
}
