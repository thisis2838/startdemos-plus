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
    [System.ComponentModel.DesignerCategory("Code")]
    public class MultiCheckTextBox : TextBox
    {
        [Description("Title of editor window"), Category("Data")]
        public string EditorWindowText
        {
            get; set;
        }

        [Description("Desription of editor window"), Category("Data")]
        public string EditorWindowDescription
        {
            get; set;
        }

        public List<(string, bool)> Members = new List<(string, bool)>();
        public List<string> CheckedMembers => Members.Where(x => x.Item2).Select(x => x.Item1).ToList();

        public EventHandler<EventArgs> MembersUpdateRequested;

        public MultiCheckTextBox() : base()
        {
            ReadOnly = true;
            Cursor = Cursors.Hand;
            Click += MultiCheckTextBox_Click;
        }

        private void MultiCheckTextBox_Click(object sender, EventArgs e)
        {
            MembersUpdateRequested?.Invoke(null, null);

            var form = new MultiCheckForm();

            var result = form.Show(EditorWindowText, EditorWindowDescription, Members.ToArray());
            Members.Clear(); result.ToList().ForEach(x => Members.Add(x));

            UpdateText();
        }

        public void UpdateMembers(List<string> members)
        {
            if (members == null)
            {
                Members.Clear();
                return;
            }
            Members = Members.Where(x => members.Contains(x.Item1)).ToList();
            members.Where(x => !Members.Any(y => y.Item1 == x)).ToList().ForEach(x => Members.Add((x, false)));
            UpdateText();
        }

        private void UpdateText()
        {
            if (CheckedMembers.Count == 0)
            {
                Text = $"Click here to edit";
                return;
            }
            Text = String.Join(", ", CheckedMembers);
        }
    }
}
