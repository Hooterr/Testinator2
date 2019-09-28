using System;
using System.Collections.Generic;

namespace Testinator.TestSystem.Editors
{
    internal class EditorNodeNEW : BaseNodeNEW
    {
        public override bool IsEditor => true;
    }

    internal class PropertyNodeNEW : BaseNodeNEW
    {
        public override bool IsEditor => false;
    }

    internal abstract class BaseNodeNEW
    {
        public string Name { get; set; }
        public BaseNodeNEW Parent { get; set; }
        public abstract bool IsEditor { get; }
        public BaseNodeNEW[] Children { get; set; }
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

        public BaseNodeNEW()
        {
            Children = new BaseNodeNEW[0];
        }
    }

    internal abstract class BaseNode
    {
        public string Name { get; set; }
        public BaseNode Parent { get; set; }
        public abstract bool IsEditor { get; }
        public BaseNode[] Children { get; set; }
        public bool HasHandler => mHandler != null;

        private Action<string> mHandler;

        public bool HandleError(string msg)
        {
            var currentHandler = this;

            while(currentHandler != null)
            {
                if (currentHandler.HasHandler)
                {
                    currentHandler.Invoke(msg);
                    return true;
                }
                currentHandler = currentHandler.Parent;
            }

            return false;
        }

        public void SetHandler(Action<string> handler)
        {
            mHandler = handler;
        }

        public void ClearErrors()
        {
            if (HasHandler)
                mHandler.Invoke(string.Empty);
        }

        private void Invoke(string msg)
        {
            mHandler.Invoke(msg);
        }
        public BaseNode()
        {
            Children = new BaseNode[0];
        }
    }
}
