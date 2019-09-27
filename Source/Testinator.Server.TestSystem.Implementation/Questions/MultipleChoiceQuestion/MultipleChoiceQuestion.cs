using System;

namespace Testinator.TestSystem.Implementation.Questions
{
    [Serializable]
    public sealed class MultipleChoiceQuestion : BaseQuestion
    {

        #region Implementation

        public new MultipleChoiceQuestionOptions Options
        {
            get => (MultipleChoiceQuestionOptions)(base.Options);
            internal set => base.Options = value;
        }

        public new MultipleChoiceQuestionScoring Scoring
        {
            get => (MultipleChoiceQuestionScoring)(base.Scoring);
            internal set => base.Scoring = value;
        }

        #endregion

        public MultipleChoiceQuestion() { }

    }
}
