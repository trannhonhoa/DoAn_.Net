using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DTO;
namespace DAL
{
    public class SQL_NhaCungCap
    {
        DataProvider db = new DataProvider();
        public DataTable GetData(string Condition)
        {
            return db.GetData("Select * from NHACUNGCAP " + Condition);
        }
        public void AddData(NhaCungCap ex)
        {
            db.ExcuteReaderData(@"INSERT INTO NHACUNGCAP (MaNcc, TenNcc, sdtncc, diachincc) VALUES (N'" + ex.MaNCC + "',N'" + ex.TenNCC + "',N'" + ex.SDTNCC + "',N'" + ex.DiaChiNCC +"')");
        }
        //  SỬA DỮ LIỆU
        public void EditData(NhaCungCap ex)
        {
            db.ExcuteReaderData(@"UPDATE NHACUNGCAP  SET  Tenncc = N'" + ex.TenNCC + "', DiaChincc = N'" + ex.DiaChiNCC + "', sdtncc = N'" + ex.SDTNCC + "' Where MaNcc = N'" + ex.MaNCC + "'");
        }
        //  XÓA DỮ LIỆU
        public void DeleteData(NhaCungCap ex)
        {
            db.ExcuteReaderData(@"DELETE FROM NHACUNGCAP Where Mancc = N'" + ex.MaNCC + "'");
        }
    }
}
