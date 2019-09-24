using System;
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

            mHandler.HandleErrorFor(name, message);
        }

        public void OnErrorFor(Expression<Func<T, object>> property, Action<string> action)
        {
            var propName = property.GetCorrectPropertyName();
            mHandler.OnErrorFor(propName, action);
        }

        private ErrorHandlerAdapter(IInternalErrorHandler handler, string parentEditorName)
        {
            if (handler == null)
                throw new ArgumentNullException();
            mHandler = handler;
            mParentEditorName = parentEditorName;
        }

        public static ErrorHandlerAdapter<T> NestedEditor(IInternalErrorHandler handler, string parentEditorName) => new ErrorHandlerAdapter<T>(handler, parentEditorName);

        public static ErrorHandlerAdapter<T> TopLevelEditor(IInternalErrorHandler handler) => new ErrorHandlerAdapter<T>(handler, string.Empty);
    }
}
