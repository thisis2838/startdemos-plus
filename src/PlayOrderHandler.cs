using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static startdemos_ui.MainForm;

namespace startdemos_ui.src
{
    class PlayOrderHandler
    {
        public List<int> Order;
        public List<OrderInfo> OrderInfo;

        public PlayOrderHandler(string input)
        {
            try
            {
                OrderInfo = ParseOrder(input);
            }
            catch 
            {
                MessageBox.Show("Play Order syntax invalid! Defaulting to \"-\"", "Play Order | Play Order parsing", MessageBoxButtons.OK);
                OrderInfo = ParseOrder("-");
            }

            Order = GenerateOrder(OrderInfo);
        }

        public List<int> GenerateOrder(List<OrderInfo> infoList)
        {
            List<int> order = new List<int>();
            foreach (OrderInfo info in infoList)
            {
                if (info.Start == info.End)
                    order.Add(info.Start);
                else
                {
                    int i = info.Start;
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

        public List<OrderInfo> ParseOrder(string input)
        {
            List<OrderInfo> infoList = new List<OrderInfo>();
            List<DemoFile> inputList = new List<DemoFile>();

            string[] list = input.Split(',');
            foreach (string member2 in list)
            {
                string[] parts = member2.Trim().Split('/');
                string member = parts[0].Trim();
                StringComparison check = new StringComparison(parts.Count() > 2 ? parts[2] : "");

                bool reversed = false;
                NumericalComparison tickCompare = new NumericalComparison(parts.Count() > 1 ? parts[1] : "");
                bool alphabetical = false;

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

                OrderInfo info = new OrderInfo();

                if (member.Contains('-'))
                {
                    if (member == "-")
                    {
                        info.Start = 0;
                        info.End = dCH.Files.Count() - 1;
                    }
                    else
                    {
                        string[] minimembers = member.Split('-');
                        info.Start = int.Parse(minimembers[0]);
                        info.End = int.Parse(minimembers[1]);
                    }

                    inputList = info.Cut(dCH.Files);

                    if (alphabetical)
                            inputList = inputList.OrderBy(x => x.Name).ToList();

                    if (reversed)
                        inputList.Reverse();

                    inputList = inputList
                        .Where(x => tickCompare.CompareTo(x.Info.TotalTicks))
                        .Where(x => check.CompareTo(string.Join("", x.Info.Events.Distinct().Select(y => y.Name))))
                        .ToList();

                    List<int> indicies = new List<int>();
                    inputList.ForEach(x => indicies.Add(dCH.Files.IndexOf(x)));

                    infoList.AddRange(ParseOrder(indicies));
                }
                else
                {
                    info.Start = int.Parse(member);
                    info.End = info.Start;

                    if (!tickCompare.CompareTo(dCH.Files[info.Start].Info.TotalTicks))
                        continue;

                    infoList.Add(info);
                }
            }
            return infoList;
        }

        public List<OrderInfo> ParseOrder(List<int> indicies)
        {
            if (indicies.Count == 0)
                return new List<OrderInfo>();

            List<OrderInfo> list = new List<OrderInfo>();
            OrderInfo info = new OrderInfo();
            info.Start = indicies[0];
            for (int i = 1; i <= indicies.Count; i++)
            {
                if ((i == indicies.Count) ||
                    !(indicies[i] == indicies[i - 1] + 1 || indicies[i] == indicies[i - 1] - 1))
                {
                    info.End = indicies[i - 1];
                    list.Add(info);
                    if (i == indicies.Count)
                        break;
                    info.Start = indicies[i];
                }
            }
            return list;
        }
    }

    public struct OrderInfo
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
            List<T> d = input.Skip(Start > End ? End : Start).Take(Math.Abs(Start - End) + 1).ToList();
            if (Start > End)
                d.Reverse();
            return d;
        }
    }
}
