using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StringSearch.SqlSearching
{
    public partial class subformSQLSearch : Form, SearchableSubform
    {
        private Action<SearchableSubform> globalSearchFunction;

        private const int ThreadCountSqlCommandExecution = 1; // TODO: Maybe make this user settable?  Or in the settings config?

        // TODO: Put in error handling for all GUI events.

        public subformSQLSearch()
        {
            InitializeComponent();
        }
        private void subformSQLSearch_Load(object sender, EventArgs e)
        {
            LoadSettings();
        }

        public string SearchName { get { return "SQL Search"; } }

        public ResultGenerator GenerateInitialSearchState()
        {
            string serverInstance = comboLocation.Text;
            if (string.IsNullOrEmpty(serverInstance))
                throw new NonfatalUserException("Please enter a server instance.", comboLocation);
            string searchTerm = txtSearchValue.Text;
            if (string.IsNullOrEmpty(searchTerm))
                throw new NonfatalUserException("Please enter a search term.", txtSearchValue);
            
            SqlOngoingSearch initialState = new SqlOngoingSearch();
            initialState.searchTerm = txtSearchValue.Text;
            initialState.serverInstance = comboLocation.Text;
            initialState.connectionSpecs = new SqlConnectionSpecs(initialState.serverInstance, SqlConnectionMethod.CreateTrusted());
            initialState.enabledSections = GetEnabledSectionsFromGui();
            initialState.databaseNameRegex = ".*"; // TODO: Use the GUI text box.  Also, property isn't actually used yet.
            initialState.copyToClipboardInsteadOfRunning = checkCopyToClipboard.Checked;
            ResultGenerator lockable = initialState;

            SaveNewSearchSettings(initialState);

            return lockable;
        }

        private void SaveNewSearchSettings(SqlOngoingSearch sqlSearchInitialState)
        {
            Properties.Settings.Default.Reload();
            if (Properties.Settings.Default.SQLSettings == null)
                Properties.Settings.Default.SQLSettings = new SqlUserSettings();
            var settings = Properties.Settings.Default.SQLSettings;

            settings.latestServerInstanceUsed = sqlSearchInitialState.serverInstance;

            if (settings.serverInstances == null)
                settings.serverInstances = new List<string>();
            if (!settings.serverInstances.Contains(sqlSearchInitialState.serverInstance, StringComparer.OrdinalIgnoreCase))
            {
                settings.serverInstances.Add(sqlSearchInitialState.serverInstance);
            }
            
            settings.enabledSections = sqlSearchInitialState.enabledSections;

            Properties.Settings.Default.Save();
            Properties.Settings.Default.Reload();
        }

        private void LoadSettings()
        {
            Properties.Settings.Default.Reload();
            if (Properties.Settings.Default.SQLSettings == null)
            {
                SetGuiFromEnabledSections(EnabledSections.GetDefault());
                return;
            }

            if (Properties.Settings.Default.SQLSettings.serverInstances != null)
                Properties.Settings.Default.SQLSettings.serverInstances.ForEach(s => comboLocation.Items.Add(s));

            if (Properties.Settings.Default.SQLSettings.enabledSections != null)
                SetGuiFromEnabledSections(Properties.Settings.Default.SQLSettings.enabledSections);
            else
                SetGuiFromEnabledSections(EnabledSections.GetDefault());

            comboLocation.SelectedItem = Properties.Settings.Default.SQLSettings.latestServerInstanceUsed;
        }


        public void RegisterSearchCallback(Action<SearchableSubform> globalSearchFunction)
        {
            this.globalSearchFunction = globalSearchFunction;
        }

        private EnabledSections GetEnabledSectionsFromGui()
        {
            EnabledSections retVal = new EnabledSections();
            retVal.JobSteps = checkJobSteps.Checked;
            retVal.JobNames = checkJobNames.Checked;
            retVal.UserLogins = checkUserLogins.Checked;
            retVal.StoredProcContents = checkStoredProcContents.Checked;
            retVal.StoredProcNames = checkStoredProcName.Checked;
            retVal.TableColumns = checkTableColumns.Checked;
            retVal.TableNames = checkTableNames.Checked;
            retVal.ViewNames = checkViewNames.Checked;
            retVal.ViewDefinitions = checkViewDefinitions.Checked;
            retVal.Indexes = checkIndexes.Checked;
            return retVal;
        }
        private void SetGuiFromEnabledSections(EnabledSections enabledSections)
        {
            checkJobSteps.Checked = enabledSections.JobSteps;
            checkJobNames.Checked = enabledSections.JobNames;
            checkUserLogins.Checked = enabledSections.UserLogins;
            checkStoredProcContents.Checked = enabledSections.StoredProcContents;
            checkStoredProcName.Checked = enabledSections.StoredProcNames;
            checkTableColumns.Checked = enabledSections.TableColumns;
            checkTableNames.Checked = enabledSections.TableNames;
            checkViewNames.Checked = enabledSections.ViewNames;
            checkViewDefinitions.Checked = enabledSections.ViewDefinitions;
            checkIndexes.Checked = enabledSections.Indexes;
        }
        
        public List<Action<ResultGenerator>> GetSearchActions()
        {
            List<Action<ResultGenerator>> retVal = new List<Action<ResultGenerator>>();
            retVal.Add(x => GenerateInstanceLevelCommands(x));
            retVal.Add(x => GenerateDBLevelCommands(x));
            for (int tCount = 1; tCount <= ThreadCountSqlCommandExecution; tCount++)
                retVal.Add(x => HandleCommands(x));
            retVal.Add(x => DisplayCommands(x));
            return retVal;
        }

        public void GenerateInstanceLevelCommands(ResultGenerator lockable)
        {
            
            SqlOngoingSearch item = (SqlOngoingSearch)lockable; // Note: if this statement fails, we don't have a way of returning the error info anyway.  (Plus it means programmer error, not bad data/state/etc)
            try
            {
                if (item.enabledSections.JobSteps)
                    item.sqlCommands.Enqueue(SqlCommandWithParser.JobStepPair(item.searchTerm));
                if (item.enabledSections.JobNames)
                    item.sqlCommands.Enqueue(SqlCommandWithParser.JobNamePair(item.searchTerm));
                if (item.enabledSections.UserLogins)
                    item.sqlCommands.Enqueue(SqlCommandWithParser.UserLoginPair(item.searchTerm));
            }
            catch (Exception ex)
            {
                item.warnings.Add("Couldn't generate instance level SQL commands: " + ex.ToString());
            }

            item.instanceCommandsGenerated = true;
        }
        public void GenerateDBLevelCommands(ResultGenerator lockable)
        {
            SqlOngoingSearch item = (SqlOngoingSearch)lockable;  // Note: if this statement fails, we don't have a way of returning the error info anyway.  (Plus it means programmer error, not bad data/state/etc)

            try
            {
                List<string> databaseNames = SqlOperations.GetDatabaseNames(item.connectionSpecs);
                foreach (string databaseName in databaseNames)
                {
                    if (item.enabledSections.StoredProcContents)
                        item.sqlCommands.Enqueue(SqlCommandWithParser.StoredProcContentsPair(dbName: databaseName, searchTerm: item.searchTerm));
                    if (item.enabledSections.StoredProcNames)
                        item.sqlCommands.Enqueue(SqlCommandWithParser.StoredProcNamePair(dbName: databaseName, searchTerm: item.searchTerm));
                    if (item.enabledSections.TableColumns)
                        item.sqlCommands.Enqueue(SqlCommandWithParser.TableColumnPair(dbName: databaseName, searchTerm: item.searchTerm));
                    if (item.enabledSections.TableNames)
                        item.sqlCommands.Enqueue(SqlCommandWithParser.TableNamePair(dbName: databaseName, searchTerm: item.searchTerm));
                    if (item.enabledSections.ViewNames)
                        item.sqlCommands.Enqueue(SqlCommandWithParser.ViewNamePair(dbName: databaseName, searchTerm: item.searchTerm));
                    if (item.enabledSections.ViewDefinitions)
                        item.sqlCommands.Enqueue(SqlCommandWithParser.ViewDefinitionPair(dbName: databaseName, searchTerm: item.searchTerm));
                    if (item.enabledSections.Indexes)
                        item.sqlCommands.Enqueue(SqlCommandWithParser.IndexPair(dbName: databaseName, searchTerm: item.searchTerm));
                }
            }
            catch (Exception ex)
            {
                item.warnings.Add("Couldn't generate DB level SQL commands: " + ex.ToString());
            }
            item.databaseCommandsGenerated = true;
        }
        public void HandleCommands(ResultGenerator lockable)
        {
            SqlOngoingSearch item = (SqlOngoingSearch)lockable;
            if (item.copyToClipboardInsteadOfRunning) return; 
            using (SqlConnection openConnection = item.connectionSpecs.GenerateConnection())
            {
                openConnection.Open();
                while (true)
                {
                    if (!item.sqlCommands.Any())
                    {
                        if (item.instanceCommandsGenerated && item.databaseCommandsGenerated)
                            if (!item.sqlCommands.Any())
                                return;
                        System.Threading.Thread.Sleep(1);
                    }
                    SqlCommandWithParser commandParserPair;
                    bool dequeWorked = item.sqlCommands.TryDequeue(out commandParserPair);
                    if (!dequeWorked) continue;
                    AttemptHandleCommand(item, commandParserPair, openConnection);
                }
            }
        }

        private void AttemptHandleCommand(SqlOngoingSearch item, SqlCommandWithParser commandParserPair, SqlConnection openConnection)
        {
            try
            {
                DataTable dt = SqlOperations.RunQueryWithConnection(openConnection, commandParserPair.command);
                foreach (DataRow dr in dt.Rows)
                {
                    Result result = commandParserPair.parser.ParseRow(dr, item.connectionSpecs);
                    if (result != null) item.results.Add(result);
                }
            }
            catch (Exception ex)
            {
                item.warnings.Add("Couldn't process command: " + commandParserPair.command.CommandText + " because: " + ex.ToString());
            }
        }

        public void DisplayCommands(ResultGenerator lockable)
        {
            SqlOngoingSearch item = (SqlOngoingSearch)lockable;
            try
            {
                DisplayCommands(item);
            }
            catch (Exception ex)
            {
                item.warnings.Add("Couldn't display commands: " + ex.ToString());
            }
        }

        public void DisplayCommands(SqlOngoingSearch item)
        {
            if (!item.copyToClipboardInsteadOfRunning) return;
            using (SqlConnection openConnection = item.connectionSpecs.GenerateConnection())
            {
                openConnection.Open();
                while (true)
                {
                    if (!item.instanceCommandsGenerated || !item.databaseCommandsGenerated)
                    {
                        System.Threading.Thread.Sleep(1);
                        continue;
                    }

                    // IMPORTANT NOTE: This Sql Statement below is *NOT BEING EXECUTED*.
                    // (If it were, it would *totally* be SQL Injection vulnerable.)
                    // Instead, it's going into a notepad window for the user.

                    string results = "";
                    results += "declare @" + SqlCommandStringTemplates.searchTermParamValue + " varchar(max) ";
                    results += Environment.NewLine;
                    results += "set @" + SqlCommandStringTemplates.searchTermParamValue +
                            " = '" + item.searchTerm.Replace("'", "''") + "' ";
                    results += Environment.NewLine;
                    results += "declare @resultsTable TABLE (findType varchar(max), DBName varchar(max), foundObjectName varchar(max))";
                    results += Environment.NewLine;
                    results += Environment.NewLine;

                    foreach(var cmdParserPair in item.sqlCommands)
                    {
                        results += "insert into @resultsTable" + Environment.NewLine;
                        results += cmdParserPair.command.CommandText + Environment.NewLine;
                    }
                    results += "select * from @resultsTable order by DBName, findType, foundObjectName";

                    NotepadOpen.OpenContents(results);
                    return;
                }
            }
        }



        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (this.globalSearchFunction != null) this.globalSearchFunction.Invoke(this);
        }
    }

}
