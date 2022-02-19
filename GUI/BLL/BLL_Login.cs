﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DTO;
using DAL;
namespace BLL
{
    public class BLL_Login
    {
        SQL_Login sql = new SQL_Login();
        public DataTable GetData(string Condition)
        {
            return sql.GetData(Condition);
        }
        
        public void AddData(Login nv)
        {
            sql.AddData(nv);
        }
        public void EditData(Login nv)
        {
            sql.EditData(nv);
        }
        public void DeleteData(Login nv)
        {
            sql.DeleteData(nv);
        }
    }
}
