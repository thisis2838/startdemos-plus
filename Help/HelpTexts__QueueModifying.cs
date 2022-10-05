using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace startdemos_plus.Help
{
    public static partial class HelpTexts
    {
        public static string QueueModifying = @"
# Modifying the Queue

## Reordering Demos  
*Reorder Demos* contains options for reordering demos in the Queue
* *Order By* decides what information should the tool use to order the demos. It contains
    * *Modified Date*, which orders the demos by their last modified date, as indicated by Windows.
	* *Demo File Name*, which orders the demos by their name, alphanumerically.
	* *Demo Map Name*, which orders the demos by the map, then by name, both alphanumerically.
	* *Custom Map Order*; which orders the demos by the map, in the order specified when clicking *Configure*; then by name; both alphanumerically.
* *Reversed* decides whether to reverse the order of the demos in the reordered list.

## Filtering Demos
*Filter Demos* contains options and conditions for deciding which demos in the Queue to play.
* *Range*, which defines the range of demos in the Queue to consider. The indexes are as indicated in the Queue list.
* *Demo Info Conditions*, which are conditions that decide which demos in the queue are played. They include:
    * A tick count condition, accepting a Numerical Comparison, comparing to either the demo's Total Tick Count or Measured Tick Count.
	* *Map*, which is a String Comparison checking the map of the demo.
	* *Demo Check Conditions*, which details the names of the Checks a demo must or must not pass in order to be played. These names are separated by a comma.

To the right of each of the first two conditions is a *Not* tickbox, which when ticked, reverses the result of their corresponding condition

### Which demos are filtered in
A demo must have its index in the Queue lie within the defined *Range*, and pass all the conditions listed here. If a condition is left blank, it is ignored.

";
    }
}
