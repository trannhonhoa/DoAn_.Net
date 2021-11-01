using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DTO;
namespace DAL
{
    public class SQL_KhachHang
    {
        DataProvider db = new DataProvider();
        // THÊM DỮ LIỆU
        public void AddData(KhachHang ex)
        {
            db.ExcuteReaderData(@"INSERT INTO KHACHHANG (MaKH, TenKH, DiaChiKH, SDTKH) VALUES (N'" + ex.MaKH + "',N'" + ex.TenKH + "', N'" + ex.DiaChiKH + "',N'" + ex.SDTKH + "')");
        }
        //  SỬA DỮ LIỆU
        public void EditData(KhachHang ex)
        {
            db.ExcuteReaderData(@"UPDATE KHACHHANG SET TenKH =N'" + ex.TenKH + "', DiaChiKH =N'" + ex.DiaChiKH + "', SDTKH =N'" + ex.SDTKH + "' Where MaKH=N'" + ex.MaKH + "'");
        }
        //  XÓA DỮ LIỆU
        public void DeleteData(KhachHang ex)
        {
            db.ExcuteReaderData(@"DELETE FROM KHACHHANG Where MaKH=N'" + ex.MaKH + "'");
        }
        //  LẤY DỮ LIỆU
        public DataTable GetData(string Condition)
        {
            return db.GetData("Select * from KHACHHANG " + Condition);
        }
        public DataTable CheckValue(string Condition)
        {
            return db.GetData("Select * from KHACHHANG where MaKH = N'" + Condition + "'");
        }
    }
}
