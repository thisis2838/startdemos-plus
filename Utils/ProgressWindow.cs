using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace startdemos_plus.Utils
{
    public partial class ProgressWindow : Form
    {
        public ProgressWindow()
        {
            InitializeComponent();
        }

        private Thread _thread;
        private const int CP_NOCLOSE_BUTTON = 0x200;
        private Stopwatch _sw = new Stopwatch();
        private Stopwatch _iterSW = new Stopwatch();

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams myCp = base.CreateParams;
                myCp.ClassStyle = myCp.ClassStyle | CP_NOCLOSE_BUTTON;
                return myCp;
            }
        }

        public void Open(string action)
        {
            labAction.Text = action;
            labProgress.Text = labCurAction.Text = "";

            _thread = new Thread(new ThreadStart(() => ShowDialog()));
            _thread.Start();
            _sw.Start();
        }

        public void Update(int now, int max, string action = "")
        {
            this.ThreadAction(() =>
            {
                if (!_iterSW.IsRunning)
                    _iterSW.Restart();
                else if (_iterSW.ElapsedMilliseconds <= 5)
                    return;
                else _iterSW.Restart();

                labProgress.Text = $"{now} / {max}";
                labCurAction.Text = action;

                progProgress.Maximum = max;
                progProgress.Value = now;

                var speed = (double)now / (double)_sw.ElapsedTicks;
                var left = TimeSpan.FromTicks(speed == 0 ? 0 : (long)((max - now) / speed));
                labTime.Text = $"Elapsed: {_sw.Elapsed.ToString(@"h\:mm\:ss\.fff")} / ETA: {left.ToString(@"h\:mm\:ss\.fff")}";

                Invalidate();
            });
        }

        public void Exit()
        {
            while (!IsHandleCreated) Thread.Sleep(10);

            this.ThreadAction(() => { Close(); });
            _sw.Stop();
            _iterSW.Stop();
        }
    }
}
