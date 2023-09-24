using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using startdemos_plus.Backend;
using startdemos_plus.Frontend;
using startdemos_plus.Utils;
using startdemos_plus.Backend.DemoChecking;
using System.Diagnostics;
using System.Net;
using System.Threading;

namespace startdemos_plus
{
    public static class Program
    {
        public static SettingsHandler Settings = new SettingsHandler();
        public static GameWorker Worker = new GameWorker();

        public static List<DemoFile> Demos = new List<DemoFile>();
        public static List<DemoCheck> Checks = new List<DemoCheck>();

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (!Test())
            {
                new Thread(new ThreadStart(() => CheckNewVersion())).Start();

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm());
            }
        }

        private static void CheckNewVersion()
        {
            using (var web = new WebClient())
            {
                try
                {
                    string ver = web.DownloadString(@"https://raw.githubusercontent.com/thisis2838/startdemos-plus/main/current_version.txt");
                    ver = ver.Trim('\r', '\n', '\t', ' ');
                    Version cur = typeof(Program).Assembly.GetName().Version;
                    if (Version.Parse(ver) > cur)
                    {
                        if 
                        (
                            MessageBox.Show
                            (
                                $"A new update is available (new {ver}, cur: {cur})\r\nWould you like to open the download page?", 
                                "Update Available", 
                                MessageBoxButtons.YesNo
                            ) 
                            == DialogResult.Yes
                        )
                            Process.Start(@"https://github.com/thisis2838/startdemos-plus/releases");
                    }
                }
                catch { }
            }
        }

        private static bool Test()
        {

            return false;

            //
            var elem = new List<int>() { 0, 1, 2, 3 };
            elem.RemapElements(Utils.Helpers.ShiftElements(elem.Count - 1, new List<int>() { 0,2 }, false));


            return true;   
            //*/
        }
    }
}
