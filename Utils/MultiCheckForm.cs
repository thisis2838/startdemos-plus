using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace startdemos_plus.Utils
{
    public partial class MultiCheckForm : Form
    {
        private bool _ok = false;

        public MultiCheckForm()
        {
            InitializeComponent();
        }

        public (string, bool)[] Show(string caption, string info, (string, bool)[] choices)
        {
            _ok = false;

            Text = $"startdemos+ | {caption}";
            labInfo.Text = info;
            listBox.Items.Clear();

            choices.ToList().ForEach(x => listBox.Items.Add(x.Item1, x.Item2));
            this.ShowDialog();

            return _ok ?
                listBox.Items.Cast<object>().Select((x, y) => (entry: x, index: y))
                .Select(x => (x.entry.ToString(), listBox.CheckedIndices.Contains(x.index)))
                .ToArray() :
                choices;
        }

        private void butOK_Click(object sender, EventArgs e)
        {
            _ok = true;
            this.Close();
        }
    }
}
