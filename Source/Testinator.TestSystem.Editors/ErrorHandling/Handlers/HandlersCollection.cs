using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Testinator.TestSystem.Editors
{
    internal class HandlersCollection : IReadOnlyCollection<BaseNode>
    {
        private BaseNode[] mHandlers;
        private IDictionary<string, BaseNode> mLookup;

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

        public IEnumerator<BaseNode> GetEnumerator()
        {
            return mHandlers.AsEnumerable().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return mHandlers.GetEnumerator();
        }

        public HandlersCollection(BaseNode[] handlers)
        {
            mHandlers = handlers;
            if (mHandlers == null)
                mHandlers = new BaseNode[0];

            // Create a quick look up table
            mLookup = new Dictionary<string, BaseNode>();
            var queue = new Queue<BaseNode>();
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

    internal class HandlersCollectionNEW
    {
        private BaseNodeNEW mRootNode;
        private IDictionary<string, BaseNodeNEW> mLookup;

        public ICollection<string> this[string Name]
        {
            set
            {
                if (mLookup.ContainsKey(Name))
                    mLookup[Name].Handler = value;
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

        public HandlersCollectionNEW(BaseNodeNEW rootNode)
        {
            mRootNode = rootNode ?? throw new ArgumentNullException(nameof(rootNode));

            // Create a quick look up table
            mLookup = new Dictionary<string, BaseNodeNEW>();
            var queue = new Queue<BaseNodeNEW>();
            queue.Enqueue(mRootNode);
            while (queue.Count > 0)
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
