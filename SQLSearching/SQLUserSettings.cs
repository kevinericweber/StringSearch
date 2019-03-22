using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringSearch.SqlSearching
{

    [Serializable]
    public class SqlUserSettings
    {
        public string latestServerInstanceUsed;
        public List<string> serverInstances;
        public EnabledSections enabledSections;
    }

    [Serializable]
    public class EnabledSections
    {
        public bool JobSteps;
        public bool JobNames;
        public bool UserLogins;
        public bool StoredProcContents;
        public bool StoredProcNames;
        public bool TableColumns;
        public bool TableNames;
        public bool ViewNames;
        public bool ViewDefinitions;
        public bool Indexes;

        public static EnabledSections GetDefault()
        {
            EnabledSections retVal = new EnabledSections();
            retVal.JobSteps = true;
            retVal.JobNames = true;
            retVal.UserLogins = true;
            retVal.StoredProcContents = true;
            retVal.StoredProcNames = true;
            retVal.TableColumns = true;
            retVal.TableNames = true;
            retVal.ViewNames = true;
            retVal.ViewDefinitions = true;
            retVal.Indexes = true;
            return retVal;
        }
    }
}
