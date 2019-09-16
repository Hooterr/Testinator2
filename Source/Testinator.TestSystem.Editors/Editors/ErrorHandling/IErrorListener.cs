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
        /// Add method to execute when an error occurs
        /// </summary>
        /// <param name="action">The method</param>
        void OnErrorFor(Expression<Func<T, object>> propertyExpression, Action<string> action);
    }
}
