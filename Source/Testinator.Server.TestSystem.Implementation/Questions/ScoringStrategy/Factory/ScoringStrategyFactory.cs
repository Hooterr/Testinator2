using System;
using System.Collections.Generic;
using System.Text;

namespace Testinator.TestSystem.Implementation.Questions.ScoringStrategy
{
    public static class ScoringStrategyFactory
    {
        public static T GetStrategy<T>()
            where T : IScoringStrategy, new()
        {
            return new T();
        }
    }
}
