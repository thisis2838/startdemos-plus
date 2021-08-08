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
using static startdemos_ui.Utils.ThreadHelper;
using static startdemos_ui.MainForm;
using System.IO;

namespace startdemos_ui.Forms
{
    public partial class DemoListForm : Form
    {
        private int _demoTickCountTotal = 0;
        private int _demoAdjustedTickCountTotal = 0;

        public DemoListForm()
        {
            InitializeComponent();
            FormClosing += DemoListForm_FormClosing;
        }

        private void DemoListForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        public void ClearAll()
        {
            ThreadAction(this, () => 
            { 
                dgvDemoList.Rows.Clear();
                dgvDemoCheckResults.Rows.Clear();
                _demoTickCountTotal = 0;
                _demoAdjustedTickCountTotal = 0;
                labTotalTime.Text = "";
            });
        }

        public void DemoListAdd(int index, DemoFile demo)
        {
            ThreadAction(this, () =>
            {
                _demoTickCountTotal += demo.Info.TotalTicks;
                _demoAdjustedTickCountTotal += demo.Info.AdjustedTicks;

                labTotalTime.Text = $"{TimeSpan.FromSeconds(_demoTickCountTotal * (double)dCF.boxTickRate.Value)} ({_demoTickCountTotal} ticks)";
                labTotalAdjustedTime.Text = $"{TimeSpan.FromSeconds(_demoAdjustedTickCountTotal * (double)dCF.boxTickRate.Value)} ({_demoAdjustedTickCountTotal} ticks)";

                string[] info = new string[8] {
                    index.ToString(),
                    demo.Name,
                    File.GetLastWriteTime(demo.FilePath).ToString(),
                    demo.Info.MapName,
                    demo.Info.TotalTicks.ToString(),
                    demo.Info.AdjustedTicks.ToString(),
                    demo.Info.PlayerName,
                    demo.Info.Events.Count().ToString()
                };

                foreach (DemoCheckResult result in demo.Info.Events)
                {
                    string[] checkInfo = new string[6] {
                        index.ToString(),
                        result.Tick.ToString(),
                        demo.Name,
                        result.Result.ToString(),
                        result.Name,
                        result.EvaluatedValue
                    };

                    dgvDemoCheckResults.Rows.Add(checkInfo);
                }

                dgvDemoList.Rows.Add(info);
            });

        }
    }
}
