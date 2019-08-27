using Testinator.Server.TestSystem.Implementation.Questions;

namespace Testinator.Server.TestSystem.Implementation
{
    /// <summary>
    /// Provides editor builders for every question type
    /// </summary>
    public static class Editors
    {
        /// <summary>
        /// The editor for <see cref="Questions.MultipleChoiceQuestion"/>
        /// </summary>
        public static IEditorBuilder<IMultipleChoiceQuestionEditor, MultipleChoiceQuestion> MultipleChoiceQuestion
            => new EditorBuilder<MultipleChoiceQuestionEditor, IMultipleChoiceQuestionEditor, MultipleChoiceQuestion>();
    }
}
