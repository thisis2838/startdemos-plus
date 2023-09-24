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
using startdemos_plus.Backend.DemoChecking;
using startdemos_plus.Backend;
using static startdemos_plus.Backend.DemoChecking.DemoCheckActionType;
using static startdemos_plus.Utils.Helpers;
using startdemos_plus.Utils;
using static startdemos_plus.Frontend.MainForm;
using System.IO;
using startdemos_plus.Frontend;
using static startdemos_plus.Globals.Events;

namespace startdemos_plus.Frontend
{
    public partial class DemoCollectionForm : UserControl
    {
        private List<UIDemoCheckResultInfo> _results = new List<UIDemoCheckResultInfo>();
        
        public DemoCollectionForm()
        {
            InitializeComponent();

            Settings.AddSetting
            (
                "collection-tick_rate",
                x => { nudTickRate.Value = decimal.TryParse(x, out decimal a) ? a : 0.015m; },
                () => nudTickRate.Value.ToString()
            );
            Settings.AddSetting
            (
                "collection-0th-tick",
                x => { chk0thTick.Checked = bool.TryParse(x, out bool a) ? a : true; },
                () => chk0thTick.Checked.ToString()
            );

            this.listDemoEvents.CellClick += ListDemoEvents_CellClick;

            Globals.Events.DemoQueueStarted += (s, e) =>
            {
                this.ThreadAction(() => { butOpenFiles.Enabled = false; });
            };

            Globals.Events.DemoQueueFinished += (s, e) =>
            {
                this.ThreadAction(() => { butOpenFiles.Enabled = true; });
            };

            ChecksModified += (s, e) =>
            {
                if (listDemos.Rows.Count > 0)
                    listDemoEvents.Visible = false;
            };
        }

        private void butOpenFiles_Click(object sender, EventArgs e)
        {
            OpenFileDialog diag = new OpenFileDialog();
            diag.Multiselect = true;
            diag.Filter = "Demo Files (*.dem)|*.dem";

            if (diag.ShowDialog() == DialogResult.OK)
                EnumerateDemos(diag.FileNames.ToList());
        }

        private void EnumerateDemos(List<string> filePaths)
        {
            var paths = filePaths.ConvertAll(x => Path.GetDirectoryName(x)).Distinct();
            if (paths.Count() == 1)
                boxFilePath.Text = paths.ElementAt(0);
            else boxFilePath.Text = $"{paths} paths";

            Demos.Clear();

            ProgressWindow prog = new ProgressWindow();
            prog.Open("Parsing demos...");

            filePaths.ForEach(x =>
            {
                prog.Update(filePaths.IndexOf(x) + 1, filePaths.Count, Path.GetFileName(x));
                DemoFile e = new DemoFile(x);
                Demos.Add(e);
            });
            prog.Exit();

            GotDemos.Invoke(this, new CommonEventArgs("demos", Demos));

            FillInfo();
        }

        private void FillInfo()
        {
            listDemos.Rows.Clear();
            listDemoEvents.Visible = true;

            if (Demos.Count == 0)
                return;

            ProgressWindow prog = new ProgressWindow();
            prog.Open("Running checks on demos...");

            Dictionary<string, int> _specialTimePairs = new Dictionary<string, int>();
            var checkResults =
                Demos.ConvertAll(x => Program.Checks?.ConvertAll(y => 
                {
                    prog.Update(
                        Demos.IndexOf(x) + 1,
                        Demos.Count, 
                        $"{x.Name} : {y.Name}");

                    return y.Check(x) ?? null;
                })
                .Where(y => y?.PassedAll ?? false));

            prog.Exit();

            foreach (var result in checkResults)
            {
                if (result == null || result.Count() == 0)
                    continue;

                var e = result.FirstOrDefault(x => x.GetActions(StartDemoTime, EndDemoTime).Count > 0);
                if (e != null)
                    _specialTimePairs.Add(e.Demo.FilePath, e.Demo.GetMeasuredTicks(e));
            }

            int demos = 0, totalTicks = 0, measuredTicks = 0;
            prog = new ProgressWindow();
            prog.Open("Adding demos to list...");
            for (int i = 0; i < Demos.Count; i++)
            {
                var x = Demos[i];

                int measured = _specialTimePairs.ContainsKey(x.FilePath) ? _specialTimePairs[x.FilePath] : x.TotalTicks;
                listDemos.Rows.Add
                (
                    x.Name,
                    x.MapName,
                    x.TotalTicks,
                    measured
                );

                labDemoCount.Text = (++demos).ToString();
                labTotalTime.Text = TimeSpan.FromSeconds((totalTicks += x.TotalTicks) * (double)nudTickRate.Value).ToString();
                labMeasuredTime.Text = TimeSpan.FromSeconds((measuredTicks += measured) * (double)nudTickRate.Value).ToString();

                prog.Update(i + 1, Demos.Count, x.Name);
            }
            prog.Exit();

            listDemoEvents.Rows.Clear();
            _results.Clear();
            var checks = checkResults.SelectMany(x => x).GroupBy(x => x.Check).ToList();

            for (int i = 0; i < checks.Count(); i++)
            {
                var check = checks[i];

                _results.Add(new UIDemoCheckResultInfo()
                {
                    Check = check.Key,
                    Results = check.ToList()
                });

                string name = check.Key.Name;
                int times = check.SelectMany(x => x.Passed).Where(x => !x.Condition.IsDemoCondition).Count();
                string seen = $"{check.Select(x => x.Demo.FilePath).Distinct().Count()} demo(s)";

                listDemoEvents.Rows.Add(name, times, seen);
            }
        }

        private void ListDemoEvents_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex != listDemoEvents.Columns.Count - 1 || e.RowIndex == -1)
                return;

            var result = _results[e.RowIndex];
            new DemoCheckResultDetailsForm().Show(result);
        }

        private void butRefresh_Click(object sender, EventArgs e)
        {
            if (Demos.Count == 0) 
                return;

            FillInfo();
        }

        private void chk0thTick_CheckedChanged(object sender, EventArgs e)
        {
            Globals.Values.ZerothTick = chk0thTick.Checked;
        }
    }

    public struct UIDemoCheckResultInfo
    {
        public DemoCheck Check;
        public List<DemoCheckResult> Results;
    }
}
