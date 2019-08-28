using Testinator.TestSystem.Abstractions;

namespace Testinator.Server.TestSystem.Implementation.Questions
{
    public sealed class MultipleChoiceQuestion : BaseQuestion
    {

        #region Implementation

        public new MultipleChoiceQuestionOptions Options { get; internal set; }

        public new MultipleChoiceQuestionScoring Scoring { get; internal set; }

        #endregion

        public MultipleChoiceQuestion() { }

    }
}
