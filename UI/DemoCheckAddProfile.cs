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
    public partial class DemoCheckAddProfile : Form
    {
        private List<string> _existing = new List<string>();
        private string _name = null;

        public DemoCheckAddProfile()
        {
            InitializeComponent();
        }

        public string Get(List<UIDemoCheckProfile> existing)
        {
            existing.ForEach(x => _existing.Add(x.Name));
            this.ShowDialog();
            return _name;
        }

        private void butOK_Click(object sender, EventArgs e)
        {
            if (_existing.Contains(boxInput.Text))
                Utils.Utils.Message("This name has already been used for another Profile!", Utils.Utils.MessageType.Warning);
            else if (string.IsNullOrWhiteSpace(boxInput.Text))
                Utils.Utils.Message("Profile name cannot be empty or only contain whitespace!", Utils.Utils.MessageType.Warning);
            else
            {
                _name = boxInput.Text;
                this.Close();
            }
        }
    }
}
