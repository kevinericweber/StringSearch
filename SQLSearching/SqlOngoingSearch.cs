using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringSearch.SqlSearching
{

    public class SqlOngoingSearch : ResultGenerator
    {
        public string serverInstance;
        public string searchTerm;
        public SqlConnectionSpecs connectionSpecs;

        public EnabledSections enabledSections;
        public string databaseNameRegex;
        public bool copyToClipboardInsteadOfRunning;

        public ConcurrentQueue<SqlCommandWithParser> sqlCommands = new ConcurrentQueue<SqlCommandWithParser>();
        public bool databaseCommandsGenerated = false;
        public bool instanceCommandsGenerated = false;

        public ConcurrentBag<Result> results = new ConcurrentBag<Result>();
        public ConcurrentBag<string> warnings = new ConcurrentBag<string>(); // TODO: Actually get this info presented to the user somehow.  Right now it just goes into a hole.
        public override string ToString()
        {
            return "Remaining SQL Commands for execution: " + this.sqlCommands.Count();
        }

        public List<Result> GenerateResults()
        {
            List<Result> retVal = this.results.ToList();
            return retVal;
        }
    }



}
