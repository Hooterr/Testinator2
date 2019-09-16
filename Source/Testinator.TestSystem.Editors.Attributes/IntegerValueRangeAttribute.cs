using System;
using System.Collections.Generic;
using System.Text;

namespace Testinator.TestSystem.Editors.Attributes
{
    public class IntegerValueRangeAttribute : BaseEditorAttribute
    {
        public int Max { get; private set; }
        public int Min { get; private set; }

        public IntegerValueRangeAttribute(int max, int fromVersion, int min = 0) : base(fromVersion)
        {
            Initialize(max, min);
        }

        private void Initialize(int max, int min)
        {
            if (max < min)
                throw new ArgumentException("Maximum possible value must be grater or equal to minimum possible value");

            Max = max;
            Min = min;
        }
    }
}
