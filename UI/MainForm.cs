using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using startdemos_plus.Help;

namespace startdemos_plus.UI
{
    public partial class MainForm : Form
    {
        public static GameHookForm FormGameHook = new GameHookForm();
        public static DemoCollectionForm FormDemoCollection = new DemoCollectionForm();
        public static DemoPlayForm FormDempPlay = new DemoPlayForm();
        public static DemoCheckForm FormDemoCheck = new DemoCheckForm();
        public static AboutForm FormAbout = new AboutForm();

        private static MainHelpForm _help = new MainHelpForm();

        public MainForm()
        {
            InitializeComponent();

            this.tabPgGameHook.Controls.Add(FormGameHook);
            this.tabPgDemoCollect.Controls.Add(FormDemoCollection);
            this.tabPgDemoPlayOrder.Controls.Add(FormDempPlay);
            this.tabPgDemoChecks.Controls.Add(FormDemoCheck);
            this.tabPgAbout.Controls.Add(FormAbout);

            Program.Settings.LoadSettings();

            this.tabCtrlMaster.Selected += (Object sender, TabControlEventArgs e) =>
            {
                TabPage current = e.TabPage;
                if (!current.Contains(FormDemoCheck))
                    FormDemoCheck.CheckDirty();
            };

            this.FormClosing += (s, e) =>
            {
                Program.Settings.WriteSettings();
                FormDemoCheck.Exit();
                Program.Worker.Stop();
            };

            Globals.Events.LostGameProcess.Invoke(null, null);

            Program.Worker.Start();
        }

        private void butHelp_Click(object sender, EventArgs e)
        {
            _help.Show();
        }
    }
}
