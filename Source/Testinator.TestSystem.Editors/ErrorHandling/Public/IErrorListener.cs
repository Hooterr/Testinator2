using System;
using System.Collections.Generic;
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
        /// <param name="handler">Collection to add/clear the errors</param>
        void OnErrorFor(Expression<Func<T, object>> propertyExpression, ICollection<string> handler);

        /// <summary>
        /// Validates without submitting
        /// Executes error handlers if provided with <see cref="OnErrorFor(Expression{Func{T, object}}, ICollection{string})"/>
        /// </summary>
        /// <returns>True if successful, otherwise false</returns>
        bool Validate();
    }
}
