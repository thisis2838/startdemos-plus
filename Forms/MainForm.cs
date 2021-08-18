using startdemos_ui.Forms;
using startdemos_ui.src;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static startdemos_ui.src.Events;

namespace startdemos_ui
{
    public partial class MainForm : Form
    {
        public static DemoCollectionForm dCF;
        public static MemoryScanningForm mSF;
        public static DemoPlayingForm dPF;
        public static DemoCheckEditor dCE;
        public static DemoListForm dLF;

        public static DemoCollectionHandler dCH;
        public static MemoryScanningHandler mSH;
        public static MemoryMonitoringHandler mMH;

        private AboutForm aboutForm;
        private Thread _msThread;
        public static CancellationTokenSource globalCTS;
        public static SynchronizationContext uiThread;

        private HelpForm _help;

        public static SettingsHandler sH;

        public MainForm()
        {
            globalCTS = new CancellationTokenSource();
            InitializeComponent();
            Load += Form1_Load;
            FormClosing += MainForm_FormClosing;
            FormClosed += MainForm_FormClosed;
            sH = new SettingsHandler();
            _help = new HelpForm();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            sH.WriteSettings();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            globalCTS.Cancel();
            _msThread.Abort();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dCF = new DemoCollectionForm()
            {
                Visible = true,
                TopLevel = false,
                Dock = DockStyle.Fill
            };
            this.tabPage3.Controls.Add(dCF);
            dCF.Show();

            dLF = new DemoListForm();

            mSF = new MemoryScanningForm()
            {
                Visible = true,
                TopLevel = false,
                Dock = DockStyle.Fill
            };
            this.tabPage2.Controls.Add(mSF);
            mSF.Show();

            dPF = new DemoPlayingForm()
            {
                Visible = true,
                TopLevel = false,
                Dock = DockStyle.Fill
            };
            this.tabPage4.Controls.Add(dPF);
            dPF.Show();

            aboutForm = new AboutForm()
            {
                Visible = true,
                TopLevel = false,
                Dock = DockStyle.Fill
            };
            this.tabPage1.Controls.Add(aboutForm);
            aboutForm.Show();

            dCE = new DemoCheckEditor()
            {
                Visible = true,
                TopLevel = false,
                Dock = DockStyle.Fill
            };
            this.tabPage5.Controls.Add(dCE);
            dCE.Show();

            PostLoad();
        }

        private void PostLoad()
        {
            sH.LoadSettings();

            dCH = new DemoCollectionHandler();
            mMH = new MemoryMonitoringHandler();
            _msThread = new Thread(new ThreadStart(() =>
            {
                while (true)
                {
                    mSH = new MemoryScanningHandler();
                    while (true)
                    {
                        if (mSH.Game == null || mSH.Game.HasExited)
                            break;

                        Thread.Sleep(700);
                    }
                    GameHasExited?.Invoke(null, null);
                }

            }));
            _msThread.Start();
        }

        private void butHelp_Click(object sender, EventArgs e)
        {
            _help.tabControl1.SelectedIndex = tabControl1.SelectedIndex;
            _help.Show();
        }
    }
}
