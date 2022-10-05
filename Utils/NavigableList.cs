using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace startdemos_plus.Utils
{
    public class NavigableList<T> : List<T>
    {
        public NavigableList()
        { }

        public NavigableList(IEnumerable<T> e)
        {
            Clear();
            CurrentIndex = 0;
            foreach (var f in e)
                this.Add(f);
        }

        public void Clear()
        {
            base.Clear();
            CurrentIndex = 0;
        }

        public int CurrentIndex = 0;
        public bool Navigate(int diff, out T value)
        {
            CurrentIndex += diff;
            if (Count - 1 < CurrentIndex || CurrentIndex < 0)
            {
                value = default(T);
                return false;
            }
            value = this[CurrentIndex];
            return true;
        }
        public T Navigate(int diff)
        {
            Navigate(diff, out var value);
            return value;
        }
    }
}
