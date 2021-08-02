using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
using System.IO;
using static startdemos_plus.Program;
using static startdemos_plus.PrintHelper;

namespace startdemos_plus
{
    class DemoFileHandler
    {
        public List<DemoFile> Files;
        public string DemoPath { get; set; }

        public DemoFileHandler()
        {
            PrintSeperator("DEMO QUEUE PREREQUISITES");
            WriteLine("Please enter the absolute path to the folder containing your demos");
            DemoPath = ReadLine().Trim();

            if (!Directory.Exists(DemoPath))
                throw new FileNotFoundException("Folder does not exist!");

            Files = new List<DemoFile>();
            var files = Directory.EnumerateFiles(DemoPath, "*.dem");

            int y = CursorTop;
            for (int j = 0; j < files.Count(); j++)
            {
                Files.Add(new DemoFile(files.ElementAt(j)));
                PrintAtX($"Processed {j}/{files.Count()} \nCurrent file: {Files[j].Name}", CursorLeft);
                CursorTop = y;
            }
            ClearAtY(CursorTop);
            ClearAtY(CursorTop + 1);

            var result = Files.OrderBy(p => p.LastModifiedDate).ThenBy(p => p.Info.Index);
            Files = result.ToList();

            Table demoInfoTable = new Table(new int[] { 6, 35, 30, 6 }, true, ' ');
            demoInfoTable.PrintHeader(new string[] { "INDEX", "DEMO NAME", "LAST MODIFY DATE", "TICKS"});
            int i = 0;
            foreach (DemoFile file in Files)
                demoInfoTable.PrintLine(new string[] { 
                    i++.ToString(),
                    file.Name,
                    File.GetLastWriteTime(file.FilePath).ToString(),
                    file.Info.TotalTicks.ToString()});

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
                return $"{CharLeader(Name, 30, 35, ' ')}{CharLeader(File.GetLastWriteTime(FilePath).ToString(), 25, 30, ' ', false)}{Info.TotalTicks}";
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
