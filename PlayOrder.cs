using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace startdemos_plus
{
    class PlayOrder
    {
        public static List<int> Reorder(List<ReorderInfo> infoList)
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

        public static List<ReorderInfo> ParseOrder(string input, int min, int max)
        {
            List<ReorderInfo> infoList = new List<ReorderInfo>();

            string[] list = input.Split(',');
            foreach (string member2 in list)
            {
                string member = member2.Trim();

                if (string.IsNullOrWhiteSpace(member))
                    continue;

                ReorderInfo info = new ReorderInfo();

                if (member.Contains('-'))
                {
                    if (member == "-")
                    {
                        info.Start = min;
                        info.End = max;
                    }
                    else
                    {
                        string[] minimembers = member.Split('-');
                        info.Start = int.Parse(minimembers[0]);
                        info.End = int.Parse(minimembers[1]);
                    }
                }
                else
                {
                    info.Start = int.Parse(member);
                    info.End = info.Start;
                }

                infoList.Add(info);
            }
            return infoList;
        }

        public static void Instructions()
        {
            WriteLine("Format for specifying demo playing order:");
            WriteLine("[,]   separates stages");
            WriteLine("[-]   will play all demos from index 0 to last in that stage");
            WriteLine("[x]   will play the demo with index of x in that stage");
            WriteLine("[x-y] will play demos fron index x to y in that stage");
            WriteLine("eg: \"1,2,-,5-12\" will play demo #1, then #2, then every demo, then demos from #5 to #12");
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
    }
}
