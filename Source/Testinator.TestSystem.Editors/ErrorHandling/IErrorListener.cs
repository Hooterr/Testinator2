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
    }
}
