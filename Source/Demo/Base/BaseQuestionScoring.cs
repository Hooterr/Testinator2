using System;
using Testinator.TestSystem.Implementation.Questions.ScoringStrategy;

namespace Demo
{
    [Serializable]
    public abstract class BaseQuestionScoring
    {

        public int MaximumScore { get; internal set; }

        public IScoringStrategy Strategy { get; internal set; }

    }
}
