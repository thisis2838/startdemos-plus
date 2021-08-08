using startdemos_ui.src;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static startdemos_ui.src.Events;
using static startdemos_ui.Utils.ThreadHelper;
using static startdemos_ui.MainForm;

namespace startdemos_ui.Forms
{
    public partial class MemoryScanningForm : Form
    {
        public MemoryScanningForm()
        {
            InitializeComponent();
            GameHasExited += ResetLabels;
            GameHasBeenFound += FillInformation;
            Load += MemoryScanningForm_Load;
            Disposed += MemoryScanningForm_Disposed;
        }

        private void MemoryScanningForm_Load(object sender, EventArgs e)
        {
            sH.SubscribedSettings.Add(new SettingEntry(
                "gameexe",
                (s) => { boxGameExe.Text = s; },
                () => { return boxGameExe.Text; }));
        }

        private void FillInformation(object sender, GameDiscoveryArgs args)
        {
            ThreadAction(this, () =>
            {
                labDemoPlayerPtr.Text = ReportPointer(args.DemoPlayerPtr);
                labHostTickPtr.Text = ReportPointer(args.HostTickCountPtr);

                labDemoIsPlayingOff.Text = $"is 0x{args.DemoIsPlayingOffset:X}";
                labDemoStartTickOff.Text = $"is 0x{args.DemoStartTickOffset:X}";
            });
        }

        private void MemoryScanningForm_Disposed(object sender, EventArgs e)
        {
            GameHasExited -= ResetLabels;
        }

        public string GameExe
        {
            get
            {
                return boxGameExe.Text;
            }
            set
            {
                boxGameExe.Text = value;
            }
        }

        public void SetStatus(string status)
        {
            ThreadAction(this, () => { labStatus.Text = status; });
        }

        public void ResetLabels(object sender, EventArgs e)
        {
            ThreadAction(this, () =>
            {
                labDemoPlayerPtr.Text = "";
                labHostTickPtr.Text = "";
                labDemoStartTickOff.Text = "";
                labDemoIsPlayingOff.Text = "";
            });
        }


        private string ReportPointer(IntPtr pointer)
        {
            if (pointer != IntPtr.Zero)
                return ("Found at 0x" + pointer.ToString("X"));
            else
                return ("Couldn't be found");
        }
    }
}
