using startdemos_ui.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static startdemos_ui.Utils.EnumHelper;
using static startdemos_ui.Utils.ByteConvert;
using static startdemos_ui.MainForm;
using static startdemos_ui.Forms.MemoryScanningForm;
using static startdemos_ui.src.Events;

namespace startdemos_ui.src
{
    public class MemoryScanningHandler
    {
        public IntPtr pDemoPlayer { get; set; }
        public IntPtr pHostTickCount { get; set; }
        public IntPtr pExecCmd { get; set; }
        public int offDemoStartTick { get; set; }
        public int offDemoIsPlaying { get; set; }
        public Process Game { get; set; }
        public RemoteOpsHandler RemoteOps { get; set; }

        private SigScanTarget _demoPlayerTarget;
        private string _gameProcesName => mSF != null ? Path.GetFileNameWithoutExtension(MainForm.mSF.GameExe) : "";

        public MemoryScanningHandler()
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

            Start();
        }

        private void Start()
        {
            while (!globalCTS.IsCancellationRequested)
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(_gameProcesName))
                    {
                        mSF.SetStatus("Waiting user to input Game .EXE Name");
                        Thread.Sleep(750);
                        continue;
                    }

                    retry:
                    Game = Process.GetProcesses().FirstOrDefault(x => x.ProcessName.ToLower() == _gameProcesName.ToLower());
                    if (Game == null)
                    {
                        mSF.SetStatus("No such .EXE running!");
                        Thread.Sleep(750);
                        continue;
                    }

                    while (!Scan())
                    {
                        Thread.Sleep(750);
                        goto retry;
                    }
                    goto finish;
                }
                catch (Exception ex) // probably a Win32Exception on access denied to a process
                {
                    Thread.Sleep(1000);
                }
            }

            finish:
            ;
        }

        private bool Scan()
        {
            mSF.SetStatus($"Scanning for {_gameProcesName}");

            if (Game == null || Game.HasExited)
                return false;

            ProcessModuleWow64Safe engine = Game.ModulesWow64Safe().FirstOrDefault(x => x.ModuleName.ToLower() == "engine.dll");
            if (engine == null)
                return false;

            RemoteOps = new RemoteOpsHandler(Game);

            SignatureScanner scanner = new SignatureScanner(Game, engine.BaseAddress, engine.ModuleMemorySize);

            #region DEMO PLAYER
            IntPtr demoPlayerPtr;
            demoPlayerPtr = scanner.Scan(_demoPlayerTarget);
            #endregion
            if (demoPlayerPtr == IntPtr.Zero)
                throw new Exception();
            else
            {
                pDemoPlayer = demoPlayerPtr;

                #region IS PLAYING OFFSET
                IntPtr tmpPtr = scanner.FindStringRef("Tried to read a demo message with no demo file");

                offDemoIsPlaying = 0;
                if (tmpPtr != IntPtr.Zero)
                {
                    SignatureScanner newScanner = new SignatureScanner(Game, tmpPtr - 0x20, 0x40);
                    SigScanTarget target = new SigScanTarget(2, "88 ?? ?? ?? 00 00");
                    tmpPtr = newScanner.Scan(target);
                    offDemoIsPlaying = Game.ReadValue<int>(tmpPtr);
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
                        break;
                    }
                }
                #endregion
            }

            IntPtr funcPtr = scanner.FindStringRef("exec config_default.cfg");
            funcPtr = scanner.ReadCall(funcPtr + 0x5);
            //ReportPointer(funcPtr, "CBuf_AddText");
            if (funcPtr == IntPtr.Zero)
                throw new Exception("Couldn't find command execute function pointer!");
            else
                pExecCmd = funcPtr;

            mSF.SetStatus($"Done!");
            GameHasBeenFound?.Invoke(null, new GameDiscoveryArgs(
               Game,
               pDemoPlayer,
               pHostTickCount,
               pExecCmd,
               offDemoIsPlaying,
               offDemoStartTick
            ));

            return true;
        }
    }
}
