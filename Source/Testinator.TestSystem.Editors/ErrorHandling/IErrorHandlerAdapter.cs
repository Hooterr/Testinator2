using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Testinator.TestSystem.Editors
{
    internal interface IErrorHandlerAdapter<T>
    {
        void HandleErrorFor(Expression<Func<T, object>> property, string msg);
        void OnErrorFor(string propertyName, Action<string> handler);
        void ClearAllErrors();
    }
}
