using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace startdemos_plus
{
    public partial class DemoListForm : Form
    {
        public DemoListForm()
        {
            InitializeComponent();
            this.FormClosing += DemoListForm_FormClosing;
        }

        private void DemoListForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }

        protected override bool ShowWithoutActivation
        {
            get { return true; }
        }
    }
}
