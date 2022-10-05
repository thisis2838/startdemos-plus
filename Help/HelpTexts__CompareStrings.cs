using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace startdemos_plus.Help
{
    public static partial class HelpTexts
    {
        public static string CompareStrings = @"
# Comparison Strings
*Comparison Strings* are specially-formatted strings which tell the tool how to evaluate values.
There are many types of *Comparison Strings*, each comparing to a different type of data.

## String Comparison  
*String Comparison* strings instruct the tool on how to evaluate a string.  
By default, the asterisk (*) is used as a wildcard, and represents any number of characters or none at all.
* For example: 
    * *abcd\*f* matches *abcdef*, *abcdf*, *abcd123f*; but doesn't match *abcd*, *abcf*.  
    * *\*abc* matches *1abc*, *gabc*; but doesn't match *abcd*, *eabcf*.

However, when the string is surrounded by a foward slash (/), the content within them are used as a regex pattern.  
* For example: 
    * */abcdef/* uses the regex *abcdef* to evaluate strings, which matches *abcdef*,  *fabcdef*
    * */[0-9]+/* uses the regex *[0-9]+* to evaluate strings, which matches *0123*, *4567*, but doesn't match *abcd*. 


## Numerical Comparison
*Numerical Comparison* strings instruct the tool on how to evaluate a number.  
They are written like a mathematical comparison.
* For example: 
    * *x<10* is true if the input number is smaller than 10
    * *25>x>3* is true if the input number is less than 25 and more than 3
    * *x>=10.365* is true if the number is higher than or equal to 10.365.  

The letter *x* is necessary and cannot be left out.  

## Positional Comparison  
*Positional Comparison* strings instruct the tool on how the evaluate a 3D point.  
They are written as a set of 3 numbers, corresponding to the X, Y, Z component of the target point, separated by a single space.  
* For example: *325 458.26 -658.14* checks if the input point has an X coordinate of *325*, Y coordinate of *458.26*, and a Z coordinate of *-658.14*.  

A Numerical Comparison can also be put at the end of the string, to tell the tool to evaluate the distance between the input point and the target point.  
* For example: *10 -20 30 25.3>x>10* checks if the input point is less than 25.3 and more than 10 units away from *10 -20 30*.

  
";
    }
}
