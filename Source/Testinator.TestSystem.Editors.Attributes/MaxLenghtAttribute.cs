using System;
using System.Collections.Generic;
using System.Text;

namespace Testinator.TestSystem.Attributes
{
    public class MaxLenghtAttribute : BaseEditorAttribute
    {
        public int MaxLength { get; private set; }

        public MaxLenghtAttribute(int maxLenght, int fromVersion) : base(fromVersion)
        {
            MaxLength = maxLenght;
        }
    }
}
