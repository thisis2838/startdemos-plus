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
using System.Threading;
using System.Runtime.InteropServices;
using static startdemos_plus.PrintHelper;

namespace startdemos_plus
{
    class Program
    {
        public static SettingsHandler settings = new SettingsHandler();
        public static DemoFileHandler demoFile;
        public static MemoryScanningHandler mScan;
        public static MemoryMonitoringHandler mMonitor;
        public static PlayOrderHandler playOrderHandler;
        public static CancellationTokenSource globalCTS;

        private const int MF_BYCOMMAND = 0x00000000;
        public const int SC_SIZE = 0xF000;

        [DllImport("user32.dll")]
        public static extern int DeleteMenu(IntPtr hMenu, int nPosition, int wFlags);

        [DllImport("user32.dll")]
        private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        [DllImport("kernel32.dll", ExactSpelling = true)]
        private static extern IntPtr GetConsoleWindow();

        static void Main(string[] args)
        {
            IntPtr handle = GetConsoleWindow();
            IntPtr sysMenu = GetSystemMenu(handle, false);

            if (handle != IntPtr.Zero)
                DeleteMenu(sysMenu, SC_SIZE, MF_BYCOMMAND);

            PrintSeperator("ABOUT");
            WriteLine("startdemos+ by 2838");
            WriteLine("Idea by donaldinio");
            WriteLine("August 2021");
            WriteLine("Please report any issues or bugs to https://github.com/thisis2838/startdemos-plus/issues!");

            if (File.Exists("config.xml"))
                settings.ReadSettings();
            else settings.FirstTimeSettings();
            WriteLine("You can edit these in the config.xml file next to the .exe file");

            globalCTS = new CancellationTokenSource();
            mScan = new MemoryScanningHandler();

            Thread detectGameExit = new Thread(new ThreadStart(() =>
            {
                while (true)
                {
                    if (mScan.Game.HasExited || 
                    Process.GetProcessesByName(Path.GetFileNameWithoutExtension(settings.GameExe)).Count() == 0)
                    {
                        globalCTS.Cancel();
                        Thread.Sleep(30);
                        Environment.Exit(0);
                        return;
                    }
                    Thread.Sleep(2000);
                }
            }));
            detectGameExit.Start();

            change:
            demoFile = new DemoFileHandler();
            
            replay:
            playOrderHandler = new PlayOrderHandler();

            PrintSeperator("COMMANDS TO EXECUTE");
            WriteLine("Commands to execute per demo start?");
            settings.PerDemoCommands = ReadLine();

            PrintSeperator("DEMO CONTROLS");
            WriteLine("[x] To skip playing all demos");
            WriteLine("[s] To skip current demo");
            WriteLine("WARNING: Do not enter these while the demo is loading in, as that might cause a game crash");
            WriteLine("Press Enter to begin");
            ReadLine();

            mMonitor = new MemoryMonitoringHandler();
            mMonitor.Monitor();

            PrintSeperator("FINISH");
            WriteLine("[0] To replay the demos");
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
            detectGameExit.Abort();
        }
    }
}
