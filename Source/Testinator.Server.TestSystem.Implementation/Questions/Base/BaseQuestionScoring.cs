using System;
using System.Collections.Generic;
using System.Text;
using Testinator.Server.TestSystem.Implementation.Attributes;
using Testinator.Server.TestSystem.Implementation.Questions.ScoringStrategy;
using Testinator.TestSystem.Abstractions;

namespace Testinator.Server.TestSystem.Implementation.Questions
{
    public abstract class BaseQuestionScoring<TQuestionUserAnswer> : IQuestionScoring
        where TQuestionUserAnswer : class
    {
        [EditorProperty]
        [IntegerValueRange(min: 1, max:100, fromVersion: 1)]
        public int MaximumScore { get; internal set; }

        [EditorProperty]
        [DefaultStrategy(typeof(AllCorrectScoringStrategy), fromVersion: 1)]
        public IScoringStrategy Strategy { get; internal set; }

        public int CheckAnswer(IUserAnswer userAnswer)
        {
            var answerImpl = userAnswer as TQuestionUserAnswer ?? throw new ArgumentException($"UserAnswer must be of type {typeof(TQuestionUserAnswer).Name}.", nameof(userAnswer));

            var percentageCorrect = CalculateCorrectPercentage(answerImpl);

            return Strategy.Evaluate(MaximumScore, percentageCorrect);
        }

        protected abstract int CalculateCorrectPercentage(TQuestionUserAnswer userAnswer);
    }
}
