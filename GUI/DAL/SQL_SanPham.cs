using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using DAL;
using System.Data;
namespace DAL
{
    public class SQL_SanPham
    {
         DataProvider db = new DataProvider();
        // THÊM DỮ LIỆU
        public void AddData(SanPham ex)
        {
            db.ExcuteReaderData(@"INSERT INTO SANPHAM (MaSP, MaNhom, TenSP, DonViTinh, SLTon, GiaBan, GiaNhap, MaNCC) VALUES (N'" + ex.MaSP + "',N'" + ex.MaNhom + "',N'" + ex.TenSP + "',N'" + ex.DonViTinh + "',N'" + ex.SLTon + "',N'" + ex.GiaBan + "',N'" + ex.GiaNhap + "',N'" + ex.MaNCC + "')");
        }
        //  SỬA DỮ LIỆU
        public void EditData(SanPham ex)
        {
            db.ExcuteReaderData(@"UPDATE SANPHAM  SET  MaNhom =N'" + ex.MaNhom + "', TenSP =N'" + ex.TenSP + "', DonViTinh =N'" + ex.DonViTinh + "', SLTon =N'" + ex.SLTon + "', GiaBan =N'" + ex.GiaBan + "', GiaNhap =N'" + ex.GiaNhap + "', MaNCC=N'" + ex.MaNCC + "' Where MaSP=N'" + ex.MaSP + "'");
        }
        public void EditSL(SanPham ex)
        {
            db.ExcuteReaderData(@"UPDATE SAN_PHAM SET SLTon = N'" + ex.SLTon + "' Where MaSP = '" + ex.MaSP + "'");
        }
        //  XÓA DỮ LIỆU
        public void DeleteData(SanPham ex)
        {
            db.ExcuteReaderData(@"DELETE FROM SANPHAM Where MaSP =N'" + ex.MaSP + "'");
        }
        //  LẤY DỮ LIỆU
        public DataTable GetData(string Condition)
        {
            return db.GetData("Select * from SANPHAM " + Condition);
        }
        public DataTable CheckValue(string Condition)
        {
            return db.GetData("Select * from SANPHAM where MaSP = '" + Condition + "'");
        }
        public string GetSL(string Condition)
        {
            return db.GetDataText("Select SLTon from SANPHAM " + Condition);
        }
        public string GetDonGia (string Condition)
        {
            return db.GetDataText("Select GiaNhap from SANPHAM " + Condition);
        }
        public void UpdateSL(SanPham ex){
            db.ExcuteReaderData("UPDATE SANPHAM SET SLTon = SLTon - '" + ex.SLTon + "' where masp='" +ex.MaSP  + "'");
        }
      
    }
}
