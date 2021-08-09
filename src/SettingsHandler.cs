using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;
using System.Reflection;

namespace startdemos_ui.src
{
    public class SettingsHandler
    {
        private List<SettingInfo> _settings;
        public List<SettingEntry> SubscribedSettings;

        public SettingsHandler()
        {
            _settings = new List<SettingInfo>();
            SubscribedSettings = new List<SettingEntry>();

            if (!File.Exists("settings.xml"))
                return;

            XmlDocument doc = new XmlDocument();

            try
            {
                doc.Load("settings.xml");
            }
            catch { return; }

            var s = doc.SelectSingleNode("settings").ChildNodes[0];

            while (s != null)
            {
                _settings.Add(new SettingInfo(s.Name, s.InnerText ?? ""));
                s = s.NextSibling;
            }
        }

        public string GetSetting(string name)
        {
            if (_settings.Count == 0)
                return "";

            if (_settings.Where(x => x.Name == name).Count() == 0)
                return "";
            return _settings.Where(x => x.Name == name)?.ElementAt(0).Setting;
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
                SubscribedSettings.ForEach(x => xmlWriter.WriteElementString(x.Name, x.Get()));
                xmlWriter.WriteEndElement();
                xmlWriter.Flush();
            }
        }

        public void LoadSettings()
        {
            SubscribedSettings.ForEach(x => x.Set(GetSetting(x.Name)));
        }
    }

    public struct SettingInfo
    {
        public string Name;
        public string Setting;
        public SettingInfo(string name, string setting)
        {
            Name = name;
            Setting = setting;
        }
    }

    public struct SettingEntry
    {
        public string Name;
        public Action<string> Set;
        public Func<string> Get;

        public SettingEntry(string name, Action<string> set, Func<string> get)
        {
            Name = name;
            Set = set;
            Get = get;
        }
    }
}
