using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using LiveSplit.ComponentUtil;
using static System.Console;

namespace startdemos_plus
{
    class GameSupportHandler
    {
        private List<Evalutation> evaluations;

        public GameSupportHandler(string gameDir)
        {
            if (!File.Exists("gamesupport.xml"))
                return;

            XmlDocument xml = new XmlDocument();
            try
            {
                xml.Load("gamesupport.xml");
            }
            catch (Exception e) { WriteLine(e.ToString()); return; }

            var parent = xml.DocumentElement.SelectSingleNode($"/games/{gameDir}");
            if (parent == null)
                return;

            evaluations = new List<Evalutation>();
            var nodes = parent.SelectSingleNode($"/games/{gameDir}").ChildNodes;
            foreach (XmlNode node in nodes)
            {
                evaluations.Add(new Evalutation(node));
            }

        }

        public List<(ResultType, string, string)> Check(EvaluationDataType type, string curMap, int tick, object candidate)
        {
            var results = new List<(ResultType, string, string)>();
            Evalutation[] tmpList = new Evalutation[evaluations.Count];
            evaluations.CopyTo(tmpList);
            foreach (Evalutation eval in tmpList)
            {
                ResultType result = eval.Evaluate(type, curMap, tick, candidate);
                if (result != ResultType.None)
                {
                    results.Add((result, eval.EventName, candidate.ToString()));
                    if (result == ResultType.BeginOnce || result == ResultType.EndOnce)
                        evaluations.Remove(eval);
                }
            }
            return results;
        }
    }

    class Evalutation
    {
        public string Map { get; set; }
        public object[] Var { get; set; }
        public EvaluationDataType Type { get; set; }
        public EvaluationDirective Directive { get; set; }
        public ResultType ResType { get; set; }
        public string EventName { get; set; }
        public bool Not { get; set; }
        public ComparisonInfo<int> TickComparison { get; set; }
        public Evalutation(
            EvaluationDataType type,
            EvaluationDirective directive,
            ResultType resType,
            string varString,
            string map,
            string tickCondition = "",
            bool not = false,
            string eventName = "")
        {
            Map = map;
            Type = type;
            Directive = directive;
            ResType = resType;
            EventName = eventName;
            TickComparison = new ComparisonInfo<int>(tickCondition);
            Not = not;
            switch (type)
            {
                case EvaluationDataType.Position:
                    {
                        Var = new object[2];
                        string[] pos = varString.Split(' ');
                        Var[0] = new Vector3f(float.Parse(pos[0]), float.Parse(pos[1]), float.Parse(pos[2]));
                        if (Directive == EvaluationDirective.Difference)
                            Var[1] = new ComparisonInfo<float>(pos[3]);
                        break;
                    }
                case EvaluationDataType.ConsoleCommand:
                case EvaluationDataType.UserCommand:
                    {
                        Var = new object[1];
                        Var[0] = new string(varString.ToCharArray());
                        break;
                    }
            }
        }

        public Evalutation(XmlNode xmlNode) :
            this(
                (EvaluationDataType)Enum.Parse(typeof(EvaluationDataType), xmlNode.SelectSingleNode($"types/evaluation/type").InnerText),
                (EvaluationDirective)Enum.Parse(typeof(EvaluationDirective), xmlNode.SelectSingleNode($"types/evaluation/directive").InnerText),
                (ResultType)Enum.Parse(typeof(ResultType), xmlNode.SelectSingleNode($"types/result").InnerText),
                xmlNode.SelectSingleNode($"var").InnerText,
                xmlNode.SelectSingleNode($"map").InnerText ?? "",
                xmlNode.SelectSingleNode($"tickcompare")?.InnerText ?? "",
                bool.Parse(xmlNode.SelectSingleNode($"not")?.InnerText ?? "False"),
                xmlNode.SelectSingleNode($"name")?.InnerText ?? ""
            ) { }

        public ResultType Evaluate(EvaluationDataType type, string curMap, int tick, object candidate)
        {
            if (type != Type || ( Map != "" && curMap != Map) || (TickComparison.Active && !TickComparison.CompareTo(tick)))
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
                                if (Not ^ ((ComparisonInfo<float>)Var[1]).CompareTo(((Vector3f)Var[0]).Distance((Vector3f)candidate)))
                                    return ResType;
                                break;
                        }
                        break;
                    }
                case EvaluationDataType.ConsoleCommand:
                case EvaluationDataType.UserCommand:
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
        UserCommand
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
        Note
    }
}
