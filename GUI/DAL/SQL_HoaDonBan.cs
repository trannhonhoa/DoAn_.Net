using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DTO;
namespace DAL
{
    public class SQL_HoaDonBan
    {
        DataProvider db = new DataProvider();
        // THÊM DỮ LIỆU
        public void AddData(HoaDonBan ex)
        {
            db.ExcuteReaderData(@"INSERT INTO HOADONBAN (MaHD, MaNV, MaKH, NgayBan, TongTienBan) VALUES (N'" + ex.MaHD + "',N'" + ex.MaNV + "',N'" + ex.MaKH + "',N'" + ex.NgayBan + "',N'" + ex.TongTienBan + "')");
        }
        //  SỬA DỮ LIỆU
        public void EditData(HoaDonBan ex)
        {
            db.ExcuteReaderData(@"UPDATE HOADONBAN SET  MaNV =N'" + ex.MaNV + "', MaKH =N'" + ex.MaKH + "', NgayBan =N'" + ex.NgayBan + "' Where MaHD=N'" + ex.MaHD + "'");
        }
        //  XÓA DỮ LIỆU
        public void DeleteData(HoaDonBan ex)
        {
            db.ExcuteReaderData(@"DELETE FROM HOADONBAN Where MaHD=N'" + ex.MaHD + "'");
        }
        //  LẤY DỮ LIỆU
        public DataTable GetData(string Condition)
        {
            return db.GetData("Select * from HOADONBAN" + Condition);
        }
        public void TongTien(HoaDonBan ex)
        {
            db.ExcuteReaderData(@"update HOADONBAN set TongTienBan = (select sum(ThanhTienBan) from 
                                CHITIETHDBAN where MaHD = '"+ex.MaHD+"') where MaHD = '"+ex.MaHD+"'");
        }
        
    }
}
