using System;
using System.Collections.Generic;
using System.Text;

namespace Testinator.Server.TestSystem.Implementation.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    internal abstract class BaseEditorAttribute : Attribute
    {
        public int FromVersion { get; protected set; }

        public BaseEditorAttribute(int fromVersion = Versions.Lowest)
        {
            FromVersion = fromVersion;
        }
    }
}
