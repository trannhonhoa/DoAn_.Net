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
    public class BLL_CTHDB
    {
        SQl_CTHDB sql = new SQl_CTHDB();
        public void AddData(CTHDB ex)
        {
            sql.AddData(ex);
        }
        //  SỬA DỮ LIỆU
        public void EditData(CTHDB ex)
        {
            sql.EditData(ex);
        }
        //  XÓA DỮ LIỆU
        public void DeleteData(CTHDB ex)
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
