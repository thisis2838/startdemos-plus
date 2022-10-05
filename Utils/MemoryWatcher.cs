using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace startdemos_plus.Utils
{
    public abstract class MemoryWatcher
    {
        public enum ReadFailAction
        {
            DontUpdate,
            SetZeroOrNull,
        }

        public string Name { get; set; }
        public bool Enabled { get; set; }
        public object Current { get; set; }
        public object Old { get; set; }
        public bool Changed { get; protected set; }
        public TimeSpan? UpdateInterval { get; set; }
        public ReadFailAction FailAction { get; set; }

        protected bool InitialUpdate { get; set; }
        protected DateTime? LastUpdateTime { get; set; }
        protected IntPtr Address { get; set; }

        protected MemoryWatcher(IntPtr address)
        {
            Address = address;
            Enabled = true;
            FailAction = ReadFailAction.DontUpdate;
        }

        /// <summary>
        /// Updates the watcher and returns true if the value has changed.
        /// </summary>
        public abstract bool Update(Process process);

        public abstract void Reset();

        protected bool CheckInterval()
        {
            if (UpdateInterval.HasValue)
            {
                if (LastUpdateTime.HasValue)
                {
                    if (DateTime.Now - LastUpdateTime.Value < UpdateInterval.Value)
                        return false;
                }
                LastUpdateTime = DateTime.Now;
            }
            return true;
        }
    }

    public class StringWatcher : MemoryWatcher
    {
        public new string Current
        {
            get { return (string)base.Current; }
            set { base.Current = value; }
        }
        public new string Old
        {
            get { return (string)base.Old; }
            set { base.Old = value; }
        }

        public delegate void StringChangedEventHandler(string old, string current);
        public event StringChangedEventHandler OnChanged;

        private ReadStringType _stringType;
        private int _numBytes;

        public StringWatcher(IntPtr address, ReadStringType type, int numBytes)
            : base(address)
        {
            _stringType = type;
            _numBytes = numBytes;
        }

        public StringWatcher(IntPtr address, int numBytes)
            : this(address, ReadStringType.AutoDetect, numBytes) { }

        public override bool Update(Process process)
        {
            Changed = false;

            if (!Enabled)
                return false;

            if (!CheckInterval())
                return false;

            string str;
            bool success;
            success = process.ReadString(Address, _stringType, _numBytes, out str);

            if (success)
            {
                base.Old = base.Current;
                base.Current = str;
            }
            else
            {
                if (FailAction == ReadFailAction.DontUpdate)
                    return false;

                base.Old = base.Current;
                base.Current = str;
            }

            if (!InitialUpdate)
            {
                InitialUpdate = true;
                return false;
            }

            if (!Current.Equals(Old))
            {
                OnChanged?.Invoke(Old, Current);
                Changed = true;
                return true;
            }

            return false;
        }

        public override void Reset()
        {
            base.Current = null;
            base.Old = null;
            InitialUpdate = false;
        }
    }

    public class MemoryWatcher<T> : MemoryWatcher where T : struct
    {
        public new T Current
        {
            get { return (T)(base.Current ?? default(T)); }
            set { base.Current = value; }
        }
        public new T Old
        {
            get { return (T)(base.Old ?? default(T)); }
            set { base.Old = value; }
        }

        public delegate void DataChangedEventHandler(T old, T current);
        public event DataChangedEventHandler OnChanged;

        public MemoryWatcher(IntPtr address)
            : base(address) { }

        public override bool Update(Process process)
        {
            Changed = false;

            if (!Enabled)
                return false;

            if (!CheckInterval())
                return false;

            base.Old = Current;

            T val;
            bool success;
            success = process.ReadValue(Address, out val);

            if (success)
            {
                base.Old = base.Current;
                base.Current = val;
            }
            else
            {
                if (FailAction == ReadFailAction.DontUpdate)
                    return false;

                base.Old = base.Current;
                base.Current = val;
            }

            if (!InitialUpdate)
            {
                InitialUpdate = true;
                return false;
            }

            if (!Current.Equals(Old))
            {
                OnChanged?.Invoke(Old, Current);
                Changed = true;
                return true;
            }

            return false;
        }

        public override void Reset()
        {
            base.Current = default(T);
            base.Old = default(T);
            InitialUpdate = false;
        }
    }
}
