using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using startdemos_plus.Utils;

namespace startdemos_plus.Utils
{
    public static class Comparisons
    {
        private static string _varName = "x";
        private static bool _onFalseFormat = true;

        public static bool NumericCompare(string fmt, decimal number)
        {
            bool compare(decimal a, decimal b, string op)
            {
                if (op.Contains('=') && a == b)
                    return true;   
                
                op = op.Replace("=", "");
                switch(op)
                {
                    case "<": return a < b;
                    case ">": return a > b;
                }

                return false;
            }

            const string condSingle = @"^(?:x([><=]{1,2})([0-9\.]+))$";
            const string condDouble = @"^([0-9\.]+)([><=]{1,2})(?:x)([><=]{1,2})([0-9\.]+)$";

            if (string.IsNullOrWhiteSpace(fmt))
                return _onFalseFormat;

            fmt = fmt.Replace(" ", "");

            Match match;

            if ((match = Regex.Match(fmt, condSingle)).Success)
                return compare(number, decimal.Parse(match.Groups[2].Value), match.Groups[1].Value);
            else if ((match = Regex.Match(fmt, condDouble)).Success)
                return
                    compare(decimal.Parse(match.Groups[1].Value), number, match.Groups[2].Value) &&
                    compare(number, decimal.Parse(match.Groups[4].Value), match.Groups[3].Value);

            return false;
        }

        public static bool StringCompare(string fmt, string str)
        {
            str = str == null ? "" : str;

            if (fmt == "")
                return _onFalseFormat;

            if (fmt.StartsWith("/") && fmt.EndsWith("/"))
                return Regex.IsMatch(str, fmt.Trim('/'));
            if (StringWildcardCompare(fmt, str)) return true;

            return false;
        }
        private static bool StringWildcardCompare(string format, string input)
        {
            if (input == "" || format == "" || !format.Contains('*'))
                return input == format;

            bool first = format[0] != '*';
            bool last = format[format.Count() - 1] != '*';

            List<string> elements = format.Split('*').ToList();
            elements = elements.Where(x => !string.IsNullOrEmpty(x)).ToList();

            while (input != "" && elements.Count() > 0)
            {
                int index = (elements.Count != 1) ? input.IndexOf(elements[0]) : input.LastIndexOf(elements[0]);
                if (index == -1)
                    return false;

                input = input.Substring(index);

                if (elements.Count == 1)
                {
                    if (last && elements[0] != input)
                        return false;
                }

                if (first && index != 0)
                    return false;

                elements.RemoveAt(0);
                first = false;
            }

            return true;
        }

        public static bool VecCompare(string fmt, Vector3f vec)
        {
            fmt = fmt.Trim().Replace("  ", " ");

            var members = fmt.Split(' ');
            if (members.Count() != 3 && members.Count() != 4)
                return _onFalseFormat;

            Vector3f targ = new Vector3f();
            if (float.TryParse(members[0], out float x) &&
                float.TryParse(members[1], out float y) &&
                float.TryParse(members[2], out float z))
                targ = new Vector3f(x, y, z);
            else return _onFalseFormat;
            
            switch (members.Count())
            {
                case 3:
                    return vec == targ;
                case 4:
                    return NumericCompare(members[3], (decimal)vec.Distance(targ));
            }

            return true;
        }
    }
}
