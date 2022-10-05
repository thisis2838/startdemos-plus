using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using startdemos_plus.Src;
using startdemos_plus.UI;
using startdemos_plus.Utils;
using startdemos_plus.Src.DemoChecking;

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
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm());
            }
        }

        private static bool Test()
        {

            return false;

            //
            var elem = new List<int>() { 0, 1, 2, 3 };
            elem.RemapElements(Utils.Utils.ShiftElements(elem.Count - 1, new List<int>() { 0,2 }, false));


            return true;   
            //*/
        }
    }
}
