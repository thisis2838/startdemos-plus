using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using static System.Console;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace startdemos_plus
{
    class SettingsHandler
    {

        public void ReadSettings()
        {
            Program.PrintSeperator("LOAD SETTINGS");
            XmlDocument xml = new XmlDocument();
            xml.Load("config.xml");

            Program.GameExe = xml.DocumentElement.SelectSingleNode("/config/gameexe").InnerText ?? "";
            WriteLine("Successfully loaded settings.");
        }

        public void FirstTimeSettings()
        {
            Program.PrintSeperator("FIRST TIME SETUP");
            WriteLine("Config not found! Starting first time setup...");
            WriteLine("Please enter without surrounding quotes the following info:");

            WriteLine("Game EXE name: ");
            Program.GameExe = ReadLine();

            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.IndentChars = ("    ");
            settings.CloseOutput = true;
            using (XmlWriter xml = XmlWriter.Create("config.xml", settings))
            {
                xml.WriteStartElement("config");
                xml.WriteElementString("gameexe", Program.GameExe);
                xml.WriteEndElement();
                xml.Flush();
            }
        }
    }
}
