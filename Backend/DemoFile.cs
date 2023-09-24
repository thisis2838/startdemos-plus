using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using startdemos_plus.Utils;
using System.IO;
using static startdemos_plus.Utils.Helpers;
using static System.Text.Encoding;
using static System.Math;
using static System.BitConverter;
using static System.Globalization.CultureInfo;
using System.Text.RegularExpressions;
using startdemos_plus.Backend.DemoChecking;

namespace startdemos_plus.Backend
{
    public class DemoFile
    {
        public string FilePath { get; private set; }
        public string Name => Path.GetFileNameWithoutExtension(FilePath);
        public string MapName { get; private set; }
        public string PlayerName { get; private set; }

        // NEVER get tick count from ticks list, as there are stray ticks we would wanna include
        // that aren't part of timing
        public int MaxIndex = 0;
        public int TotalTicks 
        { 
            get { return MaxIndex + (Globals.Values.ZerothTick ? 1 : 0); } 
            private set { MaxIndex = value; } 
        }
        public List<DemoTick> Ticks = new List<DemoTick>();
        public int Index { get; private set; }

        public DemoFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                Message($"File {filePath} does not exist.", MessageType.Warning);
                return;
            }

            FilePath = filePath;

            using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            using (var br = new BinaryReader(fs))
            {
                br.BaseStream.Seek(8 + 4 + 4 + 260, SeekOrigin.Current);
                PlayerName = ASCII.GetString(br.ReadBytes(260)).TrimEnd('\0');
                MapName = ASCII.GetString(br.ReadBytes(260)).TrimEnd('\0');
                br.BaseStream.Seek(260, SeekOrigin.Current);

                br.BaseStream.Seek(4 * 3, SeekOrigin.Current);
                var signOnLen = br.ReadInt32();

                try
                {
                    byte command = 0x0;
                    DemoTick curTick = new DemoTick(0);

                    while (command != 0x07)
                    {
                        command = br.ReadByte();
                        if (command == 0x07)
                        {
                            curTick.AddPacket(new DemoPacket(DemoPacketType.STOP));
                            Ticks.Add(curTick);
                            break; 
                        }

                        var tick = br.ReadInt32();
                        if (tick >= 0 && tick != MaxIndex)
                        {
                            Ticks.Add(curTick);
                            MaxIndex = tick;
                            curTick = new DemoTick(MaxIndex);
                        }

                        switch ((DemoPacketType)command)
                        {
                            case DemoPacketType.SIGNON:
                                {
                                    curTick.AddPacket(new DemoPacket(DemoPacketType.SIGNON));
                                    br.BaseStream.Seek(signOnLen, SeekOrigin.Current);
                                }
                                break;
                            case DemoPacketType.PACKET:
                                {
                                    br.BaseStream.Seek(4, SeekOrigin.Current);
                                    float x = br.ReadSingle();
                                    float y = br.ReadSingle();
                                    float z = br.ReadSingle();

                                    curTick.AddPacket(new DemoPacket(
                                        DemoPacketType.PACKET, 
                                        new Vector3f(x, y, z)));
                                    
                                    br.BaseStream.Seek(68L, SeekOrigin.Current);
                                    var packetLen = br.ReadInt32();
                                    br.BaseStream.Seek(packetLen, SeekOrigin.Current);
                                }
                                break;
                            case DemoPacketType.CONSOLECMD: // console commands
                                {
                                    var concmdLen = br.ReadInt32();
                                    curTick.AddPacket(new DemoPacket(
                                        DemoPacketType.CONSOLECMD, 
                                        ASCII.GetString(br.ReadBytes(concmdLen - 1)).Trim(new char[1])));
                                    br.BaseStream.Seek(1, SeekOrigin.Current); // skip null terminator
                                }
                                break;
                            case DemoPacketType.USERCMD: // user commands
                                {
                                    curTick.AddPacket(new DemoPacket(DemoPacketType.USERCMD));
                                    br.BaseStream.Seek(4, SeekOrigin.Current); // skip sequence
                                    var userCmdLen = br.ReadInt32();
                                    br.BaseStream.Seek(userCmdLen, SeekOrigin.Current);
                                }
                                break;
                            case DemoPacketType.STRINGTABLES:
                                {
                                    curTick.AddPacket(new DemoPacket(DemoPacketType.STRINGTABLES));
                                    var stringTableLen = br.ReadInt32();
                                    br.BaseStream.Seek(stringTableLen, SeekOrigin.Current);
                                }
                                break;
                        }
                    }
                }
                catch
                {
                    Message($"Problem while parsing {Path.GetFileName(filePath)}, file may be corrupted!", MessageType.Warning);
                }

                var match = Regex.Match(Name, $@"^(?:{MapName}_)([0-9]+)$", RegexOptions.IgnoreCase);
                if (match.Success && int.TryParse(match.Groups[1].Value, out int index))
                    Index = index;

                //TotalTicks++;
            }
        }

        public int GetMeasuredTicks(params DemoCheckResult[] results)
        {
            int ticks = MaxIndex;

            foreach (var result in results)
            {
                if (!result.Demo.Equals(this) || result == null || !result.PassedAll)
                    continue;

                var start = result.Check.Actions.FirstOrDefault(x => x.Type == DemoCheckActionType.StartDemoTime);
                if (start != null)
                {
                    ticks -= result.Passed.FirstOrDefault(x => x.Tick != null)?.Tick.Index ?? 0;
                    if (int.TryParse(start.Params, out int a))
                        ticks -= a;
                }

                var end = result.Check.Actions.FirstOrDefault(x => x.Type == DemoCheckActionType.EndDemoTime);
                if (end != null)
                {
                    ticks -= MaxIndex - result.Passed.FirstOrDefault(x => x.Tick != null)?.Tick.Index ?? 0;
                    if (int.TryParse(end.Params, out int a))
                        ticks += a;
                }
            }

            return ticks + (Globals.Values.ZerothTick ? 1 : 0);
        }

        public string GetPlayCommand()
        {
            return $"  playdemo \"{FilePath}\" \0";
        }

        public override string ToString()
        {
            return $"{Name} [{TotalTicks} ticks]";
        }

        public override bool Equals(object obj)
        {
            if (!(obj is DemoFile)) return false;
            // TODO: may want a more rigirous check than this...
            if ((obj as DemoFile).FilePath != FilePath) return false;
            return true;
        }
    }

    public class DemoTick
    {
        public int Index { get; private set; }
        public Vector3f? PlayerPosition { get; private set; } = null;
        public List<string> Commands { get; private set; } = new List<string>();
        public List<DemoPacket> Packets = new List<DemoPacket>();

        public DemoTick(int tick)
        {
            Index = tick;
        }

        public void AddPacket(DemoPacket packet)
        {
            Packets.Add(packet);
            if (packet.Data is Vector3f)
                PlayerPosition = (Vector3f)packet.Data;
            else if (packet.Data is string)
                Commands.Add(packet.Data.ToString());
        }

        public override string ToString()
        {
            return $"{Index} : {Packets.Count} packets";
        }
    }

    public enum DemoPacketType
    {
        SIGNON = 1,
        PACKET,
        SYNCTICK,
        CONSOLECMD,
        USERCMD,
        DATATABLES,
        STOP,
        STRINGTABLES
    }
    public struct DemoPacket
    {
        public DemoPacketType Type { get; private set; }
        public object Data { get; private set; }

        public DemoPacket(DemoPacketType type, object data = null)
        {
            Type = type;
            Data = data;
        }

        public override string ToString()
        {
            return $"{Type} : {Data}";
        }
    }
}
