using Testinator.TestSystem.Editors.Test.Builder;
using Testinator.TestSystem.Implementation.Questions;

namespace Testinator.TestSystem.Editors
{
    using ABCQuestionEditor = IQuestionEditor<MultipleChoiceQuestion, IMultipleChoiceQuestionOptionsEditor, IMultipleChoiceQuestionScoringEditor>;

    /// <summary>
    /// Provides editor builders
    /// </summary>
    public static class AllEditors
    {
        /// <summary>
        /// The editor for <see cref="Questions.MultipleChoiceQuestion"/>
        /// </summary>
        public static IEditorBuilder<ABCQuestionEditor, MultipleChoiceQuestion> MultipleChoiceQuestion
            => new EditorBuilder<ABCQuestionEditor, MultipleChoiceQuestion>();

        /// <summary>
        /// The editor for <see cref="ITest"/>
        /// </summary>
        public static ITestEditorBuilder TestEditor => new TestEditorBuilder();
    }
}
