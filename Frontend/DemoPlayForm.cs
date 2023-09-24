using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static startdemos_plus.Program;
using static startdemos_plus.Utils.Utils;
using static startdemos_plus.Globals.Events;
using startdemos_plus.Utils;
using startdemos_plus.Backend;
using static startdemos_plus.Properties.Resources;
using System.Diagnostics;

namespace startdemos_plus.Frontend
{
    public partial class DemoPlayForm : UserControl
    {
        private DemoQueueHandler _queuer = new DemoQueueHandler();
        private string _customMapOrder;

        private bool _queuePlaying = false;
        private bool _queueListHighlightDirty = false;
        private DemoPlayingHandler _player = new DemoPlayingHandler();

        public DemoPlayForm()
        {
            InitializeComponent();

            Enabled = false;

            cmbIndexOrder.SelectedIndexChanged += (s, e) =>
            {
                butIndexOrderConfigure.Visible = butIndexOrderConfigure.Enabled = cmbIndexOrder.SelectedIndex == 3;
            };
            cmbIndexOrder.SelectedIndex = 0;
            cmbTickCountType.SelectedIndex = 0;

            gPlayOptions.EnabledChanged += (s, e) => { panQueueDirectModify.Enabled = gPlayOptions.Enabled; };

            Settings.AddSetting
            (
                "playing-auto_play",
                x => {chkAutoPlayNext.Checked = (bool.TryParse(x, out var a)) ? a : true; },
                () => chkAutoPlayNext.Checked.ToString()
            );
            Settings.AddSetting
            (
                "playing-alt-demo-detect",
                x => { chkAltDemoDetect.Checked = (bool.TryParse(x, out var a)) ? a : false; },
                () => chkAltDemoDetect.Checked.ToString()
            );
            Settings.AddSetting
            (
                "playing-wait_time",
                x => { if (int.TryParse(x, out var a)) nudWaitTime.Value = a; },
                () => nudWaitTime.Value.ToString()
            );
            Settings.AddSetting
            (
                "playing-commands",
                x => { boxCommands.Text = x; },
                () => boxCommands.Text
            );

            Settings.AddSetting
            (
                "playing-order_by",
                x => { cmbIndexOrder.SelectedItem = x; },
                () => cmbIndexOrder.SelectedItem?.ToString() ?? "" 
            );
            Settings.AddSetting
            (
                "playing-reversed",
                x => { if (bool.TryParse(x, out bool a)) chkReverseOrder.Checked = a; },
                () => chkReverseOrder.Checked.ToString()
            );
            Settings.AddSetting
            (
                "playing-custom_map_order",
                x => { _customMapOrder = x; },
                () => _customMapOrder
            );

            Settings.AddSetting
            (
                "playing-queue-onlypassing",
                x => { if (bool.TryParse(x, out var a)) chkQueueOnlyPassing.Checked = a; },
                () => chkQueueOnlyPassing.Checked.ToString()
            );

            LostGameProcess += (s, e) =>
            {
                this.ThreadAction(() =>
                {
                    butPlayPause.Enabled = gTransport.Enabled = false;
                });
            };
            FoundGameProcess += (s, e) =>
            {
                this.ThreadAction(() =>
                {
                    butPlayPause.Enabled = gTransport.Enabled = true;
                });
            };
            GotDemos += (object s, CommonEventArgs e) =>
            {
                this.ThreadAction(() =>
                {
                    nudRangeTo.Value = Demos.Count - 1;
                    Enabled = true;
                    butApplyIndexOrder_Click(null, null);
                });
            };
            SessionStarted += (s, e) => this.ThreadAction(() => UpdateButtons(true));
            SessnionStopped += (s, e) => this.ThreadAction(() => UpdateButtons(false));

            chkQueueOnlyPassing.CheckedChanged += (s, e) => UpdateQueue();
            gFilter.Enabled = gFilter.Visible = false;
            picCurState.Visible = false;
            butPrevious.AddState("1", controls_rewind_000000); butPrevious.SetState("1");
            butNext.AddState("1", controls_fast_forward_000000); butNext.SetState("1");
            butPlayPause.AddState("play", controls_play_000000); butPlayPause.SetState("play");
            butPlayPause.AddState("pause", controls_pause_000000); 
            butStop.AddState("1", controls_stop_000000); butStop.SetState("1");

            void setListQueueHighlight(DemoFile demo)
            {
                listQueue.HighlightRow(
                    listQueue.Rows.Cast<DataGridViewRow>()
                    .First(x => x.Cells[0].Value.ToString() == _queuer.Ordered.IndexOf(demo).ToString()).Index);
                _queueListHighlightDirty = false;
            }
            _player.DemoTick += (object s, CommonEventArgs e) =>
            {
                var demo = ((DemoFile)e["demo"]);
                var time = (int)e["time"];

                this.ThreadAction(() =>
                {
                    progCurDemo.Maximum = demo.MaxIndex;
                    progCurDemo.SetProgressNoAnimation(time > demo.MaxIndex ? demo.MaxIndex : time);

                    if (!_player.DemoLoaded)
                        return;

                    if (_queueListHighlightDirty)
                        setListQueueHighlight(demo);

                    if (_player.Playing)
                    {
                        butPlayPause.SetState("pause");
                        picCurState.Image = playing_000000;
                    }
                    else
                    {
                        butPlayPause.SetState("play");
                        picCurState.Visible = true;
                        picCurState.Image = paused_000000;
                    }

                    labCurrent.Text = demo.Name;
                    labTotalTime.Text = demo.MaxIndex.ToString();

                    labCurTime.Text = (time > demo.MaxIndex ? demo.MaxIndex : time).ToString();
                });
            };
            _player.DemoStopPlaying += (object s, CommonEventArgs e) =>
            {
                this.ThreadAction(() =>
                {
                    progCurDemo.Value = 0;
                    picCurState.Visible = false;

                    if (!_queuePlaying)
                    {
                        this.ThreadAction(() => { ClearLabels(); });
                        return;
                    }

                    labPrevious.Text = e["demo"] == null ? "" : ((DemoFile)e["demo"]).Name;
                    labCurrent.Text = "";
                    labTotalTime.Text = "";
                    labCurTime.Text = "";
                });

            };
            _player.DemoStartPlaying += (object s, CommonEventArgs e) =>
            {
                if (e["demo"] == null)
                    return;

                var demo = (DemoFile)e["demo"];
                int index = _queuer.GetPlayed().IndexOf(demo);
                this.ThreadAction(() =>
                {
                    picCurState.Visible = true;

                    if (!_queuePlaying)
                    {
                        this.ThreadAction(() => { ClearLabels(); });
                        return;
                    }
                    lab1stNext.Text = _queuer.GetPlayed().ElementAtOrDefault(index + 1)?.Name ?? "";
                    lab2ndNext.Text = _queuer.GetPlayed().ElementAtOrDefault(index + 2)?.Name ?? "";

                    setListQueueHighlight(demo);

                });
            };
            _player.DemoQueueFinished += (s, e) =>
            {
                _queuePlaying = false;

                this.ThreadAction(() => 
                {
                    progCurDemo.Value = 0;
                    butNext.Enabled = butPrevious.Enabled = butStop.Enabled = false;
                    gPlayOptions.Enabled = butApplyFilter.Enabled = butApplyIndexOrder.Enabled = true;
                    butPlayPause.SetState("play");
                    listQueue.HighlightRow(-1);
                    ClearLabels();
                });
            };

            listQueue.SelectionChanged += (s, e) => UpdateManualQueueOrderingControls();
            chkQueueOnlyPassing.CheckedChanged += (s, e) => UpdateManualQueueOrderingControls();

            boxPassesChecks.MembersUpdateRequested += boxPassesChecks_MembersUpdateRequested;
            boxFailsChecks.MembersUpdateRequested += boxFailsChecks_MembersUpdateRequested;
            ChecksModified += (s, e) =>
            {
                boxPassesChecks_MembersUpdateRequested(null, null);
                boxFailsChecks_MembersUpdateRequested(null, null);
            };

            butNext.Enabled = butPrevious.Enabled = butStop.Enabled = false;
            ClearLabels();
        }

        private void UpdateButtons(bool enabled)
        {
            butNext.Enabled = butPlayPause.Enabled = butPrevious.Enabled = butStop.Enabled = enabled;
        }

        private void butApplyIndexOrder_Click(object sender, EventArgs e)
        {
            _queuer.Reorder(
                Demos, 
                GetValueFromDescription<DemoOrderType>(cmbIndexOrder.Text),
                chkReverseOrder.Checked,
                _customMapOrder);

            UpdateQueue();

            gFilter.Enabled = gFilter.Visible = true;
        }
        private void UpdateQueue()
        {
            _queueListHighlightDirty = true;
            listQueue.HighlightRow(-1);

            var list = _queuer.Ordered.Select((x, y) => (demo: x, index:y, played: _queuer.Played[y])).ToList();
            if (chkQueueOnlyPassing.Checked)
                list = list.Where(x => x.played).ToList();
            bool sameCount = listQueue.RowCount == list.Count;

            if (!sameCount)
                listQueue.Rows.Clear();

            for (int i = 0; i < list.Count; i++)
            {
                if (sameCount)
                    listQueue.SetText(i, list[i].index, list[i].demo.Name);
                else listQueue.Rows.Add(list[i].index, list[i].demo.Name);

                listQueue.Rows[i].DefaultCellStyle.BackColor = 
                    (list[i].played) ? SystemColors.ControlLightLight : SystemColors.ActiveBorder;
            }
        }

        private void butApplyFilter_Click(object sender, EventArgs e)
        {
            _queuer.Filter((int)nudRangeFrom.Value, (int)nudRangeTo.Value,
                boxTickCountCond.Text, cmbTickCountType.SelectedIndex, chkTickCountCondNot.Checked,
                boxMapCond.Text, chkMapCondNot.Checked,
                boxPassesChecks.CheckedMembers.ToArray(), boxFailsChecks.CheckedMembers.ToArray(),
                Program.Checks);

            UpdateQueue();
        }
        private void butIndexOrderConfigure_Click(object sender, EventArgs e)
        {
            _customMapOrder = new ConfigureCustomMapForm().Open(_customMapOrder);
        }

        private void butPlayPause_Click(object sender, EventArgs e)
        {
            if (!butPlayPause.Enabled || !gTransport.Enabled)
                return;

            if (_queuer.Played.All(x => !x))
            {
                Message("No demos in queue!", MessageType.Warning);
                return;
            }

            if (!_queuePlaying)
            {
                _player.Begin(_queuer.GetPlayed(), (int)nudWaitTime.Value, chkAutoPlayNext.Checked, chkAltDemoDetect.Checked, boxCommands.Text);
                _queuePlaying = true;
                butPlayPause.SetState("pause");
                butNext.Enabled = butPrevious.Enabled = butStop.Enabled = true;

                gPlayOptions.Enabled = butApplyFilter.Enabled = butApplyIndexOrder.Enabled = false;
            }
            else
            {
                if (_player.Playing) _player.Pause();
                else _player.Resume();
            }
            
        }
        private void butStop_Click(object sender, EventArgs e)
        {
            if (!gTransport.Enabled || !butStop.Enabled)
                return;

            _player.Stop();
            _queuePlaying = false;
        }
        private void butNext_Click(object sender, EventArgs e)
        {
            if (!gTransport.Enabled || !butNext.Enabled)
                return;

            _player.Next();
        }
        private void butPrevious_Click(object sender, EventArgs e)
        {
            if (!gTransport.Enabled || !butPrevious.Enabled)
                return;

            _player.Previous();
        }
        private void ClearLabels()
        {
            Label[] labels = new Label[]
            {
                lab2ndNext, lab1stNext, labCurrent, labPrevious,
                labTotalTime, labCurTime
            };

            labels.ToList().ForEach(x => x.Text = "");
        }

        private void ManualQueueOrdering(bool down = true)
        {
            if (listQueue.SelectedCells.Count == 0 || listQueue.Rows.Count == 0 || !panQueueDirectModify.Enabled || _queuePlaying)
                return;

            panQueueDirectModify.Enabled = chkQueueOnlyPassing.Enabled = false;

            var scroll = listQueue.FirstDisplayedScrollingRowIndex;
            var rows = listQueue.Rows.Cast<DataGridViewRow>().Select(x => (
                    index: (int)x.Cells[0].Value, 
                    selected: x.Selected))
                .ToList();
            var reordering = ShiftElements(
                _queuer.Ordered.Count - 1,
                rows.Where(x => x.selected).Select(x => x.Item1).ToList(),
                down);

            _queuer.Ordered.RemapElements(reordering);
            _queuer.Played.RemapElements(reordering);
            UpdateQueue();

            var newSelect = rows.Select(x => x.selected).ToList(); newSelect.RemapElements(reordering);
            listQueue.ClearSelection();
            newSelect.Select((x, y) => (x, y)).Where(x => x.x).ToList().ForEach(x => listQueue.Rows[x.y].Selected = true);

            if (listQueue.Rows.Count > 16)
            {
                var lowest = listQueue.SelectedRows.Cast<DataGridViewRow>().Min(x => x.Index);
                var max = listQueue.SelectedRows.Cast<DataGridViewRow>().Max(x => x.Index);

                if (scroll < max - 15)
                    listQueue.FirstDisplayedScrollingRowIndex = max - 15;
                else if (scroll > lowest)
                    listQueue.FirstDisplayedScrollingRowIndex = lowest;
                else listQueue.FirstDisplayedScrollingRowIndex = scroll;
            }
            chkQueueOnlyPassing.Enabled = true;
            UpdateManualQueueOrderingControls();
        }

        private void ManualQueueExclusion(bool play)
        {
            if (listQueue.SelectedRows.Count == 0 || listQueue.Rows.Count == 0 || !panQueueDirectModify.Enabled || _queuePlaying)
                return;

            var scroll = listQueue.FirstDisplayedScrollingRowIndex;
            var rows = listQueue.SelectedRows.Cast<DataGridViewRow>();
            var index = rows.Select(x => x.Index);
            rows.Select(x => (int)x.Cells[0].Value).ToList().ForEach(x =>
            {
                _queuer.Played[x] = play;
            });
            index.ToList().ForEach(x => listQueue.Rows[x].Selected = true);
            UpdateQueue();

            listQueue.FirstDisplayedScrollingRowIndex = scroll;
            listQueue.ClearSelection();
        }

        private void UpdateManualQueueOrderingControls()
        {
            panQueueDirectModify.Enabled = listQueue.SelectedCells.Count > 0 && !chkQueueOnlyPassing.Checked && !_queuePlaying;
        }

        private void butQueueUp_Click(object sender, EventArgs e)
        {
            ManualQueueOrdering(false);
        }

        private void butQueueDown_Click(object sender, EventArgs e)
        {
            ManualQueueOrdering(true);
        }

        private void butDoPlay_Click(object sender, EventArgs e)
        {
            ManualQueueExclusion(true);
        }

        private void butQueueDontPlay_Click(object sender, EventArgs e)
        {
            ManualQueueExclusion(false);
        }

        private void boxPassesChecks_MembersUpdateRequested(object sender, EventArgs e)
        {
            boxPassesChecks.UpdateMembers(
                Program.Checks.Select(x => x.Name)
                .Where(x => !boxFailsChecks.CheckedMembers.Contains(x)
                ).ToList());
        }

        private void boxFailsChecks_MembersUpdateRequested(object sender, EventArgs e)
        {
            boxFailsChecks.UpdateMembers(
                Program.Checks.Select(x => x.Name)
                .Where(x => !boxPassesChecks.CheckedMembers.Contains(x)
                ).ToList());
        }
    }
}
