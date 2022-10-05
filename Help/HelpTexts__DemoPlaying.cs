using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace startdemos_plus.Help
{
    public static partial class HelpTexts
    {
        public static string DemoPlaying = @"
# Playing Demos
## Transport Controls
*Transport Controls* contain controls for playing demos. It includes:
* A combination Play (▶) / Pause (⏸) button, which begins playing the queue, and pauses / resumes demo playback.
* A Stop (⏹) button, which stops demo playback and the queue altogether.
* A Skip (⏩) button, which skips to the next demo in the queue. If there is no such demo, the queue is stopped.
* A Rewind (⏪) button, which plays the previously played demo. If there is no such demo, the queue is stopped.
* A list of upcoming demos in the queue and the previously played demo.
* The name and playback state of the current demo.  

Click on the Play button to begin playing the queue. The currently played demo will be highlighted in light blue in the Queue. While playing, options for parsing demos, modifying the queue and playing options are disabled.

## Playing Options
*Options* contains options for playback. These include:
* *Auto-Play Next*, which tells to tool whether to automatically play the next demo.
* *Wait Time*, whichs tells the tool the least amount of time in milliseconds to wait after a demo stops playing to begin playing the next one.
* *Commands to run per Demo Start*, which are the commands to send to the game when a demo starts playing. They are written the same way they would be entered into the game's developer console for execution.  

These options can only be modified while the queue is not being played.
";
    }
}
