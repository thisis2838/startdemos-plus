using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveSplit.ComponentUtil;

namespace startdemos_plus
{
    static class SigScanExt
    {
        public static IntPtr FindStringRef(SignatureScanner scanner, string input)
        {
            var target = new SigScanTarget(0, BitConverter.ToString(Encoding.Default.GetBytes(input)).Replace("-", ""));
            target.OnFound = (f_proc, f_scanner, f_ptr) =>
            {
                SigScanTarget newTarg = new SigScanTarget(0, $"68 {GetByteArrayI32(f_ptr.ToInt32())}");
                return f_scanner.Scan(newTarg);
            };
            return scanner.Scan(target);
        }

        public static string GetByteArrayI32(int ptr)
        {
            byte[] b = BitConverter.GetBytes(ptr);
            return $"{b[0]:X02} {b[1]:X02} {b[2]:X02} {b[3]:X02}";
        }

        public static IntPtr ReadCall(Process proc, IntPtr ptr)
        {
            if (ptr == IntPtr.Zero)
                return IntPtr.Zero;
            return (IntPtr)(proc.ReadValue<int>(ptr + 0x1) + (int)(ptr + 5));
        }
    }
}
