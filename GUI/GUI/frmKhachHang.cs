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
using COMExcel = Microsoft.Office.Interop.Excel;
namespace QuanLyCuaHangDienThoai
{
    public partial class frmKhachHang : Form
    {
        public frmKhachHang()
        {
            InitializeComponent();
        }
        List<KhachHang> khAction = new List<KhachHang>();
        BLL_KhachHang bllkh = new BLL_KhachHang();
        KhachHang kh = new KhachHang();
        bool flagCheck; int pos = -1; bool undoingDelete = false; bool undoingUpdate = false;
        bool import = false;
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
                dgKhachHang.Columns[1].Width = 150;
                dgKhachHang.Columns[2].HeaderText = "Địa Chỉ";
                dgKhachHang.Columns[2].Width = 150;
                dgKhachHang.Columns[3].HeaderText = "Điện Thoại";
                dgKhachHang.Columns[3].Width = 150;
            }
            if(khAction.Count > 0)
            {
                btnUndo.Enabled = true;
            }
            else
            {
                btnUndo.Enabled = false;
            }
            
        }

        private void dgKhachHang_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            pos = e.RowIndex;
            if (pos == -1 || pos == dgKhachHang.Rows.Count - 1) { return; }
            AfterClickCell();
            DataRow row = (dgKhachHang.Rows[pos].DataBoundItem as DataRowView).Row;
            txtMaKH.Text = row[0].ToString();
            txtTenKH.Text = row[1].ToString();
            txtDiaChi.Text = row[2].ToString();
            txtDienThoai.Text = row[3].ToString();

            txtMaKH.Enabled = false;
        }

        private void btnThemMoi_Click(object sender, EventArgs e)
        {
            EnbleElements();
            txtMaKH.Focus();
            flagCheck = true;
            undoingDelete = false;
           
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (!import)
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
            }
            KhachHang kh = new KhachHang();
            kh.MaKH = txtMaKH.Text;
            kh.TenKH = txtTenKH.Text;
            kh.DiaChiKH = txtDiaChi.Text;
            kh.SDTKH = txtDienThoai.Text;
            if (flagCheck == true && !undoingDelete)
            {
               
                    if (bllkh.GetData("where makh = '" + kh.MaKH + "'") != null)
                    {
                        MessageBox.Show("Khách Hàng đã tồn tại", "Thông báo"); return;
                    }
                    else
                    {
                        try
                        {
                            kh.action = 1;
                            khAction.Add(kh);
                            bllkh.AddData(kh);
                            MessageBox.Show("Thêm thành công ", "Thông báo");
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
                try
                {
                    kh.action = 1;
                    //khAction.Add(kh);
                    bllkh.AddData(kh);
                    btnHuy.PerformClick();
                }
                catch (Exception)
                {

                    throw;
                }
            }
            else if(flagCheck == false && !undoingUpdate && !import)
            {    
              
                    try
                    {
                        if (pos != -1)
                        {
                            DataRow row = (dgKhachHang.Rows[pos].DataBoundItem as DataRowView).Row;
                            KhachHang kh1 = new KhachHang();
                            kh1.MaKH = row[0].ToString();
                            kh1.TenKH = row[1].ToString();
                            kh1.DiaChiKH = row[2].ToString();
                            kh1.SDTKH = row[3].ToString();
                            kh1.action = 3;
                            khAction.Add(kh1);
                        }   
                        bllkh.EditData(kh);
                        MessageBox.Show("Sửa thành công ", "Thông báo");
                        btnHuy.PerformClick();
                    }
                    catch (Exception)
                    {

                        MessageBox.Show("Lỗi! Không sửa được", "Thông báo");
                    }  
            }
            else if (undoingUpdate)
            {
                    bllkh.EditData(kh);
                    btnHuy.PerformClick();
               
            }
            else if (import)
            {
                for (int i = 0; i < dgKhachHang.Rows.Count -1; i++)
                {
                    DataRow row = (dgKhachHang.Rows[i].DataBoundItem as DataRowView).Row;
                    KhachHang khImport = new KhachHang();
                    khImport.MaKH = row[0].ToString();
                    khImport.TenKH = row[1].ToString();
                    khImport.DiaChiKH = row[2].ToString();
                    khImport.SDTKH = row[3].ToString();
                    if (bllkh.GetData("where makh = '" + khImport.MaKH + "'") != null)
                    {
                        continue;
                    }
                    else
                    {
                        try
                        {
                            khImport.action = 1;
                            khAction.Add(khImport);
                            bllkh.AddData(khImport);        
                          
                        }
                        catch (Exception)
                        {

                            MessageBox.Show("Lỗi! Không thêm được", "Thông báo");
                        }
                    }


                }
                btnHuy.PerformClick();
                MessageBox.Show("Thêm thành công ", "Thông báo");
                btnLuu.Enabled = false;
            }
            pos = -1;
            import = false;
            LoadKhacHang();
            Resettext();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            btnLuu.Enabled = true;
            flagCheck = false;
            EnbleElements();
            undoingUpdate = false;
            txtMaKH.Enabled = false;
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
            KhachHang kh = new KhachHang();
            if(pos != -1){
                DialogResult ret = MessageBox.Show("Bạn có chắc muốn xóa ?", "Cho suy nghĩ lại lần nữa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (ret == DialogResult.Yes)
                {
                    DataRow row = (dgKhachHang.Rows[pos].DataBoundItem as DataRowView).Row;


                    kh.MaKH = row[0].ToString();
                    kh.TenKH = row[1].ToString();
                    kh.DiaChiKH = row[2].ToString();
                    kh.SDTKH = row[3].ToString();
                    try
                    {
                        kh.action = 2;
                        khAction.Add(kh);
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
                

            }
            if (txtMaKH.Text != "")
            {

                kh.MaKH = txtMaKH.Text;
                try
                {

                    bllkh.DeleteData(kh);
                   
                    LoadKhacHang();
                    Resettext();
                    DisableElemnts();
                }
                catch (Exception)
                {

                    MessageBox.Show("Undo fail!", "Thông báo");
                }

            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            if (txtTimKiem.Text == "")
            {
                MessageBox.Show("Hãy nhập thông tin khách hàng cần tìm", "Thông báo"); return;
            }
            DataTable dt = null;
            try
            {
                kh.TenKH = txtTimKiem.Text.Trim();
                if (radMaKH.Checked)
                {
                    dt = bllkh.GetData("where makh like N'%" + kh.TenKH + "%'");
                }

                if (radTenKH.Checked)
                {
                    dt = bllkh.GetData("where tenkh like N'%" + kh.TenKH + "%'");
                }
                
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

        private void btnUndo_Click(object sender, EventArgs e)
        {
           
            if (khAction.Count > 0)
            {

                pos = -1;
                if ( khAction[khAction.Count - 1].action == 1)
                {
                    txtMaKH.Text = khAction[khAction.Count - 1].MaKH;
                    khAction.Remove(khAction[khAction.Count - 1]);
                    btnXoa.Enabled = true;
                    btnXoa.PerformClick();
                }
                else if (khAction[khAction.Count - 1].action == 2)
                {
                    undoingDelete = true;
                    undoingUpdate = false;
                    var kh = khAction[khAction.Count - 1];
                    txtMaKH.Text = kh.MaKH;
                    txtDiaChi.Text = kh.DiaChiKH;
                    txtDienThoai.Text = kh.SDTKH;
                    txtTenKH.Text = kh.TenKH;
                    khAction.Remove(khAction[khAction.Count - 1]);
                    btnLuu.Enabled = true;
                    btnLuu.PerformClick();
                }
                else if (khAction[khAction.Count - 1].action == 3)
                {
                    undoingDelete = false;
                    undoingUpdate = true;
                    var kh = khAction[khAction.Count - 1];
                    txtMaKH.Text = kh.MaKH;
                    txtDiaChi.Text = kh.DiaChiKH;
                    txtDienThoai.Text = kh.SDTKH;
                    txtTenKH.Text = kh.TenKH;
                    khAction.Remove(khAction[khAction.Count - 1]);
                    btnLuu.Enabled = true;
                    btnLuu.PerformClick();
                }
            }
            
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
        private DataTable oTbl;
        private string fileName;
        private void readExcel()
        {
            if (fileName == null)
            {
                MessageBox.Show("Bạn chưa chọn file excel");
                return;
            }
            COMExcel.Application excelApp = new COMExcel.Application();
            try
            {
                excelApp.Workbooks.Open(fileName);
            }
            catch{
                MessageBox.Show("Khong the mo file du lieu");
            }
            oTbl = new DataTable();
            oTbl.Columns.Add("MaNV", typeof(string));
            oTbl.Columns.Add("TenNV", typeof(string));
            oTbl.Columns.Add("DiaChi", typeof(string));
            oTbl.Columns.Add("SoDT", typeof(string));

            foreach (COMExcel.Worksheet WSheet in excelApp.Worksheets)
            {
                DataRow dr = oTbl.NewRow();
                long i = 1;
                try
                {
                    do
                    {
                        if (WSheet.Range["A" + i].Value == null && WSheet.Range["B" + i].Value == null && WSheet.Range["C" + i].Value == null && WSheet.Range["D" + i].Value == null)
                        {
                            break;
                        }
                        dr = oTbl.NewRow();
                        dr["MaNV"] = WSheet.Range["A" + i].Value;
                        dr["TenNV"] = WSheet.Range["B" + i].Value;
                        dr["DiaChi"] = WSheet.Range["C" + i].Value;
                        dr["SoDT"] = WSheet.Range["D" + i].Value;
                        oTbl.Rows.Add(dr);
                        i++;
                    } while (true);
                }
                catch
                {
                    MessageBox.Show("Co loi khi thuc hien");
                }
            }

        }
        private void btnImport_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "excel file |*.xls;*.xlsx";
            ofd.FilterIndex = 1;
            ofd.RestoreDirectory = true;
            ofd.Multiselect = false;
            ofd.Title = "Chọn file excel";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                fileName = ofd.FileName;
                readExcel();
                if (ofd != null)
                {
                    dgKhachHang.DataSource = oTbl;
                }
                else
                {
                    MessageBox.Show("Không có dữ liệu");
                }
            }
            import = true;
            btnLuu.Enabled = true;
        }
    }
}
