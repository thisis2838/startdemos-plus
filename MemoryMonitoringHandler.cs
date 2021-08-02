using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static startdemos_plus.Program;
using static System.Console;
using LiveSplit.ComponentUtil;
using System.Threading;
using static startdemos_plus.PrintHelper;

namespace startdemos_plus
{
    class MemoryMonitoringHandler
    {
        private string _queuedDemoCommands = "";
        private MemoryWatcher<bool> _demoIsPlaying;
        private MemoryWatcher<int> _curDemoStartTick;
        private bool _ticksDirty = false;
        private int _curHostTick => _game.ReadValue<int>(mScan.pHostTickCount);
        private Process _game;

        public MemoryMonitoringHandler()
        {
            _game = mScan.Game;
            _demoIsPlaying = new MemoryWatcher<bool>(mScan.pDemoPlayer + mScan.offDemoIsPlaying);
            _curDemoStartTick = new MemoryWatcher<int>(mScan.pDemoPlayer + mScan.offDemoStartTick);
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

                if (mScan.pHostTickCount == IntPtr.Zero)
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

        public void Monitor()
        {
            List<int> indicies = playOrderHandler.PlayOrderIndicies;
            List<ReorderInfo> reorder = playOrderHandler.PlayOrderList;

            PrintSeperator("DEMO QUEUE START");
            WriteLine($"{indicies.Count()} actions");
            if (reorder == null)
                WriteLine($"Began playing from demo #{indicies[0]}");
            else
            {
                WriteLine("Began playing demos in the following order: ");
                reorder.ForEach(x => Write($"{x}, "));
                WriteLine();
            }

            Stopwatch watch = new Stopwatch();
            Stopwatch demoWatch = new Stopwatch();
            Stopwatch totalDemoWatch = new Stopwatch();
            watch.Start();
            totalDemoWatch.Start();
            _demoIsPlaying.Update(_game);

            GameCommand("stopdemo");

            Thread _demoCommandThread = new Thread(new ThreadStart(ProcessDemoCommandQueue));
            _demoCommandThread.Start();

            int played = 0;
            foreach (int index in indicies)
            {
                if (globalCTS.IsCancellationRequested)
                    goto end;

                DemoFileHandler.DemoFile demo = demoFile.Files[index];
                GameCommand(demo.PlayCommand);

                if (_queuedDemoCommands == "")
                    _queuedDemoCommands = settings.PerDemoCommands;

                string indexPrint = $"[#{index:000}]";
                string actionPrint = $"[{played + 1}/{indicies.Count()}]";

                WriteLine();
                WriteLine($"{actionPrint} {indexPrint} [{demo.Name}] Began playing");
                _ticksDirty = true;
                played++;
                demoWatch.Restart();

                while (!globalCTS.IsCancellationRequested)
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
                    if (_demoIsPlaying.Changed && !_demoIsPlaying.Current)
                    {
                        demoWatch.Stop();
                        WriteLine($"{actionPrint} {indexPrint} [{demo.Name}] Finished playing after {demoWatch.ElapsedMilliseconds * 0.001f}s");
                        break;
                    }

                    Thread.Sleep(10);
                }
                continue;

                skipdemo:
                demoWatch.Stop();
                WriteLine($"{actionPrint} {indexPrint} [{demo.Name}] Skipped playing after {demoWatch.ElapsedMilliseconds * 0.001f}s");
            }

            end:
            totalDemoWatch.Stop();
            _demoCommandThread.Abort();
            WriteLine();
            WriteLine($"Finished {played} actions after {totalDemoWatch.ElapsedMilliseconds * 0.001f}s");
            _demoIsPlaying.Update(_game);
            Thread.Sleep(settings.WaitTime);
        }

        public struct QueuedDemoCommand
        {
            public QueuedDemoCommand(string command, bool persistent)
            {
                Command = command;
                Persistent = persistent;
            }
            public string Command;
            public bool Persistent;
        }

        private void GameCommand(string cmd)
        {
            mScan.RemoteOps.CallFunctionString(cmd + "\n", mScan.pExecCmd);
        }

    }
}
