using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Testinator.Server.TestSystem.Implementation.Attributes;

namespace Testinator.Server.TestSystem.Implementation
{
    internal abstract class ErrorListener<TIntereface> : IErrorListener<TIntereface>
    {
        private readonly Dictionary<string, Action<string>> mErrorHandlers;
        private readonly List<string> mUnHandledErrorMessages;

        public void OnErrorFor(Expression<Func<TIntereface, object>> propertyExpression, Action<string> action)
        {
            var propertyInfo = ExpressionHelpers.GetPropertyInfo(propertyExpression);

            if (propertyInfo.GetCustomAttributes<EditorPropertyAttribute>(true).Any() == false)
                throw new ArgumentException($"This property is not an editor property.");

            mErrorHandlers[propertyInfo.Name] = action;
        }

        protected ErrorListener()
        {
            mErrorHandlers = new Dictionary<string, Action<string>>();
            mUnHandledErrorMessages = new List<string>();

            // Get all editor properties and create the error handlers map
            var methodProperties = typeof(TIntereface).GetAllProperties()
                              .Where(field => field.GetCustomAttributes<EditorPropertyAttribute>(true).Any())
                              .Select(field => field.Name)
                              .ToList();

            mErrorHandlers = methodProperties.ToDictionary(k => k, v => default(Action<string>));
        }

        protected void HandleErrorFor(Expression<Func<TIntereface, object>> propertyExpression, string message)
        {
            var propertyName = ExpressionHelpers.GetCorrectPropertyName(propertyExpression);

            if (false == mErrorHandlers.ContainsKey(propertyName))
                throw new ArgumentException($"{propertyName} doesn't have {nameof(EditorPropertyAttribute)} thus it can't be used in {nameof(HandleErrorFor)} method.");

            // If there is handler for that method
            if (mErrorHandlers[propertyName] != null)
                mErrorHandlers[propertyName].Invoke(message);
            else
                mUnHandledErrorMessages.Add(message);
        }

        protected void HandleError(string message)
        {
            mUnHandledErrorMessages.Add(message);
        }

        protected List<string> GetUnhandledErrors()
        {
            return mUnHandledErrorMessages;
        }

        protected void ClearAllErrors()
        {
            mUnHandledErrorMessages.Clear();
        }
    }
}
