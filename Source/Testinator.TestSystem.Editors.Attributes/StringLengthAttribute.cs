using System;
using System.Collections.Generic;
using System.Text;

namespace Testinator.TestSystem.Editors.Attributes
{
    public class StringLengthAttribute : BaseEditorAttribute
    {
        public int Max { get; private set; }
        public int Min { get; private set; }

        public StringLengthAttribute(int max, int fromVersion, int min = 0) : base(fromVersion)
        {
            Initialize(max, min);
        }

        private void Initialize(int max, int min)
        {
            if (max < min)
                throw new ArgumentException("Maximum collection count must be grater or equal to minimum collection count");

            Max = max;
            Min = min;
        }
    }
}
