using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace startdemos_plus.Frontend
{
    public partial class ErrorWindow : Form
    {
        public ErrorWindow()
        {
            InitializeComponent();

            try { picErrorIcon.Image = System.Drawing.SystemIcons.Error.ToBitmap(); }
            catch { picErrorIcon.Image = picErrorIcon.ErrorImage; }
        }

        public ErrorWindow(string message) : this()
        {
            boxMessage.Text = message;
        }

        public ErrorWindow(Exception ex) : this()
        {
            boxMessage.Text = ex.ToString();
        }

        public new void ShowDialog()
        {
            System.Media.SystemSounds.Asterisk.Play();
            base.ShowDialog();
        }

        private void butReport_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/thisis2838/startdemos-plus/issues");
        }
    }
}
