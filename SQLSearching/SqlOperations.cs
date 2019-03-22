using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringSearch.SqlSearching
{

    public static class SqlOperations
    {
        public static List<string> GetDatabaseNames(SqlConnectionSpecs connectionSpecs)
        {
            List<string> retVal = new List<string>();
            string cmdText = "SELECT name " +
                    "FROM master.dbo.sysdatabases " +
                    "WHERE name NOT IN('master', 'model', 'msdb', 'tempdb') AND(status & 512 = 0)";
            SqlCommand sqlCmd = new SqlCommand(cmdText);
            DataTable dt = SqlOperations.RunOneOffQuery(connectionSpecs, sqlCmd);
            foreach (DataRow dr in dt.Rows)
            {
                retVal.Add(dr.Field<string>(0));
            }
            return retVal;
        }
        

        public static DataTable RunOneOffQuery(SqlConnectionSpecs connectionSpecs, SqlCommand sqlCmd)
        {
            DataTable data = null;
            using (SqlConnection conn = connectionSpecs.GenerateConnection())
            {
                conn.Open();
                data = RunQueryWithConnection(conn, sqlCmd);
                conn.Close();
            }
            return data;
        }

        public static DataTable RunQueryWithConnection(SqlConnection connection, SqlCommand sqlCmd)
        {
            DataTable retVal;
            sqlCmd.Connection = connection;
            using (SqlDataReader reader = sqlCmd.ExecuteReader())
            {
                retVal = new DataTable();
                retVal.Load(reader);
            }
            sqlCmd.Dispose();
            return retVal;
        }

    }


    


}
