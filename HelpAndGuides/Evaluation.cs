using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace HelpAndGuides
{
    class Evaluation
    {
        public string Map { get; set; } = "";
        public string Var { get; set; } = "";
        public string Type { get; set; } = "";
        public string Directive { get; set; } = "";
        public string ResType { get; set; } = "";
        public string EventName { get; set; } = "";
        public bool Not { get; set; } = false;
        public string TickComparison { get; set; } = "";

        public Evaluation(XmlNode xmlNode)
        {
            Type = xmlNode.SelectSingleNode($"types/evaluation/type").InnerText;
            Directive = xmlNode.SelectSingleNode($"types/evaluation/directive").InnerText;
            ResType = xmlNode.SelectSingleNode($"types/result").InnerText;
            Var = xmlNode.SelectSingleNode($"var").InnerText;
            EventName = xmlNode.SelectSingleNode($"name")?.InnerText ?? "";
            TickComparison = xmlNode.SelectSingleNode($"tickcompare")?.InnerText ?? "";
            Not = bool.Parse(xmlNode.SelectSingleNode($"not")?.InnerText ?? "False");
            Map = xmlNode.SelectSingleNode($"map")?.InnerText ?? (xmlNode.Name ?? "");
        }

        public Evaluation()
        {

        }
    }
}
