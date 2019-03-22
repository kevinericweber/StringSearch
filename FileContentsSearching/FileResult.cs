using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringSearch.FileContentsSearching
{
    public class FileResult : Result
    {
        private string fileName;
        public FileResult(string fileName)
        {
            this.fileName = fileName;
        }
        public string displayText { get { return fileName; } }
        public void Open()
        {
            NotepadOpen.OpenExistingFile(this.fileName);
        }
        public override string ToString()
        {
            return this.fileName;
        }
    }
}
