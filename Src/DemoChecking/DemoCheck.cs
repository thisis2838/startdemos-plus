using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using startdemos_plus.Utils;
using static startdemos_plus.Utils.Utils;
using static startdemos_plus.Utils.Comparisons;

namespace startdemos_plus.Src.DemoChecking
{
    public class DemoCheck
    {
        // REALLY DONT WANT SAME NAMES....
        public string DefaultName { get; private set; } = $"check_{DateTime.Now:s}_{new Random().Next(0, int.MaxValue)}";
        public string Name { get; private set; }
        public List<DemoCheckCondition> Conditions { get; private set; } = new List<DemoCheckCondition>();
        public List<DemoCheckAction> Actions { get; private set; } = new List<DemoCheckAction>();
        public DemoCheck(string name, params DemoCheckCondition[] conditions)
        {
            Name = (string.IsNullOrWhiteSpace(name)) ? DefaultName : name;
            foreach(var condition in conditions)
                Conditions.Add(condition);
        }

        public DemoCheck(string name, DemoCheckCondition[] conditions, DemoCheckAction[] actions)
        {
            Name = (string.IsNullOrWhiteSpace(name)) ? DefaultName : name;
            foreach (var condition in conditions)
                Conditions.Add(condition);
            foreach (var action in actions)
                Actions.Add(action);
        }

        // due to memory concerns, let's only store passed conditions
        public DemoCheckResult Check(DemoFile demo)
        {
            DemoCheckResult result = new DemoCheckResult(demo, this);
            bool passedDemoConds = false, passedOtherConds = false;

            var demoCond = Conditions.Where(x => x.IsDemoCondition).ToList()
                .ConvertAll(x => x.CheckDemo(demo)).ToList();
            passedDemoConds = (demoCond.All(x => x.Passed));
            demoCond.ForEach(x => { if (x.Passed) result.Passed.Add(x); });

            foreach (var tick in demo.Ticks)
            {
                var conds = Conditions.ConvertAll(x => x.CheckTick(tick)).Where(x => x != null).ToList();
                if (conds.All(x => x.Passed))
                {
                    result.Passed.AddRange(conds);
                    passedOtherConds = true;
                }
            }

            result.PassedAll = passedDemoConds && passedOtherConds;
            return result;
        }

        public override string ToString()
        {
            return $"{Name} : {Conditions.Count} conditions";
        }
    }

    public class DemoCheckResult
    {
        public DemoFile Demo { get; private set; }
        public DemoCheck Check { get; private set; }
        public List<DemoCheckConditionResult> Passed { get; private set; } = new List<DemoCheckConditionResult>();
        public bool PassedAll;

        public DemoCheckResult(DemoFile demo, DemoCheck check)
        {
            Demo = demo;
            Check = check;
        }

        public List<DemoCheckAction> GetActions(params DemoCheckActionType[] types)
        {
            if (types == null || types.Count() == 0)
                return Check.Actions;
            else return Check.Actions.Where(x => types.Contains(x.Type)).ToList();
            
        }

        public override string ToString()
        {
            return $"{Demo} : {(PassedAll? "Passed" : "Failed")} {Check}";
        }
    }
}
