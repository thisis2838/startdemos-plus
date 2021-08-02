using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
using static startdemos_plus.Program;
using static startdemos_plus.PrintHelper;
using System.Threading;
using System.Windows.Forms;

namespace startdemos_plus
{
    class PlayOrderHandler
    {
        public List<int> PlayOrderIndicies { get; set; }
        public List<ReorderInfo> PlayOrderList { get; set; }

        public PlayOrderHandler()
        {
            PrintSeperator("DEMO ORDER");
            WriteLine("Please enter demo playing order (or press Enter to play all demos in order of index)");
            WriteLine("Enter h for info on formatting demo playing order.");

            g:
            string orderString = ReadLine().Trim();
            if (orderString == "h")
            {
                Thread helpWindow = new Thread(new ThreadStart(PrintInstructions));
                helpWindow.SetApartmentState(ApartmentState.STA);
                helpWindow.Start();
                WriteLine("Please enter demo playing order (or press Enter to play all demos in order of index)");
                goto g;
            }

            List<ReorderInfo> order = new List<ReorderInfo>();
            List<int> indicies = new List<int>();
            if (orderString == "")
                orderString = "-";

            order = ParseOrder(orderString);
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

        public List<ReorderInfo> ParseOrder(string input)
        {
            List<ReorderInfo> infoList = new List<ReorderInfo>();
            List<DemoFileHandler.DemoFile> inputList = new List<DemoFileHandler.DemoFile>();

            string[] list = input.Split(',');
            foreach (string member2 in list)
            {
                string[] parts = member2.Trim().Split('/');
                string member = parts[0];

                bool reversed = false;
                bool compareTicks = false;
                ComparisonInfo tickCompare = new ComparisonInfo();
                bool alphabetical = false;

                if (parts.Count() > 1 && !string.IsNullOrWhiteSpace(parts[1]))
                {
                    tickCompare = new ComparisonInfo(parts[1]);
                    compareTicks = true;
                }
                if (member.Contains('r'))
                {
                    reversed = true;
                    member = member.Replace("r", string.Empty);
                }
                if (member.Contains('a'))
                {
                    alphabetical = true;
                    member = member.Replace("a", string.Empty);
                }

                if (string.IsNullOrWhiteSpace(member))
                    continue;

                ReorderInfo info = new ReorderInfo();

                if (member.Contains('-'))
                {
                    if (member == "-")
                    {
                        info.Start = 0;
                        info.End = demoFile.Files.Count() - 1;
                    }
                    else
                    {
                        string[] minimembers = member.Split('-');
                        info.Start = int.Parse(minimembers[0]);
                        info.End = int.Parse(minimembers[1]);
                    }

                    inputList = info.Cut(demoFile.Files);

                    if (alphabetical)
                        inputList = inputList.OrderBy(x => x.Name).ToList();

                    if (reversed)
                        inputList.Reverse();

                    if (compareTicks)
                        inputList = inputList.Where(x => tickCompare.CompareTo(x.Info.TotalTicks)).ToList();

                    List<int> indicies = new List<int>();
                    inputList.ForEach(x => indicies.Add(demoFile.Files.IndexOf(x)));

                    infoList.AddRange(ParseOrder(indicies));
                }
                else
                {
                    info.Start = int.Parse(member);
                    info.End = info.Start;

                    if (!(compareTicks && tickCompare.CompareTo(demoFile.Files[info.Start].Info.TotalTicks)))
                        continue;

                    infoList.Add(info);
                }                
            }
            return infoList;
        }

        public List<ReorderInfo> ParseOrder(List<int> indicies)
        {
            if (indicies.Count == 0)
                return new List<ReorderInfo>();

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

        private void PrintInstructions()
        {
            Application.EnableVisualStyles();
            Application.Run(new PlayOrderHelpForm());
        }
    }

    public enum ComparisonOperator
    {
        Equal,
        Greater,
        Lower
    }

    public struct ComparisonInfo
    {
        public bool Active { get; internal set; }
        public ComparisonOperator Operator { get; set; }
        public bool Equal { get; set; }
        public int Number { get; set; }
        public ComparisonInfo (string input)
        {
            Operator = ComparisonOperator.Equal;
            switch (input[0])
            {
                case '>':
                    Operator = ComparisonOperator.Greater;
                    break;
                case '<':
                    Operator = ComparisonOperator.Lower;
                    break;
                case '=':
                    Operator = ComparisonOperator.Equal;
                    break;
            }

            Equal = input.Contains('=');
            Number = int.Parse(input.Trim(new char[] { '>', '<', '=' }));
            Active = true;

        }
        public bool CompareTo(int candidate)
        {
            bool returner = false;
            switch (Operator)
            {
                case ComparisonOperator.Greater:
                    returner = Number < candidate;
                    break;
                case ComparisonOperator.Lower:
                    returner = Number > candidate;
                    break;
            }

            if (!returner && Equal)
                returner = Number == candidate;

            return returner;
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

        public List<T> Cut<T>(List<T> input)
        {
            return input.Skip(Start > End ? End : Start).Take(Math.Abs(Start - End) + 1).ToList();
        }
    }
}
