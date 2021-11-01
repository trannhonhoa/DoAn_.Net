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
    public class BLL_NhomSanPham
    {
        SQL_NhomSanPham sql = new SQL_NhomSanPham();

        public void AddData(NhomSanPham ex)
        {
            sql.AddData(ex);
        }
        public void EditData(NhomSanPham ex)
        {
            sql.EditData(ex);
        }
        public void DeleteData(NhomSanPham ex)
        {
            sql.DeleteData(ex);
        }
        public DataTable GetData(string condition)
        {
            return sql.GetData(condition);
        }
        public void DeleteConstraint(NhomSanPham ex)
        {
            sql.DeleteConstraint(ex);
        }
    }
}
