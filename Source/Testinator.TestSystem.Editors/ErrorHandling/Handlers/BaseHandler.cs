using System;
using System.Collections.Generic;
using System.Text;

namespace Testinator.TestSystem.Editors
{
    internal abstract class BaseHandler
    {
        public string Name { get; set; }
        public BaseHandler Parent { get; set; }
        public abstract bool IsEditor { get; }
        public BaseHandler[] Children { get; set; }
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
        public BaseHandler()
        {
            Children = new BaseHandler[0];
        }
    }
}
