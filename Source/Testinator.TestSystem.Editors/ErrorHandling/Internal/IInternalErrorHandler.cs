using System;

namespace Testinator.TestSystem.Editors
{
    internal interface IInternalErrorHandler
    {
        void HandleErrorFor(string propertyName, string message);
        void OnErrorFor(string propertyName, Action<string> action);
        void Clear();
    }
}
