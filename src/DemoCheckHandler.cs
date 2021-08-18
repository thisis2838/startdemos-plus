using startdemos_ui.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using static startdemos_ui.Utils.EnumHelper;

namespace startdemos_ui.src
{
    public class DemoCheckHandler
    {
        public List<Evaluation> Evaluations;
        public string Name { get; set; }

        public DemoCheckHandler(XmlNode nodes)
        {
            if (nodes == null)
                return;

            Name = nodes.Name;

            Evaluations = new List<Evaluation>();
            foreach (XmlNode node in nodes.ChildNodes)
                Evaluations.Add(new Evaluation(node));
        }

        public DemoCheckHandler(string name)
        {
            Name = name;
            Evaluations = new List<Evaluation>();
        }

        public DemoCheckHandler(DemoCheckHandler other)
        {
            Name = other.Name;
            Evaluations = new List<Evaluation>();
            other.Evaluations.ForEach(x => Evaluations.Add(x));
        }

        public List<DemoCheckResult> Check(EvaluationDataType type, string curMap, int tick, object candidate)
        {
            var results = new List<DemoCheckResult>();

            if (Evaluations.Count == 0)
                return results;

            Evaluation[] tmpList = new Evaluation[Evaluations.Count];
            Evaluations.CopyTo(tmpList);
            foreach (Evaluation eval in tmpList)
            {
                ResultType result = eval.Evaluate(type, curMap, tick, candidate);
                if (result != ResultType.None)
                {
                    results.Add(new DemoCheckResult(result, tick, eval.EventName, candidate.ToString()));
                    if (result == ResultType.BeginOnce || result == ResultType.EndOnce)
                        Evaluations.Remove(eval);
                }
            }
            return results;
        }
    }

    public class Evaluation
    {
        public string Map { get; set; }
        public object[] Var { get; set; }
        public EvaluationDataType Type { get; set; }
        public EvaluationDirective Directive { get; set; }
        public ResultType ResType { get; set; }
        public string EventName { get; set; }
        public bool Not { get; set; }
        public Comparisons TickComparison { get; set; }
        public Evaluation(
            EvaluationDataType type,
            EvaluationDirective directive,
            ResultType resType = ResultType.None,
            string varString = "",
            string map = "",
            string tickCondition = "",
            bool not = false,
            string eventName = "")
        {
            Map = map;
            Type = type;
            Directive = directive;
            ResType = resType;
            EventName = eventName;
            TickComparison = new Comparisons(tickCondition);
            Not = not;
            ParseVar(varString);
        }

        public Evaluation(XmlNode xmlNode) :
            this(
                ParseEnum<EvaluationDataType>(xmlNode.SelectSingleNode($"types/evaluation/type").InnerText),
                ParseEnum <EvaluationDirective>(xmlNode.SelectSingleNode($"types/evaluation/directive").InnerText),
                ParseEnum<ResultType>(xmlNode.SelectSingleNode($"types/result").InnerText),
                xmlNode.SelectSingleNode($"var").InnerText,
                xmlNode.SelectSingleNode($"map").InnerText ?? "",
                xmlNode.SelectSingleNode($"tickcompare")?.InnerText ?? "",
                bool.Parse(xmlNode.SelectSingleNode($"not")?.InnerText ?? "False"),
                xmlNode.SelectSingleNode($"name")?.InnerText ?? ""
            )
        { }

        public string VarToString()
        {
            if (Var == null || Var.All(x => x == null) || Var.Count() == 0)
                return "";

            string var = Var[0].ToString();
            for (int i = 1; i < Var.Count(); i++)
                var += $" / {Var[i] ?? ""}";

            return var;
        }

        public void ParseType(EvaluationDataType type)
        {
            Type = type;
            if (Var != null && !Var.All(x => x == null))
                ParseVar(VarToString());
        }

        public void ParseDirective(EvaluationDirective directive)
        {
            Directive = directive;
            if (Var != null && !Var.All(x => x == null))
                ParseVar(VarToString());
        }

        public void ParseVar(string varString)
        {
            switch (Type)
            {
                case EvaluationDataType.Position:
                    {
                        Var = new object[2];
                        string[] elements = varString.Split('/');
                        string[] pos = new string[3] { "0", "0", "0" };
                        string[] inputPos = elements[0].Split(' ');
                        for (int i = 0; i < inputPos.Length; i++)
                            if (!string.IsNullOrWhiteSpace(inputPos[i]))
                                pos[i] = inputPos[i];

                        Var[0] = new Vector3f(float.Parse(pos[0]), float.Parse(pos[1]), float.Parse(pos[2]));
                        if (Directive == EvaluationDirective.Difference)
                             Var[1] = new ComparisonInfo(elements.Count() > 1 ? elements[1] : "");
                        break;
                    }
                default:
                    {
                        Var = new object[1];
                        Var[0] = new string(varString.ToCharArray());
                        break;
                    }
            }
        }

        public ResultType Evaluate(EvaluationDataType type, string curMap, int tick, object candidate)
        {
            if (Type == EvaluationDataType.None
                || Directive == EvaluationDirective.None
                || ResType == ResultType.None
                || type != Type 
                || (Map != "" && curMap != Map) 
                || (!TickComparison.CompareTo(tick)))
                return ResultType.None;

            switch (type)
            {
                case EvaluationDataType.Position:
                    {
                        switch (Directive)
                        {
                            case EvaluationDirective.Direct:
                                if (Not ^ ((Vector3f)Var[0]).BitEquals((Vector3f)candidate))
                                    return ResType;
                                break;
                            case EvaluationDirective.Difference:
                                if (Not ^ ((ComparisonInfo)Var[1]).CompareTo(((Vector3f)Var[0]).Distance((Vector3f)candidate)))
                                    return ResType;
                                break;
                        }
                        break;
                    }
                case EvaluationDataType.ConsoleCommand:
                case EvaluationDataType.UserCommand:
                case EvaluationDataType.DemoName:
                    {
                        if (!Regex.IsMatch(candidate.ToString(), @"([ -~])+"))
                            return ResultType.None;

                        if (string.IsNullOrWhiteSpace((string)Var[0]))
                            return ResType;

                        switch (Directive)
                        {
                            case EvaluationDirective.Direct:
                                {
                                    if (Not ^ (string)Var[0] == (string)candidate)
                                        return ResType;
                                    break;
                                }
                            case EvaluationDirective.Substring:
                                {
                                    if (Not ^ ((string)candidate).Contains((string)Var[0]))
                                        return ResType;
                                    break;
                                }
                        }
                        break;
                    }
            }

            return ResultType.None;

        }
    }

    public enum EvaluationDataType
    {
        None,
        Position,
        ConsoleCommand,
        UserCommand,
        DemoName
    }

    public enum EvaluationDirective
    {
        None,
        Direct,
        Difference,
        Substring
    }

    public enum ResultType
    {
        None,
        BeginOnce,
        BeginMultiple,
        EndOnce,
        EndMultiple,
        Note,
        Remember
    }

    public struct DemoCheckResult
    {
        public DemoCheckResult(ResultType result, int tick, string name, string evaluatedValue)
        {
            Result = result;
            Tick = tick;
            Name = name;
            EvaluatedValue = evaluatedValue;
        }
        public ResultType Result;
        public int Tick;
        public string Name;
        public string EvaluatedValue;
    }
}
