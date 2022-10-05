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
    public enum DemoCheckVariable
    {
        [Description("Map Name")]
        MapName,
        [Description("Demo Name")]
        DemoName,
        [Description("Player Name")]
        PlayerName,
        [Description("Demo Tick Count")]
        DemoTickCount,
        [Description("Command")]
        Command,
        [Description("Player Position")]
        PlayerPosition,
        [Description("Tick Index")]
        TickIndex
    }
    public class DemoCheckCondition
    {
        public DemoCheckVariable Variable { get; private set; }
        public string Condition { get; private set; }
        public bool Not { get; private set; }

        public DemoCheckCondition(
            DemoCheckVariable var,
            string cond,
            bool not)
        {
            Variable = var;
            Condition = cond;
            Not = not;
        }

        public DemoCheckCondition(string var, string cond, string not) :
            this(
                GetValueFromDescription<DemoCheckVariable>(var),
                cond,
                bool.Parse(not))
        { }

        public DemoCheckConditionResult CheckDemo(DemoFile demo)
        {
            if (!IsDemoCondition)
                return null;

            switch (Variable)
            {
                case DemoCheckVariable.MapName:
                    return new DemoCheckConditionResult(this, Not ^ StringCompare(Condition, demo.MapName), value: demo.MapName);
                
                case DemoCheckVariable.DemoName:
                    return new DemoCheckConditionResult(this, Not ^ StringCompare(Condition, demo.Name), value: demo.Name);
                
                case DemoCheckVariable.PlayerName:
                    return new DemoCheckConditionResult(this, Not ^ StringCompare(Condition, demo.PlayerName), value: demo.Name);
                
                case DemoCheckVariable.DemoTickCount:
                    return new DemoCheckConditionResult(this, Not ^ NumericCompare(Condition, demo.TotalTicks), value: demo.Name);
            }

            return null;
        }

        public DemoCheckConditionResult CheckTick(DemoTick tick)
        {
            if (IsDemoCondition)
                return null;

            switch (Variable)
            {
                case DemoCheckVariable.TickIndex:
                    return new DemoCheckConditionResult(this, Not ^ NumericCompare(Condition, tick.Index), tick, tick.Index);
                
                case DemoCheckVariable.PlayerPosition:
                    return new DemoCheckConditionResult(this, (tick.PlayerPosition != null &&
                        (Not ^ VecCompare(Condition, tick.PlayerPosition ?? Vector3f.Zero))), tick, tick.PlayerPosition);
               
                case DemoCheckVariable.Command:
                    return new DemoCheckConditionResult(this, 
                        Not ^ tick.Commands.Any(x => StringCompare(Condition, x)), tick, tick.Commands);
            }

            return null;
        }

        public bool IsDemoCondition => (int)Variable < 4;

        public override string ToString()
        {
            return $"{Variable} : {Condition} | be {!Not}";
        }
    }
    public class DemoCheckConditionResult
    {
        public DemoCheckCondition Condition { get; private set; }
        public bool Passed { get; private set; } = false;
        public DemoTick Tick { get; private set; }
        public object Value { get; private set; }
        public DemoCheckConditionResult(DemoCheckCondition condition, bool passed, DemoTick tick = null, object value = null)
        {
            Condition = condition;
            Passed = passed;
            Value = value;
            Tick = tick;
        }
        public string GetValueString()
        {
            if (Value == null)
                return "";

            if (Value is IEnumerable<string>)
                return string.Join(", ", (Value as IEnumerable<string>));

            return Value.ToString();
        }
    }
}
