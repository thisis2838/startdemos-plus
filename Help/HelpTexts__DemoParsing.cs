using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace startdemos_plus.Help
{
    public static partial class HelpTexts
    {
        public static string DemoParsing = @"
# Demo Parsing  
This tab provides controls for parsing and cataloging information about demos. Demos parsed here are ones that the tool will play.

## Opening Demos  
To open demos for playing:
* Click on *Open Files*
* Select the files that should be played (selecting multiple files is allowed).  

Once files have been selected, the tool will then display general information about this set of demos and each demo in the *Demos* tab; and automatically run Checks on each demo and display the results in the *Check Results* tab. If the game has been hooked, it will enable the *Demo Ordering & Playing* tab.  

## Changing timing settings
Timing settings for the demos can be found in the *Settings* tab, which contain:
* *Tick Rate*, which defines how long each tick is in seconds.
*  *Count 0th Tick*, which tells the tool whether to add a tick to the time of each demo.  

## Refreshing demo lists  
Any change to the settings or demo checks will only be reflected in the *Demos* and *Check Results* lists if the *Refresh* button is clicked.


";
    }
}
