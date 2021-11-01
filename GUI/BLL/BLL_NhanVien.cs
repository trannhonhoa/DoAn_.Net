using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using System.Data;
namespace BLL
{
    public class BLL_NhanVien
    {
        SQL_NhanVien sql = new SQL_NhanVien();
        public DataTable GetData(string Condition)
        {
            return sql.GetData("Select * from NHAN_VIEN " + Condition);
        }
    }
}
