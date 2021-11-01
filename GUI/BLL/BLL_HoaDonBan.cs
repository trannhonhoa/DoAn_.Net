using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using DAL;
using System.Data;
namespace BLL
{
    public class BLL_HoaDonBan
    {
        SQL_HoaDonBan sql = new SQL_HoaDonBan();
        public void AddData(HoaDonBan ex)
        {
            sql.AddData(ex);
        }
        //  SỬA DỮ LIỆU
        public void EditData(HoaDonBan ex)
        {
           sql.EditData(ex);
        }
        //  XÓA DỮ LIỆU
        public void DeleteData(HoaDonBan ex)
        {
            sql.DeleteData(ex);
        }
        //  LẤY DỮ LIỆU
        public DataTable GetData(string Condition)
        {
            return sql.GetData(Condition);
        }
    }
}
