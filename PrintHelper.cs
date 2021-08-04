using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace startdemos_plus
{
    static class PrintHelper
    {
        public static void PrintSeperator(string name = "")
        {
            WriteLine();
            WriteLine($"------[{name}]------");
        }

        public static void PrintAtX(string msg, int x)
        {
            int oldX = CursorLeft;
            CursorLeft = x;
            Write(msg + new string(' ', WindowWidth - msg.Length - 1));
            CursorLeft = oldX;
        }

        public static void ClearAtY(int y)
        {
            int oldX = CursorLeft;
            Write(new string(' ', WindowWidth));
            CursorLeft = oldX;
        }

        public static string CharLeader(string text, int textMaxSize, int leaderSize, char leaderChar, bool trailingDots = true)
        {
            leaderSize = (textMaxSize > leaderSize) ? textMaxSize : leaderSize;
            if (trailingDots && leaderSize < textMaxSize + 3)
                leaderSize = textMaxSize + 3;

            string name = new string(leaderChar, leaderSize);

            string cutName = text;
            if (text.Length > textMaxSize)
            {
                if (trailingDots && textMaxSize >= 3)
                {
                    cutName = text.Substring(0, textMaxSize - 3);
                    cutName += "...";
                }
                else
                    cutName = text.Substring(0, textMaxSize);
            }
                
            name = name.Remove(0, cutName.Length).Insert(0, cutName);

            return name;
        }

    }

    public class Table
    {
        private List<int> _headerSizes;
        public bool PrintPipes { get; set; }
        public char LeaderChar { get; set; } = '\0';
        public Table(int[] sizes, bool printPipes = false, char leaderChar = '\0')
        {
            _headerSizes = new List<int>();
            sizes.ToList().ForEach(x => _headerSizes.Add(x));
            PrintPipes = printPipes;
            LeaderChar = leaderChar;
        }
        public void PrintHeader(string[] headerNames)
        {
            PrintLine(headerNames);
            WriteLine(new string('-', _headerSizes.Sum() + (PrintPipes ? _headerSizes.Count() + 1 : 0)));
        }
        public void PrintLine(string[] names)
        {
            //int oldCursor = CursorLeft;
            for (int i = 0; i < _headerSizes.Count(); i++)
            {
                if (PrintPipes)
                    Write("|");

                int x = CursorLeft + (PrintPipes ? 1 : 0);
                string name = "";
                if (LeaderChar == '\0')
                {
                    name = (i >= names.Count()) ? "" : names[i];
                    if (name.Length > _headerSizes[i])
                        name = name.Substring(0, _headerSizes[i]);
                }
                else
                    name = PrintHelper.CharLeader(names[i], _headerSizes[i], _headerSizes[i], LeaderChar, false);

                Write(name.PadRight(_headerSizes[i]));
                //CursorLeft = x + _headerSizes[i] - 1;
            }

            if (PrintPipes)
                Write("|");

            WriteLine();
            //CursorLeft = oldCursor;
        }
    }
}
