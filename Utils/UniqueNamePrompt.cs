using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static startdemos_plus.Utils.Helpers;

namespace startdemos_plus.Utils
{
    public partial class UniqueNamePrompt : Form
    {
        private bool _ok;
        private List<string> _existing;
        private string _name = "";
        private string _itemName = "";
        private string _def = "";

        public UniqueNamePrompt()
        {
            InitializeComponent();
        }

        public string Show(string itemName, List<string> existing, string def = "")
        {
            Text = $"startdemos+ | {itemName} name";
            label1.Text = $"Please enter the name of the {itemName}";

            boxInput.Text = def;
            _ok = false;
            _def = def;
            _itemName = itemName;
            _name = "";
            _existing = new List<string>();
            existing.ForEach(x => _existing.Add(x));

            this.ShowDialog();

            return _ok ? _name : null;
        }

        private void butOK_Click(object sender, EventArgs e)
        {
            if (_existing.Contains(boxInput.Text))
            {
                if (boxInput.Text != _def)
                    Message($"This name has already been used for another {_itemName}!", MessageType.Warning);
                else this.Close();
            }
            else if (string.IsNullOrWhiteSpace(boxInput.Text))
                Message("Name cannot be empty or only contain whitespace!", MessageType.Warning);
            else
            {
                _ok = true;
                _name = boxInput.Text;
                this.Close();
            }
        }
    }
}
