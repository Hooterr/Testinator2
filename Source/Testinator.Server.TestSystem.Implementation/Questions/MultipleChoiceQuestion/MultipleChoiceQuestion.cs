using Testinator.Server.TestSystem.Implementation.Questions.Base;
using Testinator.TestSystem.Abstractions;

namespace Testinator.Server.TestSystem.Implementation.Questions
{
    public sealed class MultipleChoiceQuestion : BaseQuestion
    {
        #region Private Members 

        #endregion

        #region Implementation

        public new MultipleChoiceQuestionOptions Options { get; internal set; }

        public new MultipleChoiceQuestionScoring Scoring { get; internal set; }

        #endregion

        public MultipleChoiceQuestion(IQuestionTask task, MultipleChoiceQuestionScoring scoring, MultipleChoiceQuestionOptions options, string author, int version)
            // TODO replace null with a category
            : base(task, scoring, options, author, null, version)
        {
        }
    }
}
