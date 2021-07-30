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
        public static float TickRate { get; set; } = 0.015f;
        public static int WaitTime { get; set; } = 50;

        private static SettingsHandler settings = new SettingsHandler();
        public static FileHandler FileHandler;

        static void Main(string[] args)
        {
            PrintSeperator("ABOUT");
            WriteLine("startdemos+ by 2838");
            WriteLine("Idea by donaldinio");
            WriteLine("July 2021");
            WriteLine("Please report any issues or bugs to https://github.com/thisis2838/startdemos-plus/issues!");

            if (File.Exists("config.xml"))
                settings.ReadSettings();
            else settings.FirstTimeSettings();
            WriteLine("You can edit these in the config.xml file next to the .exe file");

            MemoryHandler handler = new MemoryHandler();
            change:
            FileHandler = new FileHandler();
            PrintSeperator("CONTROLS");
            WriteLine("[x] To skip playing all demos");
            WriteLine("[s] To skip current demo");
            WriteLine();
            WriteLine("Type in the demo index corresponding with the demo you wish to start playing from (0 is first demo)");
            int startIndex = int.Parse(ReadLine());
            replay:
            handler.Monitor(startIndex);

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
