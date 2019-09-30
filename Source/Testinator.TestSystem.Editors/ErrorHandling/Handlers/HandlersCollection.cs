using System;
using System.Collections.Generic;

namespace Testinator.TestSystem.Editors
{
    internal class HandlersCollection
    {
        private BaseNode mRootNode;
        private IDictionary<string, BaseNode> mLookup;

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

        public HandlersCollection(BaseNode rootNode)
        {
            mRootNode = rootNode ?? throw new ArgumentNullException(nameof(rootNode));

            // Create a quick look up table
            mLookup = new Dictionary<string, BaseNode>();
            var queue = new Queue<BaseNode>();
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
