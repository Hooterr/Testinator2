using System;
using System.Collections.Generic;
using System.Text;

namespace Testinator.TestSystem.Attributes
{
    public class DefaultStrategyAttribute : BaseEditorAttribute
    {
        public Type DefaultStrategyType { get; private set; }

        public DefaultStrategyAttribute(Type defaultStrategy, int fromVersion) : base(fromVersion)
        {
            DefaultStrategyType = defaultStrategy;
        }
    }
}
