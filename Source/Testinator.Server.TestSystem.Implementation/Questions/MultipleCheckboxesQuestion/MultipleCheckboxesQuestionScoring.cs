using System;
using System.Collections.Generic;
using System.Text;
using Testinator.TestSystem.Implementation.Questions.ScoringStrategy;
using Testinator.TestSystem.Abstractions;

namespace Testinator.TestSystem.Implementation.Questions
{
    public class MultipleCheckboxesQuestionScoring : BaseQuestionScoring<MultipleCheckboxesQuestionUserAnswer>
    {
        public List<bool> CorrectAnswer { get; }

        protected override int CalculateCorrectPercentage(MultipleCheckboxesQuestionUserAnswer userAnswer)
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
            return (int)Math.Round((double)correctAnswersCount * 100 / CorrectAnswer.Count);
        }

        internal MultipleCheckboxesQuestionScoring(List<bool> correctAnswer)
        {
            if (correctAnswer == null)
                throw new ArgumentNullException(nameof(correctAnswer));

            if (correctAnswer.Count == 0)
                throw new ArgumentException("The list can't be empty", nameof(correctAnswer));

            CorrectAnswer = correctAnswer;
        }
    }
}
