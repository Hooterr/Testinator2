using Testinator.Server.TestSystem.Implementation.Questions;

namespace Testinator.Server.TestSystem.Implementation
{
    using ABCQuestionEditor = IQuestionEditor<MultipleChoiceQuestion, IMultipleChoiceQuestionOptionsEditor, IQuestionScoringEditor>;
    
    /// <summary>
    /// Provides editor builders for every question type
    /// </summary>
    public static class Editors
    {
        /// <summary>
        /// The editor for <see cref="Questions.MultipleChoiceQuestion"/>
        /// </summary>
        /// 
        public static IEditorBuilder<ABCQuestionEditor, MultipleChoiceQuestion> MultipleChoiceQuestion
            => new EditorBuilder<ABCQuestionEditor, MultipleChoiceQuestion>();
    }
}
