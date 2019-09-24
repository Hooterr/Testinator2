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
                }
            }

            if(name != null)
                mHandler.HandleErrorFor(name, message);
        }

        public void OnErrorFor(Expression<Func<T, object>> property, Action<string> action)
        {
            var propName = property.GetCorrectPropertyName();
            mHandler.OnErrorFor(propName, action);
        }

        public ErrorHandlerAdapter(IInternalErrorHandler handler, string parentEditorName)
        {
            mHandler = handler;
            mParentEditorName = parentEditorName;
        }
    }
}
