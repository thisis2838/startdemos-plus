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
    public enum DemoCheckActionType
    {
        [Description("Start Demo Time")]
        StartDemoTime,
        [Description("End Demo Time")]
        EndDemoTime
    }
    public class DemoCheckAction
    {
        public DemoCheckActionType Type { get; private set; }
        public string Params { get; private set; }
        public DemoCheckAction(DemoCheckActionType type, string param)
        {
            Type = type;
            Params = param;
        }
        public DemoCheckAction(string type, string param) :
            this(GetValueFromDescription<DemoCheckActionType>(type), param)
        { }

        public override string ToString()
        {
            return $"{Type}" + (String.IsNullOrWhiteSpace(Params) ? "" : $": {Params}");
        }
    }
}
