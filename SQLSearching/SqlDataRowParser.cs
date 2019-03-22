using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringSearch.SqlSearching
{
    
    public class SqlDataRowParser
    {
        // note: you might wonder why the ConnectionSpecs are at all a part of this class
        // (because, in theory, all it needs is a DataRow to generate a result listing.)
        // the problem is, the Result needs to have the information fed to it about
        // where the connection was pointed to that generated the result
        // so that if the user wants to open up the particular result, it can re-connect
        // to SQL and get the information.

        // So really ConnectionSpecs is simply being funneled through this class.
        // When it comes time to generate a result from a DataRow, the specs are passed
        // in to the ParseRow() function, which invokes the Func<> Lambda Expression,
        // which then ferries the spec into the newly created SqlResult object.
        // Kinda confusing, but there has to be something embedded in the SqlResult object,
        // and something has to bridge the gap between the actual searches and getting
        // that connection info into the SqlResult object.

        private Func<DataRow, SqlConnectionSpecs, Result> parsingFunc;
        public Result ParseRow(DataRow row, SqlConnectionSpecs specs)
        {
            return parsingFunc.Invoke(row, specs);
        }
        private SqlDataRowParser(Func<DataRow, SqlConnectionSpecs, Result> parsingFunc)
        {
            this.parsingFunc = parsingFunc;
        }
        public static SqlDataRowParser JobStep = new SqlDataRowParser((dr, specs) =>
        {
            return new SqlJobStepResult()
            {
                infoNeededToReconnect = specs,
                objectName = dr.Field<string>(2)
            };
        } );
        public static SqlDataRowParser JobName = new SqlDataRowParser((dr, specs) =>
        {
            return new SqlJobNameResult()
            {
                infoNeededToReconnect = specs,
                objectName = dr.Field<string>(2)
            };
        } );
        public static SqlDataRowParser UserLogin = new SqlDataRowParser((dr, specs) =>
        {
            return new SqlUserLoginResult()
            {
                infoNeededToReconnect = specs,
                objectName = dr.Field<string>(2)
            };
        } );

        public static SqlDataRowParser StoredProcContents = new SqlDataRowParser((dr, specs) =>
        {
            return new SqlStoredProcContentsResult()
            {
                infoNeededToReconnect = specs,
                DBName = dr.Field<string>(1),
                objectName = dr.Field<string>(2)
            };
        });
        public static SqlDataRowParser StoredProcName = new SqlDataRowParser((dr, specs) =>
        {
            return new SqlStoredProcNameResult()
            {
                infoNeededToReconnect = specs,
                DBName = dr.Field<string>(1),
                objectName = dr.Field<string>(2)
            };
        });
        public static SqlDataRowParser TableColumn = new SqlDataRowParser((dr, specs) =>
        {
            return new SqlTableColumnResult()
            {
                infoNeededToReconnect = specs,
                DBName = dr.Field<string>(1),
                objectName = dr.Field<string>(2)
            };
        });
        public static SqlDataRowParser TableName = new SqlDataRowParser((dr, specs) =>
        {
            return new SqlTableNameResult()
            {
                infoNeededToReconnect = specs,
                DBName = dr.Field<string>(1),
                objectName = dr.Field<string>(2)
            };
        });
        public static SqlDataRowParser ViewName = new SqlDataRowParser((dr, specs) =>
        {
            return new SqlViewNameResult()
            {
                infoNeededToReconnect = specs,
                DBName = dr.Field<string>(1),
                objectName = dr.Field<string>(2)
            };
        });
        public static SqlDataRowParser ViewDefintion = new SqlDataRowParser((dr, specs) =>
        {
            return new SqlViewDefinitionResult()
            {
                infoNeededToReconnect = specs,
                DBName = dr.Field<string>(1),
                objectName = dr.Field<string>(2)
            };
        });
        public static SqlDataRowParser Indexes = new SqlDataRowParser((dr, specs) =>
        {
            return new SqlIndexResult()
            {
                infoNeededToReconnect = specs,
                DBName = dr.Field<string>(1),
                objectName = dr.Field<string>(2)
            };
        });
        
    }


}
