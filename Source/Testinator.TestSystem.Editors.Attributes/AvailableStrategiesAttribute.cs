using System;
using System.Collections.Generic;
using System.Text;

namespace Testinator.TestSystem.Attributes
{
    public class AvailableStrategiesAttribute : BaseEditorAttribute
    {
        public Type[] AvailableStrategies { get; private set; }

        public AvailableStrategiesAttribute(int fromVersion, params Type[] strategies) : base(fromVersion)
        {
            AvailableStrategies = strategies;
        }
    }
}
