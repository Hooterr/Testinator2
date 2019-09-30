using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Testinator.TestSystem.Editors
{
    internal interface IErrorHandlerAdapter<T>
    {
        void HandleErrorFor(Expression<Func<T, object>> property, string msg);
        void OnErrorFor(Expression<Func<T, object>> property, Action<string> handler);
        void OnErrorFor(Expression<Func<T, object>> property, ICollection<string> handler);
        void Clear();
    }
}
