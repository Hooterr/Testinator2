using System;
using System.Collections.Generic;

namespace Testinator.TestSystem.Editors
{
    internal interface IInternalErrorHandler
    {
        void HandleErrorFor(string propertyName, string message);
        void OnErrorFor(string propertyName, Action<string> action);
        void OnErrorFor(string propertyName, ICollection<string> handler);
        void Clear();
    }
}
