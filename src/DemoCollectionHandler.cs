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

            IndexOrder order = (IndexOrder)dCF.DemoOrder;
            List<string> custMapOrderList = new List<string>();
            Func<DemoFile, dynamic> sel = p => p.LastModifiedDate;
            Func<List<DemoFile>, List<DemoFile>> orderAction = null;
            switch (order)
            {
                case IndexOrder.LastModifiedDate:
                    orderAction = s => s.OrderBy(p => p.LastModifiedDate).ThenBy(p => p.Info.Index).ThenBy(p => p.Name).ToList();
                    break;
                case IndexOrder.DemoMapName:
                    orderAction = s => s.OrderBy(p => p.Info.MapName).ThenBy(p => p.Info.Index).ThenBy(p => p.Name).ToList();
                    break;
                case IndexOrder.DemoFileName:
                    orderAction = s => s.OrderBy(p => p.Info.Index).ThenBy(p => p.Name).ToList();
                    break;
                case IndexOrder.CustomMapOrder:
                    {
                        string custMapOrderPath = dCF.CustomMapOrderPath;

                        if (!File.Exists(custMapOrderPath))
                        {
                            MessageBox.Show("Input File for Custom Map Ordering does not exist!", "Demo Collection | Custom Map Ordering", MessageBoxButtons.OK);
                            return;
                        }

                        var custMapOrderFile = File.OpenText(custMapOrderPath);
                        string mapEntry = "";
                        while ((mapEntry = custMapOrderFile.ReadLine()) != null)
                            custMapOrderList.Add(mapEntry);

                        orderAction = s =>
                        {
                            List<DemoFile> included = new List<DemoFile>();
                            List<DemoFile> other = new List<DemoFile>();

                            s.ForEach(entry => 
                            {
                                if (custMapOrderList.Contains(entry.Info.MapName))
                                    included.Add(entry);
                                else
                                    other.Add(entry);
                            });

                            included = included
                                .OrderBy(p => custMapOrderList.IndexOf(p.Info.MapName))
                                .ThenBy(p => p.Info.Index)
                                .ThenBy(p => p.Name)
                                .ToList();

                            other = other
                                .OrderBy(p => p.Info.MapName)
                                .ThenBy(p => p.Info.Index)
                                .ThenBy(p => p.Name)
                                .ToList();

                            return included.Concat(other).ToList();                            
                        };

                        break;
                    }
            }

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
            Files = orderAction(Files);

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

    public enum IndexOrder
    {
        LastModifiedDate,
        DemoFileName,
        DemoMapName,
        CustomMapOrder
    }
}
