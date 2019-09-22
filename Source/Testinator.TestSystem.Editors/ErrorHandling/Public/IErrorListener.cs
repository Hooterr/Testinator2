using System;
using System.Linq.Expressions;

namespace Testinator.TestSystem.Editors
{
    /// <summary>
    /// Provides the option to listen for errors
    /// </summary>
    public interface IErrorListener<T>
    {
        /// <summary>
        /// Adds a method to execute when an error for a given property occurs
        /// </summary>
        /// <param name="propertyExpression">The property</param>
        /// <param name="action">The method</param>
        void OnErrorFor(Expression<Func<T, object>> propertyExpression, Action<string> action);

        /// <summary>
        /// Validates without submitting
        /// Executes error handler if provided with <see cref="OnErrorFor(Expression{Func{T, object}}, Action{string})"/>
        /// </summary>
        /// <returns>True if successful, otherwise false</returns>
        bool Validate();
    }
}
