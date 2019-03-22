using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringSearch.SqlSearching
{

    public abstract class SqlResult : Result
    {
        public SqlConnectionSpecs infoNeededToReconnect;
        public string objectName;
        //public abstract string displayText { get; }
        public virtual string displayText
        {
            get
            {
                return "_ServerLevel : " + this.ResultTypeName + " - " + this.objectName;
            }
        }
        public abstract string ResultTypeName { get; }
        public abstract void Open();
        public override string ToString()
        {
            return this.displayText;
        }
    }
    public abstract class SqlDBResult : SqlResult
    {
        public string DBName;

        public override string displayText
        {
            get
            {
                return this.DBName.Replace("[","").Replace("]","") + " : " + this.ResultTypeName + " - " + this.objectName;
            }
        }
    }

    public class SqlJobStepResult : SqlResult
    {
        public override string ResultTypeName { get { return "Job Step"; } }

        public override void Open()
        {
            SqlObjectOpeningTechniques.OpenJob(this);
        }
    }
    public class SqlJobNameResult : SqlResult
    {
        public override string ResultTypeName { get { return "Job Name"; } }

        public override void Open()
        {
            SqlObjectOpeningTechniques.OpenJob(this);
        }
    }

    public class SqlUserLoginResult : SqlResult
    {
        public override string ResultTypeName { get { return "User Login"; } }

        public override void Open()
        {
            return;  // not much relevant we could display upon opening.  Maybe user login creation?  For now, don't do anything.
            // TODO: Improve this from a UX perspective.  At least tell the user they can't do it.
        }
    }
    public class SqlStoredProcContentsResult : SqlDBResult
    {
        public override string ResultTypeName { get { return "Stored Proc Contents"; } }

        public override void Open()
        {
            SqlObjectOpeningTechniques.OpenStoredProcOrView(this);
        }
    }
    public class SqlStoredProcNameResult : SqlDBResult
    {
        public override string ResultTypeName { get { return "Stored Proc Name"; } }

        public override void Open()
        {
            SqlObjectOpeningTechniques.OpenStoredProcOrView(this);
        }
    }
    public class SqlTableColumnResult : SqlDBResult
    {
        public override string ResultTypeName { get { return "Table Column"; } }

        public override void Open()
        {
            SqlObjectOpeningTechniques.OpenTable(this);
        }
    }
    public class SqlTableNameResult : SqlDBResult
    {
        public override string ResultTypeName { get { return "Table Name"; } }

        public override void Open()
        {
            SqlObjectOpeningTechniques.OpenTable(this);
        }
    }
    public class SqlViewNameResult : SqlDBResult
    {
        public override string ResultTypeName { get { return "View Name"; } }

        public override void Open()
        {
            SqlObjectOpeningTechniques.OpenStoredProcOrView(this);
        }
    }
    public class SqlViewDefinitionResult : SqlDBResult
    {
        public override string ResultTypeName { get { return "View Definition"; } }

        public override void Open()
        {
            SqlObjectOpeningTechniques.OpenStoredProcOrView(this);
        }
    }
    public class SqlIndexResult : SqlDBResult
    {
        public override string ResultTypeName { get { return "Index"; } }

        public override void Open()
        {
            return;  // not much relevant we could display upon opening.  For now, do nothing
            // TODO: Improve this from a UX perspective.  At least tell the user they can't do it.
        }
    }
}
