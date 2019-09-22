using System;

namespace Testinator.TestSystem.Editors
{
    internal interface IInternalErrorHandler
    {
        void HandleError(string propertyName, string message);
        void OnErrorFor(string propertyName, Action<string> action);
        void Clear();
    }
}
