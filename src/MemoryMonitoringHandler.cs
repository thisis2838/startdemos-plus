using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using startdemos_ui.Utils;
using static startdemos_ui.src.Events;
using static startdemos_ui.MainForm;
using System.Threading;

namespace startdemos_ui.src
{
    public class MemoryMonitoringHandler
    {
        private string _queuedDemoCommands;
        private MemoryWatcher<bool> _demoIsPlaying;
        private MemoryWatcher<int> _curDemoStartTick;
        private bool _ticksDirty = false;
        private IntPtr _pHostTickCount;
        private IntPtr _pExecCmd;
        private int _curHostTick => _game.ReadValue<int>(_pHostTickCount);
        private Process _game;
        public PlayAction Action;

        public MemoryMonitoringHandler()
        {
            GameHasBeenFound += Initialize;
        }

        private void Initialize(object sender, GameDiscoveryArgs e)
        {
            _pHostTickCount = e.HostTickCountPtr;
            _demoIsPlaying = new MemoryWatcher<bool>(e.DemoPlayerPtr + e.DemoIsPlayingOffset);
            _curDemoStartTick = new MemoryWatcher<int>(e.DemoPlayerPtr + e.DemoStartTickOffset);
            _pExecCmd = e.ExecCmdPtr;
            _game = e.Game;
        }

        private void ProcessDemoCommandQueue()
        {
            while (true)
            {
                Thread.Sleep(10);

                if (_game.HasExited || _game == null)
                    return;

                if (!_demoIsPlaying.Current || _queuedDemoCommands == "")
                    continue;

                if (_pHostTickCount == IntPtr.Zero)
                    GameCommand(_queuedDemoCommands);
                else
                {
                    _curDemoStartTick.Update(_game);

                    if (_ticksDirty)
                    {
                        _ticksDirty = !(_curHostTick == _curDemoStartTick.Current);
                        continue;
                    }

                    int curDemoTick = _curHostTick - _curDemoStartTick.Current;
                    if (curDemoTick > 0)
                    {
                        GameCommand(_queuedDemoCommands);
                        _queuedDemoCommands = "";
                    }
                }
            }
        }


        public void BeginPlaying(string order, string commands, int waitTime, bool autoNext)
        {
            if (order == "")
                order = "-";

            var d = new PlayOrderHandler(order);
            List<int> indicies = d.Order;
            List<OrderInfo> reorder = d.OrderInfo;

            int total = indicies.Count();
            dPF.UpdatePlayOrder(reorder);

            _demoIsPlaying.Update(_game);

            GameCommand("stopdemo");

            Thread _demoCommandThread = new Thread(new ThreadStart(ProcessDemoCommandQueue));
            _demoCommandThread.Start();

            for (int i = 0; i < indicies.Count(); i++)
            {                   
                int index = indicies[i];

                if (globalCTS.IsCancellationRequested)
                    goto end;

                DemoFile demo = dCH.Files[index];
                GameCommand(demo.PlayCommand);
                _queuedDemoCommands = commands;

                dPF.UpdateCurrentPlayInfo(i + 1, 
                    total, 
                    $"{demo.Name} (index {indicies[i]})",
                    i == 0 ? "" : $"{dCH.Files[indicies[i - 1]].Name} (index {indicies[i - 1]})",
                    i == total - 1 ? "" : $"{dCH.Files[indicies[i + 1]].Name} (index {indicies[i + 1]})");

                _ticksDirty = true;

                while (!globalCTS.IsCancellationRequested)
                {

                    if (Action != PlayAction.None)
                    {
                        switch (Action)
                        {
                            case PlayAction.Next:
                                Action = PlayAction.None;
                                goto skipdemo;
                            case PlayAction.Stop:
                                Action = PlayAction.None;
                                goto end;
                            case PlayAction.Previous:
                                Action = PlayAction.None;
                                goto previous;
                            default:
                                break;
                        }
                    }


                    if (_game == null || _game.HasExited)
                        return;

                    _demoIsPlaying.Update(_game);
                    if (_demoIsPlaying.Changed && !_demoIsPlaying.Current && autoNext)
                        break;

                    Thread.Sleep(10);
                }
                continue;

                previous:
                i = i == 0 ? i - 1 : i - 2;

                skipdemo:
                ;
            }

            end:
            _demoIsPlaying.Update(_game);
            _demoCommandThread.Abort();

            Thread.Sleep(waitTime);
        }

        private void GameCommand(string cmd)
        {
            mSH.RemoteOps.CallFunctionString(cmd + "\n", _pExecCmd);
        }
    }

    public enum PlayAction
    {
        None,
        Stop,
        Previous,
        Next
    }
}
