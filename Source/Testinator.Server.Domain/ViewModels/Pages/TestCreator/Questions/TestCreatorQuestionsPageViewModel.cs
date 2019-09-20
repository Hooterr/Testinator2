using System;
using System.Windows.Input;
using Testinator.Core;
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
            FinishQuestionsCommand = new RelayCommand(GoToNextPage);
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

            // Show the page
            GoToPage(QuestionsPage.MultipleChoice);

            // Pass the editor for this question
            (CurrentPageViewModel as QuestionsMultipleChoicePageViewModel).InitializeEditor(editor);
        }

        /// <summary>
        /// Starts the edit for checkboxes question
        /// </summary>
        /// <param name="questionNumber">The question's data that can be pre-loaded, if its null, we create new question</param>
        private void GoToCheckboxesQuestion(int? questionNumber = null)
        {
            // TODO: Get editor checkboxes

            // Show the page
            GoToPage(QuestionsPage.Checkboxes);
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
