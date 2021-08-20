using startdemos_ui.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using static startdemos_ui.src.ComparatorDefaults;

namespace startdemos_ui.src
{
    public enum ComparisonOperator
    {
        None,
        Equal,
        Greater,
        Lower,
        Substring,
        Regex
    }

    static class ComparatorDefaults
    {
        public static string _default = "";
    }

    public abstract class Comparator<T>
    {
        public ComparisonOperator Operator { get; set; }
        internal Dictionary<ComparisonOperator, string> _operators;
        public T Target { get; set; }
        public virtual bool Effective => Operator != ComparisonOperator.None;
        public virtual void Init(string input)
        {

        }
        public virtual bool CompareTo(T candidate)
        {
            return true;
        }

        public virtual string Clean(string input)
        {
            return input.Trim(' ').Replace("\\\"", "");
        }

        public override string ToString()
        {
            if (!Effective)
                return _default;

            return $"{_operators[Operator]}{Target}";
        }
    }

    public class NumericalComparator : Comparator<double>
    {
        private bool _equal = false;
        public NumericalComparator() : this("") { }
        public NumericalComparator(string input)
        {
            _operators = new Dictionary<ComparisonOperator, string>()
            {
                { ComparisonOperator.Greater, ">" },
                { ComparisonOperator.Lower, "<"  },
            };

            Init(input);
        }
        public override void Init(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return;

            input = Clean(input);

            _equal = input.Contains('=');
            input = input.Replace("=", "");

            foreach (var entry in _operators)
            {
                if (input.Contains(entry.Value))
                {
                    Operator = entry.Key;
                    input = input.Replace(entry.Value, "");
                }
            }

            Target = double.Parse(input);
        }

        public override bool CompareTo(double candidate)
        {
            if (!Effective)
                return true;

            bool returner = false;
            switch (Operator)
            {
                case ComparisonOperator.Greater:
                    returner = Target < candidate;
                    break;
                case ComparisonOperator.Lower:
                    returner = Target > candidate;
                    break;
            }

            if (!returner && _equal)
                returner = Target == candidate;

            return returner;
        }

        public bool CompareTo(int candidate)
        {
            return CompareTo((float)candidate);
        }

        public override string ToString()
        {
            if (!Effective)
                return _default;

            return $"{_operators[Operator]}{(_equal ? "=" : "")}{Target}";
        }
    }

    public class StringComparator : Comparator<string>
    {
        public StringComparator() : this("") { }
        public StringComparator(string input)
        {
            _operators = new Dictionary<ComparisonOperator, string>()
            {
                { ComparisonOperator.Equal, "=" },
                { ComparisonOperator.Substring, "*"  },
                { ComparisonOperator.Regex, "@"  }
            };

            Init(input);
        }
        public override void Init(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return;

            input = Clean(input);

            Operator = ComparisonOperator.Equal;

            foreach (var entry in _operators)
            {
                if (input[0].ToString() == entry.Value)
                {
                    Operator = entry.Key;
                    input = input.Substring(1);
                    break;
                }
            }

            Target = input;
        }

        public override bool CompareTo(string candidate)
        {
            if (!Effective)
                return true;

            bool returner = false;
            switch (Operator)
            {
                case ComparisonOperator.Equal:
                    returner = Target == candidate;
                    break;
                case ComparisonOperator.Substring:
                    returner = candidate.Contains(Target);
                    break;
                case ComparisonOperator.Regex:
                    returner = Regex.IsMatch(candidate, Target);
                    break;
            }
            return returner;
        }
    }

    public class NumericalComparison : Comparison<NumericalComparator, double>
    {
        public NumericalComparison(string input) : base(input) { }
    }

    public class StringComparison : Comparison<StringComparator, string>
    {
        public StringComparison(string input) : base(input) { }
    }

    public class Comparison<T, E> where T : Comparator<E>, new()
    {
        private List<Token> Members;
        public bool Effective => InfoList?.All(x => x.Effective) ?? false;
        public List<T> InfoList { get; internal set; }
        private string _input;
        public Comparison(string input)
        {
            _input = input;
            List<Token> members = (new Parser(input)).Result;
            Members = new List<Token>();
            InfoList = new List<T>();

            if (members.Count == 0)
                _input = _default;

            foreach (Token t in members)
            {
                Token s = new Token();
                if (t.Type == TokenType.Member)
                {
                    var d = new T(); d.Init(t.Value);
                    InfoList.Add(d);
                    s.Value = (InfoList.Count - 1).ToString();
                }
                else
                    s.Value = t.Value;
                s.Type = t.Type;
                Members.Add(s);
            }
        }

        public bool CompareTo(E input)
        {
            if (Members.Count == 0 || !Effective)
                return true;

            Stack<bool> num = new Stack<bool>();
            foreach (Token token in Members)
            {
                switch (token.Type)
                {
                    case TokenType.Member:
                        num.Push(InfoList[int.Parse(token.Value)].CompareTo(input));
                        break;
                    case TokenType.Operator:
                        {
                            bool res = false;

                            switch (token.Value)
                            {
                                case "|":
                                case "&":
                                case "^":
                                    {
                                        bool num2 = num.Pop();
                                        bool num1 = num.Pop();

                                        switch (token.Value)
                                        {
                                            case "|":
                                                res = num1 | num2;
                                                break;
                                            case "&":
                                                res = num1 & num2;
                                                break;
                                            case "^":
                                                res = num1 ^ num2;
                                                break;
                                        }
                                        break;
                                    }

                                case "!":
                                    bool num0 = num.Pop();
                                    res = !num0;
                                    break;
                            }
                            num.Push(res);
                            break;
                        }
                }
            }

            return num.Pop();
        }

        public override string ToString()
        {
            return _input;
        }
    }
}
