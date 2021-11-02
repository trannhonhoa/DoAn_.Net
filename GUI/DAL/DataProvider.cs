using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
namespace DAL
{
    public class DataProvider
    {
        private SqlConnection conn = null;
        string strConn = "Server=DESKTOP-P9L00KA; Database=CuaHangDienThoai; User Id=sa; pwd=trannhonhoa";
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
        public void ExcuteReaderData(String sql, SqlParameter[] parameters)
        {
            try
            {
                OpenConnection();
                SqlCommand command = new SqlCommand(sql, conn);
                for (int i = 0; i < parameters.Length; i++)
                {
                    command.Parameters.Add(parameters[i]);
                }
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
