using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringSearch
{
    public interface SearchableSubform
    {
        string SearchName { get; }
        ResultGenerator GenerateInitialSearchState();
        
        List<Action<ResultGenerator>> GetSearchActions();

        void RegisterSearchCallback(Action<SearchableSubform> globalSearchFunction);
        
    }

    public static class SubformExtensionClass
    {
        // realistically, SearchableSubform should be an abstract class deriving from Form
        // with the below function as code within that abstract class
        // However, VisualStudio won't let you pull up in designer view
        // something that derives from an abstract class that derives from Form
        // because of that, we're using an interface and then adding this function
        // to the interface via an extension method.
        public static List<Task>PerformSearchAsTasks(this SearchableSubform subform, ResultGenerator InitialState)
        {
            List<Action<ResultGenerator>> searchActions = subform.GetSearchActions();
            List<Task> retVal = new List<Task>();
            foreach (var searchAction in searchActions)
            {
                Task t = new Task(() => searchAction(InitialState));
                t.Start();
                retVal.Add(t);
            }
            return retVal;

        }
    }
}
