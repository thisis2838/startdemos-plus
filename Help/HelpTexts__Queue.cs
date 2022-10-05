using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace startdemos_plus.Help
{
    public static partial class HelpTexts
    {
        public static string Queue = @"
# The Queue  

The Queue is the list of demos that the tool will play. It is populated with demos parsed in *Demo Parsing*.  

The Queue can be ordered in multiple ways, and can be filtered so that only specific demos will be played. Those that aren't played are greyed out.  


## Manually editing the Queue  

To manually edit the queue, first uncheck *Only show allowed demos* and select the demos you want to edit. From here, you can:  
* Click ▲ to move the selected demos up in the queue.
* Click ▼ to move the selected demos down in the queue.
* Click ✔ to allow the selected demos to be played.
* Click ✖ to exclude the selected demos from being played.

";
    }
}
