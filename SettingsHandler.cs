using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using static System.Console;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static startdemos_plus.PrintHelper;

namespace startdemos_plus
{
    class SettingsHandler
    {
        public string GameExe { get; set; } = "";
        public int WaitTime { get; set; } = 50;
        public float TickRate { get; set; } = 0.015f;
        public string PerDemoCommands { get; set; } = "";

        public void ReadSettings()
        {
            PrintSeperator("LOAD SETTINGS");
            XmlDocument xml = new XmlDocument();
            xml.Load("config.xml");

            GameExe = xml.DocumentElement.SelectSingleNode("/config/gameexe")?.InnerText ?? "";
            WriteLine($"Game EXE is {GameExe}");
            TickRate = float.Parse(xml.DocumentElement.SelectSingleNode("/config/tickrate")?.InnerText ?? "0.015");
            WriteLine($"Tickrate is {TickRate}");
            WaitTime = int.Parse(xml.DocumentElement.SelectSingleNode("/config/waittime")?.InnerText ?? "50");
            WaitTime = WaitTime < 50 ? 50 : WaitTime;
            WriteLine($"Wait time between demos is {WaitTime}");
            WriteLine("Successfully loaded settings.");
        }

        public void FirstTimeSettings()
        {
            PrintSeperator("FIRST TIME SETUP");
            WriteLine("Config not found! Starting first time setup...");
            WriteLine("Please enter without surrounding quotes the following info:");

            WriteLine("Game EXE name: ");
            GameExe = ReadLine();

            WriteLine("Tickrate: ");
            TickRate = float.Parse(ReadLine());

            WriteLine("Wait time between demos (in milliseconds, minimum is 50): ");
            WaitTime = int.Parse(ReadLine());
            WaitTime = WaitTime < 50 ? 50 : WaitTime;

            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.IndentChars = ("    ");
            settings.CloseOutput = true;
            using (XmlWriter xml = XmlWriter.Create("config.xml", settings))
            {
                xml.WriteStartElement("config");
                xml.WriteElementString("gameexe", GameExe);
                xml.WriteElementString("tickrate", TickRate.ToString("0.000000000"));
                xml.WriteElementString("waittime", WaitTime.ToString());
                xml.WriteElementString("commands", PerDemoCommands.ToString());
                xml.WriteEndElement();
                xml.Flush();
            }
        }   
    }
}
