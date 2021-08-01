using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
using static startdemos_plus.Program;

namespace startdemos_plus
{
    class PlayOrderHandler
    {
        public List<int> PlayOrderIndicies { get; set; }
        public List<ReorderInfo> PlayOrderList { get; set; }

        public PlayOrderHandler()
        {
            PrintSeperator("DEMO ORDER");
            WriteLine("Format for specifying demo playing order:");
            WriteLine("[,]   separates stages");
            WriteLine("[-]   will play all demos from first to last according to index in that stage");
            WriteLine("[a]   will play all demos in alphabetical order according to it's file name in that stage");
            WriteLine("[x]   will play the demo with index of x in that stage");
            WriteLine("[x-y] will play demos fron index x to y in that stage");
            WriteLine("[r]   will reverse the order of the demos in that state (can be typed anywhere)");
            WriteLine("\nExamples:");
            WriteLine("\"-\"        will play all demos from first to last according to index");
            WriteLine("\"r-\"       will play all demos in reverse order");
            WriteLine("\"1,2\"      will play demo #1, then #2");
            WriteLine("\"ra,5-12\"  will play every demo in reversed alphabetical order, then demos from #5 to #12");

            WriteLine("\nPlease enter demo playing order (or press Enter to play all demos in order of index)");

            List<ReorderInfo> order = new List<ReorderInfo>();
            List<int> indicies = new List<int>();
            string orderString = ReadLine().Trim();
            if (orderString == "")
                orderString = "-";

            order = ParseOrder(orderString, demoFile.Files);
            indicies = Reorder(order);

            PlayOrderIndicies = indicies;
            PlayOrderList = order;
        }

        public List<int> Reorder(List<ReorderInfo> infoList)
        {
            List<int> order = new List<int>();
            foreach (ReorderInfo info in infoList)
            {
                if (info.Start == info.End)
                    order.Add(info.Start);
                else
                {
                    int i = i = info.Start;
                    int end = info.End + ((info.Start < info.End) ? 1 : -1);
                    while (i != end)
                    {
                        order.Add(i);
                        i = (info.Start < info.End) ? i + 1 : i - 1;
                    }
                }
            }
            return order;
        }

        public List<ReorderInfo> ParseOrder(string input, List<DemoFileHandler.DemoFile> inputList)
        {
            List<ReorderInfo> infoList = new List<ReorderInfo>();

            string[] list = input.Split(',');
            foreach (string member2 in list)
            {
                string member = member2.Trim();

                bool reversed = false;
                if (member.Contains('r'))
                {
                    reversed = true;
                    member = member.Replace("r", string.Empty);
                }

                if (string.IsNullOrWhiteSpace(member))
                    continue;

                ReorderInfo info = new ReorderInfo();

                if (member.Contains('-'))
                {
                    if (member == "-")
                    {
                        info.Start = 0;
                        info.End = inputList.Count() - 1;
                    }
                    else
                    {
                        string[] minimembers = member.Split('-');
                        info.Start = int.Parse(minimembers[0]);
                        info.End = int.Parse(minimembers[1]);
                    }
                }
                else if (member.Trim() == "a")
                {
                    var indicies = AlphabeticalReorder(inputList.ConvertAll(x => x.Name));
                    if (reversed)
                        indicies.Reverse();
                    infoList.AddRange(ParseOrder(indicies));
                    continue;
                }
                else
                {
                    info.Start = int.Parse(member);
                    info.End = info.Start;
                }

                if (reversed)
                    info.Reverse();

                infoList.Add(info);
            }
            return infoList;
        }

        public List<ReorderInfo> ParseOrder(List<int> indicies)
        {
            List<ReorderInfo> list = new List<ReorderInfo>();
            ReorderInfo info = new ReorderInfo();
            info.Start = indicies[0];
            for (int i = 1; i <= indicies.Count; i++)
            {
                if ((i == indicies.Count) || 
                    !(indicies[i] == indicies[i-1] + 1 || indicies[i] == indicies[i-1] - 1))
                {
                    info.End = indicies[i-1];
                    list.Add(info);
                    if (i == indicies.Count)
                        break;
                    info.Start = indicies[i];
                }
            }
            return list;
        }

        public List<int> AlphabeticalReorder(List<string> names)
        {
            List<Tuple<int, string>> entries = new List<Tuple<int, string>>();

            int i = 0;
            foreach (string name in names)
            {
                entries.Add(new Tuple<int, string>(i, name));
                i++;
            }

            List<int> indicies = new List<int>();
            entries.OrderBy(x => x.Item2).ToList().ForEach(x => indicies.Add(x.Item1));
            return indicies;
        }
    }

    public struct ReorderInfo
    {
        public int Start;
        public int End;

        public override string ToString()
        {
            if (Start == End)
                return $"{Start}";
            return $"{Start} -> {End}";
        }

        public void Reverse()
        {
            if (Start == End)
                return;

            int tmp = End;
            End = Start;
            Start = tmp;
        }
    }
}
