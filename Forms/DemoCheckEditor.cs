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
using System.Xml;
using System.IO;
using static startdemos_ui.Utils.ThreadHelper;
using static startdemos_ui.MainForm;

namespace startdemos_ui.Forms
{
    public partial class DemoCheckEditor : Form
    {
        private int _curConfigIndex
        {
            get
            {
                return (_configs?.Count ?? 0) == 0 ? 
                    -1 : 
                    GetValue(this, cbDCEChecksListGames, x => x?.SelectedIndex ?? -1);
            }
        }
        private int _curEvalIndex 
        {
            get
            {
                return GetValue(this, dgvDCEChecksListGrid,
                    x => (dgvDCEChecksListGrid.SelectedCells?.Count ?? -1) <= 0 ? 
                    -1 : 
                    dgvDCEChecksListGrid.SelectedCells[0].RowIndex);
            }
        }

        private List<DemoCheckHandler> _configs;
        private Evaluation _curEval => _configs?[_curConfigIndex].Evaluations?[_curEvalIndex] ?? null;
        private DemoCheckHandler _curDemoChecks => (_curConfigIndex == -1 || _configs?.Count() == 0) ? null : _configs[_curConfigIndex];

        public DemoCheckHandler GetCurrentDemoChecks()
        {
            // copy the current one for safety
            return new DemoCheckHandler(_curDemoChecks);
        }

        private Timer _errorTimer;

        private void LoadSettings()
        {
            if (!File.Exists("checks.xml"))
                return;

            XmlDocument doc = new XmlDocument();
            doc.Load("checks.xml");

            _configs = new List<DemoCheckHandler>();

            var s = doc.SelectSingleNode("games").ChildNodes[0];
            while (s != null)
            {
                _configs.Add(new DemoCheckHandler(s));
                s = s.NextSibling;
            }

            WriteNoteLabel("Loaded settings from checks.xml!");

            RefreshConfigDropDown();
        }

        public DemoCheckEditor()
        {
            InitializeComponent();

            LoadSettings();
            dgvDCEChecksListGrid.SelectionChanged += DgvDCEChecksListGrid_SelectionChanged;

            if (_configs.Count > 0)
                cbDCEChecksListGames.SelectedIndex = 0;

            _errorTimer = new Timer();
            _errorTimer.Interval = 3000;
            _errorTimer.Tick += (sender, e) =>
            {
                labError.Visible = false;
                _errorTimer.Stop();
            };
        }

        private void WriteNoteLabel(string note)
        {
            if (_errorTimer != null)
            {
                _errorTimer.Stop();
                labError.Text = note;
                labError.Visible = true;
                _errorTimer.Start();
            }
        }

        private void DgvDCEChecksListGrid_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvDCEChecksListGrid.SelectedCells.Count <= 0)
                return;

            FillInformation();
        }

        private void FillInformation()
        {
            if (_configs[_curConfigIndex].Evaluations.Count == 0)
                return;

            boxDCEConditionsTargetVariable.Text = _curEval.VarToString();
            boxDCEConditionsMap.Text = _curEval.Map;
            chkDCEConditionsNot.Checked = _curEval.Not;
            boxDCEConditionsTickCompare.Text = _curEval.TickComparison.ToString();
            boxDCEConditionsName.Text = _curEval.EventName;

            ComboBoxEvaluateInput(ref cbDCEEvaluationDataType, _curEval.Type.ToString());
            ComboBoxEvaluateInput(ref cbDCEEvaluationDataDirective, _curEval.Directive.ToString());
            ComboBoxEvaluateInput(ref cbDCEReturnType, _curEval.ResType.ToString());


            if (dgvDCEChecksListGrid.RowCount > _curEvalIndex)
                dgvDCEChecksListGrid.Rows[_curEvalIndex].Cells[0].Value = boxDCEConditionsName.Text;
        }

        private void EmptyInformation()
        {
            ComboBoxEvaluateInput(ref cbDCEEvaluationDataType, "");
            ComboBoxEvaluateInput(ref cbDCEEvaluationDataDirective, "");
            ComboBoxEvaluateInput(ref cbDCEReturnType, "");

            boxDCEConditionsTargetVariable.Text = "";
            boxDCEConditionsMap.Text = "";
            chkDCEConditionsNot.Checked = false;
            boxDCEConditionsTickCompare.Text = "";
            boxDCEConditionsName.Text = "";
        }

        private void ComboBoxEvaluateInput<T>(ref ComboBox box, T input)
        {
            box.SelectedIndex = (input != null && input.ToString() != "")
                ? box.Items.IndexOf(input) : -1;
        }

        private void DemoCheckEditor_Load(object sender, EventArgs e)
        {

        }

        private void RefreshConfigDropDown()
        {
            cbDCEChecksListGames.Items.Clear();
            cbDCEChecksListGames.Text = "";
            cbDCEChecksListGames.Items.AddRange(_configs.Select(x => x.Name).ToArray());

            if (cbDCEChecksListGames.Items.Count > 0)
                cbDCEChecksListGames.SelectedIndex = 0;
        }

        private void cbDCEChecksListGames_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_configs.Count == 0)
                return;

            List<Evaluation> evals = _configs[cbDCEChecksListGames.SelectedIndex].Evaluations;
            dgvDCEChecksListGrid.Rows.Clear();
            evals.ForEach(x => dgvDCEChecksListGrid.Rows.Add(x.EventName));
        }

        private void butDCEChecksListGameAdd_Click(object sender, EventArgs e)
        {
            if (_configs == null)
                _configs = new List<DemoCheckHandler>();

            var form = new DemoCheckEditorAddConfigForm(_configs?.Select(x => x.Name)?.ToArray() ?? new string[0]);

            if (form.Result != "")
            {
                _configs.Add(new DemoCheckHandler(form.Result));
                RefreshConfigDropDown();
                cbDCEChecksListGames.SelectedIndex = cbDCEChecksListGames.Items.Count - 1;
            }
        }

        private void butDCEChecksListGameRemove_Click(object sender, EventArgs e)
        {
            if (_configs.Count == 0)
                return;

            if (_curConfigIndex < 0)
                return;

            string name = _configs[_curConfigIndex].Name;
            DialogResult result = MessageBox.Show($"Are you sure you want to delete {name} from the demo checks?", "Demo Checks | Remove Game", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                _configs.RemoveAt(_curConfigIndex);
                RefreshConfigDropDown();
                if (_configs.Count > 0)
                    cbDCEChecksListGames.SelectedIndex = cbDCEChecksListGames.Items.Count - 1;
                else
                {
                    dgvDCEChecksListGrid.Rows.Clear();
                    EmptyInformation();
                }
            }
        }

        private void butDCEChecksListAdd_Click(object sender, EventArgs e)
        {
            if ( _curConfigIndex == -1)
                return;

            _configs[_curConfigIndex].Evaluations.Add(new Evaluation(EvaluationDataType.None, EvaluationDirective.None));

            dgvDCEChecksListGrid.Rows.Add("");
            dgvDCEChecksListGrid.ClearSelection();
            dgvDCEChecksListGrid.Rows[dgvDCEChecksListGrid.Rows.Count - 1].Selected = true;
        }

        private void butDCEChecksListRemove_Click(object sender, EventArgs e)
        {
            if (_curEvalIndex == -1 || _curConfigIndex == -1)
                return;

            if (dgvDCEChecksListGrid.SelectedCells.Count == 0 
                || _configs[_curConfigIndex].Evaluations.Count == 0)
                return;

            _configs[_curConfigIndex].Evaluations.RemoveAt(_curEvalIndex);
            dgvDCEChecksListGrid.Rows.RemoveAt(_curEvalIndex);

            if (dgvDCEChecksListGrid.Rows.Count == 0)
                EmptyInformation();
        }

        private bool IsCurEvalValid()
        {
            return !(_curEvalIndex == -1 || _curConfigIndex == -1 || _curEval == null);
        }

        private void cbDCEEvaluationDataType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!IsCurEvalValid())
                return;

            try
            {
                _curEval.ParseType((EvaluationDataType)Enum.Parse(typeof(EvaluationDataType), cbDCEEvaluationDataType.Text));
                boxDCEConditionsTargetVariable.Text = _curEval.VarToString();
            }
            catch
            {
                WriteNoteLabel("Incorrect Target Variable format!");
            }
        }

        private void cbDCEEvaluationDataDirective_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!IsCurEvalValid())
                return;

            try
            {
                _curEval.ParseDirective((EvaluationDirective)Enum.Parse(typeof(EvaluationDirective), cbDCEEvaluationDataDirective.Text));
                boxDCEConditionsTargetVariable.Text = _curEval.VarToString();
            }
            catch
            {
                WriteNoteLabel("Incorrect Target Variable format!");
            }
        }

        private void cbDCEReturnType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!IsCurEvalValid())
                return;

            _curEval.ResType = (ResultType)Enum.Parse(typeof(ResultType), cbDCEReturnType.Text);
        }

        private void boxDCEConditionsTargetVariable_TextChanged(object sender, EventArgs e)
        {
            if (!IsCurEvalValid() || !boxDCEConditionsTargetVariable.Modified)
                return;

            try
            {
                _curEval.ParseType((EvaluationDataType)Enum.Parse(typeof(EvaluationDataType), cbDCEEvaluationDataType.Text));
                _curEval.ParseDirective((EvaluationDirective)Enum.Parse(typeof(EvaluationDirective), cbDCEEvaluationDataDirective.Text));
                _curEval.ParseVar(boxDCEConditionsTargetVariable.Text);
                WriteNoteLabel("");
            }
            catch 
            {
                WriteNoteLabel("Incorrect Target Variable format! Culprit variable will be deleted!");
            }
        }

        private void boxDCEConditionsMap_TextChanged(object sender, EventArgs e)
        {
            if (!IsCurEvalValid())
                return;

            _curEval.Map = boxDCEConditionsMap.Text;
        }

        private void boxDCEConditionsTickCompare_TextChanged(object sender, EventArgs e)
        {
            if (!IsCurEvalValid())
                return;
            try
            {
                _curEval.TickComparison = new Comparisons(boxDCEConditionsTickCompare.Text);
                WriteNoteLabel("");
            }
            catch 
            {
                WriteNoteLabel("Incorrect Tick Comparison format! Setting will not be saved!");
            }
        }

        private void chkDCEConditionsNot_CheckedChanged(object sender, EventArgs e)
        {
            if (!IsCurEvalValid())
                return;

            _curEval.Not = chkDCEConditionsNot.Checked;
        }

        private char[] _disallowedConditionNameChars = { ',', '&', '/' };

        private void boxDCEConditionsName_TextChanged(object sender, EventArgs e)
        {
            labNameNote.Visible = _disallowedConditionNameChars.Any(x => boxDCEConditionsName.Text.Contains(x));

            if (!IsCurEvalValid())
                return;

            _curEval.EventName = boxDCEConditionsName.Text;
            if (dgvDCEChecksListGrid.RowCount > _curEvalIndex)
                dgvDCEChecksListGrid.Rows[_curEvalIndex].Cells[0].Value = boxDCEConditionsName.Text;
        }

        private void butOpenDemoEvents_Click(object sender, EventArgs e)
        {
            dLF.tabControl1.SelectedIndex = 1;
            dLF.Show();
        }

        private void butDCERefresh_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show($"Are you sure you want to Restore check definitions to Saved Settings?", "Demo Checks | Restore", MessageBoxButtons.YesNo);
            
            if (result == DialogResult.Yes)
            {
                LoadSettings();
                FillInformation();
            }
        }

        private void butSave_Click(object sender, EventArgs e)
        {
            SaveSettings();
            WriteNoteLabel("Saved checks to checks.xml!");
        }

        private void SaveSettings()
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.IndentChars = ("    ");
            settings.CloseOutput = true;
            using (XmlWriter xml = XmlWriter.Create("checks.xml", settings))
            {
                xml.WriteStartElement("games");
                foreach (DemoCheckHandler config in _configs)
                {
                    xml.WriteStartElement(config.Name);
                    int i = 0;
                    foreach (Evaluation eval in config.Evaluations)
                    {
                        xml.WriteStartElement($"check{i++}");
                        xml.WriteStartElement("types");
                        xml.WriteStartElement("evaluation");
                        xml.WriteElementString("type", eval.Type.ToString());
                        xml.WriteElementString("directive", eval.Directive.ToString());
                        xml.WriteEndElement();
                        xml.WriteElementString("result", eval.ResType.ToString());
                        xml.WriteEndElement();
                        xml.WriteElementString("var", eval.VarToString());
                        xml.WriteElementString("map", eval.Map);
                        xml.WriteElementString("tickcompare", eval.TickComparison.ToString());
                        xml.WriteElementString("not", eval.Not.ToString());
                        xml.WriteElementString("name", eval.EventName);
                        xml.WriteEndElement();
                    }
                    xml.WriteEndElement();
                }
                xml.Flush();
            }
        }
    }
}
