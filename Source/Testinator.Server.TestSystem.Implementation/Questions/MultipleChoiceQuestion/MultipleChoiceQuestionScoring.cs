using System;
using Testinator.TestSystem.Attributes;
using Testinator.TestSystem.Implementation.Questions.ScoringStrategy;

namespace Testinator.TestSystem.Implementation.Questions
{
    [Serializable]
    public sealed class MultipleChoiceQuestionScoring : BaseQuestionScoring<MultipleChoiceQuestionUserAnswer>
    {
        /// <summary>
        /// 0-based index of the correct answer for this question
        /// 0 - A, 1 - B etc...
        /// </summary>
        public int CorrectAnswerIdx { get; internal set; }

        [AvailableStrategies(fromVersion: 1, typeof(AllCorrectScoringStrategy), typeof(CorrespondingFractionScoringStrategy))]
        [DefaultStrategy(typeof(AllCorrectScoringStrategy), fromVersion: 1)]
        public new IScoringStrategy Strategy { get; internal set; }

        protected override int CalculateCorrectPercentage(MultipleChoiceQuestionUserAnswer answer)
        {
            return answer.SelectedAnswerIdx == CorrectAnswerIdx ? MaximumScore : 0;
        }
    }
}
