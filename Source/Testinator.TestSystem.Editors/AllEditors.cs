using Testinator.TestSystem.Editors.Test.Builder;
using Testinator.TestSystem.Implementation.Questions;

namespace Testinator.TestSystem.Editors
{
    using IQuestionEditorMultipleChoice = IQuestionEditor<MultipleChoiceQuestion, IMultipleChoiceQuestionOptionsEditor, IMultipleChoiceQuestionScoringEditor>;
    using IQuestionEditorMultipleCheckBoxes = IQuestionEditor<MultipleCheckBoxesQuestion, IMultipleCheckBoxesQuestionOptionsEditor, IMultipleCheckBoxesQuestionScoringEditor>;

    /// <summary>
    /// Provides editor builders
    /// </summary>
    public static class AllEditors
    {
        /// <summary>
        /// The editor for <see cref="MultipleChoiceQuestion"/>
        /// </summary>
        public static IEditorBuilder<IQuestionEditorMultipleChoice, MultipleChoiceQuestion> MultipleChoiceQuestion
            => new EditorBuilder<IQuestionEditorMultipleChoice, MultipleChoiceQuestion>();

        /// <summary>
        /// The editor for <see cref="MultipleCheckBoxesQuestion"/>
        /// </summary>
        public static IEditorBuilder<IQuestionEditorMultipleCheckBoxes, MultipleCheckBoxesQuestion> MultipleCheckBoxesQuestion
            => new EditorBuilder<IQuestionEditorMultipleCheckBoxes, MultipleCheckBoxesQuestion>();

        /// <summary>
        /// The editor for <see cref="ITest"/>
        /// </summary>
        public static ITestEditorBuilder TestEditor => new TestEditorBuilder();
    }
}
