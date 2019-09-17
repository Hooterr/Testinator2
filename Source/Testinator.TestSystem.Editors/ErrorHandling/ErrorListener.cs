using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Testinator.TestSystem.Attributes;

namespace Testinator.TestSystem.Editors
{
    /// <summary>
    /// The implementation of <see cref="IErrorListener{T}"/>
    /// </summary>
    /// <typeparam name="TIntereface">The interface the implementation of the editor is hidden behind</typeparam>
    internal abstract class ErrorListener<TIntereface> : IErrorListener<TIntereface>
    {
        #region Private Members

        /// <summary>
        /// Key: property name Value: action to execute when error occurs concerning that property
        /// </summary>
        private readonly Dictionary<string, Action<string>> mErrorHandlers;

        /// <summary>
        /// Errors for properties that don't have error handlers defined by the user and all other errors
        /// </summary>
        private readonly List<string> mUnHandledErrorMessages;

        private Action<string> mInterceptUnhandledErrorHandler;
        #endregion

        #region Internal Methods

        internal void OnUnhandledError(Action<string> handler)
        {
            mInterceptUnhandledErrorHandler = handler;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Sets the method to execute when an error occurs for the given editor property
        /// </summary>
        /// <param name="propertyExpression">The editor property</param>
        /// <param name="action">The action to execute when an error occurs of the editor property</param>
        public void OnErrorFor(Expression<Func<TIntereface, object>> propertyExpression, Action<string> action)
        {
            var propertyInfo = propertyExpression.GetPropertyInfo();

            if (propertyInfo.GetCustomAttributes<EditorPropertyAttribute>(true).Any() == false)
                throw new ArgumentException($"This property is not an editor property.");

            mErrorHandlers[propertyInfo.Name] = action;
        }

        protected bool HasHandlerFor(Expression<Func<TIntereface, object>> propertyExpression)
        {
            var propertyName = propertyExpression.GetCorrectPropertyName();
            return mErrorHandlers.ContainsKey(propertyName);
        }

        #endregion

        #region All Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        protected ErrorListener()
        {
            // Create defaults
            mErrorHandlers = new Dictionary<string, Action<string>>();
            mUnHandledErrorMessages = new List<string>();

            // Get all editor properties and create the error handlers map
            var editorProperties = typeof(TIntereface)
                              .GetAllProperties()
                              .Where(field => field.GetCustomAttributes<EditorPropertyAttribute>(inherit: true).Any())
                              .Select(field => field.Name)
                              .ToList();

            mErrorHandlers = editorProperties.ToDictionary(k => k, v => default(Action<string>));
        }

        #endregion

        #region Protected Properties

        /// <summary>
        /// Handles the error for the given editor property
        /// </summary>
        /// <param name="propertyExpression">The property this error concerns</param>
        /// <param name="message">The error message</param>
        protected void HandleErrorFor(Expression<Func<TIntereface, object>> propertyExpression, string message)
        {
            var propertyName = propertyExpression.GetCorrectPropertyName();

            if (false == mErrorHandlers.ContainsKey(propertyName))
                throw new ArgumentException($"{propertyName} doesn't have {nameof(EditorPropertyAttribute)} thus it can't be used in {nameof(HandleErrorFor)} method.");

            // If there is handler for that method execute the associated action
            if (mErrorHandlers[propertyName] != null)
                mErrorHandlers[propertyName].Invoke(message);
            // If we got an intercept handler
            else if (mInterceptUnhandledErrorHandler != null)
            {
                // Handle the error and quit
                mInterceptUnhandledErrorHandler.Invoke(message);
                return;
            }
            // The error has not been handled, add it to the list
            mUnHandledErrorMessages.Add(message);
        }

        /// <summary>
        /// Handles an error not associated with any property
        /// </summary>
        /// <param name="message">The error message</param>
        protected void HandleError(string message)
        {
            // If we got an intercept handler
            if (mInterceptUnhandledErrorHandler != null)
            {
                // Handle the error and quit
                mInterceptUnhandledErrorHandler.Invoke(message);
                return;
            }

            mUnHandledErrorMessages.Add(message);
        }

        /// <summary>
        /// Gets all of the errors that haven't already been handled 
        /// </summary>
        /// <returns>The list of error messages strings</returns>
        protected List<string> GetUnhandledErrors()
        {
            return mUnHandledErrorMessages;
        }

        /// <summary>
        /// Clears all error messages
        /// </summary>
        protected void ClearAllErrors()
        {
            // Invoke all the methods with empty string to clear the message
            foreach (var action in mErrorHandlers.Values)
                if (action != null)
                    action.Invoke(string.Empty);

            // Clear the unhandled errors too
            mUnHandledErrorMessages.Clear();
        }

        /// <summary>
        /// Validates the current state of the editor.
        /// Called during build process. During this validation implementer should call HandleError/HandleErrorFor to populate error messages
        /// </summary>
        /// <returns>True if validation was successful, otherwise false</returns>
        public virtual bool Validate()
        {
            return true;
        }


        #endregion
    }
}
