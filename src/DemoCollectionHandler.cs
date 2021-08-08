using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static startdemos_ui.Utils.FileHelper;
using static startdemos_ui.MainForm;
using static startdemos_ui.Utils.ThreadHelper;
using static startdemos_ui.src.Events;
using System.Windows.Forms;

namespace startdemos_ui.src
{
    public class DemoCollectionHandler
    {
        public DemoCollectionHandler()
        {
        }

        public List<DemoFile> Files;

        public void Scan(string folder)
        {
            if (!Directory.Exists(folder))
                return;

            Files = new List<DemoFile>();
            var files = Directory.EnumerateFiles(folder, "*.dem");
            ThreadAction(dCF, () => { dCF.labFoundDemosCount.Text = files.Count().ToString(); });

            DemoCheckHandler checkHandler = dCE.GetCurrentDemoChecks();

            dLF.ClearAll();
            for (int j = 0; j < files.Count(); j++)
            {
                DemoFile file = new DemoFile(files.ElementAt(j), checkHandler);
                Files.Add(file);
                dCF.SetCurDemoInfo(j, file.Name);
            }

            dCF.SetCurDemoInfo(files.Count() - 1, "");

            Files = Files.OrderBy(p => p.LastModifiedDate).ThenBy(p => p.Info.Index).ToList();

            for (int j = 0; j < files.Count(); j++)
                dLF.DemoListAdd(j, Files[j]);

            GotDemos?.Invoke(null, null);
        }
    }

    public class DemoFile
    {
        public string FilePath { get; set; }
        public string Name => Path.GetFileName(FilePath);
        public long LastModifiedDate { get; internal set; }
        public DemoParseResult Info { get; internal set; }
        public string PlayCommand { get; internal set; }

        public DemoFile(string filePath, DemoCheckHandler curDemoChecks = null)
        {
            FilePath = filePath;
            LastModifiedDate = ToUnixTime(File.GetLastWriteTime(filePath));
            Info = new DemoParseResult(filePath, curDemoChecks);
            PlayCommand = $"playdemo \"{filePath}\";";
        }
    }
}
