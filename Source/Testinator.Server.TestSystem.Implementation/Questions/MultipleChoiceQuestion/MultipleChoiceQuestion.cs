using Testinator.TestSystem.Abstractions;

namespace Testinator.Server.TestSystem.Implementation.Questions
{
    public sealed class MultipleChoiceQuestion : Question
    {
        #region Private Members 


        #endregion

        #region Abstract Members Implementation

        public override IQuestionTask Task => TaskImpl;

        public override IQuestionAnswer Answer => Options;

        public override IEvaluable Scoring => PointScoring;

        #endregion


        #region Public Properties

        public new int Version { get; internal set; }

        public MultipleChoiceAnswer Options { get; internal set; }

        public PointScoring PointScoring { get; internal set; }

        public QuestionTask TaskImpl { get; internal set; }

        #endregion
    }
}
