using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace StringSearch
{
    public class ContentSearcher
    {
        private Func<string, string, bool> compareFunc;

        private ContentSearcher(Func<string, string, bool> compareFunc)
        {
            this.compareFunc = compareFunc;
        }

        public static ContentSearcher Text = new ContentSearcher(IsTextMatch);
        public static ContentSearcher Regex = new ContentSearcher(IsRegexMatch);

        public bool IsMatch(string contentsToSearch, string searchTerm)
        {
            return this.compareFunc.Invoke(contentsToSearch, searchTerm);
        }

        private static bool IsTextMatch(string fileContents, string searchTerm)
        {
            int firstFindPos = fileContents.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase);
            // TODO: Case sensitivity really should be a user pref at some point.
            if (firstFindPos >= 0)
                return true;
            return false;
        }
        private static bool IsRegexMatch(string fileContents, string searchTerm)
        {
            Regex regex = new Regex(searchTerm, RegexOptions.IgnoreCase);
                    // TODO: Case sensitivity really should be a user pref at some point.
            Match match = regex.Match(fileContents);
            if (match == null) return false;
            return match.Success;
        }
    }
    
}
