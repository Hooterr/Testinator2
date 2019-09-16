using Testinator.TestSystem.Editors.Test.Builder;
using Testinator.TestSystem.Implementation.Questions;

namespace Testinator.TestSystem.Editors
{
    using ABCQuestionEditor = IQuestionEditor<MultipleChoiceQuestion, IMultipleChoiceQuestionOptionsEditor, IMultipleChoiceQuestionScoringEditor>;

    /// <summary>
    /// Provides editor builders for every question type
    /// </summary>
    public static class AllEditors
    {
        /// <summary>
        /// The editor for <see cref="Questions.MultipleChoiceQuestion"/>
        /// </summary>
        public static IEditorBuilder<ABCQuestionEditor, MultipleChoiceQuestion> MultipleChoiceQuestion
            => new EditorBuilder<ABCQuestionEditor, MultipleChoiceQuestion>();

        public static ITestEditorBuilder TestEditor => new TestEditorBuilder();
    }
}
