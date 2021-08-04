using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HelpAndGuides
{
    public partial class DemoCheckAddGameForm : Form
    {
        private List<string> _unacceptableEntries;
        private Timer _warningLabelTimer;
        public string Result { get; private set; } = "";
        public DemoCheckAddGameForm(string[] entries)
        {
            InitializeComponent();
            _unacceptableEntries = new List<string>(entries);
            _warningLabelTimer = new Timer();
            _warningLabelTimer.Interval = 3000;
            _warningLabelTimer.Tick += (sender, e) =>
            {
                labWarning.Visible = false;
            };
            this.ShowDialog();
        }

        private void butOK_Click(object sender, EventArgs e)
        {
            if (_unacceptableEntries.Contains(boxName.Text))
            {
                _warningLabelTimer.Start();
                labWarning.Visible = true;
                return;
            }

            Result = boxName.Text;
            this.Close();
        }
    }
}
