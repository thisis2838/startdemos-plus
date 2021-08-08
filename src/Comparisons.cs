using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace startdemos_ui.src
{
    public class ComparisonInfo
    {
        public ComparisonOperator Operator { get; set; }
        public bool Equal { get; set; }
        public float Number { get; set; }
        public bool Effective => Operator != ComparisonOperator.None;
        public ComparisonInfo(string input)
        {
            if (string.IsNullOrWhiteSpace(input) || input.Contains("x"))
                return;

            input = input.Trim(' ');

            Operator = ComparisonOperator.Equal;
            switch (input[0])
            {
                case '>':
                    Operator = ComparisonOperator.Greater;
                    break;
                case '<':
                    Operator = ComparisonOperator.Lower;
                    break;
                case '=':
                    Operator = ComparisonOperator.Equal;
                    break;
            }

            Equal = input.Contains('=');
            Number = float.Parse(input.Trim(new char[] { '>', '<', '=' }));
        }

        public bool CompareTo(float candidate)
        {
            if (!Effective)
                return true;

            bool returner = false;
            switch (Operator)
            {
                case ComparisonOperator.Greater:
                    returner = Number < candidate;
                    break;
                case ComparisonOperator.Lower:
                    returner = Number > candidate;
                    break;
            }

            if (!returner && Equal)
                returner = Number == candidate;

            return returner;
        }

        public override string ToString()
        {
            string name = "";

            if (!Effective)
                return "x";

            switch (Operator)
            {
                case ComparisonOperator.Equal:
                    name = "=";
                    break;
                case ComparisonOperator.Greater:
                    name = ">";
                    break;
                case ComparisonOperator.Lower:
                    name = "<";
                    break;
            }

            if (name != "=" && Equal)
                name += "=";

            name += Number.ToString();
            return name;
        }

        public bool CompareTo(int candidate)
        {
            return CompareTo((float)candidate);
        }

        public enum ComparisonOperator
        {
            None,
            Equal,
            Greater,
            Lower
        }
    }
}
