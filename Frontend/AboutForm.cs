using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static startdemos_plus.Program;
using startdemos_plus.Backend.DemoChecking;
using startdemos_plus.Backend;
using static startdemos_plus.Backend.DemoChecking.DemoCheckActionType;
using static startdemos_plus.Utils.Helpers;
using startdemos_plus.Utils;
using static startdemos_plus.Frontend.MainForm;
using System.IO;
using startdemos_plus.Frontend;
using static startdemos_plus.Globals.Events;

namespace startdemos_plus.Frontend
{
    public partial class AboutForm : UserControl
    {
        private int _curState = 0;
        private Image[] _bgs = new Image[]
        {
            Properties.Resources.Comp_2_000000,
            Properties.Resources.Comp_3_000000,
            Properties.Resources.Comp_4_000000,
        };

        public AboutForm()
        {
            InitializeComponent();

            Settings.AddSetting
            (
                "about-bg",
                x => { if (int.TryParse(x, out int a)) _curState = a; },
                () => _curState.ToString()
            );

            this.labVersion.Text = $"version {typeof(Program).Assembly.GetName().Version}";
            this.labVersion.Location = new Point((this.label3.Width + this.label3.Location.X) - this.labVersion.Width, this.labVersion.Location.Y);

            this.Load += (s, e) => pictureBox1.Image = _bgs[Math.Abs((_curState++) % _bgs.Count())];
        }
    }
}
