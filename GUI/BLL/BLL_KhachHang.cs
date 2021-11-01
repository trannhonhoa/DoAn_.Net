using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DTO;
using DAL;
namespace BLL
{
    public class BLL_KhachHang
    {
       SQL_KhachHang sql = new  SQL_KhachHang();
        public void AddData(KhachHang ex)
        {
            sql.AddData(ex);
        }
        //  SỬA DỮ LIỆU
        public void EditData(KhachHang ex)
        {
            sql.EditData(ex);
        }
        //  XÓA DỮ LIỆU
        public void DeleteData(KhachHang ex)
        {
            sql.DeleteData(ex);
        }
        //  LẤY DỮ LIỆU
        public DataTable GetData(string Condition)
        {
            return sql.GetData(Condition);
        }
        public DataTable CheckValue(KhachHang ex)
        {
            return sql.GetData(ex.MaKH);
        }
    }
}
