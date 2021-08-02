using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveSplit.ComponentUtil;
using System.IO;
using static System.Console;
using System.Diagnostics;
using System.Threading;
using System.Runtime.InteropServices;
using static startdemos_plus.SigScanExt;
using static startdemos_plus.PrintHelper;

namespace startdemos_plus
{
    class MemoryScanningHandler
    {
        public IntPtr pDemoPlayer { get; set; }
        public IntPtr pHostTickCount { get; set; }
        public IntPtr pExecCmd { get; set; }
        public int offDemoStartTick { get; set; }
        public int offDemoIsPlaying { get; set; }
        public string GameDir { get; set; } = "";
        public Process Game { get; set; }
        public RemoteOpsHandler RemoteOps { get; set; }

        private SigScanTarget _gameDirTarget;
        private SigScanTarget _demoPlayerTarget;
        private string _gameProcesName;

        public MemoryScanningHandler()
        {
            PrintSeperator("SIGSCANNING PREREQUISITES");
            _gameDirTarget = new SigScanTarget(0, "25732F736176652F25732E736176"); // "%s/save/%s.sav"
            _gameDirTarget.OnFound = (proc, scanner, ptr) =>
            {
                byte[] b = BitConverter.GetBytes(ptr.ToInt32());
                var target = new SigScanTarget(-4, $"68 {b[0]:X02} {b[1]:X02} {b[2]:X02} {b[3]:X02}");
                return proc.ReadPointer(scanner.Scan(target));
            };

            _demoPlayerTarget = new SigScanTarget(0, "43616e2774207265636f726420647572696e672064656d6f20706c61796261636b2e");
            _demoPlayerTarget.OnFound = (proc, scanner, ptr) =>
            {
                var target = new SigScanTarget(-95, $"68 {GetByteArrayI32(ptr.ToInt32())}");

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

            _gameProcesName = Path.GetFileNameWithoutExtension(Program.settings.GameExe);

            Start();
        }
        private void ReportPointer(IntPtr pointer, string name = "")
        {
            name += " pointer";
            if (pointer != IntPtr.Zero)
                WriteLine("Found " + name + " at 0x" + pointer.ToString("X"));
            else
                WriteLine("Couldn't find " + name);
        }

        private void Start()
        {
            while (true)
            {
                try
                {
                    WriteLine($"Scanning for game process \"{_gameProcesName}\".");
                    retry:
                    Game = Process.GetProcesses().FirstOrDefault(x => x.ProcessName.ToLower() == _gameProcesName.ToLower());
                    while (!Scan())
                    {
                        Thread.Sleep(750);
                        goto retry;
                    }
                    goto finish;
                }
                catch (Exception ex) // probably a Win32Exception on access denied to a process
                {
                    WriteLine(ex.ToString());
                    Thread.Sleep(1000);
                }
            }

            finish:
            ;
        }

        private bool Scan()
        {
            if (Game == null || Game.HasExited)
                return false;

            ProcessModuleWow64Safe engine = Game.ModulesWow64Safe().FirstOrDefault(x => x.ModuleName.ToLower() == "engine.dll");
            if (engine == null)
                return false;

            RemoteOps = new RemoteOpsHandler(Game);

            SignatureScanner scanner = new SignatureScanner(Game, engine.BaseAddress, engine.ModuleMemorySize);

            IntPtr demoPlayerPtr, gameDirPtr;

            #region GAME DIRECTORY
            WriteLine("Scanning for game directory pointer...");
            gameDirPtr = scanner.Scan(_gameDirTarget);
            ReportPointer(gameDirPtr, "directory");
            if (gameDirPtr == IntPtr.Zero)
                throw new Exception();
            else
                GameDir = Game.ReadString(gameDirPtr, 260);
            #endregion

            #region DEMO PLAYER
            WriteLine("Scanning for g_pClientDemoPlayer pointer...");
            demoPlayerPtr = scanner.Scan(_demoPlayerTarget);
            ReportPointer(demoPlayerPtr, "g_pClientDemoPlayer");
            #endregion
            if (demoPlayerPtr == IntPtr.Zero)
                throw new Exception();
            else
            {
                pDemoPlayer = demoPlayerPtr;

                #region IS PLAYING OFFSET
                IntPtr tmpPtr = FindStringRef(scanner, "Tried to read a demo message with no demo file");

                offDemoIsPlaying = 0;
                if (tmpPtr != IntPtr.Zero)
                {
                    SignatureScanner newScanner = new SignatureScanner(Game, tmpPtr - 0x20, 0x40);
                    SigScanTarget target = new SigScanTarget(2, "88 ?? ?? ?? 00 00");
                    tmpPtr = newScanner.Scan(target);
                    offDemoIsPlaying = Game.ReadValue<int>(tmpPtr);
                    WriteLine($"m_bPlayingBack offset is 0x{offDemoIsPlaying:X}");
                }
                if (offDemoIsPlaying == 0)
                    throw new Exception("Couldn't find m_bPlayingBack offset");

                #endregion

                #region HOST TICK & START TICK
                int StartTickOffset = 0;
                for (int i = 0; i < 10; i++)
                {
                    SignatureScanner tmpScanner = new SignatureScanner(Game, Game.ReadPointer(Game.ReadPointer(demoPlayerPtr) + i * 4), 0x100);
                    SigScanTarget StartTickAccess = new SigScanTarget(2, $"2B ?? ?? ?? 00 00");
                    SigScanTarget hostTickAccess = new SigScanTarget(1, "A1");
                    hostTickAccess.OnFound = (f_proc, f_scanner, f_ptr) => f_proc.ReadPointer(f_ptr);
                    StartTickAccess.OnFound = (f_proc, f_scanner, f_ptr) => 
                    {
                        IntPtr hostTickOffPtr = f_scanner.Scan(hostTickAccess);
                        if (hostTickOffPtr != IntPtr.Zero)
                        {
                            StartTickOffset = f_proc.ReadValue<int>(f_ptr);
                            offDemoStartTick = StartTickOffset;
                            WriteLine($"m_nStartTick offset is 0x{StartTickOffset:X}");
                            return hostTickOffPtr;
                        }
                        return IntPtr.Zero;
                    };

                    IntPtr ptr = tmpScanner.Scan(StartTickAccess);
                    if (ptr == IntPtr.Zero)
                        continue;
                    else
                    {
                        pHostTickCount = ptr;
                        WriteLine($"Found host_tickcount at 0x" + ptr.ToString("X"));
                        break;
                    }
                }

                if (pHostTickCount == IntPtr.Zero || StartTickOffset == 0)
                {
                    WriteLine("Couldn't find host_tickcount pointer!");
                    WriteLine("Per-demo commands will only be executed upon demo load!");
                };
                #endregion
            }

            WriteLine("Scanning for target command execute function pointer...");
            IntPtr funcPtr = FindStringRef(scanner, "exec config_default.cfg");
            funcPtr = ReadCall(Game, funcPtr + 0x5);
            ReportPointer(funcPtr, "CBuf_AddText");
            if (funcPtr == IntPtr.Zero)
                throw new Exception("Couldn't find command execute function pointer!");
            else
                pExecCmd = funcPtr;

            return true;
        }
    }
}
