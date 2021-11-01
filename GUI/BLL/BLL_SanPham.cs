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
        SQL_SanPham sqlsp = new SQL_SanPham();

        public void AddData(SanPham sp)
        {
            
                sqlsp.AddData(sp);
           
            
        }
        public void EditData(SanPham sp)
        {
            sqlsp.EditData(sp);
        }
        public void DeleteData(SanPham sp)
        {
            sqlsp.DeleteData(sp);
        }
        public DataTable GetData(string condition)
        {
            return sqlsp.GetData(condition);
        }
        public DataTable CheckValue(SanPham sp)
        {
            return sqlsp.GetData(sp.MaSP);
        }
       
    }
}
