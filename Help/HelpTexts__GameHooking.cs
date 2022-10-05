using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace startdemos_plus.Help
{
    public static partial class HelpTexts
    {
        public static string GameHooking = @"
# Game Hooking  
This tab displays the current status of startdemos+'s memory hooking functions, which is required for initiating and monitoring demo playback.  

The tool will actively search out for processes that have specific executable names matching those of popular Source games and mods. Once it has found a suitable process, it will scan the process' memory to enumerate required memory addresses for monitoring Demo playback. If scanning succeeds, and some demos have been parsed, functions in the *Demo Ordering & Playing* tab will be enabled. 

If the game is launched but the tool does not indicate it has successfully hooked it, then the game is most likely unsupported by the tool. If this is the case, you should contact the developer.

";
    }
}
