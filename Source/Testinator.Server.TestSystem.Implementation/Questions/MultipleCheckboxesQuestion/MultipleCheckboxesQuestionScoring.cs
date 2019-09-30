using System;
using System.Collections.Generic;
using Testinator.TestSystem.Attributes;
using Testinator.TestSystem.Implementation.Questions.ScoringStrategy;

namespace Testinator.TestSystem.Implementation.Questions
{
    public class MultipleCheckBoxesQuestionScoring : BaseQuestionScoring<MultipleCheckBoxesQuestionUserAnswer>
    {
        public List<bool> CorrectAnswer { get; internal set; }

        [AvailableStrategies(fromVersion: 1, typeof(AllCorrectScoringStrategy), typeof(CorrespondingFractionScoringStrategy))]
        [DefaultStrategy(typeof(AllCorrectScoringStrategy), fromVersion: 1)]
        public new IScoringStrategy Strategy { get; internal set; }

        protected override int CalculateCorrectPercentage(MultipleCheckBoxesQuestionUserAnswer userAnswer)
        {
            if (userAnswer.CheckedOptions.Count != CorrectAnswer.Count)
                throw new ArgumentException("Incompatible user answer length.", nameof(userAnswer));

            // TODO make a simple fraction class
            var correctAnswersCount = 0;

            for(var i = 0; i < CorrectAnswer.Count; i++)
            {
                if (CorrectAnswer[i] == userAnswer.CheckedOptions[i])
                    correctAnswersCount++;
            }

            // Yeah really need that fraction class
            // eg. return new Fraction(correctAnswerCount, TotalCount);
            // or something
            // Yep, i would need this in view models as well
            return (int)Math.Round((double)correctAnswersCount * 100 / CorrectAnswer.Count);
        }
    }
}
