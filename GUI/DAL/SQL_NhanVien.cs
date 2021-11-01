using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DTO;
namespace DAL
{
    public class SQL_NhanVien
    {
        DataProvider db = new DataProvider();
        // THÊM DỮ LIỆU
        public DataTable GetData(string Condition)
        {
            return db.GetData("Select * from NHANVIEN " + Condition);
        }
    }
}
