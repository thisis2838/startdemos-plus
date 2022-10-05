using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using startdemos_plus.Src.DemoChecking;
using static startdemos_plus.Utils.Utils;

namespace startdemos_plus.UI
{
    public partial class DemoCheckForm : UserControl
    {
        private static string _fileName = "checks.xml";
        private void LoadSettings()
        {
            if (!File.Exists(_fileName))
                return;

            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(_fileName);
                _profiles = new List<UIDemoCheckProfile>();

                foreach (XmlNode profileNode in doc.SelectSingleNode("profiles"))
                {
                    string profileName = profileNode.LocalName;
                    List<DemoCheck> profile = new List<DemoCheck>();

                    foreach (XmlNode checkNode in profileNode.ChildNodes)
                    {
                        var check = new DemoCheck(checkNode.SelectSingleNode("name").InnerText);
                        checkNode.SelectSingleNode("conditions").ChildNodes.Cast<XmlNode>().ToList().ForEach(x =>
                        {
                            DemoCheckVariable var = GetValueFromDescription<DemoCheckVariable>(x.SelectSingleNode("variable").InnerText);
                            string con = x.SelectSingleNode("condition").InnerText;
                            bool not = bool.Parse(x.SelectSingleNode("not").InnerText);

                            check.Conditions.Add(new DemoCheckCondition(var, con, not));
                        });
                        profile.Add(check);

                        checkNode.SelectSingleNode("actions").ChildNodes.Cast<XmlNode>().ToList().ForEach(x =>
                        {
                            DemoCheckActionType actType = GetValueFromDescription<DemoCheckActionType>(x.SelectSingleNode("type").InnerText);
                            string actParam = x.SelectSingleNode("param").InnerText;

                            check.Actions.Add(new DemoCheckAction(actType, actParam));
                        });
                    }

                    _profiles.Add(new UIDemoCheckProfile(profileName, profile));
                }

                _profiles.ForEach(x => cmbProfiles.Items.Add(x.Name));
                cmbProfiles.SelectedIndex = _profiles.Count - 1;
            }
            catch (Exception ex)
            {
                Warning(ex, "Error while trying to load checks file!");
            }

        }

        private void WriteSettings()
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.IndentChars = ("    ");
            settings.CloseOutput = true;
            using (XmlWriter xml = XmlWriter.Create(_fileName, settings))
            {
                xml.WriteStartElement("profiles");
                foreach (var profile in _profiles)
                {
                    xml.WriteStartElement(profile.Name);
                    foreach (DemoCheck check in profile.Checks)
                    {
                        xml.WriteStartElement("check");
                        xml.WriteElementString("name", check.Name);
                        xml.WriteStartElement("conditions");
                        foreach (var condition in check.Conditions)
                        {
                            xml.WriteStartElement("condition");
                            xml.WriteElementString("variable", condition.Variable.GetDescription());
                            xml.WriteElementString("condition", condition.Condition);
                            xml.WriteElementString("not", condition.Not.ToString());
                            xml.WriteEndElement();
                        }
                        xml.WriteEndElement();
                        xml.WriteStartElement("actions");
                        foreach (var action in check.Actions)
                        {
                            xml.WriteStartElement("action");
                            xml.WriteElementString("type", action.Type.GetDescription());
                            xml.WriteElementString("param", action.Params);
                            xml.WriteEndElement();
                        }
                        xml.WriteEndElement();
                        xml.WriteEndElement();
                    }
                    xml.WriteEndElement();
                }
                xml.WriteEndElement();
                xml.Flush();
            }
        }
    }
}
