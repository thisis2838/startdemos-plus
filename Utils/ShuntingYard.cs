using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace startdemos_ui.Utils
{
    public enum TokenType
    {
        Member,
        Parenthesis,
        Operator,
        WhiteSpace
    };

    public struct Token
    {
        public TokenType Type { get; set; }
        public string Value { get; set; }

        public override string ToString() => $"{Type}: {Value}";

        public Token(TokenType type, string value)
        {
            Type = type;
            Value = value;
        }
    }

    class Operator
    {
        public string Name { get; set; }
        public int Precedence { get; set; }
        public bool RightAssociative { get; set; }
    }

    public class Parser
    {
        public List<Token> Result { get; private set; }

        public Parser(string input)
        {
            Result = ShuntingYard(Tokenize(new StringReader(input))).ToList();
            Console.WriteLine(String.Join(" ", Result.Select(x => x.Value)));
        }

        private IDictionary<string, Operator> operators = new Dictionary<string, Operator>
        {
            ["|"] = new Operator { Name = "|", Precedence = 1 },
            ["!"] = new Operator { Name = "!", Precedence = 2, RightAssociative = true },
            ["&"] = new Operator { Name = "&", Precedence = 1 },
            ["^"] = new Operator { Name = "^", Precedence = 1 },
        };

        private bool CompareOperators(Operator op1, Operator op2)
        {
            return op1.RightAssociative ? op1.Precedence < op2.Precedence : op1.Precedence <= op2.Precedence;
        }

        private bool CompareOperators(string op1, string op2) => CompareOperators(operators[op1], operators[op2]);

        private TokenType DetermineType(string ch)
        {
            if (ch == "(" || ch == ")")
                return _ignoreParentheses ? TokenType.Member : TokenType.Parenthesis;
            if (operators.ContainsKey(Convert.ToString(ch)))
                return TokenType.Operator;
            return TokenType.Member;
        }

        private bool _ignoreParentheses = false;

        public IEnumerable<Token> Tokenize(TextReader reader)
        {
            var token = new StringBuilder();

            int curr;
            while ((curr = reader.Read()) != -1)
            {
                var ch = (char)curr;
                token.Append(ch);

                if (ch == '/' && (char)reader.Peek() == '"')
                {
                    _ignoreParentheses = !_ignoreParentheses;
                    continue;
                }
                var currType = DetermineType(ch.ToString());


                var next = reader.Peek();
                var nextType = next != -1 ? DetermineType(((char)next).ToString()) : TokenType.WhiteSpace;
                if (currType != nextType || nextType == currType && currType != TokenType.Member)
                {
                    yield return new Token(currType, token.ToString());
                    token.Clear();
                }
            }
        }

        public IEnumerable<Token> ShuntingYard(IEnumerable<Token> tokens)
        {
            var stack = new Stack<Token>();
            foreach (var tok in tokens)
            {
                switch (tok.Type)
                {
                    case TokenType.Member:
                        yield return tok;
                        break;
                    case TokenType.Operator:
                        while (stack.Any() && stack.Peek().Type == TokenType.Operator && CompareOperators(tok.Value, stack.Peek().Value))
                            yield return stack.Pop();
                        stack.Push(tok);
                        break;
                    case TokenType.Parenthesis:
                        if (tok.Value == "(")
                            stack.Push(tok);
                        else
                        {
                            while (stack.Peek().Value != "(")
                                yield return stack.Pop();
                            stack.Pop();
                        }
                        break;
                    default:
                        throw new Exception("Wrong token");
                }
            }
            while (stack.Any())
            {
                var tok = stack.Pop();
                if (tok.Type == TokenType.Parenthesis)
                    throw new Exception("Mismatched parentheses");
                yield return tok;
            }
        }
    }
}