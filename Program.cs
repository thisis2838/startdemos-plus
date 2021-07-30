using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using static System.Console;
using LiveSplit.ComponentUtil;

namespace startdemos_plus
{
    class Program
    {
        public static string DemoPath { get; set; }
        public static string GameExe { get; set; }
        public static string GameDir { get; set; }

        private static SettingsHandler settings = new SettingsHandler();
        public static FileHandler FileHandler;

        static void Main(string[] args)
        {
            PrintSeperator("ABOUT");
            WriteLine("startdemos+ by 2838");
            WriteLine("July 2021");

            if (File.Exists("config.xml"))
                settings.ReadSettings();
            else settings.FirstTimeSettings();

            MemoryHandler handler = new MemoryHandler();
            change:
            FileHandler = new FileHandler();
            WriteLine("[x] To skip playing all demos");
            WriteLine("[s] To skip current demo");
            WriteLine("Press Enter to begin playing demos");
            ReadLine();
            replay:
            handler.Monitor();

            PrintSeperator("FINISH");
            WriteLine("[0] To replay all demos");
            WriteLine("[1] To choose another directory");
            WriteLine("[2] To quit");

            int.TryParse(ReadLine(), out int option);
            switch (option)
            {
                case 0:
                    goto replay;
                case 1:
                    goto change;
            }
        }

        public static void PrintSeperator(string name = "")
        {
            WriteLine();
            WriteLine($"------[{name}]------");
        }
    }
}
