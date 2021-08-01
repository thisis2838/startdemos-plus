using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
using System.IO;
using static startdemos_plus.Program;

namespace startdemos_plus
{
    class DemoFileHandler
    {
        public List<DemoFile> Files;
        public string DemoPath { get; set; }

        public DemoFileHandler()
        {
            Program.PrintSeperator("DEMO QUEUE PREREQUISITES");
            WriteLine("Please enter the absolute path to the folder containing your demos");
            DemoPath = ReadLine().Trim();

            if (!Directory.Exists(DemoPath))
                throw new FileNotFoundException("Folder does not exist!");

            Files = new List<DemoFile>();
            var files = Directory.EnumerateFiles(DemoPath, "*.dem");

            foreach (string demo in files)
                Files.Add(new DemoFile(demo));

            var result = Files.OrderBy(p => p.LastModifiedDate).ThenBy(p => p.Info.Index);
            Files = result.ToList();

            WriteLine($"Got {Files.Count()} files, listed from order of last modified date and demo index:");
            int i = 0;
            foreach (DemoFile file in Files)
                WriteLine($"[{i++:000}] - {file}");

            double estTime =
                (ToUnixTime(File.GetLastWriteTime(Files.Last().FilePath)) - ToUnixTime(File.GetLastWriteTime(Files.First().FilePath)))
                + Files.First().Info.TotalTicks * settings.TickRate;

            int estDemoTime = 0;
            Files.ForEach(x => estDemoTime += x.Info.TotalTicks);

            WriteLine($"Estimated run time (from file creation dates): {TimeSpan.FromSeconds(estTime)}");
            WriteLine($"Estimated run time (from demo times): {TimeSpan.FromSeconds(estDemoTime * settings.TickRate)}");
            WriteLine($"(estimated times calculated with tickrate of {settings.TickRate:0.000000})");
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
                LastModifiedDate = ToUnixTime(File.GetLastWriteTime(filePath));
                Info = new DemoParseResult(filePath);

                if ((new DriveInfo(mScan.GameDir)).RootDirectory.FullName.ToLower() !=
                    (new DriveInfo(filePath)).RootDirectory.FullName.ToLower())
                    throw new Exception("Demo and game directories have to be in the same drive!");
                PlayCommand = $"playdemo \"{GetRelativePath(filePath, mScan.GameDir)}\";";
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

                return $"{name}{File.GetLastWriteTime(FilePath)}";
            }
        }
        private static string GetRelativePath(string filespec, string folder)
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

        private static long ToUnixTime(DateTime time)
        {
            return ((DateTimeOffset)time).ToUnixTimeSeconds();
        }
    }
}
