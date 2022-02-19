using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DTO;

namespace DAL
{
    public class SQL_Login
    {
        DataProvider db = new DataProvider();
        public DataTable GetData(string condition)
        {
            return db.GetData("select username, CONVERT(varchar(50), password, 2), per, manv  from LOGIN " + condition);
        }
        public void AddData(Login ex)
        {
            db.ExcuteReaderData(@"INSERT INTO Login (username, password, per, manv) VALUES (N'" + ex.username + "', CONVERT(varchar(50), HashBytes('md5','" + ex.password + "'), 2), '" + ex.per + "', N'" + ex.manv + "')");
        }
        //  SỬA DỮ LIỆU
        public void EditData(Login ex)
        {
            db.ExcuteReaderData(@"UPDATE Login SET  username = N'" + ex.username + "' , password =  CONVERT(varchar(50), HashBytes('md5','" + ex.password + "'), 2), per = N'" + ex.per + "' where manv = N'" + ex.manv + "'");
        }
        //  XÓA DỮ LIỆU
        public void DeleteData(Login ex)
        {
            db.ExcuteReaderData(@"DELETE FROM Login Where manv = N'" + ex.manv + "'");
        }
    }
}
