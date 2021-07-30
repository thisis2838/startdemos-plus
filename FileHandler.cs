using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Text.RegularExpressions;

namespace startdemos_plus
{
    class FileHandler
    {
        public static string GetRelativePath(string filespec, string folder)
        {
            Uri pathUri = new Uri(filespec);
            // Folders must end in a slash
            if (!folder.EndsWith(Path.DirectorySeparatorChar.ToString()))
            {
                folder += Path.DirectorySeparatorChar;
            }
            Uri folderUri = new Uri(folder);
            return Uri.UnescapeDataString(folderUri.MakeRelativeUri(pathUri).ToString().Replace('/', Path.DirectorySeparatorChar));
        }

        public class DemoFile
        {
            public string FilePath { get; set; }
            public string Name => Path.GetFileName(FilePath);
            public long LastModifiedDate { get; internal set; }
            public DemoParseResult Info { get; internal set; }
            public string PlayCommand { get; internal set; }

            public DemoFile(string filePath)
            {
                FilePath = filePath;
                LastModifiedDate = ((DateTimeOffset)File.GetLastWriteTime(filePath)).ToUnixTimeSeconds();
                Info = new DemoParseResult(filePath);

                if ((new DriveInfo(Program.GameDir)).RootDirectory.FullName !=
                    (new DriveInfo(filePath)).RootDirectory.FullName)
                    throw new Exception("Demo and game have to be in the same drive!");
                PlayCommand = "playdemo " + GetRelativePath(filePath, Program.GameDir);
            }

            public override string ToString()
            {
                const int len = 35;
                string name = new string('.', len + 3);
                string cutName = (Name.Length > len) ? Name.Substring(0, len) : Name;

                name = name
                    .Remove(0, cutName.Length)
                    .Insert(0, cutName);

                name += "...";

                return $"{name}{File.GetLastWriteTimeUtc(FilePath)}";
            }
        }

        public List<DemoFile> Files;
        public string DemoPath { get; set; }

        public FileHandler()
        {
            Program.PrintSeperator("DEMO QUEUE PREREQUISITES");
            WriteLine("Please enter the absolute path to the folder containing your demos");
            DemoPath = ReadLine();

            if (!Directory.Exists(DemoPath))
                throw new FileNotFoundException("Folder does not exist!");

            Files = new List<DemoFile>();
            var files = Directory.EnumerateFiles(DemoPath, "*.dem");

            foreach (string demo in files)
                Files.Add(new DemoFile(demo));

            var result = Files.OrderBy(p => p.LastModifiedDate).ThenBy(p => p.Info.Index);
            Files = result.ToList();

            WriteLine($"Got {Files.Count()} files, listed from order of last modified date and demo index:");
            foreach (DemoFile file in Files)
                WriteLine(file);
        }
    }
}
