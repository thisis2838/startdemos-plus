using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static startdemos_plus.Utils.Utils;
using System.Windows.Forms;

namespace startdemos_plus.Frontend
{
    public partial class DemoCheckResultDetailsForm : Form
    {
        public DemoCheckResultDetailsForm()
        {
            InitializeComponent();
        }

        private List<string> _demos = new List<string>();
        private List<List<(string, string, string)>> _info = new List<List<(string, string, string)>>();

        public void Show(UIDemoCheckResultInfo infos)
        {
            labCheckName.Text = infos.Check.Name;
            labSeenStats.Text =
                $"Check contains {infos.Check.Conditions.Count} condition(s), passing " +
                $"{infos.Results.Sum(x => x.Passed.Count)} time(s) " +
                $"in {infos.Results.Select(x => x.Demo.FilePath).Distinct().Count()} demo(s). ";

            infos.Results.ForEach(x =>
            {
                _demos.Add(x.Demo.Name);
                listDemos.Rows.Add(x.Demo.Name);
                _info.Add(x.Passed.Select(y => 
                ((y.Tick?.Index ?? -1).ToString(), 
                y.Condition.Variable.GetDescription(),
                y.GetValueString())).ToList());
            });

            listDemos.SelectionChanged += (s, e) =>
            {
                listPassingInfo.Rows.Clear();
                if (listDemos.SelectedRows.Count > 0)
                {
                    _info[listDemos.SelectedRows[0].Index].ToList().ForEach(x =>
                    {
                        listPassingInfo.Rows.Add(x.Item1, x.Item2, x.Item3);
                    });
                }
            };

            listDemos.ClearSelection();

            this.ShowDialog();
        }
    }
}
