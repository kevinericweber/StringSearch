using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringSearch.SqlSearching
{

    public class SqlConnectionMethod
    {
        private string additionalConnectionStringArgs;

        private SqlConnectionMethod(string additionalConnectionStringArgs)
        {
            this.additionalConnectionStringArgs = additionalConnectionStringArgs;
        }

        public static SqlConnectionMethod CreateTrusted()
        {
            return new SqlConnectionMethod("Trusted_Connection=True;");
        }
        public static SqlConnectionMethod CreateWithSqlLogin(string user, string pass)
        {
            return new SqlConnectionMethod("User id=" + user + ";Password=" + pass + ";");
        }
        
        public string GenerateConnectionString(string sqlInstance)
        {
            string retVal = "Data Source=" + sqlInstance + ";" + additionalConnectionStringArgs;
            return retVal;
        }
        
    }
    
}
