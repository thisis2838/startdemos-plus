using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpAndGuides
{
    static class Texts
    {
		static public string textDemoOrderFormatting = @"
## Stages
Demo playback is split into stages, which are handled one after another. When inputted, stages are separated using commas
( , ). For example:<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;`1, 2, 4, ra-, 25-36/>=35`  
Represents 4 stages, each playing a different set of demos in specific orders.

## Formatting a stage
Each stage is formatted as such:  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;`<attributes> <index or array of demos> / <tick condition>`  
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
This the condition to apply on the tick counts of demos to filter them. Note that this is done after Sorting. 
It is formatted as a comparison, for example:  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;`<10`  
means to only select demos that are less than 10 ticks long.  
Accepted operators include: `>=`, `<=`, `>`, `<`, `=`. Note that the tick amount must always be preceeded by the operator.
";

		static public string textDemoOrderFormattingExamples = @"
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
";

		static public string textDemoCheckInfo = @"
## Info
Demos contain information about entities and network data, including player position and console commands sent by both the game and the player.  
startdemos+, currently, is only able to monitor those 2 values, however you can customize how that data is collected through various conditions.

## Prerequisites
Before defining these checksm you'll need to make sure that:
* A file called `gamesupport.xml` exists next to the startdemos+ executable. This is where you'll be defining the checks.
* Knowing what your game directory name is. 

## Note
Since the format of this file is xml, we need to keep note of character escaping. The ones needed in configuring checks are:

* `>` is escaped as `&gt;`
* `<` is escaped as `&lt;`  

As such, for example, when writing `>=`, you should write it as `$gt;=` in the code to prevent problems.  
For simplicity's sake, we'll be writing these as is without escaping them in this guide.
";

		static public string textDemoCheckSyntax = @"
This is a typical entry in the Checks list. 
```
<games>
<gameDir>
	<check>
		<var>-544 -368.75 160</var>
		<types>  
			<evaluation>
				<type>Position</type>
				<directive>Direct</directive>
			</evaluation>  
			<result>Begin</result>
		</types>
		<map>testchmb_a_00</map>
		<tickcompare><=0</tickcompare>
		<not>True</not>
		<name>Crosshair appear</name>
	</check>
</gameDir>
</games>
```

Let's dissect each of these tags one by one.

## `<games>`
This is the outmost tag, and is necessary for parsing the file.

## `<gameDir>`
This indicates what games these checks apply for. The tag name is the  game directory name, for example:  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; `<portal>` for Portal.  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; `<hl2>` for Half-Life 2.  
The tool will use this info to compare with that of the demos, to determine what list of checks should be ran.

## `<check>`
This indicates a new check definition

## `<types>`
This block defines the types used in the check. These include:  
* `<evaluation>` This tag determines what kind of data should the comparisons be made against and how those comparisons. It includes:
	* `<type>` which determines what data should be used. It accepts:
		* `Position` for current player position.
		* `ConsoleCommand` for commands sent by the game.
		* `UserCommand` for command sent by the player.
	* `<directive>` which determines how to work with this data. This accepts:
		* `Direct` for a direct comparison betweent target and demo data.
		* `Difference` (only applies `Position`) compares the difference between  target and demo data.
		* `Substring` (only applies to commands) checks if demo data contains the our target data.
* `<result>` This tag determines what to do when the check passes on a certain demo tick. Entrties include:
	* `BeginOnce` and `BeginMultiple` which marks the current tick as the start point of the demo.
	* `EndOnce` and `EndMultiple` which marks the current tick as the end of the demo.  
	Both `Begin` and `End` modifies the Adjusted Tick variable of Demo accordingly. The `Once` variants also disable the check indefinitely for the rest of the demos.
	* `Note` only notifies the tool of the check's passing, and do not modify demo timing in any way.
		
## `<var>`
This tag determines the *target* data against which data from the demo are compared. The way this is formatted depends on the data type specified in the `<types>`:
* Positional data is formatted in the same way as described in *Shared Syntax and Formatting*. If the `<directive>` is `Difference` then an additional Conditional is appended at the end. For example:  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; `-544 -368.75 160 >10`  
means checking if the player's position is more than 10 units further away from `-544 -368.75 160`.  
* String data is written without quotes. If this is empty and the `<type>` is a Command then the check will pass every time.

## `<map>`
This tag determines what map should this check be run on. If this is left blank or omitted from the definition then the checks will be run regardless of map.

## `<tickcompare>`
This tag determines the a Conditional accepting an integer against which the current tick of the demo is compared. If the current tick does not pass this comparison, the checks will not be run for that tick.  
For example:  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; `>0`  
means only run this check if the current tick higher than 0.

## `<not>`
This tag determines if the evaluations in the check should be reversed or not.  If this is left blank or omitted from the definition then this will default to False.

## `<name>` 
This tag determines the name of the check. This is purely for identification when results are printed. 
	

";

		public static string textGeneralFormatting = @"
## Conditional
A Conditional describes a comparison which is done in the tool against numeric values.  
A Conditional is formatted like so:  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; `<operator><value>`  
Where:  
*   `<operator>` is the desired comparision operator, which includes:
	* `>` to signify that our candidate must be greater than `<value>`.
	* `<` to signify that our candidate must be smaller than `<value>`.
	* `=` to signify that our candidate must be equal to `<value>`. This sign can be placed after `>` or `<` to signify that the number can also equal to our `<value>` in those cases.
* `<value>` is the desired value that is compared against, its type depends on its use case.

Examples:  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; `>9` means our number must exceed 9.  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; `<=3` means our number must be 3 or lower.  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; `>=12.256` means our number must be 12.256 or higher.

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

		public static string textDemoCheckExamples = @"
Here are some example check definitions with explanation of what they do.

```
<check2>
	<types>
		<evaluation>
			<type>ConsoleCommand</type>
			<directive>Direct</directive>
		</evaluation>
		<result>EndOnce</result>
	</types>
	<var>startneurotoxins 99999</var>
	<map>escape_02</map>
	<name>Crosshair disappear</name>
</check2>
```

* `<type>ConsoleCommand</type>`  
This check should be done on game-inputted Console Commands.
* `<directive>Direct</directive>`  
Our data target data is directly compared against the demo's data.
* `<result>End</result>`  
The tick on which this check passes should be the last tick in that demo. And this check must only be passed once.
* `<var>startneurotoxins 99999</var>`  
Our target data, the command `startneurotoxins 99999`
* `<map>escape_02</map>`  
This check must only be done if the demo's map is `escape_02`
* `<name>Crosshair disappear</name>`  
The name of this check is `Crosshair disappear`, which is what the tool should refer to this as when printing relevant info on.

When combined together, this check will tell the tool to: If a demo is recorded in `escape_02`, search the Console Command found in each tick and see if it is exactly `startneurotoxins 99999`. If so, disable this check indefinitely and modify the demo's adjust time accordingly.

___


```
<check1>
	<types>
		<evaluation>
			<type>ConsoleCommand</type>
			<directive>Substring</directive>
		</evaluation>
		<result>EndMultiple</result>
	</types>
	<var>#SAVE#</var>
	<tickcompare>>0</tickcompare>
	<name>End Segment</name>
	<map></map>
</check1>
```

* `<type>ConsoleCommand</type>`  
This check should be done on game-inputted Console Commands.
* `<directive>Substring</directive>`  
The target data is a substring of the demo's data.
* `<result>EndMultiple</result>`  
The tick on which this check passes should be the last tick in that demo. And this check can be passed multiple times without limit.
* `<var>#SAVE#</var>`  
Our target data, the substring `#SAVE#`
* `<tickcompare>>0</tickcompare>`  
This check should only be done when the tick count is over 0.
* `<map></map>`  
This check can be done on all maps.
* `<name>Segment End</name>`  
The name of this check is `Segment End`, which is what the tool should refer to this as when printing relevant info on.

When combined together, this check will tell the tool to: Check all demos and find mentions of `#SAVE#` in Console Commands. If a match is found, only adjust the end tick for that demo accordingly and keep the check active.

___


```	
<check3>
	<types>
		<evaluation>
			<type>Position</type>
			<directive>Difference</directive>
		</evaluation>
		<result>Note</result>
	</types>
	<var>-544 -368.75 160 >256</var>
	<not>True</not>
	<map></map>
	<name>Near</name>
</check3>
```

* `<type>Position</type>`  
This check should be done on the player's position.
* `<directive>Difference</directive>`  
This check should monitor the difference between the player's coordinates and a point in space.
* `<result>Note</result>`  
This check only notifies the tool of is check passing, and can fire again in the same demo.
* `<var>-544 -368.75 160 >256</var>`  
This check find the distance between the player's position and `-544 -368.75 160`, and will pass if that distance is over `256` units.
* `<not>True</not>`  
This check's comparisons are reversed.
* `<map></map>`  
This check can be done on all maps.
* `<name>Away</name>`  
This check's name is Away.  

When combined together, this check will tell the tool to: Record every single tick in which the player is NOT `>256` (less or equal than 256) units away from `-544 -368.75 160`.



";

		public static string textAbout = @"
## startdemos+
An external alternative to the in-game Source command `startdemos`.  

Features include:
* Listing demos and indexing and sorting data using user-definable scripts.
* Playing Demos in any order, including sorting.
* Game Memory Monitoring and External Function Calling to queue demos up for playing.


## Credts
* 2838  
Project lead, Programming
* Donaldinio  
Original concept, Testing.  
* Traderain  
Listdemo- codebase which was used in the Demo processing code.

Version dated August 2021. Please mention any encountered bug at [the project's Github page](https://github.com/thisis2838/startdemos-plus/issues)!
";

		public static string textToolsAbout = @"
## Tools
As settings are saved in `.xml` format and can be disorentating or time-consuming to edit at times. These tools have been created to hopefully speed up this process.  
These include:
* Demo Check Editor for editing Demo Checking routines.
* startdemos+ Settings Editor for editing startdemos+ configurations.
";
	}
}
