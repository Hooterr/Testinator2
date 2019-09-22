using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Testinator.TestSystem.Editors
{
    internal class HandlersCollection : IReadOnlyCollection<BaseHandler>
    {
        private BaseHandler[] mHandlers;
        private IDictionary<string, BaseHandler> mLookup;

        public int Count => mHandlers.Length;

        public bool IsReadOnly => true;

        public Action<string> this[string Name]
        {
            set
            {
                if (mLookup.ContainsKey(Name))
                    mLookup[Name].SetHandler(value);
            }
        }

        public bool HandleError(string name, string msg)
        {
            if (mLookup.ContainsKey(name))
                return mLookup[name].HandleError(msg);

            return false;
        }

        public bool Contains(string Name)
        {
            return mLookup.ContainsKey(Name);
        }

        public IEnumerator<BaseHandler> GetEnumerator()
        {
            return mHandlers.AsEnumerable().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return mHandlers.GetEnumerator();
        }

        public HandlersCollection(BaseHandler[] handlers)
        {
            mHandlers = handlers;
            if (mHandlers == null)
                mHandlers = new BaseHandler[0];

            // Create a quick look up table
            mLookup = new Dictionary<string, BaseHandler>();
            var queue = new Queue<BaseHandler>();
            Array.ForEach(mHandlers, (x) => queue.Enqueue(x));
            while(queue.Count > 0)
            {
                var current = queue.Dequeue();
                mLookup.Add(current.Name, current);
                Array.ForEach(current.Children, (x) => queue.Enqueue(x));
            }
        }

        public void ClearErrors()
        {
            foreach (var handler in mLookup.Values)
                handler.ClearErrors();
        }
    }
}
