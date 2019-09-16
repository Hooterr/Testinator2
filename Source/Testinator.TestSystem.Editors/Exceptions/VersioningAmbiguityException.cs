using System;

namespace Testinator.TestSystem.Editors
{
    /// <summary>
    /// Thrown when a property contains ambiguous versioning
    /// /// </summary>
    public class VersioningAmbiguityException : Exception
    {
        /// <summary>
        /// Initializes the exception with the error message explaining the reason for this exception
        /// </summary>
        /// <param name="msg">The reason for this exception</param>
        public VersioningAmbiguityException(string msg) : base(msg)
        { }

        /// <summary>
        /// Initializes the exception
        /// </summary>
        public VersioningAmbiguityException() : base()
        { }
    }
}
