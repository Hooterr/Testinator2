namespace Testinator.TestSystem.Implementation.Questions
{
    public sealed class MultipleChoiceQuestionScoring : BaseQuestionScoring<MultipleChoiceQuestionUserAnswer>
    {
        /// <summary>
        /// 0-based index of the correct answer for this question
        /// 0 - A, 1 - B etc...
        /// </summary>
        public int CorrectAnswerIdx { get; internal set; }

        protected override int CalculateCorrectPercentage(MultipleChoiceQuestionUserAnswer answer)
        {
            return answer.SelectedAnswerIdx == CorrectAnswerIdx ? MaximumScore : 0;
        }
    }
}
