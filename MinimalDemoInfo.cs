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
            }

            string index = Path.GetFileNameWithoutExtension(filePath).ToLower().Replace(MapName + "_", "");
            if (int.TryParse(index, out int tmp))
                Index = tmp;
        }
    }
}
