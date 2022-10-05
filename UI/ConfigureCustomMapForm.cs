using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace startdemos_plus.UI
{
    public partial class ConfigureCustomMapForm : Form
    {
        public ConfigureCustomMapForm()
        {
            InitializeComponent();
            this.butOK.Click += butOK_Click;
        }

        private string _retStr = "";

        public string Open(string existing)
        {
            existing = existing.Trim('\n', ' ');
            boxInput.Text = _retStr = existing;
            boxInput.SelectionLength = 0;
            this.ShowDialog();
            return _retStr;
        }

        private void butOK_Click(object sender, EventArgs e)
        {
            _retStr = boxInput.Text;
            this.Close();
        }
    }
}
