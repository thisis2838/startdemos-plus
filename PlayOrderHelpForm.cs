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

namespace startdemos_plus
{
    public partial class PlayOrderHelpForm : Form
    {
        static string d = @"
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

        static string e = @"
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

        public PlayOrderHelpForm()
        {
            InitializeComponent();
            var html = Markdown.ToHtml(d);
            var html2 = Markdown.ToHtml(e);
            display.DocumentText = html;
            example.DocumentText = html2;
        }
    }
}
