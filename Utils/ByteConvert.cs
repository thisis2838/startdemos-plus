using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace startdemos_ui.Utils
{
    static class ByteConvert
    {
        public static string GetByteArray<T>(T ptr) where T : struct
        {
            byte[] bytes = (byte[])typeof(BitConverter).GetMethod("GetBytes", new[] { typeof(T) })
                .Invoke(null, new[] { (object)ptr });

            string d = "";
            foreach (byte b in bytes)
                d += $"{b:X02} ";

            return d;
        }
    }
}
