using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringSearch.FileContentsSearching
{
    public class FileSizes
    {
        private static List<FileSizes> allValues = new List<FileSizes>();
        public string displayText { get; private set; }
        public long maxFileSize { get; private set; }

        private FileSizes(string displayText, long maxFileSize)
        {
            this.displayText = displayText;
            this.maxFileSize = maxFileSize;
            allValues.Add(this);
        }

        private const int bytesPerK = 1024;
        private const int bytesPerMB = 1024 * 1024;

        public static FileSizes tenK = new FileSizes("10k", 10 * bytesPerK);
        public static FileSizes twentyK = new FileSizes("20k", 20 * bytesPerK);
        public static FileSizes fiftyK = new FileSizes("50k", 50 * bytesPerK);
        public static FileSizes hundredK = new FileSizes("100k", 100 * bytesPerK);
        public static FileSizes twohundredK = new FileSizes("200k", 200 * bytesPerK);
        public static FileSizes halfMB = new FileSizes("500k", 500 * bytesPerK);
        public static FileSizes oneMB = new FileSizes("1 MB", 1 * bytesPerMB);
        public static FileSizes twoMB = new FileSizes("2 MB", 2 * bytesPerMB);
        public static FileSizes fiveMB = new FileSizes("5 MB", 5 * bytesPerMB);
        public static FileSizes tenMB = new FileSizes("10 MB", 10 * bytesPerMB);
        public static FileSizes noLimit = new FileSizes("No Limit", long.MaxValue);

        public static List<FileSizes> GetAll()
        {
            List<FileSizes> retVal = new List<FileSizes>();
            retVal.AddRange(allValues); // shallow copy so we don't lose encapsulation
            return retVal;
        }

        public override string ToString()
        {
            return this.displayText;
        }
    }
}
