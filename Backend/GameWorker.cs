using startdemos_plus.Globals;
using startdemos_plus.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace startdemos_plus.Backend
{
    public class GameWorker
    {
        public GameValues Values = new GameValues();
        private Thread _thread;
        private CancellationTokenSource _cts = new CancellationTokenSource();

        private MemoryWatcher<HostState> _hostState;
        
        public void Start()
        {
            _cts = new CancellationTokenSource();
            _thread = new Thread(new ThreadStart(() =>
            {
                while (!_cts.IsCancellationRequested)
                {
                    if (Process.GetProcesses().ToList().Any(x =>
                    {
                        Events.BeganScanningProcess?.Invoke(null, new CommonEventArgs("name", x.ProcessName));
                        bool result = Values.Scan(x);
                        Events.StoppedScanningProcess?.Invoke(null, null);
                        return result;
                    }))
                    {
                        _hostState = new MemoryWatcher<HostState>(Values.HostStatePtr);
                        _hostState.Update(Values.Game);

                        Globals.Events.FoundGameProcess.Invoke(null, new CommonEventArgs("values", Values));
                        while (!Values.Game.HasExited) 
                        {
                            if (_cts.IsCancellationRequested)
                                return;

                            _hostState.Update(Values.Game);

                            if (_hostState.Current == HostState.Run && _hostState.Old != HostState.Run)
                                Globals.Events.SessionStarted?.Invoke(null, null);
                            if (_hostState.Old == HostState.Run && _hostState.Current != HostState.Run)
                                Globals.Events.SessnionStopped?.Invoke(null, null);

                            Globals.Events.OnWorkerUpdate?.Invoke(null, null);
                            Thread.Sleep(10); 
                        }

                        Globals.Events.LostGameProcess?.Invoke(null, null);
                    }
                    Thread.Sleep(750);
                }
            }));
            _thread.Start();
        }

        public void Stop()
        {
            _cts.Cancel();
        }

        public void SendCommand(string command)
        {
            if (Values.CBufAddTextPtr != IntPtr.Zero) WinAPI.CallFunctionString(Values.Game, Values.CBufAddTextPtr, command);
            else WinAPI.SendMessage(Values.Game, command);
        }
    }

    public enum HostState
    {
        NewGame = 0,
        LoadGame = 1,
        ChangeLevelSP = 2,
        ChangeLevelMP = 3,
        Run = 4,
        GameShutdown = 5,
        Shutdown = 6,
        Restart = 7
    }
}
