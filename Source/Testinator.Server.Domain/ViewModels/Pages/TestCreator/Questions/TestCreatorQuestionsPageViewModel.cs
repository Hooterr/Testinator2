using System;
using System.Windows.Input;
using Testinator.Core;
using Testinator.TestSystem.Abstractions;
using Testinator.TestSystem.Editors;

namespace Testinator.Server.Domain
{
    /// <summary>
    /// The view model for questions page in Test Creator
    /// This is the master page that contains specific question types pages inside
    /// </summary>
    public class TestCreatorQuestionsPageViewModel : PageHostViewModel<QuestionsPage>
    {
        #region Private Members

        private readonly ITestCreatorService mTestCreator;
        private readonly ApplicationViewModel mApplicationVM;

        /// <summary>
        /// The editor for questions list in this page
        /// </summary>
        private readonly IQuestionEditorCollection mEditor;

        /// <summary>
        /// The function of current question's view model that should be fired to submit the question state
        /// </summary>
        private Func<IQuestion> mSubmitQuestionAction;

        #endregion

        #region Public Properties

        /// <summary>
        /// Indicates if user is in question creation mode
        /// </summary>
        public bool IsCreatingQuestion { get; set; }

        #endregion

        #region Commands

        /// <summary>
        /// The command to create new question of the type multiple choice
        /// </summary>
        public ICommand NewQuestionMultipleChoiceCommand { get; private set; }

        /// <summary>
        /// The command to create new question of the type checkboxes
        /// </summary>
        public ICommand NewQuestionCheckboxesCommand { get; private set; }

        /// <summary>
        /// The command to submit the question that is created
        /// </summary>
        public ICommand SubmitQuestionCommand { get; private set; }

        /// <summary>
        /// The command to finish this page and move forward with test creation
        /// </summary>
        public ICommand FinishQuestionsCommand { get; private set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public TestCreatorQuestionsPageViewModel(ITestCreatorService testCreatorService, ApplicationViewModel applicationVM)
        {
            // Inject DI services
            mTestCreator = testCreatorService;
            mApplicationVM = applicationVM;

            // Create commands
            NewQuestionMultipleChoiceCommand = new RelayCommand(() => GoToMultipleChoiceQuestion());
            NewQuestionCheckboxesCommand = new RelayCommand(() => GoToCheckboxesQuestion());
            SubmitQuestionCommand = new RelayCommand(SubmitCurrentQuestion);
            FinishQuestionsCommand = new RelayCommand(GoToNextPage);

            // Get the editor for questions
            mEditor = mTestCreator.GetEditorTestQuestions();
        }

        #endregion

        #region Command Methods

        /// <summary>
        /// Starts the edit for multiple choice question
        /// </summary>
        /// <param name="questionNumber">The question's data that can be pre-loaded, if its null, we create new question</param>
        private void GoToMultipleChoiceQuestion(int? questionNumber = null)
        {
            // Get the editor for this specific question
            var editor = mTestCreator.GetEditorMultipleChoice(questionNumber);

            // Create the view model for this question
            var viewModel = new QuestionsMultipleChoicePageViewModel();

            // Initialize the view model with given editor, getting the submit action in return
            mSubmitQuestionAction = viewModel.InitializeEditor(editor);

            // Show the page
            IsCreatingQuestion = true;
            GoToPage(QuestionsPage.MultipleChoice, viewModel);
        }

        /// <summary>
        /// Starts the edit for checkboxes question
        /// </summary>
        /// <param name="questionNumber">The question's data that can be pre-loaded, if its null, we create new question</param>
        private void GoToCheckboxesQuestion(int? questionNumber = null)
        {
            // TODO: Get editor checkboxes

            // Show the page
            IsCreatingQuestion = true;
            GoToPage(QuestionsPage.Checkboxes);
        }

        /// <summary>
        /// Tries to submit current state of question to the test
        /// </summary>
        private void SubmitCurrentQuestion()
        {
            // Fire the submit action
            var question = mSubmitQuestionAction();

            // If question was not built successfully...
            if (question == null)
            {
                // Don't do anything there since the question page itself will show every error
                return;
            }

            // Otherwise we have a ready question
            // Add it to test
            mEditor.Add(question);

            // We're done creating the question, hide the page
            IsCreatingQuestion = false;
        }

        /// <summary>
        /// Checks if we have enough questions in the test and goes to the grading page
        /// </summary>
        private void GoToNextPage()
        {
            // TODO: Validate questions and stuff

            // Go to grading page
            mApplicationVM.GoToPage(ApplicationPage.TestCreatorGrading);
        }

        #endregion
    }
}
