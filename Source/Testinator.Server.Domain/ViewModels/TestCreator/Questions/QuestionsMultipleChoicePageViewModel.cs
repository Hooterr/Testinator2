using Testinator.Core;
using Testinator.TestSystem.Editors;

namespace Testinator.Server.Domain
{
    using QuestionEditorMultipleChoice = IQuestionEditor<TestSystem.Implementation.Questions.MultipleChoiceQuestion, IMultipleChoiceQuestionOptionsEditor, IMultipleChoiceQuestionScoringEditor>;

    /// <summary>
    /// The view model for multiple choice question page in Test Creator
    /// </summary>
    public class QuestionsMultipleChoicePageViewModel : BaseViewModel
    {
        #region Private Members

        private readonly ITestCreatorService mTestCreator;

        /// <summary>
        /// The editor for multiple choice question
        /// </summary>
        private readonly QuestionEditorMultipleChoice mEditor;

        #endregion

        #region Public Properties

        public string Task { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public QuestionsMultipleChoicePageViewModel(ITestCreatorService testCreatorService)
        {
            // Inject DI services
            mTestCreator = testCreatorService;

            // Get the editor associated with this page
            mEditor = mTestCreator.GetEditorMultipleChoice();
        }

        #endregion
    }
}
