using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static startdemos_ui.Utils.ThreadHelper;
using static startdemos_ui.src.Events;
using static startdemos_ui.MainForm;
using startdemos_ui.src;
using System.Threading;

namespace startdemos_ui.Forms
{
    public partial class DemoPlayingForm : Form
    {
        private Thread _playThread;
        public DemoPlayingForm()
        {
            InitializeComponent();
            Load += DemoPlayingForm_Load;
            GameHasBeenFound += (s, e) =>
            {
                if (dCH?.Files?.Count() >> 0 > 0)
                    GotDemos(null, null);
            };
            GotDemos += (s, e) => 
            {
                SetTransportControlsState(false);
                SetTransportControlsState(true, false);
            };
            GameHasExited += (s, e) => { SetTransportControlsState(true); butPlayStop.Text = "Play"; };
        }

        private void DemoPlayingForm_Load(object sender, EventArgs e)
        {
            SetTransportControlsState(true);

            sH.SubscribedSettings.Add(new SettingEntry(
                "playorder",
                s => boxPlayOrder.Text = s,
                () => boxPlayOrder.Text));
            sH.SubscribedSettings.Add(new SettingEntry(
                "waittime",
                s => boxWaitTime.Value = decimal.Parse(s == "" ? "50" : s),
                () => boxWaitTime.Value.ToString()));
            sH.SubscribedSettings.Add(new SettingEntry(
                "perdemocommands",
                s => boxCommands.Text = s,
                () => boxCommands.Text));
            sH.SubscribedSettings.Add(new SettingEntry(
                "autoplaynext",
                s => chkAutoNext.Checked = bool.Parse(s == "" ? "True" : s),
                () => chkAutoNext.Checked.ToString()));
        }

        public void UpdateCurrentPlayInfo(int curIndex, int total, string current, string previous, string next)
        {
            ThreadAction(this, () =>
            {
                labCount.Text = $"{curIndex}/{total}";
                labCurDemo.Text = current;
                labPrevDemo.Text = previous;
                labNextDemo.Text = next;
            });
        }

        public void ResetCurrentPlayInfo()
        {
            ThreadAction(this, () =>
            {
                labCount.Text = "";
                labCurDemo.Text = "";
                labPrevDemo.Text = "";
                labNextDemo.Text = "";
                SetTransportControlsState(true, false);
                gPlayOptions.Enabled = true;
                butPlayStop.Text = "Play";
                StoppedPlaying?.Invoke(null, null);
            });
        }

        public void SetTransportControlsState(bool disabled, bool affectPlayStop = true)
        {
            ThreadAction(this, () =>
            {
                butPlayPrev.Enabled = !disabled;
                butPlayNext.Enabled = !disabled;
                if (affectPlayStop)
                {
                    butPlayStop.Enabled = !disabled;
                }
            });
        }

        public void UpdatePlayOrder(List<OrderInfo> info)
        {
            string d = "";
            info.ForEach(x => d += $"{x}, ");
            d.TrimEnd(',');

            if (string.IsNullOrWhiteSpace(d))
                d = "(empty)";

            ThreadAction(this, () => { boxEvalDemoOrder.Text = d; });
        }

        private void butPlayStop_Click(object sender, EventArgs e)
        {
            if ((mSH?.Game ?? null) == null || mSH.Game.HasExited)
            {
                MessageBox.Show(
                    $"Game not running or Game Process not found!\nPlease check the Memory Scanning tab!", 
                    "Demo Checks | Start Demo Queue", 
                    MessageBoxButtons.OK);
                return;
            }

            if (_playThread != null && _playThread.IsAlive)
            {
                mMH.Action = PlayAction.Stop;
                StoppedPlaying?.Invoke(null, null);
                return;
            }

            _playThread = new Thread(new ThreadStart(() => 
            {
                mMH.BeginPlaying(
                    boxPlayOrder.Text,
                    boxCommands.Text,
                    (int)boxWaitTime.Value,
                    chkAutoNext.Checked);
                dPF.ResetCurrentPlayInfo();
            }));

            _playThread.Start();
            SetTransportControlsState(false, false);
            gPlayOptions.Enabled = false;
            butPlayStop.Text = "Stop";
            StartedPlaying?.Invoke(null, null);
        }

        private void butPlayPrev_Click(object sender, EventArgs e)
        {
            mMH.Action = PlayAction.Previous;
        }

        private void butPlayNext_Click(object sender, EventArgs e)
        {
            mMH.Action = PlayAction.Next;
        }
    }
}
