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
using static startdemos_plus.Utils.Utils;
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
                GameValues handler = e.Data["values"] as GameValues;

                labGameProcName.ThreadAction(() =>
                {
                    labGameProcName.Text = $"{handler.Game.ProcessName}";
                });

                listGameValues.ThreadAction(() =>
                {
                    listGameValues.Rows.Clear();
                    listGameValues.Rows.Add("Demo Player Pointer", handler.DemoPlayerPtr.ToString("X"));
                    listGameValues.Rows.Add("Host Tick Count Pointer", handler.HostTickCountPtr.ToString("X"));
                    listGameValues.Rows.Add("Host State Pointer", handler.HostStatePtr.ToString("X"));
                    listGameValues.Rows.Add("Start Tick Offset", handler.DemoStartTickOffset.ToString("X"));
                    listGameValues.Rows.Add("Is Playing Offset", handler.DemoIsPlayingOffset.ToString("X"));

                    if (handler.CBufAddTextPtr != IntPtr.Zero)
                    {
						listGameValues.Rows.Add("Cbuf_AddText Pointer", handler.CBufAddTextPtr.ToString("X"));
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
                    labStatus.Text = "Searching ...";
                    labGameProcName.Text = "";
                    listGameValues.Rows.Clear();

                });
            };

            labStatus.Text = "Searching ...";

            listGameValues.ReadOnly = true;
        }
    }
}
