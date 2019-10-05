using System;
using System.Collections.Generic;
using System.Linq;
using Testinator.TestSystem.Abstractions;
using Testinator.TestSystem.Attributes;

namespace Testinator.TestSystem.Implementation.Questions
{
    public class SingleTextBoxQuestionScoring : BaseQuestionScoring<SingleTextBoxQuestionUserAnswer>
    {
        // Maybe add some alternative answers or something
        [CollectionCount(max: 5, min: 1, fromVersion: 1)]
        public IDictionary<string, float> CorrectAnswers { get; internal set; }

        public bool IsCaseSensitive { get; internal set; }

        public override int CheckAnswer(IUserAnswer userAnswer)
        {
            var answerImpl = userAnswer as SingleTextBoxQuestionUserAnswer ?? throw new ArgumentException($"UserAnswer must be of type {typeof(SingleTextBoxQuestionUserAnswer).Name}.", nameof(userAnswer));

            var multiplier = 0d;

            if (IsCaseSensitive)
                multiplier = CorrectAnswers.Keys.Contains(answerImpl.UserAnswerText) ? 1 : 0;
            else
            {
                var userAnswerLower = answerImpl.UserAnswerText.ToLower();
                var possibleMultipler = CorrectAnswers.Where(x => x.Key.ToLower() == userAnswerLower).Select(x => x.Value).ToList();
                if (possibleMultipler.Count > 1)
                    throw new NotSupportedException("More than one matching correct answer was found");
                multiplier = possibleMultipler.FirstOrDefault();
            }

            return (int)(MaximumScore * multiplier);
                
        }

        // Not used
        protected override int CalculateCorrectPercentage(SingleTextBoxQuestionUserAnswer userAnswer)
        {
            throw new NotSupportedException();
        }
    }
}
