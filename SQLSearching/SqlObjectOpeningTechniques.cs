using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringSearch.SqlSearching
{
    public static class SqlObjectOpeningTechniques
    {
        public static void OpenJob(SqlResult result)
        {
            string cmdText = "select 'Step=' + convert(varchar,step_id) + " +
                "'    Step Name=' + step_name + " +
                "'    Command=' + command " +
                "FROM	msdb.dbo.sysjobs j with (nolock) " +
                "JOIN	msdb.dbo.sysjobsteps js with (nolock) ON js.job_id = j.job_id " +
                "WHERE	j.name = @objectName";
            SqlCommand sqlCmd = new SqlCommand(cmdText);
            sqlCmd.Parameters.AddWithValue("objectName", result.objectName);
            DisplaySqlResultToUser(result.infoNeededToReconnect, sqlCmd, PrettifyObjectName(result));
        }

        public static void OpenStoredProcOrView(SqlDBResult result)
        {
            // Note: although this dynamic statement directly injects a variable into the SQL statement
            // it's not vulnerable to injection attack since the DBName is coming from SQL's list of DBs, not the user.
            string cmdText = "USE " + result.DBName + "; " +
                "SELECT OBJECT_DEFINITION(OBJECT_ID(@objectName)) as definition";
            SqlCommand sqlCmd = new SqlCommand(cmdText);
            sqlCmd.Parameters.AddWithValue("objectName", result.objectName);
            DisplaySqlResultToUser(result.infoNeededToReconnect, sqlCmd, PrettifyObjectName(result));
        }

        public static void OpenTable(SqlDBResult result)
        {
            string cmdText = "select b.name + ' ' + d.name + ' (' + convert(varchar,b.max_length) + ')' " +
                "from " + result.DBName + ".sys.tables a with (nolock) " +
                "JOIN " + result.DBName + ".sys.columns b with (nolock) ON a.object_id = b.object_id " +
                "JOIN " + result.DBName + ".sys.schemas c with (nolock) on a.schema_id = c.schema_id " +
                "JOIN " + result.DBName + ".sys.types d with (nolock) on b.system_type_id = d.system_type_id " +
                "where c.name + '.' + a.name = @objectName " +
                "order by column_id";
            SqlCommand sqlCmd = new SqlCommand(cmdText);
            sqlCmd.Parameters.AddWithValue("objectName", result.objectName);
            DisplaySqlResultToUser(result.infoNeededToReconnect, sqlCmd, PrettifyObjectName(result));
        }

        private static string PrettifyObjectName(SqlResult result)
        {
            string prettyName = "-------------------- ";
            prettyName += result.ToString();
            prettyName += " --------------------";
            return prettyName;
        }

        private static void DisplaySqlResultToUser(SqlConnectionSpecs connectionSpecs, SqlCommand sqlCmd, string prefaceString)
        {
            StringBuilder results = new StringBuilder();
            if (!string.IsNullOrEmpty(prefaceString)) results.AppendLine(prefaceString);
            DataTable dt = SqlOperations.RunOneOffQuery(connectionSpecs, sqlCmd);
            foreach(DataRow dr in dt.Rows)
            {
                string line = dr.Field<string>(0);
                results.AppendLine(line);
            }
            NotepadOpen.OpenContents(results.ToString());
        }
    }
}
