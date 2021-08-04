using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static HelpAndGuides.Texts;
using Markdig;
using System.IO;
using System.Xml;

namespace HelpAndGuides
{
	public partial class MainForm : Form
	{
		private Evaluation _curEval => _games[_curGameIndex].Item2[_curListIndex];
		private int _curGameIndex;
		private int _curListIndex;
		private List<(string, List<Evaluation>)> _games;

		public MainForm()
		{
			InitializeComponent();
            #region GUIDES TEXT
            var html = Markdown.ToHtml(textDemoOrderFormatting);
			dispDemoOrderFormatting.DocumentText = html;
			html = Markdown.ToHtml(textDemoOrderFormattingExamples);
			dispDemoOrderExamples.DocumentText = html;
			html = Markdown.ToHtml(textDemoCheckInfo);
			dispDemoCheckInfo.DocumentText = html;
			html = Markdown.ToHtml(textDemoCheckSyntax);
			dispDemoCheckSyntax.DocumentText = html;
			html = Markdown.ToHtml(textGeneralFormatting);
			dispGeneralFormatting.DocumentText = html;
			html = Markdown.ToHtml(textDemoCheckExamples);
			dispDemoCheckExamples.DocumentText = html;
			html = Markdown.ToHtml(textAbout);
			dispAbout.DocumentText = html;
			html = Markdown.ToHtml(textToolsAbout);
			dispToolsAbout.DocumentText = html;
			#endregion

			_games = new List<(string, List<Evaluation>)>();

			butSERefresh_Click(null, null);

            cbDCEChecksListGames.TextChanged += CbDCEChecksListGames_TextChanged;
            dgvDCEChecksListGrid.SelectionChanged += DgvDCEChecksListGrid_SelectionChanged;
		}

        private void DgvDCEChecksListGrid_SelectionChanged(object sender, EventArgs e)
        {
			if (dgvDCEChecksListGrid.SelectedCells.Count <= 0)
				return;

			UpdateHighlightedRow(dgvDCEChecksListGrid.SelectedCells[0].RowIndex);
		}

		private void UpdateHighlightedRow(int newIndex)
        {
			_curListIndex = newIndex;
			FillInformation();
		}

		private bool IsCurrentEvalValid()
        {
			return !(_games.Count == 0 || _games[_curGameIndex].Item2.Count == 0);
		}

		private void FillInformation()
        {
			if (_games[_curGameIndex].Item2.Count == 0)
				return;

			ComboBoxEvaluateInput(ref cbDCEEvaluationDataType, _curEval.Type);
			ComboBoxEvaluateInput(ref cbDCEEvaluationDataDirective, _curEval.Directive);
			ComboBoxEvaluateInput(ref cbDCEReturnType, _curEval.ResType);

			boxDCEConditionsTargetVariable.Text = _curEval.Var;
			boxDCEConditionsMap.Text = _curEval.Map;
			chkDCEConditionsNot.Checked = _curEval.Not;
			boxDCEConditionsTickCompare.Text = _curEval.TickComparison;
			boxDCEConditionsName.Text = _curEval.EventName;

			dgvDCEChecksListGrid.Rows[_curListIndex].Cells[0].Value = boxDCEConditionsName.Text;
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

		private void SyncInformation()
        {
			if (_games.Count == 0 || _games[_curGameIndex].Item2.Count == 0)
				return;

			
			_curEval.Directive = ComboBoxTransferInput<string>(ref cbDCEEvaluationDataDirective);
			_curEval.ResType = ComboBoxTransferInput<string>(ref cbDCEReturnType );

			
			_curEval.Map = boxDCEConditionsMap.Text;
			_curEval.Not = chkDCEConditionsNot.Checked;
			_curEval.TickComparison = boxDCEConditionsTickCompare.Text;
			_curEval.EventName = boxDCEConditionsName.Text;
		}

		private void ComboBoxEvaluateInput<T>(ref ComboBox box, T input)
        {
			box.SelectedIndex = (input != null && input.ToString() != "") 
				? box.Items.IndexOf(input) : -1;
        }

		private T ComboBoxTransferInput<T>(ref ComboBox box)
        {
			return (T)((box.SelectedIndex == -1) ? default(T) : (T)box.Items[box.SelectedIndex]);
		}

        private void CbDCEChecksListGames_TextChanged(object sender, EventArgs e)
        {
			if (!_games?.Select(x => x.Item1)?.Contains(cbDCEChecksListGames.Text) ?? true)
				return;

			var evals = _games.Where(x => x.Item1 == cbDCEChecksListGames.Text).ToList()[0];

			_curGameIndex = _games.IndexOf(evals);

			dgvDCEChecksListGrid.Rows.Clear();
			evals.Item2.ForEach(x => dgvDCEChecksListGrid.Rows.Add(x.EventName ?? ""));

        }

        private void butDCERefresh_Click(object sender, EventArgs e)
        {
			if (_games.Count > 0)
            {
				DialogResult result = MessageBox.Show($"Save changes before Refreshing?", "Demo Checks | Refresh", MessageBoxButtons.YesNo);

				if (result == DialogResult.Yes)
					butDCESave_Click(null, null);
			}

			if (!File.Exists("gamesupport.xml"))
				return;

			XmlDocument xml = new XmlDocument();
			xml.Load("gamesupport.xml");

			_games = new List<(string, List<Evaluation>)> ();
			List<Evaluation> evaluations = new List<Evaluation>();

			var games = xml.SelectSingleNode("//games").ChildNodes[0];
			while (games != null)
            {
				var list = games.ChildNodes[0];
				while (list != null)
                {
					evaluations.Add(new Evaluation(list));
					list = list.NextSibling;
				}
				_games.Add((games.Name, evaluations));
				evaluations = new List<Evaluation>();
				games = games.NextSibling;
			}

			RefreshGameDropDown();
		}

		private void RefreshGameDropDown()
        {
			cbDCEChecksListGames.Items.Clear();
			cbDCEChecksListGames.Text = "";

			if (_games.Count == 0)
            {
				dgvDCEChecksListGrid.Rows.Clear();
				EmptyInformation();
				return;
			}

			_games.ForEach(x => cbDCEChecksListGames.Items.Add(x.Item1));
			cbDCEChecksListGames.SelectedIndex = 0;
		}

        private void butDCEChecksListAdd_Click(object sender, EventArgs e)
        {
			if (_games == null || _games.Count == 0)
				return;

			_games[_curGameIndex].Item2.Add(new Evaluation());
			UpdateHighlightedRow(_games[_curGameIndex].Item2.Count() - 1);

			dgvDCEChecksListGrid.Rows.Add("");
			dgvDCEChecksListGrid.ClearSelection();
			dgvDCEChecksListGrid.Rows[_curListIndex].Selected = true;
		}

        private void butDCEChecksListRemove_Click(object sender, EventArgs e)
        {
			if (_games == null || _games.Count == 0)
				return;

			if (dgvDCEChecksListGrid.SelectedCells.Count == 0 || _games[_curGameIndex].Item2.Count == 0)
				return;

			_games[_curGameIndex].Item2.RemoveAt(_curListIndex);
			dgvDCEChecksListGrid.Rows.RemoveAt(_curListIndex);

			if (dgvDCEChecksListGrid.Rows.Count == 0)
				EmptyInformation();
		}

        private void butDCEChecksListGameAdd_Click(object sender, EventArgs e)
        {
			if (_games == null)
				_games = new List<(string, List<Evaluation>)>();

			var form = new DemoCheckAddGameForm(_games?.Select(x => x.Item1)?.ToArray() ?? new string[0]);
			
			if (form.Result != "")
            {
				_games.Add((form.Result, new List<Evaluation>()));
            }
			RefreshGameDropDown();

		}

        private void butDCEChecksListGameRemove_Click(object sender, EventArgs e)
        {
			if (_games.Count == 0)
				return;

			string name = _games[_curGameIndex].Item1;
			DialogResult result = MessageBox.Show($"Are you sure you want to delete {name} from the demo checks?", "Demo Checks | Remove Game", MessageBoxButtons.YesNo);
			
			if (result == DialogResult.Yes)
            {
				_games.RemoveAt(_curGameIndex);
				_curGameIndex = _curGameIndex == 0 ? _curGameIndex - 1 : _curGameIndex;
				RefreshGameDropDown();
			}
		}

		private void butDCESave_Click(object sender, EventArgs e)
		{
			XmlWriterSettings settings = new XmlWriterSettings();
			settings.Indent = true;
			settings.IndentChars = ("    ");
			settings.CloseOutput = true;
			using (XmlWriter xml = XmlWriter.Create("gamesupport.xml", settings))
			{
				xml.WriteStartElement("games");
				foreach ((string, List<Evaluation>) game in _games)
                {
					xml.WriteStartElement(game.Item1);
					int i = 0;
					foreach (Evaluation eval in game.Item2)
                    {
						xml.WriteStartElement($"check{i++}");
						xml.WriteStartElement("types");
						xml.WriteStartElement("evaluation");
						xml.WriteElementString("type", eval.Type);
						xml.WriteElementString("directive", eval.Directive);
						xml.WriteEndElement();
						xml.WriteElementString("result", eval.ResType);
						xml.WriteEndElement();
						xml.WriteElementString("var", eval.Var);
						xml.WriteElementString("map", eval.Map);
						xml.WriteElementString("tickcompare", eval.TickComparison);
						xml.WriteElementString("not", eval.Not.ToString());
						xml.WriteElementString("name", eval.EventName);
						xml.WriteEndElement();
					}
					xml.WriteEndElement();
				}
				xml.Flush();
			}
		}

        private void cbDCEEvaluationDataType_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (IsCurrentEvalValid())
				_curEval.Type = ComboBoxTransferInput<string>(ref cbDCEEvaluationDataType);
		}

        private void cbDCEEvaluationDataDirective_SelectedIndexChanged(object sender, EventArgs e)
        {
			if (IsCurrentEvalValid())
				_curEval.Directive = ComboBoxTransferInput<string>(ref cbDCEEvaluationDataDirective);
		}

        private void cbDCEReturnType_SelectedIndexChanged(object sender, EventArgs e)
        {
			if (IsCurrentEvalValid())
				_curEval.ResType = ComboBoxTransferInput<string>(ref cbDCEReturnType);
		}

        private void boxDCEConditionsTargetVariable_TextChanged(object sender, EventArgs e)
        {
			if (IsCurrentEvalValid())
				_curEval.Var = boxDCEConditionsTargetVariable.Text;
		}

        private void boxDCEConditionsMap_TextChanged(object sender, EventArgs e)
        {
			if (IsCurrentEvalValid())
				_curEval.Map = boxDCEConditionsMap.Text;
		}

        private void boxDCEConditionsTickCompare_TextChanged(object sender, EventArgs e)
        {
			if (IsCurrentEvalValid())
				_curEval.TickComparison = boxDCEConditionsTickCompare.Text;
		}

        private void chkDCEConditionsNot_CheckedChanged(object sender, EventArgs e)
        {
			if (IsCurrentEvalValid())
				_curEval.Not = chkDCEConditionsNot.Checked;
		}

        private void boxDCEConditionsName_TextChanged(object sender, EventArgs e)
        {
			if (IsCurrentEvalValid())
            {
				_curEval.EventName = boxDCEConditionsName.Text;
				dgvDCEChecksListGrid.Rows[_curListIndex].Cells[0].Value = boxDCEConditionsName.Text;
			}
		}

        private void butSERefresh_Click(object sender, EventArgs e)
        {
			if (!File.Exists("config.xml"))
				return;

			XmlDocument xml = new XmlDocument();
			xml.Load("config.xml");

			boxSEExeName.Text = xml.DocumentElement.SelectSingleNode("/config/gameexe")?.InnerText ?? "";
			boxSETickrate.Text = xml.DocumentElement.SelectSingleNode("/config/tickrate")?.InnerText ?? "";
			boxSEWaitTime.Value = int.Parse(xml.DocumentElement.SelectSingleNode("/config/waittime")?.InnerText ?? "50");
			boxSECommands.Text = xml.DocumentElement.SelectSingleNode("/config/commands")?.InnerText ?? "";
			chkSEZerothTick.Checked = bool.Parse(xml.DocumentElement.SelectSingleNode("/config/zerothtick")?.InnerText ?? "False");
		}

        private void butSESave_Click(object sender, EventArgs e)
        {
			XmlWriterSettings settings = new XmlWriterSettings();
			settings.Indent = true;
			settings.IndentChars = ("    ");
			settings.CloseOutput = true;
			using (XmlWriter xml = XmlWriter.Create("config.xml", settings))
			{
				xml.WriteStartElement("config");
				xml.WriteElementString("gameexe", boxSEExeName.Text);
				xml.WriteElementString("tickrate", boxSETickrate.Text);
				xml.WriteElementString("waittime", boxSEWaitTime.Value.ToString());
				xml.WriteElementString("commands", boxSECommands.Text);
				xml.WriteElementString("zerothtick", chkSEZerothTick.Checked.ToString());
				xml.WriteEndElement();
				xml.Flush();
			}
		}
    }
}
