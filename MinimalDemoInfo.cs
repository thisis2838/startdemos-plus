using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Text.Encoding;
using static System.Math;
using static System.BitConverter;
using static System.Globalization.CultureInfo;
using System.IO;

namespace startdemos_plus
{
    public class DemoParseResult
    {
        public string MapName { get; set; } = "-";
        public string PlayerName { get; set; } = "-";
        public string GameName { get; set; } = "-";
        public string Protocol { get; set; } = "-";
        public string NProtocol { get; set; } = "-";
        public string ServerName { get; set; } = "-";
        public int Index { get; set; } = 0;
        public int TotalTicks { get; set; } = 0;

        public DemoParseResult(string filePath)
        {
            using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            using (var br = new BinaryReader(fs))
            {
                ASCII.GetString(br.ReadBytes(8)).TrimEnd('\0');
                Protocol = (ToInt32(br.ReadBytes(4), 0)).ToString(InvariantCulture);
                NProtocol = (ToInt32(br.ReadBytes(4), 0)).ToString(InvariantCulture);
                ServerName = ASCII.GetString(br.ReadBytes(260)).TrimEnd('\0');
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
                    if (tick > 0)
                        TotalTicks = tick;

                    switch (command)
                    {
                        case 0x01:
                            br.BaseStream.Seek(signOnLen, SeekOrigin.Current);
                            break;
                        case 0x02:
                            {
                                br.BaseStream.Seek(4 + 0x44 + 4 * 3, SeekOrigin.Current);
                                var packetLen = br.ReadInt32();
                                br.BaseStream.Seek(packetLen, SeekOrigin.Current);
                            }
                            break;
                        case 0x04:
                            {
                                var concmdLen = br.ReadInt32();
                                br.ReadBytes(concmdLen - 1);
                                br.BaseStream.Seek(1, SeekOrigin.Current); // skip null terminator
                            }
                            break;
                        case 0x05:
                            {
                                br.BaseStream.Seek(4, SeekOrigin.Current); // skip sequence
                                var userCmdLen = br.ReadInt32();
                                br.BaseStream.Seek(userCmdLen, SeekOrigin.Current);
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

            TotalTicks++;

            string index = Path.GetFileNameWithoutExtension(filePath).ToLower().Replace(MapName + "_", "");
            if (int.TryParse(index, out int tmp))
                Index = tmp;
        }
    }
}
