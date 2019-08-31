using System;
using System.Collections.Generic;
using System.Text;
using Testinator.Server.TestSystem.Implementation.Questions.ScoringStrategy;
using Testinator.TestSystem.Abstractions;

namespace Testinator.Server.TestSystem.Implementation.Questions
{
    public abstract class BaseQuestionScoring<TQuestionUserAnswer> : IQuestionScoring
        where TQuestionUserAnswer : class
    {
        // TODO [MaxValue] or value range
        public int MaximumScore { get; internal set; }

        //TODO [Required]
        public IScoringStrategy Strategy { get; internal set; }

        public int CheckAnswer(IUserAnswer userAnswer)
        {
            var answerImpl = userAnswer as TQuestionUserAnswer ?? throw new ArgumentException($"UserAnswer must be of type {typeof(TQuestionUserAnswer).Name}.", nameof(userAnswer);

            var percentageCorrect = CalculateCorrectPercentage(answerImpl);

            return Strategy.Evaluate(MaximumScore, percentageCorrect);
        }

        protected abstract int CalculateCorrectPercentage(TQuestionUserAnswer userAnswer);

        public BaseQuestionScoring()
        {
            // TODO attribute validation or somewhere else, I dunno yet
        }
    }
}
