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
        public bool ZerothTick { get; set; } = false;

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
            PerDemoCommands = xml.DocumentElement.SelectSingleNode("/config/commands")?.InnerText ?? "";
            WriteLine($"Commands to executre per Demo is \"{PerDemoCommands}\"");
            ZerothTick = bool.Parse(xml.DocumentElement.SelectSingleNode("/config/zerothtick")?.InnerText ?? "False");
            WriteLine($"Accounting for Zeroth tick is {ZerothTick}");
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

            WriteLine("Account for Zeroth tick (adding 1 tick per demo): ");
            ZerothTick = bool.Parse(ReadLine());

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
                xml.WriteElementString("zerothtick", ZerothTick.ToString());
                xml.WriteEndElement();
                xml.Flush();
            }
        }   
    }
}
