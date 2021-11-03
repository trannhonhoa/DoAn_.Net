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
            return db.GetData("select * from LOGIN " + condition);
        }
    }
}
