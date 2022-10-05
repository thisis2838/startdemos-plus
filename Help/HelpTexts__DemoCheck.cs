using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace startdemos_plus.Help
{
    public static partial class HelpTexts
    {
        public static string DemoCheck = @"
# Demo Check
A Demo Check is a series of Demo Conditions, which must all pass for the Check to pass.

## Demo Condition
A Demo Condition is a condition that evalutes a single Variable in a demo using a Comparison String.

### Variables
Variables that Demo Checks can evaluate includes:  
* *Demo Variables*, which are attributes of a demo and not of a particular. They include:
    * *Map Name*, which is the map the demo was recorded on.
    * *Demo Name*, which is the file name of the demo, without extension.
    * *Player Name*, which is the in-game name of the player that recorded the demo. (Note that this doesn't necessarily have to be their Steam name.)
    * *Demo Tick Count*, which is the Total Tick Count of the demo, as indicated in *Demo Parsing*.
*  *Tick Variables*, which are data stored in ticks. They include:
    * *Command*, which is the console command sent in the tick.
    * *Player Position*, which is the Player's position in the tick.
    * *Tick Index*, which is the index of the tick.

### Comparison String
Demo Conditions evalute data according to a Comparison String. The type of Comparison String used by the Condition is dependent on the type of Variable.
* *Map Name*, *Demo Name*, *Player Name*, and *Command* Conditions use a String Comparison string. 
* *Demo Tick Count*, and *Tick Index* Conditions use a Numerical Comparison string.
* *Player Position* Conditions use a Positional Comparison string.

## How Checks are evaluated
Conditions evaluating *Demo Variables* are checked first in a demo, and only once. If there exists any in the Check, and any fail, the Check fails.  
If there are any Conditions evaluating *Tick Variables*, then for the Check to pass, there must exist one tick in the demo which contain data that passes all of them.  
A Check can pass any number of times, on any number of demos.

";
    }
}
