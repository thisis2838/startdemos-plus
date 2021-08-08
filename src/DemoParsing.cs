﻿using startdemos_ui.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static startdemos_ui.MainForm;
using static System.Text.Encoding;
using static System.Math;
using static System.BitConverter;
using static System.Globalization.CultureInfo;
using System.Windows.Forms;

namespace startdemos_ui.src
{
    public class DemoParseResult
    {
        public string MapName { get; set; } = "-";
        public string PlayerName { get; set; } = "-";
        public string GameName { get; set; } = "-";
        public int Index { get; set; } = 0;
        public int TotalTicks { get; set; } = 0;
        public int AdjustedTicks { get; set; } = 0;
        public List<DemoCheckResult> Events { get; set; }
        private ResultType gameSupportResult = ResultType.None;

        public DemoParseResult(string filePath, DemoCheckHandler curDemoChecks = null)
        {

            Events = new List<DemoCheckResult>();

            using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            using (var br = new BinaryReader(fs))
            {
                ASCII.GetString(br.ReadBytes(8)).TrimEnd('\0');
                _ = (ToInt32(br.ReadBytes(4), 0)).ToString(InvariantCulture);
                _ = (ToInt32(br.ReadBytes(4), 0)).ToString(InvariantCulture);
                _ = ASCII.GetString(br.ReadBytes(260)).TrimEnd('\0');
                PlayerName = ASCII.GetString(br.ReadBytes(260)).TrimEnd('\0');
                MapName = ASCII.GetString(br.ReadBytes(260)).TrimEnd('\0');
                GameName = ASCII.GetString(br.ReadBytes(260)).TrimEnd('\0');

                br.BaseStream.Seek(4 * 3, SeekOrigin.Current);
                var signOnLen = br.ReadInt32();

                byte command = 0x0;
                while (command != 0x07)
                {
                    command = br.ReadByte();

                    if (command == 0x07) // dem_stop
                        break;

                    var tick = br.ReadInt32();
                    if (tick >= 0)
                        TotalTicks = tick;

                    switch (command)
                    {
                        case 0x01:
                            br.BaseStream.Seek(signOnLen, SeekOrigin.Current);
                            break;
                        case 0x02:
                            {
                                br.BaseStream.Seek(4, SeekOrigin.Current);
                                float x = br.ReadSingle();
                                float y = br.ReadSingle();
                                float z = br.ReadSingle();

                                Vector3f pos = new Vector3f(x, y, z);
                                if (curDemoChecks != null)
                                    HandleResultType(curDemoChecks.Check(EvaluationDataType.Position, MapName, TotalTicks, pos));

                                br.BaseStream.Seek(68L, SeekOrigin.Current);
                                var packetLen = br.ReadInt32();
                                br.BaseStream.Seek(packetLen, SeekOrigin.Current);
                            }
                            break;
                        case 0x04: // console commands
                            {
                                var concmdLen = br.ReadInt32();
                                if (curDemoChecks != null)
                                    HandleResultType(curDemoChecks.Check(
                                        EvaluationDataType.ConsoleCommand,
                                        MapName,
                                        TotalTicks,
                                        ASCII.GetString(br.ReadBytes(concmdLen - 1)).Trim(new char[1])));
                                    br.BaseStream.Seek(1, SeekOrigin.Current); // skip null terminator
                            }
                            break;
                        case 0x05: // user commands
                            {
                                br.BaseStream.Seek(4, SeekOrigin.Current); // skip sequence
                                var userCmdLen = br.ReadInt32();
                                if (curDemoChecks != null)
                                    HandleResultType(curDemoChecks.Check(
                                        EvaluationDataType.UserCommand,
                                        MapName,
                                        TotalTicks,
                                        ASCII.GetString(br.ReadBytes(userCmdLen)).Trim(new char[1])));
                                //br.BaseStream.Seek(userCmdLen, SeekOrigin.Current);
                            }
                            break;
                        case 0x08:
                            {
                                var stringTableLen = br.ReadInt32();
                                br.BaseStream.Seek(stringTableLen, SeekOrigin.Current);
                            }
                            break;
                    }
                }
            }

            if (dCF.chk0thTick.Checked)
                TotalTicks++;

            switch (gameSupportResult)
            {
                case ResultType.BeginOnce:
                case ResultType.BeginMultiple:
                    AdjustedTicks = TotalTicks - (AdjustedTicks + 1);
                    break;
                case ResultType.EndOnce:
                case ResultType.EndMultiple:
                    AdjustedTicks = (AdjustedTicks + 1);
                    break;
                case ResultType.None:
                    AdjustedTicks = TotalTicks;
                    break;
            }

            string index = Path.GetFileNameWithoutExtension(filePath).ToLower().Replace(MapName + "_", "");
            if (int.TryParse(index, out int tmp))
                Index = tmp;
        }

        private void HandleResultType(List<DemoCheckResult> results)
        {
            if (results.All(x => x.Result == ResultType.None))
                return;

            foreach (DemoCheckResult result in results)
            {
                if (result.Result != ResultType.None)
                {
                    Events.Add(result);

                    if (gameSupportResult == ResultType.None && result.Result != ResultType.Note)
                        gameSupportResult = result.Result;
                }
            }

            AdjustedTicks = TotalTicks;
        }
    }
}