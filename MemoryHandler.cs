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

namespace startdemos_plus
{
    class MemoryHandler
    {

        private SigScanTarget _gameDirTarget;
        private SigScanTarget _demoPlayerTarget;
        private MemoryWatcher<bool> _demoIsPlaying;
        private string _gameProcesName;
        private int _startTickOffset = -1;
        private Process _game;
        private FileHandler _fileHandler;
        private IntPtr _execCmdPtr;

        private RemoteOps remoteOps;
        public MemoryHandler()
        {
            Program.PrintSeperator("SIGSCANNING PREREQUISITES");
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
                var target = new SigScanTarget(-95, $"68 {GetByteArrayI32(ptr)}");

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

            _gameProcesName = Path.GetFileNameWithoutExtension(Program.GameExe);

            Start();
        }

        private void Start()
        {
            while (true)
            {
                try
                {
                    WriteLine($"Scanning for game process \"{_gameProcesName}\".");
                    retry:
                    _game = Process.GetProcesses().FirstOrDefault(x => x.ProcessName.ToLower() == _gameProcesName.ToLower());
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
            if (_game == null || _game.HasExited)
                return false;

            ProcessModuleWow64Safe engine = _game.ModulesWow64Safe().FirstOrDefault(x => x.ModuleName.ToLower() == "engine.dll");
            if (engine == null)
                return false;

            remoteOps = new RemoteOps(_game);

            SignatureScanner scanner = new SignatureScanner(_game, engine.BaseAddress, engine.ModuleMemorySize);

            IntPtr demoPlayerPtr, gameDirPtr;

            WriteLine("Scanning for game directory pointer...");
            gameDirPtr = scanner.Scan(_gameDirTarget);
            ReportPointer(gameDirPtr, "directory");
            if (gameDirPtr == IntPtr.Zero)
                throw new Exception();
            else
                Program.GameDir = _game.ReadString(gameDirPtr, 260);


            WriteLine("Scanning for g_pClientDemoPlayer pointer...");
            demoPlayerPtr = scanner.Scan(_demoPlayerTarget);
            ReportPointer(demoPlayerPtr, "g_pClientDemoPlayer");
            if (demoPlayerPtr == IntPtr.Zero)
                throw new Exception();
            else
            {
                IntPtr tmpPtr = FindStringRef(scanner, "Tried to read a demo message with no demo file");

                _startTickOffset = 0;
                if (tmpPtr != IntPtr.Zero)
                {
                    SignatureScanner newScanner = new SignatureScanner(_game, tmpPtr - 0x20, 0x40);
                    SigScanTarget target = new SigScanTarget(2, "88 ?? ?? ?? 00 00");
                    tmpPtr = newScanner.Scan(target);
                    _startTickOffset = _game.ReadValue<int>(tmpPtr);
                    WriteLine($"Found m_nStartTick offset at 0x{_startTickOffset:X}");
                }
                if (_startTickOffset == 0)
                    throw new Exception("Couldn't find m_nStartTick offset");

                _demoIsPlaying = new MemoryWatcher<bool>(demoPlayerPtr + _startTickOffset);
            }

            WriteLine("Scanning for target command execute function pointer...");
            IntPtr funcPtr = FindStringRef(scanner, "exec config_default.cfg");
            funcPtr = ReadCall(_game, funcPtr + 0x5);
            ReportPointer(funcPtr, "CBuf_AddText");
            if (funcPtr == IntPtr.Zero)
                throw new Exception("Couldn't find command execute function pointer!");
            else
                _execCmdPtr = funcPtr;

            return true;
        }

        private void ReportPointer(IntPtr pointer, string name = "")
        {
            name += " pointer";
            if (pointer != IntPtr.Zero)
                WriteLine("Found " + name + " at 0x" + pointer.ToString("X"));
            else
                WriteLine("Couldn't find " + name);
        }

        private void GameCommand(string cmd)
        {
            remoteOps.CallFunctionString(cmd, _execCmdPtr);
        }

        public void Monitor()
        {
            Program.PrintSeperator("DEMO QUEUE START");
            Stopwatch watch = new Stopwatch();
            Stopwatch demoWatch = new Stopwatch();
            Stopwatch totalDemoWatch = new Stopwatch();
            watch.Start();
            totalDemoWatch.Start();
            _demoIsPlaying.Update(_game);

            GameCommand("stopdemo");

            int played = 0;

            foreach (FileHandler.DemoFile demo in Program.FileHandler.Files)
            {
                GameCommand(demo.PlayCommand);
                WriteLine();
                WriteLine($"Began playing {demo.Name}");
                played++;
                demoWatch.Restart();

                while (true)
                {
                    if (KeyAvailable)
                    {
                        ConsoleKeyInfo key = ReadKey(true);
                        switch (key.Key)
                        {
                            case ConsoleKey.S:
                                goto skipdemo;
                            case ConsoleKey.X:
                                goto end;
                            default:
                                break;
                        }
                    }

                    if (_game == null || _game.HasExited)
                        return;

                    _demoIsPlaying.Update(_game);
                    if (_demoIsPlaying.Changed && _demoIsPlaying.Current == false)
                    {
                        demoWatch.Stop();
                        WriteLine($"Finished playing {demo.Name} after {demoWatch.ElapsedMilliseconds * 0.001f}s");
                        break;
                    }

                    Thread.Sleep(10);
                }
                continue;

                skipdemo:
                demoWatch.Stop();
                WriteLine($"Skipped playing {demo.Name} after {demoWatch.ElapsedMilliseconds * 0.001f}s");
            }

            end:
            totalDemoWatch.Stop();
            WriteLine();
            WriteLine($"Finished playing {played} demos after {totalDemoWatch.ElapsedMilliseconds * 0.001f}s");
            GameCommand("stopdemo");
            Thread.Sleep(50);
        }
    }
}
