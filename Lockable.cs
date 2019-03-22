using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringSearch
{


    // stragely, .NET doesn't seem to have a 'Concurrent' object that contains
    // data that is both ordered and viewable as a whole (aka, a List)
    // So below is a minimalist implementation of List as a thread-safe object
    public class LockableList<T>
    {
        private List<T> list = new List<T>();
        private object lockObj = new object();

        public void Add(T item)
        {
            lock(lockObj)
            {
                this.list.Add(item);
            }
        }
        public void AddRange(IEnumerable<T> items)
        {
            lock(lockObj)
            {
                this.list.AddRange(items);
            }
        }
        public List<T> GetShallowSnapshot()
        {
            List<T> shallowCopy = new List<T>();
            lock (lockObj)
            {
                shallowCopy.AddRange(this.list);
            }
            return shallowCopy;
        }
        public int Count()
        {
            lock (lockObj)
            {
                return this.list.Count();
            }
        }
    }
}
