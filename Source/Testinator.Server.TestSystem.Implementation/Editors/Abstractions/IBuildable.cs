using System;
using System.Collections.Generic;
using System.Text;

namespace Testinator.Server.TestSystem.Implementation
{
    /// <summary>
    /// Represents a the ability to finalize a process by building the object
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IBuildable<T>
    {
        /// <summary>
        /// Finalize the process by building
        /// </summary>
        /// <returns>The result of the operation containing the status and eventual error messages</returns>
        OperationResult<T> Build();
    }
}
