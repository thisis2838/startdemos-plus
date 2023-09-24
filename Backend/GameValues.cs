using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static startdemos_plus.Utils.Utils;
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
                throw new Exception();
            else
            {
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
                    throw new Exception("Couldn't find m_bPlayingBack offset");

                #endregion

                #region HOST TICK & START TICK
                for (int i = 0; i < 10; i++)
                {
                    SignatureScanner tmpScanner = new SignatureScanner(Game, Game.ReadPointer(Game.ReadPointer(demoPlayerPtr) + i * 4), 0x100);
                    SigScanTarget startTickAccess = new SigScanTarget(2, $"2B ?? ?? ?? 00 00");
                    SigScanTarget hostTickAccess = new SigScanTarget(1, "A1");
                    hostTickAccess.OnFound = (f_proc, f_scanner, f_ptr) => f_proc.ReadPointer(f_ptr);
                    startTickAccess.OnFound = (f_proc, f_scanner, f_ptr) =>
                    {
                        IntPtr hostTickOffPtr = f_scanner.Scan(hostTickAccess);
                        if (hostTickOffPtr != IntPtr.Zero)
                        {
                            DemoStartTickOffset = f_proc.ReadValue<int>(f_ptr);
                            return hostTickOffPtr;
                        }
                        return IntPtr.Zero;
                    };

                    IntPtr ptr = tmpScanner.Scan(startTickAccess);
                    if (ptr == IntPtr.Zero)
                        continue;
                    else
                    {
                        HostTickCountPtr = ptr;
                        break;
                    }
                }
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
                    if (loc == IntPtr.Zero) throw new Exception("Couldn't find CBufAddText string ptr");

                    target = new SigScanTarget(string.Join(" ", BitConverter.GetBytes(loc.ToInt32()).Select(x => x.ToString("X2"))));
                    loc = scanner.Scan(target);
					if (loc == IntPtr.Zero) throw new Exception("Couldn't find CBufAddText string reference ptr");

					for (int i = 0; i < 0x200; i++)
                    {
                        var ptr = loc - i;
                        if (Game.ReadBytes(ptr, 4).SequenceEqual(Enumerable.Repeat((byte)0xCC, 4)))
                        {
                            CBufAddTextPtr = ptr + 4;
                            goto end;
                        }
                    }

                    throw new Exception("Couldn't find CBufAddText ptr");
                    end:;
                }
				#endregion
			}

			return true;
        }
    }
}
