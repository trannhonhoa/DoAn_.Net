using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DTO;
using System.Data;
namespace BLL
{
    public class BLL_NhanVien
    {
        SQL_NhanVien sql = new SQL_NhanVien();
        public DataTable GetData(string Condition)
        {
            return sql.GetData(Condition);
        }
        public void AddData(NhanVien nv)
        {
            sql.AddData(nv);
        }
        public void EditData(NhanVien nv)
        {
            sql.EditData(nv);
        }
        public void DeleteData(NhanVien nv)
        {
            sql.DeleteData(nv);
        }
       
    }
}
