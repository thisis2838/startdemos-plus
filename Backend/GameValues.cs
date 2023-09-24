using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static startdemos_plus.Utils.Helpers;
using startdemos_plus.Utils;
using System.ComponentModel;

namespace startdemos_plus.Backend
{
    public class GameValues
    {
        public IntPtr DemoPlayerPtr { get; private set; }
        public IntPtr HostTickCountPtr { get; private set; }
        public int DemoStartTickOffset { get; private set; }
        public int DemoIsPlayingOffset { get; private set; }
        public int DemoFilePtrOffset { get; private set; }
        public IntPtr HostStatePtr { get; private set; }
        public IntPtr CBufAddTextPtr { get; private set; }  
        public Process Game { get;private set; }

        private SigScanTarget _demoPlayerTarget;
        private SigScanTarget _hostStateTarget;
        private static string[] _goodProcesses = new string[] { "hl2", "bms" };

        public GameValues()
        {
            _demoPlayerTarget = new SigScanTarget(0, "43616e2774207265636f726420647572696e672064656d6f20706c61796261636b2e");
            _demoPlayerTarget.OnFound = (proc, scanner, ptr) =>
            {
                var target = new SigScanTarget(-95, $"68 {GetByteArray(ptr.ToInt32())}");

                IntPtr byteArrayPtr = scanner.Scan(target);
                if (byteArrayPtr == IntPtr.Zero)
                    return IntPtr.Zero;

                byte[] bytes = new byte[100];
                proc.ReadBytes(scanner.Scan(target), 100).CopyTo(bytes, 0);
                for (int i = 98; i >= 0; i--)
                {
                    if (bytes[i] == 0x8B && bytes[i + 1] == 0x0D)
                        return proc.ReadPointer(proc.ReadPointer(byteArrayPtr + i + 2));
                }

                return IntPtr.Zero;
            };

            _hostStateTarget = new SigScanTarget(2,
                "C7 05 ?? ?? ?? ?? 07 00 00 00",    // mov     g_HostState_m_nextState, 7
                "C3"                                // retn
                );
            _hostStateTarget.OnFound = (proc, scanner, ptr) => proc.ReadPointer(ptr, out ptr) ? (ptr - 4) : IntPtr.Zero;

        }

        public bool Scan(Process game)
        {   
            if (!_goodProcesses.Contains(game.ProcessName))
                return false;

            ProcessModuleWow64Safe engine = null;
            try
            {
                engine = game.GetModule("engine.dll");
            }
            catch { return false; }

            if (engine == null) return false;

            Game = game;

            SignatureScanner scanner = new SignatureScanner(Game, engine.BaseAddress, engine.ModuleMemorySize);

            #region DEMO PLAYER
            IntPtr demoPlayerPtr;
            demoPlayerPtr = scanner.Scan(_demoPlayerTarget);
            #endregion

            if (demoPlayerPtr == IntPtr.Zero) 
                return false;

            DemoPlayerPtr = demoPlayerPtr;

            #region IS PLAYING OFFSET

            IntPtr tmpPtr = scanner.FindStringRef("Tried to read a demo message with no demo file");

            DemoIsPlayingOffset = 0;
            if (tmpPtr != IntPtr.Zero)
            {
                SignatureScanner newScanner = new SignatureScanner(Game, tmpPtr - 0x20, 0x40);
                SigScanTarget target = new SigScanTarget(2, "88 ?? ?? ?? 00 00");
                tmpPtr = newScanner.Scan(target);
                DemoIsPlayingOffset = Game.ReadValue<int>(tmpPtr);
            }

            if (DemoIsPlayingOffset == 0)
                return false; // throw new Exception("Couldn't find m_bPlayingBack offset");

            #endregion

            #region HOST TICK & START TICK

            for (int i = 0; i < 10; i++)
            {
                IntPtr start = Game.ReadPointer(Game.ReadPointer(demoPlayerPtr) + i * 4);
                IntPtr hostTick = IntPtr.Zero;
                int startTickOff = 0;
                for (int j = 0; j < 0x100; j++)
                {
                    if (Game.ReadBytes(start + j, 4).SequenceContains(new byte[] { 0xC3, 0xCC }))
                        break; // we've hit the end of this func.

                    if (hostTick != IntPtr.Zero)
                    {
                        if (Game.ReadValue<byte>(start + j) == 0x2B)
                        {
                            int off = Game.ReadValue<int>(start + j + 2);
                            if (off <= 0x1000)
                            {
                                startTickOff = off;
                                break;
                            }
                        }
                    }
                    else if (Game.ReadValue<byte>(start + j) == 0xA1)
                    {
                        uint candidate = Game.ReadValue<uint>(start + (j + 1));
                        if (candidate >= engine.BaseAddress.ToInt32() && candidate <= (engine.BaseAddress.ToInt32() + engine.ModuleMemorySize))
                        {
                            hostTick = (IntPtr)(int)candidate;
                            j += 4;
                        }
                    }
                }

                if (hostTick != IntPtr.Zero && startTickOff != 0)
                {
                    HostTickCountPtr = hostTick;
                    DemoStartTickOffset = startTickOff;
                    break;
                }
            }
            if (HostTickCountPtr == IntPtr.Zero || DemoStartTickOffset == 0)
                return false;

            #endregion

            #region DEMO FILE POINTER

            // the buffer used by the demo file handle gets set to null when a demo finishes playing and gets unloaded
            // we'll try to find the instruction which sets this field to all zeroes
            // then find if the function in question does anything with this field first
            // this should fail in old engine.
            for (int i = 1; i < 5; i++)
            {
                int off = DemoStartTickOffset - 4 * i;
                string rep = string.Join(" ", BitConverter.GetBytes(off).Select(x => x.ToString("X2")));
                SigScanTarget setZeroTarget = new SigScanTarget($"{rep} 00 00 00 00");
                foreach (var loc in scanner.ScanAll(setZeroTarget))
                {
                    for (int j = 0; j <= 0x100; j++)
                    {
                        IntPtr cur = loc - j;

                        if (game.ReadBytes(cur, 2).All(x => x == 0xCC))
                            break;

                        if (game.ReadValue<byte>(cur) == 0x8B && game.ReadValue<int>(cur + 2) == off)
                        {
                            for (int k = 0; k < 4 * 4; k++)
                            {
                                var bytes = game.ReadBytes(cur + 6 + k, 4);
                                if (bytes[0] >= 0x84 && bytes[0] <= 0x86 && bytes[2] == 0x74)
                                {
                                    DemoFilePtrOffset = off;
                                    goto demFilePtrEnd;
                                }
                            }
                        }
                    }
                }
            }
            DemoFilePtrOffset = 0;
            demFilePtrEnd:;

            #endregion

            #region HOST STATE

            HostStatePtr = scanner.Scan(_hostStateTarget);
            if (HostStatePtr == IntPtr.Zero)
                return false;

            #endregion

            #region COMMAND SENDING
            if (WinAPI.SendMessage(this.Game, "echo Hi, this is a startdemos+ dummy message, please carry on.") == 1)
            {
                CBufAddTextPtr = IntPtr.Zero;
            }
            else
            {
                var target = new SigScanTarget("43 62 75 66 5F 41 64 64 54 65 78 74 3A 20 62 75 66 66 65 72 20 6F 76 65 72 66 6C 6F 77");
                var loc = scanner.Scan(target);
                if (loc == IntPtr.Zero) return false; //throw new Exception("Couldn't find CBufAddText string ptr");

                target = new SigScanTarget(string.Join(" ", BitConverter.GetBytes(loc.ToInt32()).Select(x => x.ToString("X2"))));
                loc = scanner.Scan(target);
                if (loc == IntPtr.Zero) return false; //throw new Exception("Couldn't find CBufAddText string reference ptr");

                for (int i = 0; i < 0x200; i++)
                {
                    var ptr = loc - i;
                    if (Game.ReadBytes(ptr, 4).SequenceEqual(Enumerable.Repeat((byte)0xCC, 4)))
                    {
                        CBufAddTextPtr = ptr + 4;
                        goto end;
                    }
                }

                return false; //throw new Exception("Couldn't find CBufAddText ptr");
                end:;
            }
            #endregion

            return true;
        }
    }
}
