using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using startdemos_plus.Utils;
using static startdemos_plus.Program;

namespace startdemos_plus.Backend
{
    internal class DemoPlayingHandler
    {
        public EventHandler<CommonEventArgs> DemoPaused;
        public EventHandler<CommonEventArgs> DemoResumed;
        public EventHandler<CommonEventArgs> DemoStopPlaying;
        public EventHandler<CommonEventArgs> DemoStartPlaying;
        public EventHandler<CommonEventArgs> DemoQueueFinished;
        public EventHandler<CommonEventArgs> DemoTick;
        public bool Playing => (_demoPlaybackActive?.Current ?? false) && !(_demoIsPaused?.Current ?? true);
        public bool DemoLoaded => (_demoPlaybackActive?.Current ?? false);

        private MemoryWatcher<int> _curHostTick;
        private MemoryWatcher<int> _curDemoStartTick;
        private MemoryWatcher<IntPtr> _curDemoPtr;
        private MemoryWatcher<bool> _demoPlaybackActive;
        private MemoryWatcher<bool> _demoIsPaused;

        private DemoFile _current => _files.Navigate(0);
        private bool _suspendUpdate = true;

        private NavigableList<DemoFile> _files = new NavigableList<DemoFile>();
        private int _waitTime = 50;
        private bool _autoNext = true;
        private bool _alternateDetection = false;
        private string _commands = "";

        public DemoPlayingHandler()
        {
            Globals.Events.OnWorkerUpdate += (s, e) => 
            { 
                Update(); 
            };

            Globals.Events.FoundGameProcess += (s, e) =>
            {
                Init();
            };

            Globals.Events.LostGameProcess += (s, e) =>
            {
                Stop();
            };
        }

        public void Init()
        {
            var worker = Program.Worker;

            _curHostTick = new MemoryWatcher<int>(worker.Values.HostTickCountPtr);
            _curDemoStartTick = new MemoryWatcher<int>(worker.Values.DemoPlayerPtr + worker.Values.DemoStartTickOffset);
            _curDemoPtr = new MemoryWatcher<IntPtr>(worker.Values.DemoPlayerPtr + worker.Values.DemoStartTickOffset - 0x4);
            _demoPlaybackActive = new MemoryWatcher<bool>(worker.Values.DemoPlayerPtr + worker.Values.DemoIsPlayingOffset);
            _demoIsPaused = new MemoryWatcher<bool>(worker.Values.DemoPlayerPtr + worker.Values.DemoIsPlayingOffset + 0x1);
        }

        public void Begin(List<DemoFile> files, int waitTime, bool autoNext, bool alternateDetection, string command)
        {
            _suspendUpdate = true;

            if (files.Count == 0)
                return;

            Globals.Events.DemoQueueStarted.Invoke(null, null);

            _waitTime = waitTime;
            _autoNext = autoNext;
            _alternateDetection = alternateDetection;
            _commands = command;

            _files.Clear();
            files.ForEach(x => _files.Add(x));

            _files.Navigate(0, out var cur);
            Play(cur);

            _suspendUpdate = false;
        }

        public void Play(DemoFile file)
        {
            Worker.SendCommand("stopdemo");
            Worker.SendCommand(file.GetPlayCommand());
            Worker.SendCommand(_commands);
            DemoStartPlaying?.BeginInvoke(null, new CommonEventArgs("demo", file), null, null);
        }

        public void Stop()
        {
            _suspendUpdate = true;

            Worker.SendCommand("stopdemo");
            DemoStopPlaying?.BeginInvoke(null, new CommonEventArgs("demo", _current), null, null);
            Globals.Events.DemoQueueFinished.Invoke(null, null);
            _files.Clear();
            DemoQueueFinished?.BeginInvoke(null, null, null, null);
        }

        public void Pause()
        {
            DemoPaused?.BeginInvoke(null, new CommonEventArgs("demo", _current), null, null);
            Worker.SendCommand($"demo_pause");
        }

        public void Resume()
        {
            DemoResumed?.BeginInvoke(null, new CommonEventArgs("demo", _current), null, null);
            Worker.SendCommand($"demo_resume");
        }

        public void Next()
        {
            var cur = _current;
            if (_files.Navigate(1, out var file))
            {
                DemoStopPlaying?.BeginInvoke(null, new CommonEventArgs("demo" , cur), null, null);
                Play(file);
            }
            else Stop();
        }

        public void Previous()
        {
            var cur = _current;
            if (_files.Navigate(-1, out var file))
            {
                DemoStopPlaying?.BeginInvoke(null, new CommonEventArgs("demo", cur), null, null);
                Play(file);
            }
            else Stop();
        }

        private void Update()
        {
            _curHostTick.Update(Worker.Values.Game);
            _curDemoStartTick.Update(Worker.Values.Game);
            _demoPlaybackActive.Update(Worker.Values.Game);
            _demoIsPaused.Update(Worker.Values.Game);
            _curDemoPtr.Update(Worker.Values.Game);

            if (_suspendUpdate || _current == null)
                return;

            bool stopped = _alternateDetection 
                ? (!_demoPlaybackActive.Current && _demoPlaybackActive.Old)
                : (_curDemoPtr.Old != IntPtr.Zero && _curDemoPtr.Current == IntPtr.Zero);
            if (stopped)
            {
                DemoStopPlaying?.BeginInvoke(null, new CommonEventArgs("demo", _current), null, null);
                if (_autoNext)
                {
                    if (_files.Navigate(1, out var file))
                    {
                        Thread.Sleep(_waitTime);
                        Play(file);
                    }
                    else Stop();

                    return;
                }
            }

            if (_current == null || _suspendUpdate)
                return;

            var diff = _curHostTick.Current - _curDemoStartTick.Current;
            DemoTick?.BeginInvoke(null, new CommonEventArgs(
                "demo", _current,
                "time", diff > 0 ? diff : 0), null, null);
        }
    }
}
