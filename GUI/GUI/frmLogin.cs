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
        Writelog wlg = new Writelog();
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
                DataTable dt = blllg.GetData(" where username = '" + lg.username + "' and password like CONVERT(varchar(50), HashBytes('md5', '" + lg.password + "'), 2)");
		        if( dt != null){
                    MessageBox.Show("Đăng nhập thành công", "Thông báo");
                    frmMain fr = new frmMain();
                  
                    this.Hide();
                    fr.Per = dt.Rows[0][2].ToString();
                    fr.NameUser = dt.Rows[0][0].ToString();
                    fr.Manv = dt.Rows[0][3].ToString();
                    fr.Show();
                    string path = (AppDomain.CurrentDomain.BaseDirectory + "LogFile.txt");
                    if (!System.IO.File.Exists(path))
                    {
                        System.IO.File.Create(AppDomain.CurrentDomain.BaseDirectory + "LogFile.txt");
                        
                    }
                    wlg.WriteLog(dt.Rows[0][0].ToString(), "Login");
                  
                }
                else
                {
                    MessageBox.Show("Sai thông tin đăng nhập", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
	        }
	        catch (Exception)
	        {

                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
	        }
        }

        private void lbclear_Click(object sender, EventArgs e)
        {
            txtpass.ResetText();
            txtusername.ResetText();
        }

        private void txtusername_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void txtpass_KeyPress(object sender, KeyPressEventArgs e)
        {
           
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmLogin_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
