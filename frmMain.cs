using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StringSearch
{
    public partial class frmMain : Form
    {
        // TODO: I'm not happy with the results pane display mechanism, and it should probably be redone.
        // Maybe force <Result> to implement IComparable so the sorting is better?  Maybe change it
        // to a tree structure?  Not sure.  Will have to give it some thought.
        // (Noted problems: not the most user-friendly, and in File Searches, subdirectories interspersed amongst subfiles.
        //    A listing of a directory should have all the subdirectories in one place, not mixed amongst the files.)

        Form subformOnDisplay = null;
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            lblMainStatus.Text = "";
            PopulateSearchTypeCombo();
            SetStartSearchType();
        }

        private void SetStartSearchType()
        {
            Properties.Settings.Default.Reload();
            if (string.IsNullOrEmpty(Properties.Settings.Default.LastSearchUsedName))
                return;
            comboOverallSearchType.SelectedItem = Properties.Settings.Default.LastSearchUsedName;
        }

        private void PopulateSearchTypeCombo()
        {
            List<SearchableSubform> subforms = GetAllSearchableSubforms();
            comboOverallSearchType.Items.AddRange(subforms.Select(s => s.SearchName).ToArray());
            comboOverallSearchType.SelectedIndex = 0;
        }

        private void LoadSubform(string subformSearchName)
        {
            List<SearchableSubform> subforms = GetAllSearchableSubforms();
            SearchableSubform subformNeeded = subforms.FirstOrDefault(
                s => s.SearchName.Equals(subformSearchName, StringComparison.OrdinalIgnoreCase));
            if (subformNeeded == null)
            {
                MessageBox.Show("Couldn't load search type: " + subformSearchName);
                return; // should never happen, but better to play it safe
            }
            if (!(subformNeeded is Form))
            {
                MessageBox.Show("Unable to handle search type - it didn't implement Windows.Form: " + subformSearchName);
                return; // again, should never happen, but better to play it safe
            }
            Form formCast = (Form)subformNeeded;
            DisplaySubform(formCast);
        }
        private void DisplaySubform(Form subform)
        {
            subform.TopLevel = false;
            subform.FormBorderStyle = FormBorderStyle.None;
            subform.AutoSize = true;
            subform.Visible = true;
            ((Control)subform).Location = new Point(10, listResults.Top);
            ((Control)subform).Size = new Size(350, listResults.Height);
            ((Control)subform).TabIndex = 1; // put it after the main type combo, but before results
            ((SearchableSubform)subform).RegisterSearchCallback(PerformSearch);
            this.Controls.Add(subform);

            if (this.subformOnDisplay != null)
            {
                this.Controls.Remove(this.subformOnDisplay);
                this.subformOnDisplay.Close();
            }
            this.subformOnDisplay = subform;
        }

        private List<SearchableSubform> GetAllSearchableSubforms()
        {
            return ReflectionHelper.GetAllNonabstractClassesThatImplement<SearchableSubform>();
        }

        private void PerformSearch(SearchableSubform subform)
        {
            ToggleGuiForInProgress(searchIsStarting: true);
            listResults.Items.Clear();
            try
            {
                AttemptPerformSearch(subform);
            }
            catch (NonfatalUserException userEx)
            {
                MessageBox.Show(userEx.Message);
                userEx.SetFocusOnRelevantControl();
            }
            catch (Exception ex)
            {
                MessageBox.Show("There was a problem performing the search: " + ex.ToString());
            }
            ToggleGuiForInProgress(searchIsStarting: false);
        }
        private void AttemptPerformSearch(SearchableSubform subform)
        {
            ResultGenerator lockable = subform.GenerateInitialSearchState();

            Stopwatch sw = new Stopwatch();
            sw.Start();

            List<Task> tasksToWaitOn = subform.PerformSearchAsTasks(lockable);

            while (tasksToWaitOn.Any(t => !t.IsCompleted))
            {
                this.lblMainStatus.Text = lockable.ToString();

                if (tasksToWaitOn.All(t => t.IsCompleted))
                    break;
                Application.DoEvents(); 
                System.Threading.Thread.Sleep(100);
                UpdateResults(lockable);
            }

            UpdateResults(lockable);
            sw.Stop();
            this.lblMainStatus.Text = "Finished.  "
                    + listResults.Items.Count + " results." +
                    "  Time taken: " + (sw.ElapsedMilliseconds / 1000).ToString("0.00") + "s";

            SaveLastSearch(subform);
        }

        private void UpdateResults(ResultGenerator lockable)
        {
            string currentlySelectedResult = listResults.Text;
            List<Result> results = lockable.GenerateResults();
            Result[] newResults = results.Where(r => !listResults.Items.Contains(r)).ToArray();
            listResults.Items.AddRange(newResults);
        }
        
        private void ToggleGuiForInProgress(bool searchIsStarting)
        {
            progressMainStatus.Visible = searchIsStarting;
            if (this.subformOnDisplay != null) this.subformOnDisplay.Enabled = !searchIsStarting;
        }

        private void comboOverallSearchType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string searchName = (string)comboOverallSearchType.SelectedItem;
            if (string.IsNullOrEmpty(searchName)) return;
            LoadSubform(searchName);
        }

        private void listResults_DoubleClick(object sender, EventArgs e)
        {
            object chosen = listResults.SelectedItem;
            if (chosen == null) return;
            if (!(chosen is Result)) return;
            Result result = (Result)chosen;
            result.Open();
        }

        private void SaveLastSearch(SearchableSubform subform)
        {
            Properties.Settings.Default.Reload();
            Properties.Settings.Default.LastSearchUsedName = subform.SearchName;
            Properties.Settings.Default.Save();
            Properties.Settings.Default.Reload();
        }
    }
}
