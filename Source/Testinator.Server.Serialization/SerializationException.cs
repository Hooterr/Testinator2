using System;
using System.Collections.Generic;
using System.Text;

namespace Testinator.Server.Serialization
{
    public class SerializationException : Exception
    {
        public SerializationException(string message) : base(message) { }
        public SerializationException(string message, Exception innerException) : base(message, innerException) { }
    }

}
