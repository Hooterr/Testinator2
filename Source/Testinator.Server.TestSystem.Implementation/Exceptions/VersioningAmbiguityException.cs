using System;
using System.Collections.Generic;
using System.Text;

namespace Testinator.Server.TestSystem.Implementation.Exceptions
{
    public class VersioningAmbiguityException : Exception
    {
        public VersioningAmbiguityException(string msg) : base(msg)
        { }

        public VersioningAmbiguityException() : base()
        { }
    }
}
