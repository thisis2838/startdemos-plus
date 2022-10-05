using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;

namespace startdemos_plus.Utils
{
    [System.ComponentModel.DesignerCategory("Code")]
    public class CleanListBox : DataGridView
    {
        private bool _editable => AllowUserToAddRows && !ReadOnly;
        private static object _DEFAULT_CELL_VALUE = "";

        public List<List<string>> GetValues()
        {
            List<List<string>> values = new List<List<string>>();
            foreach (DataGridViewRow row in this.Rows)
            {
                if (row.IsNewRow)
                    continue;

                values.Add(row.Cells.Cast<DataGridViewCell>().ToList().ConvertAll(x => x.Value.ToString()));
            }
            return values;
        }

        public string GetValue(int row, int col)
        {
            if (row > RowCount - 2 || row < 0 ||
                col > ColumnCount - 1 || col < 0)
                return null;

            return this[col, row].Value.ToString();
        }

        public CleanListBox()
        {
            this.AllowUserToResizeRows = false;
            this.RowHeadersVisible = false;
            this.ColumnHeadersVisible = true;
            //this.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            this.CellBorderStyle = DataGridViewCellBorderStyle.Single;
            this.BorderStyle = BorderStyle.FixedSingle;
            this.BackgroundColor = SystemColors.Window;

            //this.RowTemplate.Height = base.Font.Height + 5;

            this.KeyDown += EditableListBox_KeyDown;
            EnabledChanged += CleanListBox_EnabledChanged;
        }

        private (int index, Color oldColor) _highlightedRow = (-1, Color.White);
        public void HighlightRow(int row)
        {
            if (_highlightedRow.index != -1)
                Rows[_highlightedRow.index].DefaultCellStyle.BackColor = _highlightedRow.oldColor;

            if (row == -1)
            {
                _highlightedRow = (-1, Color.White);
                return;
            }

            if (row > RowCount - 1)
                return;

            _highlightedRow = (row, Rows[row].DefaultCellStyle.BackColor);
            Rows[row].DefaultCellStyle.BackColor = Color.LightBlue;
            FirstDisplayedScrollingRowIndex = row > 2 ? row - 2 : 0;
        }

        private void CleanListBox_EnabledChanged(object sender, EventArgs e)
        {
            BackgroundColor = Enabled ? Color.White : SystemColors.Control;
        }

        public void SetText(int index, params object[] content)
        {
            if (index > RowCount - 1 || index < 0)
                return;

            for (int i = 0; i < ColumnCount && i < content.Count(); i++)
            {
                if (!Rows[index].Cells[i].Value.Equals(content[i]))
                    Rows[index].Cells[i].Value = content[i];
            }
        }

        public void SwapEntries(int index1, int index2, params int[] excludeCols)
        {
            if (index1 >= RowCount - 1 || index1 <= 0 ||
                index2 >= RowCount - 1 || index2 <= 0)
                return;

            for (int i = 0; i < ColumnCount; i++)
            {
                if (excludeCols?.Contains(i) ?? false)
                    continue;

                var value = Rows[index1].Cells[i].Value.ToString();
                Rows[index1].Cells[i].Value = Rows[index2].Cells[i].Value;
                Rows[index2].Cells[i].Value = value;
            }
        }

        private void EditableListBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (!_editable)
                return;

            if (SelectedCells.Count == 0)
                return;

            var cells = SelectedCells.Cast<DataGridViewCell>().ToList();
            if (e.KeyCode == Keys.Back)
            {
                foreach (var cell in cells)
                {
                    if (cell.OwningRow.IsNewRow)
                        continue;

                    cell.Value = _DEFAULT_CELL_VALUE;
                }
            }
            else if (e.KeyCode == Keys.Delete)
            {
                foreach (var row in cells.ConvertAll(x => x.OwningRow).DistinctBy(x => x.Index))
                {
                    if (row.IsNewRow)
                        continue;

                    this.Rows.Remove(row);
                }
            }
        }
    }
}
