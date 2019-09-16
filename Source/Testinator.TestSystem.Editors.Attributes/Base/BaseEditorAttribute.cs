using System;
using System.Collections.Generic;
using System.Text;

namespace Testinator.TestSystem.Editors.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public abstract class BaseEditorAttribute : Attribute
    {
        public int FromVersion { get; protected set; }

        public BaseEditorAttribute(int fromVersion)
        {
            FromVersion = fromVersion;
        }
    }
}
