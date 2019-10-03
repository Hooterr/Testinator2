using System;
using Testinator.TestSystem.Implementation.Questions;

namespace Testinator.TestSystem.Implementation
{
    [Serializable]
    public class SingleTextBoxQuestion : BaseQuestion
    {
        public new SingleTextBoxQuestionScoring Scoring
        {
            get => base.Scoring as SingleTextBoxQuestionScoring;
            internal set => base.Scoring = value;
        }
        public new SingleTextBoxQuestionOptions Options
        {
            get => base.Options as SingleTextBoxQuestionOptions;
            internal set => base.Options = value;
        }

    }
}
