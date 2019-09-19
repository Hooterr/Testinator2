using System;

namespace Demo
{
    [Serializable]
    public sealed class MultipleChoiceQuestionScoring : BaseQuestionScoring
    {
        /// <summary>
        /// 0-based index of the correct answer for this question
        /// 0 - A, 1 - B etc...
        /// </summary>
        public int CorrectAnswerIdx { get; internal set; }
    }
}
