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
using Ookii.Dialogs.WinForms;
using static startdemos_ui.MainForm;
using static startdemos_ui.src.Events;
using static startdemos_ui.Utils.ThreadHelper;
using System.IO;
using startdemos_ui.src;

namespace startdemos_ui.Forms
{
    public partial class DemoCollectionForm : Form
    {
        public DemoCollectionForm()
        {
            InitializeComponent();
            Load += DemoCollectionForm_Load;
            StartedPlaying += (s, e) => { butProcess.Enabled = false; };
            StoppedPlaying += (s, e) => { butProcess.Enabled = true; };
        }

        private void DemoCollectionForm_Load(object sender, EventArgs e)
        {
            sH.SubscribedSettings.Add(new SettingEntry(
                "tickrate", 
                (s) => { boxTickRate.Value = decimal.Parse(s == "" ? "0.001" : s); }, 
                () => { return boxTickRate.Value.ToString("0.0000000"); }));
            sH.SubscribedSettings.Add(new SettingEntry(
                "zerothtick", 
                (s) => { chk0thTick.Checked = bool.Parse(s == "" ? "False" : s); }, 
                () => { return chk0thTick.Checked.ToString(); }));

            butProcess.Enabled = false;
        }

        public void SetCurDemoInfo(int index, string name)
        {
            ThreadAction(this, () =>
            {
                labProcessedDemosCount.Text = (index + 1).ToString();
                labProcessing.Text = name;
            });
        }

        private void butDemoPathBrowse_Click(object sender, EventArgs e)
        {
            VistaFolderBrowserDialog dialog = new VistaFolderBrowserDialog();
            DialogResult result = dialog.ShowDialog();

            if (result == DialogResult.OK)
                boxDemoPath.Text = dialog.SelectedPath;
        }

        private void butProcess_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(boxDemoPath.Text))
            {
                Task d = Task.Factory.StartNew(() => 
                {
                    dCH.Scan(boxDemoPath.Text);
                    ThreadAction(this, () => { butOpenDemoList_Click(null, null); });
                });
            }
        }

        private void butOpenDemoList_Click(object sender, EventArgs e)
        {
            dLF.tabControl1.SelectedIndex = 0;
            dLF.Show();
        }

        private void boxDemoPath_TextChanged(object sender, EventArgs e)
        {
            butProcess.Enabled = Directory.Exists(boxDemoPath.Text);
        }
    }
}
