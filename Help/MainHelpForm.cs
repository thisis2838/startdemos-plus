using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace startdemos_plus.Help
{
    public partial class MainHelpForm : Form
    {
        public MainHelpForm()
        {
            InitializeComponent();

            this.FormClosing += (s, e) =>
            {
                this.Hide();
                e.Cancel = true;    
            };

            List<(TabPage, string)> pairs = new List<(TabPage, string)>()
            {
                (pgWelcome, Properties.Resources.docs_welcome),
                (pgM_ComparisonStrings, Properties.Resources.docs_miscellaneous_comparison_strings),
                (pgGetitngStarted, Properties.Resources.docs_getting_started),
                (pgGameHooking, Properties.Resources.docs_game_hooking),
                (pgDOP_TheQueue, Properties.Resources.docs_demo_ordering_and_playing_the_queue),
                (pgDOP_ModifyQueue, Properties.Resources.docs_demo_ordering_and_playing_modify_queue),
                (pgDemoParsing, Properties.Resources.docs_demo_parsing),
                (pgDC_EditDemoChecks, Properties.Resources.docs_demo_checks_edit_demo_checks),
                (pgDC_DemoCheck, Properties.Resources.docs_demo_checks_demo_check),
                (pgDC_DemoAction, Properties.Resources.docs_demo_checks_demo_action),
                (pgM_ComparisonStrings, Properties.Resources.docs_miscellaneous_comparison_strings),
                (pgDOP_Play, Properties.Resources.docs_demo_ordering_and_playing_play)
            };

            pairs.ForEach(x =>
            {
                RichTextBox b = new RichTextBox()
                {
                    BackColor = Color.White,
                    BorderStyle = BorderStyle.None,
                    Dock = DockStyle.Fill,
                    Rtf = x.Item2,
                    ReadOnly = true
                };

                x.Item1.Padding = new Padding(9, 0, 9, 9);
                x.Item1.Controls.Add(b);
            });
        }

        public new void Show()
        {
            base.Show();
            this.Focus();
        }
    }
}
