using System;
using System.Collections.Generic;
using System.Text;

namespace Testinator.TestSystem.Abstractions
{
    /// <summary>
    /// Represents evaluable object
    /// </summary>
    /// <typeparam name="T">The type of object to be evaluated</typeparam>
    public interface IPercentageEvaluable<T>
    {
        /// <summary>
        /// Evaluates the given object
        /// </summary>
        /// <param name="toEvaluate">The object to evaluate</param>
        /// <returns>The percentage representation of the evaluated object</returns>
        int Evaluate(T toEvaluate);
    }
}
