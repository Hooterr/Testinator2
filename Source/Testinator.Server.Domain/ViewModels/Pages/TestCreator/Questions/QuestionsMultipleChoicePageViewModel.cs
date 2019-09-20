using System.Windows.Input;
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
        private QuestionEditorMultipleChoice mEditor;

        #endregion

        #region Public Properties

        public InputField<string> Task { get; set; }
        public InputField<string> AnswerA { get; set; }
        public InputField<string> AnswerB { get; set; }
        public InputField<string> AnswerC { get; set; }
        public InputField<string> Points { get; set; }
        public InputField<string> RightAnswer { get; set; }

        #endregion

        #region Commands

        /// <summary>
        /// The command to submit current state of question
        /// </summary>
        public ICommand SubmitCommand { get; private set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public QuestionsMultipleChoicePageViewModel(ITestCreatorService testCreatorService)
        {
            // Inject DI services
            mTestCreator = testCreatorService;

            // Create commands
            SubmitCommand = new RelayCommand(Submit);
        }

        #endregion

        #region Command Methods

        public void InitializeEditor(QuestionEditorMultipleChoice editor)
        {
            // Get the editor itself
            mEditor = editor;

            Task = "DDD";
        }

        /// <summary>
        /// Tries to submit the question to the editor
        /// </summary>
        private void Submit()
        {

        }

        #endregion
    }
}
