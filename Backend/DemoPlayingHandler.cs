using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using startdemos_plus.Utils;
using static startdemos_plus.Program;

namespace startdemos_plus.Backend
{
    internal class DemoPlayingHandler
    {
        public bool Active = false;

        public EventHandler<CommonEventArgs> DemoPaused;
        public EventHandler<CommonEventArgs> DemoResumed;
        public EventHandler<CommonEventArgs> DemoStopPlaying;
        public EventHandler<CommonEventArgs> DemoStartPlaying;
        public EventHandler<CommonEventArgs> DemoQueueFinished;
        public EventHandler<CommonEventArgs> DemoTick;
        public bool Playing => _playing.Current;
        public bool Paused => Playing && _demoIsPaused.Current;

        private MemoryWatcher<int> _curHostTick;
        private MemoryWatcher<int> _curDemoStartTick;
        private MemoryWatcher<IntPtr> _curDemoPtr;
        private MemoryWatcher<bool> _demoisPlaying;
        private MemoryWatcher<bool> _demoIsPaused;

        private DemoFile _current;
        private DemoFile _requestedNext;
        private ValueWatcher<bool> _playing = new ValueWatcher<bool>(false);

        private List<DemoFile> _files = new List<DemoFile>();
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
            _curDemoPtr = worker.Values.DemoFilePtrOffset == 0 
                ? null
                // file ptr offset is away from m_demofile which is the first field in demoplayer
                : new MemoryWatcher<IntPtr>(worker.Values.DemoPlayerPtr + 0x4 +  worker.Values.DemoFilePtrOffset); 
            _demoisPlaying = new MemoryWatcher<bool>(worker.Values.DemoPlayerPtr + worker.Values.DemoIsPlayingOffset);
            _demoIsPaused = new MemoryWatcher<bool>(worker.Values.DemoPlayerPtr + worker.Values.DemoIsPlayingOffset + 0x1);
        }

        public void Begin(List<DemoFile> files, int waitTime, bool autoNext, bool alternateDetection, string command)
        {
            if (files.Count == 0)
                return;

            Globals.Events.DemoQueueStarted.Invoke(null, null);

            _waitTime = waitTime;
            _autoNext = autoNext;
            _alternateDetection = alternateDetection;
            _commands = command;

            _files.Clear();
            files.ForEach(x => _files.Add(x));

            Active = true;

            Play(_files.First());
        }

        public void Play(DemoFile file)
        {
            Debug.WriteLine($"Playing {file.Name}.");

            if (_playing.Current)
            {
                Worker.SendCommand("stopdemo");
                _requestedNext = file;
            }
            else
            {
                _current = file;
                Worker.SendCommand(file.GetPlayCommand());
                Worker.SendCommand(_commands);
            }
        }

        public void Stop()
        {
            Debug.WriteLine($"Stopping queue.");

            _current = null;
            _files.Clear();
            Worker.SendCommand("stopdemo");
            
            if (Active)
            {
                Active = false;

                DemoStopPlaying?.Invoke(null, new CommonEventArgs("demo", _current));
                DemoQueueFinished?.Invoke(null, null);
                Globals.Events.DemoQueueFinished.Invoke(null, null);
            }
        }

        public void Pause()
        {
            Worker.SendCommand($"demo_pause");
        }

        public void Resume()
        {
            Worker.SendCommand($"demo_resume");
        }

        public void Next()
        {
            if (_current != null)
            {
                int nextIndex = _files.IndexOf(_current) + 1;
                if (nextIndex >= _files.Count) Stop();
                else Play(_files.ElementAt(nextIndex));
            }
        }

        public void Previous()
        {
            if (_current != null)
            {
                int prevIndex = _files.IndexOf(_current) - 1;
                if (prevIndex < 0) Stop();
                else Play(_files.ElementAt(prevIndex));
            }
        }

        private void Update()
        {
            _curHostTick.Update(Worker.Values.Game);
            _curDemoStartTick.Update(Worker.Values.Game);
            _demoisPlaying.Update(Worker.Values.Game);
            _demoIsPaused.Update(Worker.Values.Game);
            _curDemoPtr?.Update(Worker.Values.Game);
            _playing.Current = (_curDemoPtr == null || _alternateDetection) ? _demoisPlaying.Current : _curDemoPtr.Current != IntPtr.Zero;

            if (!Active) return;

            if (!_demoIsPaused.Current && _demoIsPaused.Old)
                DemoResumed?.BeginInvoke(null, new CommonEventArgs("demo", _current), null, null);
            else if (_demoIsPaused.Current && !_demoIsPaused.Old)
                DemoPaused?.BeginInvoke(null, new CommonEventArgs("demo", _current), null, null);

            if (_playing.Current && !_playing.Old)
            {
                if (Active)
                    DemoStartPlaying?.BeginInvoke(null, new CommonEventArgs("demo", _current), null, null);
            }
            else if (!_playing.Current && _playing.Old)
            {
                if (Active)
                    DemoStopPlaying?.BeginInvoke(null, new CommonEventArgs("demo", _current), null, null);

                Thread.Sleep(_waitTime);

                if (_requestedNext != null)
                {
                    Play(_requestedNext);
                    _requestedNext = null;
                }
                else if (_autoNext)
                {
                    Next();
                }

                return;
            }

            if (_playing.Current && _current != null)
            {
                var diff = _curHostTick.Current - _curDemoStartTick.Current;
                DemoTick?.BeginInvoke
                (
                    null,
                    new CommonEventArgs
                    (
                        "demo", _current,
                        "time", diff > 0 ? diff : 0
                    ),
                    null,
                    null
                );
            }
        }
    }
}
