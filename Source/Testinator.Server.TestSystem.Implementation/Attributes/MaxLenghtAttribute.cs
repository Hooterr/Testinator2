using System;
using System.Collections.Generic;
using System.Text;

namespace Testinator.Server.TestSystem.Implementation.Attributes
{
    internal class MaxLenghtAttribute : BaseEditorAttribute
    {
        public int MaxLength { get; private set; }

        public MaxLenghtAttribute(int maxLenght)
        {
            MaxLength = maxLenght;
        }

        public MaxLenghtAttribute(int maxLenght, int fromVersion) : base(fromVersion)
        {
            MaxLength = maxLenght;
        }
    }
}
