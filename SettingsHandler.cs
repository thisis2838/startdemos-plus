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
            WriteLine($"Game EXE is {Program.GameExe}");
            Program.TickRate = float.Parse(xml.DocumentElement.SelectSingleNode("/config/tickrate")?.InnerText ?? "0.015");
            WriteLine($"Tickrate is {Program.TickRate}");
            Program.WaitTime = int.Parse(xml.DocumentElement.SelectSingleNode("/config/waittime")?.InnerText ?? "50");
            Program.WaitTime = Program.WaitTime < 50 ? 50 : Program.WaitTime;
            WriteLine($"Wait time between demos is {Program.WaitTime}");
            WriteLine("Successfully loaded settings.");
        }

        public void FirstTimeSettings()
        {
            Program.PrintSeperator("FIRST TIME SETUP");
            WriteLine("Config not found! Starting first time setup...");
            WriteLine("Please enter without surrounding quotes the following info:");

            WriteLine("Game EXE name: ");
            Program.GameExe = ReadLine();

            WriteLine("Tickrate: ");
            Program.TickRate = float.Parse(ReadLine());

            WriteLine("Wait time between demos (in milliseconds, minimum is 50): ");
            Program.WaitTime = int.Parse(ReadLine());
            Program.WaitTime = Program.WaitTime < 50 ? 50 : Program.WaitTime;

            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.IndentChars = ("    ");
            settings.CloseOutput = true;
            using (XmlWriter xml = XmlWriter.Create("config.xml", settings))
            {
                xml.WriteStartElement("config");
                xml.WriteElementString("gameexe", Program.GameExe);
                xml.WriteElementString("tickrate", Program.TickRate.ToString("0.000000000"));
                xml.WriteElementString("waittime", Program.WaitTime.ToString());
                xml.WriteEndElement();
                xml.Flush();
            }
        }
    }
}
