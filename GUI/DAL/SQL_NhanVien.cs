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
            return db.GetData("Select * from NHANVIEN, Login " + Condition);
        }
        public void AddData(NhanVien ex)
        {
            db.ExcuteReaderData(@"INSERT INTO NHANVIEN (MaNV, TenNV, DiaChiNV, SDT, GioiTinh, NgaySinh) VALUES (N'" + ex.MaNV + "',N'" + ex.TenNV + "',N'" + ex.DiaChiNV + "',N'" + ex.SDTNV + "',N'" + ex.GioiTinh + "',N'" + ex.NgaySinh + "')");
        }
        //  SỬA DỮ LIỆU
        public void EditData(NhanVien ex)
        {
            db.ExcuteReaderData(@"UPDATE NhanVien  SET  TenNV = N'" + ex.TenNV+ "', DiaChiNV = N'" + ex.DiaChiNV + "', SDT =N'" + ex.SDTNV + "', GioiTinh =N'" + ex.GioiTinh + "', NgaySinh = N'" + ex.NgaySinh + "' Where MaNV =N'" + ex.MaNV + "'");
        }
        //  XÓA DỮ LIỆU
        public void DeleteData(NhanVien ex)
        {
            db.ExcuteReaderData(@"DELETE FROM NhanVien Where MaNV =N'" + ex.MaNV + "'");
        }
    }
}
