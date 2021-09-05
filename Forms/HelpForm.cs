using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Markdig;

namespace startdemos_ui.Forms
{
	public partial class HelpForm : Form
	{
		private static string _textDemoOrderFormatting = @"
## Stages
Demo playback is split into stages, which are handled one after another. When inputted, stages are separated using commas
( , ). For example:<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;`1, 2, 4, ra-, 25-36/>=35`  
Represents 4 stages, each playing a different set of demos in specific orders.

## Formatting a stage
Each stage is formatted as such:  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;`<attributes> <index or array of demos> / <tick condition> / <check condition>`  
Note that a space isn't required between these variables.
<br><br>
### `<index or array of demos>`
This is the initial demo array which is the demo array that is selected before sorted and filtered using the `<attributes>` and `<tick condition>`.
Accepted entries include:
* `x` which will only include the demo with index of `x`.
* `x-y` which will include (in order) demos from index `x` to `y`.
*  `-` which will include all demos from first to last.

### `<attributes>`
These are the attributes used to sort the defined demo array. Accepted entires include:
* `a` which will sort the demos in alphabetical order according to their names.
* `r` which will reverse the array. Note that this is done after all other Sorting operations.

### `<tick condition>`
This is the Numerical Condition to apply on the tick counts of demos to filter them. Note that this is done after Sorting. 

### `<check condition>`
This is the String Condition to apply on the names of the Demo Checks that a demo passed at least once. Note that this is done on all of those Checks, and the Condition will fail if one of the names do not pass.

";

		static private string _textDemoOrderFormattingExamples = @"
## Examples

### `6`
This can be understood as ""Play demo with index #6"" 

### `ra-`
This can be understood as ""Play all demos in reversed alphabetical order""  
* `-` means to select all demos from first to last.  
* `a` means to sort the demos alphabetically according to their name.
* `r` means to reverse all the demos  

### `a25-36/>=35`
This can be understood as ""Play demos between index #25 and #36 which are at least 35 ticks long and in alphabetical order. 
* `25-36` means to select demos from index 25 to 36, in that order.  
* `a` means to sort the demos alphabetically according to their name.  
* `>=35` means to only select demos which are at least 35 ticks long. 

### `a-, r12-24/=65, 8/<90`
This expresion has 3 stages, which include:
* `a-`  
This can be understood as ""First play all demos in alphabetical order according to their name"".
	* `-` means to select all demos from first to last.
	* `a` means to sort the demos alphabetically according to their name.
* `24-12/=65`  
This can be understood as ""Then play demos from index #24 to #12 which are exactly 65 ticks long"". 
	* `24-12` means to select demos from index #24 down to index #12, in that order.
	* `=65` means to select demos which are exactly 65 ticks long.
* `8/<90`
This can be understood as ""Finally, play demo 8 only if it is under 90 ticks long"".
	* `8` means to select demo #8
	* `<90` means to only select demos which are under 90 ticks long.
	
### `a-/x/sens change, -/>25/*go&!*vault`
This expression has 2 stages, which include:
* `a-/x/sens change`  
This can be understood as ""Sort all demos in alphabetical order according to their names, then only play demos that ONLY passed the check named ""sens change"".""
	* `-` means to select all demos from first to last.
	* `a` means to sort the demos alphabetically according to their name.
	* `x` means the Tick Condition is Ineffective
	* `sens change` means to select demos that ONLY passed the check ""sens change"".
* `-/>25/*go&!*vault`  
This can be understood as ""Play demos that are more than 25 ticks long and must have passed the checks which include ""go"", but not ones which have ""vault"" in their names.""
	* `-` means to select all demos from first to last.
	* `>25` means to select demos which are over 25 ticks long.
	* `*go&!*vault` means to select demos whose events include those with `go` in their names, and must NOT include `vault` in their names.
";

		private static string _textSSConditions = @"
## Comparison
A Comparison describes a condition which a candidate value must pass.  
It is formatted as so  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; `<operator><value>`   
Where:
* `<operator>` is the desired comparision operator, this depends on the type of Comparison used, however all of them allow:
	* `!` to signify that the result of the Comparison is reversed. This must always be put in front of all other operators.
	
If a Comparison is left empty or only filled with whitespaces then it is left as *Ineffective*, meaning it will always return True no matter what.
	
There are 2 types of Comparisons used in the tool:
* Numerical Comparison
* String Comparison

## Numerical Comparison
A Numerical Comparison describes a Comparison which is done in the tool against numeric values.  
Its' operators are:
* `>` to signify that our candidate must be greater than `<value>`.
* `<` to signify that our candidate must be smaller than `<value>`.
* `=` to signify that our candidate must be equal to `<value>`. This sign can be placed after `>` or `<` to signify that the number can also equal to our `<value>` in those cases.

For example:  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; `<=3` means our number must be 3 or lower.  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; `>=12.256` means our number must be 12.256 or higher.  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; `!=100` means our number must NOT be 100

## String Comparison
A String Comparison describes a comparison which is done in the tool against strings.  
Its' operators are:  
* `*` to signify that candidate values are evaluated as substrings of `<value>`
* `@` to signify that candidate values are evaluated using a Regex with `<value>` as the pattern.
* Leaving only the bare string will signify the candidate values must match out `<value>` exactly.

For example:  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; `this` matches the string `this`  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; `!*hello` signifies that the candidate string must NOT include the string `hello`

Due to limitations in combining Comparisons (which is described below), the characters `!` `|` `^` `&` `(` `)` are reserved. To combat this, surround your string with `\""` to signify that characters in your string are not to be used in combining Comparisons.  
For example:  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; `\""(hello)\""` matches the string `(hello)`  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; `@\""([0-9]+)\""` matches the regex expression `([0-9]+)`  

## Combining Comparisons  
You can combine comparisons using logical OR `|`, logical AND `&` and logical XOR `^` along with parentheses `(` `)`  

For example:  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; `<20|>500` means the candidate value can be either less than 20 or larger than 500   
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; `!=10|(>500&!=1024)` means the candidate value must not be 10, or be more than 500 but must not be 1024.  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; `* hello&!\""hello world\""` means that the candidate string must contain the word `hello` but not be the phrase `hello world`.<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; `@\""[A-z ]+[0-9]+\""&!*dsp` means that the candidate string must pass the regex expression `[A-z ]+[0-9]+` (any string of letters or spaces followed by numbers) but not include the word `dsp`.

You can also put the logical negation `!` in front of nested groups of conditions like so:  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; `!(<20|>500)&(!=300)` means the candidate value must NOT be smaller than 20 or larger than 50, and that it must also NOT be equal to 300.  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; `*a&!(*b|*c)` means the candidate string must include the character `a` but must NOT include either the characters `b` or `c`.
";

		private static string _textSSPositionCoordinate = @"
## Position / Coordinate
A position or coordinate value describes a point in 3D space, or more specifically an array of 3 floating point numbers within the tool.  
It is formatted as such  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; `x y z`  

Where `x`, `y` and `z` are the corresponding axial components of the desired Position or Coordinate value.

Examples:  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; `25 36 124`  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; `17.269 25.14 96.125`  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; `0 10 20`

";

		private static string _textWelcome = @"
## Welcome
Welcome to the startdemos+ Help window, laden with Times New Roman due to WinForms limitations!  

## Contents
In this Help window, you'll find info and exmaples on the following:  
* startdemos+ usage and info on how it works.
* Demo Indexing
* Demo Order formatting
* Demo Checking documentation 
* Syntax shared across the tool's functions.

To begin, click on the tabs at the top of the page. Some may contain sub-tabs of their own.
";

		private static string _textBasicGuide = @"
## Elements

startdemos+ is comprised of a few elements:
* a Memory Scanner and Monitor which collects neccesary info about the game in which the demos are played.
* a Demo Collector which indexes demos and collect data specified by the Demo Checks
* a Demo Player which handles demo ordering, queuing and playing.

## Collecting your demos
To tell startdemos+ what demos to use, head to the *Demo Collection* tab and enter in the path to your demos, then click *Process Demos*. If the Demo Collector found any demos in the folder provided, it will then process them until it has gone through every file in that folder (excluding ones in sub-directories).  
After this process, the *Demo LIst* window will open automatically if not already, which lists info from all the found demos.  

Note that this function can be run without the game being launched.


## Starting a playing session
To start a demo playing session, you first launch your game.  
After the game has fully loaded, enter in your game's Process Name in the *Memory Scanning* tab for the Memory Scanner to function. For example:  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; `hl2` for Half-Life 2  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; `bms` for Black Mesa  
If everything succeeds, all the info gathered by the Memory Scanner should be printed in that tab.

Next, if not already, head over to the *Demo Collection* tab to specify the demos you wish to play.

Finally, go to the *Demo Playing* tab to begin playing your demos.
";

		private static string _textDemoCheckingInfo = @"
As mentioned in *Basic Guide*, the Demo Collector collects data specified by the Demo Checks.  

These checks are run each tick where there is usable data, and if a check succeeds, the Collector will take note and print the result in the *Demo Events* tab in the *Demo List* window.  

Along with cataloging data, checks can also tell the Collector what is the start and/or end tick of a demo, which will affect the *Adjusted Ticks* variable of a demo.
";

		private static string _textDemoCheckingElements = @"
A Check comprises:
* Evaluation Data
* Result Type
* Conditions
* Identification  

## Evaluation Data
This decribes how to evaluate data coming into the Check. This data includes:
* *Type* which describes the data type used in the Check. The Types include:
	* *Position* which is the player's position.
	* *ConsoleCommand* which are the commands passed into the console by the user or the game.
	* *UserCommand* which are the commands passed by the User.
	* *DemoName* which is the name of the demo file without it's file extension (*.dem*). Note that this check is ran on the last tick of a demo and as such *Tick Compare* will be comparing against the total tick count of the demo.
* *Directive* which describes how to check the demo data against the *Target Variables*. These Directives include:
	* *Direct* which directly compares the demo data to the *Target Variables*. If the *Type* is a string type then the *Target Variables* will be the String Comparison against which the demo data is compared.
	* *Difference* (only works with the *Position* type) which interprets the first value in the *Target Variables* as Positional data and the 2nd as a Numerical Comparison. Then it checks if the difference between the aforementioned Positional data and the player's position passes the Numerical Comparison.

## Result Type  
This tells the Demo Collector what to do if the check passes. This includes:  
* *None* which will only tell the Collector not to catalog this check and print info even if it passes.
* *Remember* which will only tell the Collector to catalog this check but not to print the result in the *Demo Events*.
* *Note* which will tell the Collector to catalog and print the result in the *Demo Events*.
* *BeginOnce* and *EndOnce* which will respectively indicate the Collector that the current tick is the start and end tick of the demo and to print the result in the *Demo Events*. These will also disable this check for the rest of the demos.
* *BeginMultiple* and *EndMultiple* works the same as their *Once* counterparts, but do not disable the check if it passes.

## Conditions
These are the conditional data used in determining if data passes the Check. If the demo data fails any of these then it will fail the Check.
* *Target Variable(s)* are what values passed into the checks should be compared to.  These values are separated with a backslash `/` and are formatted depending on the *Directive* described above.
* *Map* is the String Comparison against which the demo's map should be compared. If it fails, the checks will not be run for that tick.
* *Tick Compare* is what Numerical Comparison should the demo's current tick be checked against. If it fails, the checks will not be run for that tick.
* *Not* determines if the result from the conditions should be reversed.

## Identification
This field determines the identification data for the check. This includes:
* *Name* which is the name of the check. Note that the name shouldn't contain and commas, slashes or ampersands if you wish to use this check in creating a *Play Order*.


";

		private static string _textDemoCheckingExmaples = @"
Here are some example check definitions with explanation of what they do.

* *Evaluation Data*
	* *Type* `ConsoleCommand`
	* *Directive* `Direct`
* *Result* `EndOnce`
* *Conditions*
	* *Target Variable(s)* `startneurotoxins 99999`
	* *Map* `escape_02`
* Identification
	* *Name* `Crosshair disappear`

When combined together, this check will tell the tool to: If a demo is recorded in `escape_02`, search the Console Command found in each tick and see if it is exactly `startneurotoxins 99999`. If so, disable this check indefinitely and modify the demo's adjust time accordingly.

___

* *Evaluation Data*
	* *Type* `ConsoleCommand`
	* *Directive* `Direct`
* *Result* `EndMultiple`
* *Conditions*
	* *Target Variable(s)* `*#SAVE#`
	* *Tick Compare* `>0`
* Identification
	* *Name* `Segment End`
	

When combined together, this check will tell the tool to: Check all demos and find mentions of `#SAVE#` in Console Commands. If a match is found, only adjust the end tick for that demo accordingly and keep the check active.

___


* *Evaluation Data*
	* *Type* `Position`
	* *Directive* `Difference`
* *Result* `Note`
* *Conditions*
	* *Target Variable(s)* `-544 -368.75 160 / >256`
	* *Tick Compare* `>0&<500`
	* *Not* `True`
* Identification
	* *Name* `Away`
	

When combined together, this check will tell the tool to: Record every single tick between 0 and 500 in which the player is NOT `>256` (less or equal than 256) units away from `-544 -368.75 160`.


";

		private static string _textDemoIndexing = @"
When collecting demos, the tool will assign each demo with an Index which is used in determining what order demos should be played.  

## Indexing using demos' data
There are multiple data sources with which the tool can index demos, these include:
* Last Modified Date (from newest to oldest)
* Demo File Name (sorted alphabetically)
* Demo Map Name (sorted alphabetically)


## Indexing using a Custom Map Order
Alternatively, you can choose to use a *Custom Map Order* to index the demos. Maps are laid out line-by-line in an external file specified by the *Input List* field. The tool will go through the maps from top to bottom, for example:

```
map01
map02
map03
mapend
```
will tell the tool to place demos made in `map01` above those in `map02` and etc. These maps don't necessarily have to be of the same game and as such you can hoard map names of multiple games in the same file, provided there are no duplicates.

If there are multiple demos made in the same map, they are sorted alphabetically according to their names.  
Demos whose maps aren't included in the list will also be sorted alphabetically by map, then by name, and will be put at the end of the Demo List. For example, utilizing the example map list from before, if the demos are:
```
Demo Name           Map
-------------------------
c                   map01
a                   map01
z                   map02
outlier             map00
outlier2            map00
b                   map05
```
then the Demo List will be
```
Index       Demo Name       Map
---------------------------------
0           a               map01
1           c               map01
2           z               map02
3           outlier         map00
4           outlier2        map00
5           b               map05

```

";
		public HelpForm()
		{
			InitializeComponent();
			FillForm(_textDemoOrderFormatting, dispDOFormatting);
			FillForm(_textDemoOrderFormattingExamples, dispDOExamples);
			FillForm(_textSSConditions, dispSSConditions);
			FillForm(_textSSPositionCoordinate, dispSSPositionCoordinate);
			FillForm(_textWelcome, dispWelcome);
			FillForm(_textBasicGuide, dispBasicGuide);
			FillForm(_textDemoCheckingInfo, dispDCInfo);
			FillForm(_textDemoCheckingElements, dispDCElements);
			FillForm(_textDemoCheckingExmaples, dispDCExamples);
			FillForm(_textDemoIndexing, dispDemoIndexing);

			FormClosing += (s, e) =>
			{
				this.Hide();
				e.Cancel = true;
			};
		}

		private void FillForm(string text, WebBrowser targetDisp)
		{
			var html = Markdown.ToHtml(text);
			targetDisp.DocumentText = html;
		}
	}
}
