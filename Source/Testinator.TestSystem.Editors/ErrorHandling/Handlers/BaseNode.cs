using System;
using System.Collections.Generic;

namespace Testinator.TestSystem.Editors
{
    internal abstract class BaseNode
    {
        public string Name { get; set; }
        public BaseNode Parent { get; set; }
        public abstract bool IsEditor { get; }
        public BaseNode[] Children { get; set; }
        public bool HasHandler => Handler != null;
        public ICollection<string> Handler { get; set; }

        public bool HandleError(string msg)
        {
            var currentHandler = this;

            while (currentHandler != null)
            {
                if (currentHandler.HasHandler)
                {
                    currentHandler.Handler.Add(msg);
                    return true;
                }
                currentHandler = currentHandler.Parent;
            }

            return false;
        }

        public void ClearErrors()
        {
            if (HasHandler)
                Handler.Clear();
        }

        public BaseNode()
        {
            Children = new BaseNode[0];
        }
    }
}
