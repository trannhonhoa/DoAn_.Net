using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DTO;
namespace DAL
{
    public class SQL_NhomSanPham
    {
        DataProvider db = new DataProvider();
        public void AddData(NhomSanPham ex)
        {
            
        }
        //  SỬA DỮ LIỆU
        public void EditData(NhomSanPham ex)
        {

        }
        public void EditSL(NhomSanPham ex)
        {

        }
        //  XÓA DỮ LIỆU
        public void DeleteData(NhomSanPham ex)
        {

        }
        //  LẤY DỮ LIỆU
        public DataTable GetData(string Condition)
        {
            return db.GetData(@"select * from NhomSanPham");
        }
        public void DeleteConstraint(NhomSanPham ex)
        {


        }
    }
}
