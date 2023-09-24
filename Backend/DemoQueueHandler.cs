using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using startdemos_plus.Utils;
using startdemos_plus.Backend.DemoChecking;
using System.Threading.Tasks;
using System.IO;
using static startdemos_plus.Utils.Helpers;

namespace startdemos_plus.Backend
{
    public class DemoQueueHandler
    {
        public List<DemoFile> Ordered = new List<DemoFile>();
        public List<bool> Played = new List<bool>();

        public List<DemoFile> GetPlayed()
        {
            return Ordered.Where((x, y) => Played[y]).ToList();
        }

        public void Reorder(List<DemoFile> files, DemoOrderType type, bool reversed, object configureObject)
        {
            Ordered = new List<DemoFile>();
            switch (type)
            {
                case DemoOrderType.LastModifiedDate:
                    {
                        Ordered = files
                            .OrderBy(x => File.GetLastWriteTime(x.FilePath))
                            .ToList();
                        break;
                    }
                case DemoOrderType.DemoFileName:
                    {
                        Ordered = files
                            .OrderBy(x => x.Name.PadNumbers())
                            .ThenBy(x => x.Index)
                            .ToList();
                        break;
                    }
                case DemoOrderType.DemoMapName:
                    {
                        Ordered = files
                            .OrderBy(X => X.MapName.PadNumbers())
                            .ThenBy(x => x.Name.PadNumbers())
                            .ThenBy(x => x.Index)
                            .ToList();
                        break;
                    }
                case DemoOrderType.CustomMapOrder:
                    {
                        var mapList = configureObject == null ? 
                            new List<string>() : 
                            ((string)configureObject).Replace("\r\n", "\n").Split('\n').Select(x => x.Trim()).ToList();
                        var included = new List<DemoFile>();
                        var others = new List<DemoFile>();

                        files.ForEach(x =>
                        {
                            if (mapList?.Contains(x.MapName) ?? false)
                                included.Add(x);
                            else others.Add(x);
                        });

                        Ordered = included
                            .OrderBy(x => mapList.IndexOf(x.MapName))
                            .ThenBy(x => x.Name.PadNumbers())
                            .ThenBy(x => x.Index)
                            .ToList();

                        Ordered.AddRange(others
                            .OrderBy(X => X.MapName.PadNumbers())
                            .ThenBy(x => x.Name.PadNumbers())
                            .ThenBy(x => x.Index)
                            .ToList());
                        break;
                    }
            }

            if (reversed)
                Ordered.Reverse();

            Played = new List<bool>();
            Ordered.ForEach(x => Played.Add(true));
        }

        public void Filter(
            int from, int to,
            string tickCond, int tickType, bool tickNot,
            string mapCond, bool mapNot,
            string[] passes,
            string[] fails,
            List<DemoCheck> checks
            )
        {
            Played = new List<bool>();
            Ordered.ForEach(x => Played.Add(false));

            for (int i = from; i <= (to >= Ordered.Count ? Ordered.Count - 1 : to); i++)
            {
                var curDemo = Ordered[i];
                var results = checks?.ConvertAll(x => x.Check(curDemo)).ToArray() ?? new DemoCheckResult[] { };

                if (!(Comparisons.NumericCompare(tickCond, tickType == 1 ? curDemo.GetMeasuredTicks(results) : curDemo.TotalTicks) ^ tickNot))
                    continue;

                if (!(Comparisons.StringCompare(mapCond, curDemo.MapName) ^ mapNot))
                    continue;

                if (passes.Length > 0 
                    && passes.Any(x => !results.Any(y => y.Check.Name == x.Trim() && y.PassedAll)))
                    continue;

                if (fails.Length > 0
                    && fails.Any(x => results.Any(y => y.Check.Name == x.Trim() && y.PassedAll)))
                    continue;

                Played[i] = true;
            }
        }
    }

    public enum DemoOrderType
    {
        [Description("Last Modified Date (from oldest to earliest)")]
        LastModifiedDate,
        [Description("Demo File Name")]
        DemoFileName,
        [Description("Demo Map Name")]
        DemoMapName,
        [Description("Custom Map Order")]
        CustomMapOrder
    }
}
