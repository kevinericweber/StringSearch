using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringSearch.SqlSearching
{

    public static class SqlCommandStringTemplates
    {
        // I know what you're thinking: *#$!%* - doesn't this guy know anything about SQL Injection Vulnerabilities?
        // It *is* dynamic SQL generation, but if you'll examine it closely, the user's search parameter is fed into the SQL
        // commands as a parameterized input (so it's *not* parsed by the SQL engine) and the only other input is
        // the Database name - which is gathered from the list of DBs on the server (not provided by user input)
        // So while the SQL is being generated on-the-fly (necessary, considering the alternative is to require
        // each SQL server to have a StoredProc stored on the server that this app uses) it's not vulnerable to an
        // SQL injection attack from the user (which, to be honest, doesn't matter much anyways; it requires them
        // to have an authorized connection to the DB to begin with, which they could use to run whatever they wanted.)

        public static string searchTermParamValue = "searchStr";
        public static string databaseNameReplaceStr = "$DBName$";

        public static string JobSteps = "SELECT	'JobStep' as findType, NULL as sourceDatabase, j.name as ObjectName " +
            "FROM	msdb.dbo.sysjobs j with (nolock) " +
            "JOIN	msdb.dbo.sysjobsteps js with (nolock) ON js.job_id = j.job_id " +
            "WHERE	js.command LIKE '%' + @searchStr + '%'";

        public static string JobNames = "SELECT	'JobName' as findType, NULL as sourceDatabase, j.name as ObjectName " +
            "FROM	msdb.dbo.sysjobs j with (nolock) " +
            "WHERE	j.name LIKE '%' + @searchStr + '%'";

        public static string UserLogins = "select 'UserLogin' as findType, NULL as DBName,name as ObjectName " +
            "from master.sys.syslogins " +
            "where name like '%' + @searchStr + '%'";

        public static string StoredProcNames = "SELECT DISTINCT 'StoredProcName' as findType, '[$DBName$]' as DBName, c.name + '.' + a.Name as ObjectName " +
            "FROM [$DBName$].sys.procedures a with (nolock) " +
            "JOIN [$DBName$].sys.schemas c with (nolock) " +
            "ON a.schema_id = c.schema_id " +
            "WHERE a.name LIKE '%' + @searchStr + '%'";

        public static string StoredProcContents = "SELECT 'StoredProcContents' as findType, '[$DBName$]' as DBName, c.name + '.' + a.Name as ObjectName " +
            "FROM [$DBName$].sys.procedures a with (nolock) " +
            "JOIN [$DBName$].sys.sql_modules b with (nolock) " +
            "ON a.object_id = b.object_id " +
            "JOIN [$DBName$].sys.schemas c with (nolock) " +
            "ON a.schema_id = c.schema_id " +
            "WHERE b.definition like '%' + @searchStr + '%'";

        public static string TableColumnNames = "SELECT 'TableColumn' as findType, '[$DBName$]' as DBName, c.name + '.' + a.name as objectName " +
            "from [$DBName$].sys.tables a with (nolock) " +
            "JOIN [$DBName$].sys.columns b with (nolock) ON a.object_id = b.object_id " +
            "JOIN [$DBName$].sys.schemas c with (nolock) on a.schema_id = c.schema_id " +
            "WHERE b.name like '%' + @searchStr + '%'";

        public static string TableNames = "SELECT 'TableName' as findType, '[$DBName$]' as DBName, c.name + '.' + a.name as objectName " +
            "from [$DBName$].sys.tables a with (nolock) " +
            "JOIN [$DBName$].sys.schemas c with (nolock) on a.schema_id = c.schema_id " +
            "WHERE a.name like '%' + @searchStr + '%'";

        public static string ViewNames = "SELECT 'ViewName' as findType, '[$DBName$]' as DBName, c.name + '.' + a.name as objectName " +
            "from [$DBName$].sys.views a with (nolock) " +
            "JOIN [$DBName$].sys.schemas c with (nolock) on a.schema_id = c.schema_id " +
            "WHERE a.name like '%' + @searchStr + '%'";

        public static string ViewDefinitions = "select 'ViewDefinition' as findType, '[$DBName$]' as DBName,o.name as ObjectName " +
            "from [$DBName$].sys.objects o " +
            "join [$DBName$].sys.sql_modules m on m.object_id = o.object_id " +
            "where o.type = 'V' and m.definition like '%' + @searchStr + '%'";

        public static string Indexes = "select 'Index' as findType,  '[$DBName$]' as DBName, t.name + ' : ' + c.name + ' (' + i.name + ')' as ObjectName " +
            "from [$DBName$].sys.tables t " +
            "inner join [$DBName$].sys.schemas s on t.schema_id = s.schema_id " +
            "inner join [$DBName$].sys.indexes i on i.object_id = t.object_id " +
            "inner join [$DBName$].sys.index_columns ic on ic.object_id = t.object_id " +
            "inner join [$DBName$].sys.columns c on c.object_id = t.object_id and ic.column_id = c.column_id " +
            "where i.index_id > 0 and i.type in (1, 2) and i.is_unique_constraint = 0 " +
            "and i.is_disabled = 0 and i.is_hypothetical = 0 and ic.key_ordinal > 0 " +
            "and (c.name like '%' + @searchStr + '%' or i.name like '%' + @searchStr + '%') " +
            "order by t.name, c.name, i.name";
    }

}
