using System;
using System.Collections.Generic;
using System.Text;

namespace Testinator.Server.TestSystem.Implementation.Attributes
{
    internal class DefaultStrategyAttribute : BaseEditorAttribute
    {
        public Type DefaultStrategyType { get; private set; }

        public DefaultStrategyAttribute(Type defaultStrategy) : base()
        {
            DefaultStrategyType = defaultStrategy;
        }

        public DefaultStrategyAttribute(Type defaultStrategy, int fromVersion) : base(fromVersion)
        {
            DefaultStrategyType = defaultStrategy;
        }
    }
}
