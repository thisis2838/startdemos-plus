using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;
using System.Reflection;
using static startdemos_plus.Utils.Helpers;

namespace startdemos_plus.Backend
{
    public class SettingsHandler
    {
        private List<SettingEntry> _settings = new List<SettingEntry>();

        public void AddSetting(string name, Action<string> set, Func<string> get)
        {
            _settings.Add(new SettingEntry() { Name = name, Set = set, Get = get });
        }

        public void LoadSettings()
        {
            if (!File.Exists("settings.xml"))
                return;

            XmlDocument doc = new XmlDocument();

            try 
            { 
                doc.LoadXml(File.ReadAllText("settings.xml"));

                var s = doc.SelectSingleNode("settings").ChildNodes[0];
                while (s != null)
                {
                    var d = _settings.Where(x => x.Name == s.Name);
                    if (d.Any())
                        d.First().Set(s.InnerText ?? "");
                    s = s.NextSibling;
                }
            }
            catch (Exception ex) { Warning(ex); }

        }

        public void WriteSettings()
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.IndentChars = ("    ");
            settings.CloseOutput = true;
            using (var xmlWriter = XmlWriter.Create("settings.xml", settings))
            {
                xmlWriter.WriteStartElement("settings");
                _settings.ForEach(x => xmlWriter.WriteElementString(x.Name, x.Get()));
                xmlWriter.WriteEndElement();
                xmlWriter.Flush();
            }
        }
    }

    public class SettingEntry
    {
        public string Name;
        public Action<string> Set;
        public Func<string> Get;

        public override string ToString()
        {
            return $"{Name} : {Get}";
        }
    }
}
