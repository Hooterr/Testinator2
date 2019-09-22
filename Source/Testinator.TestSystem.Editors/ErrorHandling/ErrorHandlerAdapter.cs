using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Testinator.TestSystem.Editors
{
    internal class ErrorHandlerAdapter<T> : IErrorHandlerAdapter<T>
    {
        private readonly IInternalErrorHandler mHandler;

        public void ClearAllErrors()
        {
            mHandler.Clear();
        }

        public void HandleErrorFor(Expression<Func<T, object>> property, string message)
        {
            var name = property.GetCorrectPropertyName();
            mHandler.HandleError(name, message);
        }

        public void OnErrorFor(Expression<Func<T, object>> property, Action<string> action)
        {
            var propName = property.GetCorrectPropertyName();
            mHandler.OnErrorFor(propName, action);
        }

        public void OnErrorFor(string propertyName, Action<string> action)
        {
            mHandler.OnErrorFor(propertyName, action);
        }

        public ErrorHandlerAdapter(IInternalErrorHandler handler)
        {
            mHandler = handler;
        }
    }
}
