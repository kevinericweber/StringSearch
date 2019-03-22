using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;

namespace StringSearch.FileContentsSearching
{
    public partial class subformFileContents : Form, SearchableSubform
    {
        private const int FileReadingThreadCount = 1;

        private Action<SearchableSubform> globalSearchFunction;
        public subformFileContents()
        {
            InitializeComponent();
        }
        private void subformFileContents_Load(object sender, EventArgs e)
        {
            List<FileSizes> sizes = FileSizes.GetAll();
            sizes.OrderBy(s => s.maxFileSize).ToList().ForEach(x => comboMaxFilesize.Items.Add(x));

            comboSearchType.SelectedIndex = 0; // default to Text search
            comboMaxFilesize.SelectedIndex = 6; // default to 1 MB for size


            var settings = Properties.Settings.Default.FileContentsSettings;
            if (settings != null)
            {
                List<string> previousDirectories = settings.startDirectories;
                comboLocation.Items.AddRange(previousDirectories.Cast<object>().ToArray());
            }
        }

        public string SearchName { get { return "File Contents Search"; } }

        public ResultGenerator GenerateInitialSearchState()
        {
            string startDir = comboLocation.Text;
            if (string.IsNullOrEmpty(startDir)) throw new NonfatalUserException("Please fill out the start directory first.", comboLocation);
            if (!Directory.Exists(startDir)) throw new NonfatalUserException("The start directory you've specified doesn't exist.", comboLocation);

            string searchString = txtSearchValue.Text;
            if (string.IsNullOrEmpty(searchString)) throw new NonfatalUserException("Please fill out the search string first.", txtSearchValue);

            AddStartDirToSettingsIfNeeded(startDir);
            
            FileContentsOngoingSearch initialState = new FileContentsOngoingSearch();
            initialState.startingDirectory = startDir;
            initialState.searchTerm = searchString;
            initialState.filenameRegexPattern = txtFilenameRegex.Text;
            initialState.maxFileSize = ((FileSizes)comboMaxFilesize.SelectedItem).maxFileSize;

            ContentSearcher searcher = ContentSearcher.Text;
            if (this.comboSearchType.SelectedIndex == 1) searcher = ContentSearcher.Regex;
            initialState.searchType = searcher;

            initialState.directoriesToLookThrough.Push(initialState.startingDirectory);
            ResultGenerator lockable = initialState;
            return lockable;
        }

        private void AddStartDirToSettingsIfNeeded(string startDir)
        {
            if (Properties.Settings.Default.FileContentsSettings == null)
                Properties.Settings.Default.FileContentsSettings = new FileContentsUserSettings();
            var settings = Properties.Settings.Default.FileContentsSettings;
            if (settings.startDirectories == null)
                settings.startDirectories = new List<string>();
            if (!settings.startDirectories.Contains(startDir, StringComparer.OrdinalIgnoreCase))
            {
                settings.startDirectories.Add(startDir);
                Properties.Settings.Default.Save();
                Properties.Settings.Default.Reload();
            }
        }

        public List<Action<ResultGenerator>> GetSearchActions()
        {
            List<Action<ResultGenerator>> retVal = new List<Action<ResultGenerator>>();
            retVal.Add(FindDirectories);
            for (int i = 1; i <= FileReadingThreadCount; i++)
                retVal.Add(ExamineFiles);
            return retVal;
        }

        private void FindDirectories(ResultGenerator lockable)
        {
            // Note: we have to be extra careful about exceptions in this fuction - it's being done in a separate thread/task
            // (so if we have an exception, it won't bubble up; it'll just crash the program.)
            try
            {
                AttemptFindDirectories(lockable);
            }
            catch (Exception ex)
            {
                try
                {
                    ((FileContentsOngoingSearch)lockable).warnings.Add("Serious error enumerating directories - required abort.  Error = " + ex.ToString());
                }
                catch (Exception criticalEx)
                {
                    MessageBox.Show("Critical error: " + criticalEx.ToString());
                }

            }
        }

        private void AttemptFindDirectories(ResultGenerator lockable)
        {
            while (true)
            {
                FileContentsOngoingSearch itemCast = (FileContentsOngoingSearch)lockable;
                if (!itemCast.directoriesToLookThrough.Any())
                {
                    itemCast.doneFindingDirectories = true;
                    return;
                }

                string directoryToExamine;
                bool popWorked = itemCast.directoriesToLookThrough.TryPop(out directoryToExamine);

                if (!popWorked)
                {
                    System.Threading.Thread.Sleep(1);
                    continue;
                }
                try
                {
                    string[] files = System.IO.Directory.GetFiles(directoryToExamine);
                    foreach (string file in files)
                        itemCast.filesToLookThrough.Enqueue(file);
                    // TODO: Add Regex filter for file names

                    string[] subdirectories = System.IO.Directory.GetDirectories(directoryToExamine);
                    subdirectories = subdirectories.Reverse().ToArray();
                    if (subdirectories.Any())
                        itemCast.directoriesToLookThrough.PushRange(subdirectories);
                }
                catch (Exception)
                {
                    itemCast.warnings.Add("Could not successfully enumerate files and/or folders for: " + directoryToExamine);
                }
            }
        }

        private void ExamineFiles(ResultGenerator lockable)
        {
            // Note: we have to be extra careful about exceptions in this fuction - it's being done in a separate thread/task
            // (so if we have an exception, it won't bubble up; it'll just crash the program.)
            //try
            //{
                AttemptExamineFiles(lockable);
            //}
            //catch (Exception ex)
            //{
            //    try
            //    {
            //        ((FileContentsSearch)lockable.item).warnings.Add("Serious error examining files - required abort.  Error = " + ex.ToString());
            //    }
            //    catch (Exception criticalEx)
            //    {
            //        MessageBox.Show("Critical error: " + criticalEx.ToString());
            //    }

            //}
        }
        private void AttemptExamineFiles(ResultGenerator lockable)
        {
            while (true)
            {
                FileContentsOngoingSearch itemCast = (FileContentsOngoingSearch)lockable;
                if (!itemCast.filesToLookThrough.Any())
                {
                    System.Threading.Thread.Sleep(1);
                    if (itemCast.doneFindingDirectories)
                        if (!itemCast.filesToLookThrough.Any())
                            return;
                    continue;
                }

                string fileToSearch;
                bool dequeueWorked = itemCast.filesToLookThrough.TryDequeue(out fileToSearch);
                if (!dequeueWorked)
                {
                    System.Threading.Thread.Sleep(1);
                    continue;
                }

                if (!IsFileSomethingWeShouldSearch(fileToSearch, itemCast)) continue;
                FileResult possibleMatch = SearchFile(fileToSearch, itemCast.searchTerm, itemCast.searchType);
                if (possibleMatch != null)
                    itemCast.fileMatches.Add(possibleMatch);
            }
        }
        
        private bool IsFileSomethingWeShouldSearch(string filePath, FileContentsOngoingSearch searchInfo)
        {
            var fileAttributes = new FileInfo(filePath);
            if (fileAttributes.Length > searchInfo.maxFileSize) return false;

            if (string.IsNullOrEmpty(searchInfo.filenameRegexPattern)) return true;
            string fileNameOnly = Path.GetFileName(filePath);
            Regex regex = new Regex(searchInfo.filenameRegexPattern);
            Match match = regex.Match(fileNameOnly);
            return match != null;
        }

        private FileResult SearchFile(string filePath, string searchTerm, ContentSearcher searcher)
        {
            string fileContents = File.ReadAllText(filePath);
            bool isMatch = searcher.IsMatch(fileContents, searchTerm);
            if (isMatch)
                return new FileResult(filePath);
            return null;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (this.globalSearchFunction == null)
                throw new InvalidOperationException("Global Search Function hasn't been registered for this class!");

            this.globalSearchFunction.Invoke(this);
        }

        public void RegisterSearchCallback(Action<SearchableSubform> globalSearchFunction)
        {
            this.globalSearchFunction = globalSearchFunction;
        }

    }


}
