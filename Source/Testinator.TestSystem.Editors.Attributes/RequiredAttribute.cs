using System;
using System.Collections.Generic;
using System.Text;

namespace Testinator.TestSystem.Attributes
{
    public class RequiredAttribute : BaseEditorAttribute
    {
        public bool Required { get; private set; }

        public RequiredAttribute(int fromVersion, bool required = true) : base(fromVersion)
        {
            Required = true;
        }
    }
}
