using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace startdemos_plus.Help
{
    public static partial class HelpTexts
    {
        public static string EditDemoChecks = @"
# Editing Demo Checks  
This tab contains options for selecting the current Profile (a set of Checks); and adding, removing and modifying the conditions and actions of a check; as well as adding and removing Checks and Profiles.

## Profile  
A Profile is a set of Checks.  

To change the current Profile, select one from the drop down menu.  
If there aren't any, or you wish to add more, click on the *Add* button and specify the name of the new Profile. Note that no two Profiles can have the same name!  
If you wish to delete a Profile, select that Profile and click *Remove*  

Other parts of this tab will be disabled if there is no Profile present or selected.


## Checks list  
This is the list of Checks present in the current Profile.   

To add a new Check, right click the list and click Add. A new entry will appear at the bottom of the list for you to edit.  
To remove a Check, highlight that Check, then right click the list and click *Delete Selected*  

## Check Settings  
This is the information and conditions of the Check. 

### Name  
This is the name of the Check, which is used to idenfity it throughout parts of the tool. It shouldn't be similar to another Check's name in the current Profile, and shouldn't contain a comma (,).

### Conditions and Actions  
This section allows you to modify the Conditions and Actions of the Check.
#### Conditions 
Here is where you edit the Conditions of the Check.  

To add a Condition, right click the list and click on *Add*. A new entry should now show up.  
To remove a Condition, select it, then right click the list and click *Delete Selected*.

The *Variable* column is the variable from the demo that the Condition will evaluate.  
The *Condition* column is the Comparison string that the Condition will use to evaluate.

#### Actions
Here is where you edit the Actions of the Check.  

To add an Action, right click the list and click on Add. A new entry should now show up.
To remove an Action, select it, then right click the list and click Delete Selected.  

The *Type* column is the type of the Action.  
The *Parameters* column are the parameters of the Action.  

### Applying changes  
All changes to the current Check must be applied before any other action within the tool. A warning window will open if there are unsaved changes.


";
    }
}
