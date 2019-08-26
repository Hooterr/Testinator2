using System;
using System.Collections.Generic;
using System.Text;
using Testinator.TestSystem.Abstractions;

namespace Testinator.Server.TestSystem.Implementation.Questions
{
    public class MultipleCheckboxesQuestionScoring : BaseQuestionScoring
    {

        public List<bool> CorrectAnswer { get; }



        public override int CheckAnswer(IUserAnswer userAnswer)
        {
            var answerImpl = userAnswer as MultipleCheckboxesQuestionUserAnswer ?? throw new NotSupportedException($"Incompatible UserAnswer type.");

            if (answerImpl.CheckedOptions.Count != CorrectAnswer.Count)
                throw new ArgumentException("Incompatible user answer length.", nameof(userAnswer));

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
