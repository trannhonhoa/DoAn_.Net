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
    public partial class frm_Master_HDB : Form
    {
        public frm_Master_HDB()
        {
            InitializeComponent();
        }
        public string MaHD { get; set; }
        private void frm_Master_HDB_Load(object sender, EventArgs e)
        {
            mahdToolStripTextBox.Text = MaHD;
            // TODO: This line of code loads data into the 'dataSet_Master_HDB.DataTable1' table. You can move, or remove it, as needed.
            this.dataTable1TableAdapter.Fill(this.dataSet_Master_HDB.DataTable1, MaHD);

            this.reportViewer1.RefreshReport();
        }

        private void fillToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                
                this.dataTable1TableAdapter.Fill(this.dataSet_Master_HDB.DataTable1, mahdToolStripTextBox.Text);
                this.reportViewer1.RefreshReport();
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

        }

        private void reportViewer1_Load(object sender, EventArgs e)
        {

        }

        private void fillToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}
