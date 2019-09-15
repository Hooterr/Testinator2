using System;
using System.Collections.Generic;
using System.Text;

namespace Testinator.Server.TestSystem.Implementation.Attributes
{
    internal class TimeSpanLimitAttribute : BaseEditorAttribute
    {
        public TimeSpan Max { get; private set; }
        public TimeSpan Min { get; private set; }

        public TimeSpanLimitAttribute(int maxSeconds, int minSeconds, int fromVersion) : base(fromVersion)
        {
            Max = TimeSpan.FromSeconds(maxSeconds);
            Min = TimeSpan.FromSeconds(minSeconds);
        }
    }
}
