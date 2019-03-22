using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringSearch.SqlSearching
{
    public class SqlCommandWithParser
    {
        public SqlCommand command;
        public SqlDataRowParser parser;
        
        public static SqlCommandWithParser JobStepPair(string searchTerm)
        {
            return GenerateInstancePair(SqlCommandStringTemplates.JobSteps, searchTerm, SqlDataRowParser.JobStep);
        }
        public static SqlCommandWithParser JobNamePair(string searchTerm)
        {
            return GenerateInstancePair(SqlCommandStringTemplates.JobNames, searchTerm, SqlDataRowParser.JobName);
        }
        public static SqlCommandWithParser UserLoginPair(string searchTerm)
        {
            return GenerateInstancePair(SqlCommandStringTemplates.UserLogins, searchTerm, SqlDataRowParser.UserLogin);
        }
        public static SqlCommandWithParser StoredProcContentsPair(string dbName, string searchTerm)
        {
            return GenerateDBPair(SqlCommandStringTemplates.StoredProcContents, dbName, searchTerm, SqlDataRowParser.StoredProcContents);
        }
        public static SqlCommandWithParser StoredProcNamePair(string dbName, string searchTerm)
        {
            return GenerateDBPair(SqlCommandStringTemplates.StoredProcNames, dbName, searchTerm, SqlDataRowParser.StoredProcName);
        }
        public static SqlCommandWithParser TableColumnPair(string dbName, string searchTerm)
        {
            return GenerateDBPair(SqlCommandStringTemplates.TableColumnNames, dbName, searchTerm, SqlDataRowParser.TableColumn);
        }
        public static SqlCommandWithParser TableNamePair(string dbName, string searchTerm)
        {
            return GenerateDBPair(SqlCommandStringTemplates.TableNames, dbName, searchTerm, SqlDataRowParser.TableName);
        }
        public static SqlCommandWithParser ViewNamePair(string dbName, string searchTerm)
        {
            return GenerateDBPair(SqlCommandStringTemplates.ViewNames, dbName, searchTerm, SqlDataRowParser.ViewName);
        }
        public static SqlCommandWithParser ViewDefinitionPair(string dbName, string searchTerm)
        {
            return GenerateDBPair(SqlCommandStringTemplates.ViewDefinitions, dbName, searchTerm, SqlDataRowParser.ViewDefintion);
        }
        public static SqlCommandWithParser IndexPair(string dbName, string searchTerm)
        {
            return GenerateDBPair(SqlCommandStringTemplates.Indexes, dbName, searchTerm, SqlDataRowParser.Indexes);
        }

        private static SqlCommandWithParser GenerateInstancePair(string baseCmdText, string searchTerm, SqlDataRowParser parser)
        {
            SqlCommand sqlCmd = new SqlCommand(baseCmdText);
            sqlCmd.Parameters.AddWithValue(SqlCommandStringTemplates.searchTermParamValue, searchTerm);
            return new SqlCommandWithParser() { command = sqlCmd, parser = parser };
        }
        private static SqlCommandWithParser GenerateDBPair(string baseCmdText, string dbName, string searchTerm, SqlDataRowParser parser)
        {
            string cmdText = baseCmdText.Replace(SqlCommandStringTemplates.databaseNameReplaceStr, dbName);
            SqlCommand sqlCmd = new SqlCommand(cmdText);
            sqlCmd.Parameters.AddWithValue(SqlCommandStringTemplates.searchTermParamValue, searchTerm);
            return new SqlCommandWithParser() { command = sqlCmd, parser = parser };
        }
    }
}
