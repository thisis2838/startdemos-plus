using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace startdemos_plus
{
    public class ComparisonInfo<T> where T : IComparable<T>
    {
        public bool Active { get; internal set; } = false;
        public ComparisonOperator Operator { get; set; }
        public bool Equal { get; set; }
        public T Number { get; set; }
        public ComparisonInfo(string input)
        {
            if (input == "")
                return;

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
            Number = (T)Convert.ChangeType(input.Trim(new char[] { '>', '<', '=' }), typeof(T));
            Active = true;

        }

        public bool CompareTo(T candidate)
        {
            bool returner = false;
            switch (Operator)
            {
                case ComparisonOperator.Greater:
                    returner = Comparer<T>.Default.Compare((T)Number, candidate) < 0;
                    break;
                case ComparisonOperator.Lower:
                    returner = Comparer<T>.Default.Compare((T)Number, candidate) > 0;
                    break;
            }

            if (!returner && Equal)
                returner = Comparer<T>.Default.Compare((T)Number, candidate) == 0;

            return returner;
        }
    }
}
