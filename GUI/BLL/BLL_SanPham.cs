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
    public class BLL_SanPham
    {
        SQL_SanPham sql = new SQL_SanPham();

        public void AddData(SanPham sp)
        {
            
                sql.AddData(sp);
           
            
        }
        public void EditData(SanPham sp)
        {
            sql.EditData(sp);
        }
        public void DeleteData(SanPham sp)
        {
            sql.DeleteData(sp);
        }
        public DataTable GetData(string condition)
        {
            return sql.GetData(condition);
        }
        public DataTable CheckValue(SanPham sp)
        {
            return sql.GetData(sp.MaSP);
        }
        public string GetSL(string Condition)
        {
            return sql.GetSL(Condition);
        }
        public string GetGiaBan(string Condition)
        {
            return sql.GetDonGia(Condition);
        }
        public void UpdateSL(SanPham sp)
        {
             sql.UpdateSL(sp);
        }
    }
}
