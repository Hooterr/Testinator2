using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Testinator.TestSystem.Editors
{
    /// <summary>
    /// The implementation of <see cref="IErrorListener{T}"/>
    /// </summary>
    /// <typeparam name="TIntereface">The interface the implementation of the editor is hidden behind</typeparam>
    internal class ErrorListener<TIntereface> : IErrorListener<TIntereface>, IInternalErrorHandler
    {
        #region Private Members

        private HandlersCollection mHandlers;

        private IList<string> mUnhandled;

        #endregion

        #region Public Methods

        /// <summary>
        /// Sets the method to execute when an error occurs for the given editor property
        /// </summary>
        /// <param name="propertyExpression">The editor property</param>
        /// <param name="action">The action to execute when an error occurs of the editor property</param>
        public void OnErrorFor(Expression<Func<TIntereface, object>> propertyExpression, Action<string> action)
        {
            var propName = propertyExpression.GetCorrectPropertyName();
            mHandlers[propName] = action;

        }

        public void OnErrorFor(string propertyName, Action<string> action)
        {
            mHandlers[propertyName] = action;
        }

        #endregion

        #region All Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public ErrorListener()
        {
            mHandlers = new HandlersCollection(typeof(TIntereface).GetHandlersTree());
            mUnhandled = new List<string>();
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
            HandleError(propertyName, message);
        }

        /// <summary>
        /// Handles an error not associated with any property or editor
        /// </summary>
        /// <param name="message">The error message</param>
        protected void HandleError(string message)
        {
            mUnhandled.Add(message);
        }

        /// <summary>
        /// Gets all of the errors that haven't already been handled 
        /// </summary>
        /// <returns>The list of error messages strings</returns>
        protected IList<string> GetUnhandledErrors()
        {
            return mUnhandled;
        }

        /// <summary>
        /// Clears all error messages
        /// </summary>
        public void Clear()
        {
            mUnhandled.Clear();
            mHandlers.ClearErrors();
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

        public void HandleError(string propertyName, string message)
        {
            if (!mHandlers.HandleError(propertyName, message))
                mUnhandled.Add(message);
        }

        #endregion
    }
}
