using System;

namespace Demo
{
    [Serializable]
    public sealed class MultipleChoiceQuestion : BaseQuestion
    {

        #region Implementation

        public new MultipleChoiceQuestionOptions Options { get; internal set; }

        public new MultipleChoiceQuestionScoring Scoring { get; internal set; }

        #endregion

    }
}
