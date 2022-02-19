using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using DTO;
namespace DAL
{
    public class DataProvider
    {
        public SqlConnection conn = null;
        string path = (AppDomain.CurrentDomain.BaseDirectory + "config.xml");
        string strConn = "";
        public DataProvider()
        {
            loadDataConfig();
        }
        private void loadDataConfig()
        {
            try
            {
               
                DataSet dataSet = new DataSet();
                dataSet.ReadXml(path);
                DataTable dt = new DataTable();
                dt = dataSet.Tables["Config"];
                foreach (DataRow dr in dt.Rows)
                {
                    
                    var server = dr["server"].ToString();
                    var db = dr["db"].ToString();
                    var user = dr["user"].ToString();
                    var pass = dr["pass"].ToString();
                    strConn = @"Data Source="+server+";Initial Catalog="+db+"; User = "+user+"; password="+pass+"; Integrated Security=True";
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        public void OpenConnection()
        {
            if (conn == null)
                conn = new SqlConnection(strConn);
            if (conn.State == ConnectionState.Closed)
                conn.Open();
        }
        public void CloseConnection()
        {
            if (conn != null && conn.State == ConnectionState.Open)
                conn.Close();
        }
        // lay du lieu
        public DataTable GetData(string sql)
        {
            OpenConnection();
            DataTable ds = new DataTable();
            try
            {

                SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
                adapter.Fill(ds);

                CloseConnection();
                if (ds.Rows.Count > 0)
                {
                    return ds;
                }
                return null;
            }
            catch (Exception)
            {

                throw;
            }



        }
        // truy van du lieu
        public void ExcuteReaderData(string sql)
        {

            try
            {
                OpenConnection();
                SqlCommand command = new SqlCommand(sql, conn);
                command.ExecuteNonQuery();
            }
            catch (Exception)
            {

                throw;
            }
            CloseConnection();

        }
        public string GetDataText(string sql)
        {
            string ketqua = "";
            OpenConnection();
            SqlCommand command = new SqlCommand(sql, conn);
            SqlDataReader sqldr = command.ExecuteReader();
            while (sqldr.Read())
            {
                ketqua = sqldr[0].ToString();
            }
            CloseConnection();
            return ketqua;
        }
       

    }
}
