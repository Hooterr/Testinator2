using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Testinator.TestSystem.Editors
{
    internal class ErrorHandlerAdapter<T> : IErrorHandlerAdapter<T>
    {
        private readonly IInternalErrorHandler mHandler;
        private readonly string mParentEditorName;

        public void Clear()
        {
            mHandler.Clear();
        }

        public void HandleErrorFor(Expression<Func<T, object>> expression, string message)
        {
            var name = GetPropertyName(expression);
            mHandler.HandleErrorFor(name, message);
        }

        public void OnErrorFor(Expression<Func<T, object>> expression, ICollection<string> handler)
        {
            var propName = GetPropertyName(expression);
            mHandler.OnErrorFor(propName, handler);
        }

        private ErrorHandlerAdapter(IInternalErrorHandler handler, string parentEditorName)
        {
            mHandler = handler ?? throw new ArgumentNullException(nameof(handler));
            mParentEditorName = parentEditorName;
        }

        public static ErrorHandlerAdapter<T> NestedEditor(IInternalErrorHandler handler, string parentEditorName) => new ErrorHandlerAdapter<T>(handler, parentEditorName);

        public static ErrorHandlerAdapter<T> TopLevelEditor(IInternalErrorHandler handler) => new ErrorHandlerAdapter<T>(handler, string.Empty);

        private string GetPropertyName(Expression<Func<T, object>> expression)
        {
            string name = null;
            try
            {
                name = expression.GetCorrectPropertyName();
            }
            catch { }
            finally
            {
                if (name == null)
                {
                    if (expression.GetObjectType() == typeof(T))
                        name = mParentEditorName;
                    else
                        name = string.Empty;
                }
            }
            return name;
        }

    }
}
