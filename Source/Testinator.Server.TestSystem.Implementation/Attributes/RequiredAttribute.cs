using System;
using System.Collections.Generic;
using System.Text;

namespace Testinator.Server.TestSystem.Implementation.Attributes
{
    internal class RequiredAttribute : BaseEditorAttribute
    {
        public bool Required { get; private set; }

        public RequiredAttribute(bool required = true) : base() 
        {
            Required = required;
        }

        public RequiredAttribute(int fromVersion, bool required = true) : base(fromVersion)
        {
            Required = true;
        }
    }
}
