using Testinator.TestSystem.Attributes;

namespace Testinator.TestSystem.Editors
{
    /// <summary>
    /// The editor for scoring part of the question
    /// </summary>
    public interface IMultipleChoiceQuestionScoringEditor : IQuestionScoringEditor//, IErrorListener<IMultipleChoiceQuestionScoringEditor>
    {
        /// <summary>
        /// 0-based index of the correct answer for this question
        /// </summary>
        [EditorProperty]
        int CorrectAnswerIdx { get; set; }
    }
}
