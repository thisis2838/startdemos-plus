using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace startdemos_ui.src
{
    static class Events
    {
        public static EventHandler GameHasExited;
        public class GameDiscoveryArgs : EventArgs
        {
            public IntPtr DemoPlayerPtr { get; private set; }
            public IntPtr HostTickCountPtr { get; private set; }
            public IntPtr ExecCmdPtr { get; private set; }
            public int DemoIsPlayingOffset { get; private set; }
            public int DemoStartTickOffset { get; private set; }
            public Process Game { get; private set; }
            public GameDiscoveryArgs( 
                Process game,
                IntPtr demoPlayerPtr,
                IntPtr hostTickCount,
                IntPtr execCmdPtr,
                int demoIsPlayingOffset,
                int demoStartTick)
            {
                DemoPlayerPtr = demoPlayerPtr;
                HostTickCountPtr = hostTickCount;
                DemoIsPlayingOffset = demoIsPlayingOffset;
                DemoStartTickOffset = demoStartTick;
                ExecCmdPtr = execCmdPtr;
                Game = game;
            }
        }
        public static EventHandler<GameDiscoveryArgs> GameHasBeenFound;
        public static EventHandler GotDemos;
        public static EventHandler StartedPlaying;
        public static EventHandler StoppedPlaying;
    }
}
