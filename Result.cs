using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringSearch
{

    public interface ResultGenerator
    {
        List<Result> GenerateResults();
    }
    public interface Result
    {
        string displayText { get; }
        void Open();
    }
}
