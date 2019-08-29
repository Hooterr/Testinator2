using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Testinator.Server.TestSystem.Implementation.Attributes;

namespace Testinator.Server.TestSystem.Implementation
{
    internal abstract class BaseErrorListener<TIntereface> : IErrorListener<TIntereface>
    {
        private readonly Dictionary<string, Action<string>> mErrorHandlers;
        private readonly Dictionary<string, string> mUnHandledErrorMessages;

        public void OnErrorFor(Expression<Func<TIntereface, object>> propertyExpression, Action<string> action)
        {
            var expression = (MemberExpression)propertyExpression.Body;
            var propertyInfo = (PropertyInfo)expression.Member;

            if (propertyInfo.GetCustomAttributes<EditorFieldAttribute>().Any() == false)
                throw new ArgumentException($"This property is not an editor property.");

            mErrorHandlers[propertyInfo.Name] = action;
        }

        protected BaseErrorListener()
        {
            mErrorHandlers = new Dictionary<string, Action<string>>();
            mUnHandledErrorMessages = new Dictionary<string, string>();

            // Get all editor methods and create the error handlers map
            var methodNames = typeof(TIntereface).GetProperties()
                              .Where(field => field.GetCustomAttributes<EditorFieldAttribute>().Any())
                              .Select(field => field.Name)
                              .ToList();

            mErrorHandlers = methodNames.ToDictionary(k => k, v => default(Action<string>));
            mUnHandledErrorMessages = methodNames.ToDictionary(k => k, v => default(string));
        }

        protected void HandleErrorFor(Expression<Func<TIntereface, object>> propertyExpression, string message)
        {
            var propertyName = ExpressionHelpers.GetCorrectPropertyName(propertyExpression);
            // If there is handler for that method
            if (mErrorHandlers[propertyName] != null)
                mErrorHandlers[propertyName].Invoke(message);
            else
                mUnHandledErrorMessages[propertyName] = message;
        }

        protected List<string> GetUnhandledErrors()
        {
            var UnhanledErrorMessages = mUnHandledErrorMessages.Values.Where(x => !string.IsNullOrEmpty(x)).ToList();
            return UnhanledErrorMessages;
        }

        protected void ClearAllErrors()
        {
            mUnHandledErrorMessages.Keys.ToList().ForEach(k => mUnHandledErrorMessages[k] = default);
        }
    }
}
