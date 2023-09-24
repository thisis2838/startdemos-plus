using startdemos_plus.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using startdemos_plus.Backend;
using static startdemos_plus.Globals.Events;
using static startdemos_plus.Utils.Helpers;
using System.Threading;

namespace startdemos_plus.Frontend
{
    public partial class GameHookForm : UserControl
    {
        public GameHookForm()
        {
            InitializeComponent();

            FoundGameProcess += (object s, CommonEventArgs e) =>
            {
                GameValues vals = e.Data["values"] as GameValues;

                labGameProcName.ThreadAction(() =>
                {
                    labGameProcName.Text = $"{vals.Game.ProcessName}";
                });

                listGameValues.ThreadAction(() =>
                {
                    listGameValues.Rows.Clear();
                    listGameValues.Rows.Add("Demo Player Pointer", vals.DemoPlayerPtr.ToString("X"));
                    listGameValues.Rows.Add("Host Tick Count Pointer", vals.HostTickCountPtr.ToString("X"));
                    listGameValues.Rows.Add("Host State Pointer", vals.HostStatePtr.ToString("X"));
                    listGameValues.Rows.Add("Start Tick Offset", vals.DemoStartTickOffset.ToString("X"));
                    if (vals.DemoFilePtrOffset != 0)
                    {
                        listGameValues.Rows.Add("Demo File Pointer Offset", vals.DemoFilePtrOffset.ToString("X"));
                    }
                    listGameValues.Rows.Add("Is Playing Offset", vals.DemoIsPlayingOffset.ToString("X"));

                    if (vals.CBufAddTextPtr != IntPtr.Zero)
                    {
						listGameValues.Rows.Add("Cbuf_AddText Pointer", vals.CBufAddTextPtr.ToString("X"));
					}
				});

                labStatus.ThreadAction(() =>
                {
                    labStatus.Text = "Found";
                });
            };


            LostGameProcess += (s, e) =>
            {
                this.ThreadAction(() =>
                {
                    labStatus.Text = "Searching...";
                    labGameProcName.Text = "";
                    listGameValues.Rows.Clear();
                });
            };

            BeganScanningProcess += (s, e) =>
            {
                this.ThreadAction(() =>
                {
                    labStatus.Text = $"Scanning {e["name"]}...";
                });
            };
            StoppedScanningProcess += (s, e) =>
            {
                this.ThreadAction(() =>
                {
                    labStatus.Text = $"Searching...";
                });
            };

            labStatus.Text = "Searching...";

            listGameValues.ReadOnly = true;
        }
    }
}
