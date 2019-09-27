using System;
using System.Collections.Generic;
using System.Text;

namespace Testinator.Server.Serialization
{
    /// <summary>
    /// Thrown when either serialization or deserialization fails
    /// </summary>
    public class SerializationException : Exception
    {
        public SerializationException(string message) : base(message) { }
        public SerializationException(string message, Exception innerException) : base(message, innerException) { }
    }

}
