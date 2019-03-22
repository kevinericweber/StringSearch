using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringSearch
{
    public static class NotepadOpen
    {
        public static void OpenContents(string contents)
        {
            // TODO: The Path.GetTempFileName() is kinda flaky - it only allows 65k calls to it between reboots
            // (which sounds good, except that amount is shared amongst all apps that use the function, and
            // some apps generate *lots* of temp files.)
            // So this should get replaced with the temp fol + GuidGeneratedFilename.  Or maybe
            // also include information about the thing being opened?  As a second arg to this function?
            string tempFile = System.IO.Path.GetTempFileName();
            System.IO.File.WriteAllText(path: tempFile, contents: contents);
            OpenExistingFile(tempFile);
        }
        public static void OpenExistingFile(string fileLoc)
        {

            var pathToWindows = Environment.GetFolderPath(Environment.SpecialFolder.Windows);
            var pathToNotepad = System.IO.Path.Combine(pathToWindows, "system32", "notepad.exe");

            System.Diagnostics.Process.Start(pathToNotepad, fileLoc);
        }
    }
}
