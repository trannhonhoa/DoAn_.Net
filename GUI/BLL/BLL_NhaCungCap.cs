﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DTO;
using System.Data;
namespace BLL
{
    public class BLL_NhaCungCap
    {
        SQL_NhaCungCap sql = new SQL_NhaCungCap();
        public DataTable GetData(string Condition)
        {
            return sql.GetData(Condition);
        }
    }
}
