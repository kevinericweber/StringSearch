using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringSearch.FileContentsSearching
{

    public class FileContentsOngoingSearch : ResultGenerator
    {
        public string startingDirectory;
        public string searchTerm;
        public ContentSearcher searchType;
        public long maxFileSize;
        public string filenameRegexPattern;
        public bool doneFindingDirectories = false;
        public ConcurrentStack<string> directoriesToLookThrough = new ConcurrentStack<string>();
        public ConcurrentQueue<string> filesToLookThrough = new ConcurrentQueue<string>();
        public ConcurrentBag<FileResult> fileMatches = new ConcurrentBag<FileResult>();
        public ConcurrentBag<string> warnings = new ConcurrentBag<string>(); // TODO: I don't think this info is shown anywhere to the users at this point.  Need to figure out best way to handle that

        public override string ToString()
        {
            return "Folders: " + this.directoriesToLookThrough.Count() +
                    "  Files: " + this.filesToLookThrough.Count() +
                    "  Matches: " + this.fileMatches.Count();
        }

        public List<Result> GenerateResults()
        {
            return this.fileMatches.Cast<Result>().ToList();
        }
    }
}
