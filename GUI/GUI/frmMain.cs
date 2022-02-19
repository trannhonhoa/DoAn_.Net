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
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Common;
using System.Data.SqlClient;

namespace QuanLyCuaHangDienThoai
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }
        public string Per { get; set; }
        public string NameUser { get; set; }
        Writelog wlg = new Writelog();
        private void frmMain_Load(object sender, EventArgs e)
        {
            if (NameUser != "")
            {
                MessageBox.Show("Xin Chào " + NameUser.TrimEnd() + ", Have a nice day !!!!", "Hello", MessageBoxButtons.OK);
            }
          
        }

        private void sanPhamToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDienThoai dt = new frmDienThoai(this.NameUser, this.Per);
            dt.Show();
         

        }

        private void kháchHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (int.Parse(Per) == 1)
            {
                MessageBox.Show("Bạn không có quyền này!");
                return;
            }
            frmKhachHang kh = new frmKhachHang();
            kh.Show();
           
        }

        private void hóaĐơnBánHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (int.Parse(Per) == 1)
            {
                MessageBox.Show("Bạn không có quyền này!");
                return;
            }
            frmHoaDonBan fr = new frmHoaDonBan();
            fr.Show();
        }

        private void khoHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (int.Parse(Per) == 2)
            {
                MessageBox.Show("Bạn không có quyền này!");
                return;
            }
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
            wlg.WriteLog(NameUser, "Logout");
        }

        private void sảnPhẩmBánChạyToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void nhânViênToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (int.Parse(Per) == 1 || int.Parse(Per) == 2)
            {
                MessageBox.Show("Bạn không có quyền này!");
                return;
            }
            frmNhanVien fr = new frmNhanVien();
            fr.Show();
        }

        private void nhàCungCấpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmNCC ncc = new frmNCC();
            ncc.Show();
        }

        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            string s = DateTime.Now.ToString("dd/MM/yy HH:mm:ss");
            lblDate.Text = s;
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            for (int i = 0; i <= 100; i+=25)
            {
                int percent = i;
                backgroundWorker1.ReportProgress(percent, i);
                System.Threading.Thread.Sleep(100);
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            progressBarPercent.Value = 0;
            backgroundWorker1.CancelAsync();
            
        }

        private void progressToolStripMenuItem_Click(object sender, EventArgs e)
        {
            backgroundWorker1.RunWorkerAsync();  
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBarPercent.Value = e.ProgressPercentage;
        }
            
        SqlConnection conn = new SqlConnection("Data Source=ADMIN\\SQLEXPRESS;Initial Catalog=CuaHangDienThoai; User = sa; password=trannhonhoa; Integrated Security=True");
        private void backupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                try
                {
                   
                    string database = conn.Database.ToString();
                    string cmd = "BACKUP DATABASE [" + database + "] TO DISK ='" + dlg.SelectedPath + "\\" + "database" + "-" + DateTime.Now.ToString("yyyy-MM-dd--HH-mm-ss") + ".bak'";
                    conn.Open();
                    SqlCommand command = new SqlCommand(cmd, conn);
                    command.ExecuteNonQuery();
                    backgroundWorker1.RunWorkerAsync();  
                    MessageBox.Show("Data Bakup done successfuly !");
                  
                }
                catch{
                    MessageBox.Show("Data Backup Fail !");
                }
                conn.Close();
            }   
        }

        private void restoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "SQL SERVER database backup files|*.bak";
            dlg.Title = "Data restore";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                string database = conn.Database.ToString();
                conn.Open();
                try
                {
                  
                    string str1 = string.Format("ALTER DATABASE [" + database + "] SET SINGLE_USER WITH ROLLBACK IMMEDIATE");
                    SqlCommand command = new SqlCommand(str1, conn);
                    command.ExecuteNonQuery();

                    string str2 = "USE MASTER RESTORE DATABASE [" + database + "] FROM DISK ='" + dlg.FileName + "' WITH REPLACE;";
                    SqlCommand cd2 = new SqlCommand(str2, conn);
                    cd2.ExecuteNonQuery();
                    string cmd3 = string.Format("ALTER TABLE [" + database + "] SET MULTI_USER");
                    SqlCommand cd3 = new SqlCommand(cmd3, conn);
                    backgroundWorker1.RunWorkerAsync();  
                    MessageBox.Show("Data restore done successfuly");
                    
                }
                catch{
                    MessageBox.Show("Data restore Fail");
                }
                conn.Close();
            }
        }



       
    }
}
 