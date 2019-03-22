using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringSearch.SqlSearching
{
    public class SqlConnectionSpecs
    {
        private string serverInstance;
        private SqlConnectionMethod connectionMethod;

        public SqlConnectionSpecs(string serverInstance, SqlConnectionMethod connectionMethod)
        {
            this.serverInstance = serverInstance;
            this.connectionMethod = connectionMethod;
        }

        public SqlConnection GenerateConnection()
        {
            string connectionString = connectionMethod.GenerateConnectionString(this.serverInstance);
            return new SqlConnection(connectionString);
        }
    }
}
