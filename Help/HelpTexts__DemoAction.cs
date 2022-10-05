using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace startdemos_plus.Help
{
    public static partial class HelpTexts
    {
        public static string DemoAction = @"
# Demo Action
Demo Actions are actions which are ran when a Check passes.

## Parameters
Demo Action Parameters are information used by the action. The format and value of the parameters depend on the Action's typen.  

## Types of Demo Action  
### Start Demo Time
This action marks the earliest tick that passes the Check as the start tick of the demo.  
Upon passing, the action will affect a demo's Measured Tick Count.  
The parameter it accepts is a single number, to add onto the start tick's index (i.e. to offset the start time that was found)  

### End Demo Time
This action marks the earliest tick that passes the Check as the end tick of the demo.  
Upon passing, the action will affect a demo's Measured Tick Count.  
The parameter it accepts is a single number, to add onto the start tick's index (i.e. to offset the start time that was found)  
";
    }
}
