using System;
using System.Collections.ObjectModel;
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
        private readonly IViewModelProvider mViewModelProvider;

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

        /// <summary>
        /// The errors that are displayed in case there is a problem with questions
        /// </summary>
        public InputField<ObservableCollection<QuestionListItemViewModel>> Questions { get; set; } = new InputField<ObservableCollection<QuestionListItemViewModel>>(new ObservableCollection<QuestionListItemViewModel>());

        #endregion

        #region Commands

        /// <summary>
        /// The command to create new question of the type multiple choice
        /// </summary>
        public ICommand NewQuestionMultipleChoiceCommand { get; private set; }

        /// <summary>
        /// The command to create new question of the type checkboxes
        /// </summary>
        public ICommand NewQuestionMultipleCheckBoxesCommand { get; private set; }

        /// <summary>
        /// The command to create new question of the type single text box
        /// </summary>
        public ICommand NewQuestionSingleTextBoxCommand { get; private set; }

        /// <summary>
        /// The command to edit existing question from the list
        /// </summary>
        public ICommand EditQuestionCommand { get; private set; }

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
        public TestCreatorQuestionsPageViewModel(ITestCreatorService testCreatorService, ApplicationViewModel applicationVM, IViewModelProvider viewModelProvider)
        {
            // Inject DI services
            mTestCreator = testCreatorService;
            mApplicationVM = applicationVM;
            mViewModelProvider = viewModelProvider;

            // Create commands
            NewQuestionMultipleChoiceCommand = new RelayCommand(() => GoToMultipleChoiceQuestion());
            NewQuestionMultipleCheckBoxesCommand = new RelayCommand(() => GoToCheckboxesQuestion());
            NewQuestionSingleTextBoxCommand = new RelayCommand(() => GoToSingleTextBoxQuestion());
            EditQuestionCommand = new RelayParameterizedCommand(EditQuestion);
            SubmitQuestionCommand = new RelayCommand(SubmitCurrentQuestion);
            FinishQuestionsCommand = new RelayCommand(GoToNextPage);

            // Get the editor for questions
            mEditor = mTestCreator.GetEditorTestQuestions();

            foreach (var question in mEditor)
            {
                Questions.Value.Add(new QuestionListItemViewModel { Task = question.Task.Text.Text });
            }
            // Catch all the errors and display them
            mEditor.OnErrorFor(x => x, Questions.ErrorMessages);
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
            var viewModel = mViewModelProvider.GetInjectedPageViewModel<QuestionsMultipleChoicePageViewModel>();

            // Initialize the view model with given editor, getting the submit action in return
            mSubmitQuestionAction = viewModel.InitializeEditor(editor);

            // Show the page
            IsCreatingQuestion = true;
            GoToPage(QuestionsPage.MultipleChoice, viewModel);
        }

        /// <summary>
        /// Starts the edit for multiple checkboxes question
        /// </summary>
        /// <param name="questionNumber">The question's data that can be pre-loaded, if its null, we create new question</param>
        private void GoToCheckboxesQuestion(int? questionNumber = null)
        {
            // Get the editor for this specific question
            var editor = mTestCreator.GetEditorMultipleCheckBoxes(questionNumber);

            // Create the view model for this question
            var viewModel = mViewModelProvider.GetInjectedPageViewModel<QuestionsMultipleCheckBoxesPageViewModel>();

            // Initialize the view model with given editor, getting the submit action in return
            mSubmitQuestionAction = viewModel.InitializeEditor(editor);

            // Show the page
            IsCreatingQuestion = true;
            GoToPage(QuestionsPage.MultipleCheckBoxes, viewModel);
        }

        /// <summary>
        /// Starts the edit for single text box question
        /// </summary>
        /// <param name="questionNumber">The question's data that can be pre-loaded, if its null, we create new question</param>
        private void GoToSingleTextBoxQuestion(int? questionNumber = null)
        {
            // Get the editor for this specific question
            var editor = mTestCreator.GetEditorSingleTextBox(questionNumber);

            // Create the view model for this question
            var viewModel = mViewModelProvider.GetInjectedPageViewModel<QuestionsSingleTextBoxPageViewModel>();

            // Initialize the view model with given editor, getting the submit action in return
            mSubmitQuestionAction = viewModel.InitializeEditor(editor);

            // Show the page
            IsCreatingQuestion = true;
            GoToPage(QuestionsPage.SingleTextBox, viewModel); 
        }

        /// <summary>
        /// Edits specified question from the list
        /// </summary>
        /// <param name="param">The question view model displayed in the list</param>
        private void EditQuestion(object param)
        {
            // Get the question view model itself
            var viewModel = param as QuestionListItemViewModel;

            // Calculate the index
            var index = Questions.Value.IndexOf(viewModel);

            // TODO: Somehow determine the question type here
            // Run appropriate function
            GoToMultipleChoiceQuestion(index);
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
            // Check if everything is alright with question list
            if (mEditor.Validate())
            {
                // Successful validation, go to grading page
                mApplicationVM.GoToPage(ApplicationPage.TestCreatorTestGrading);
            }

            // Validation failed, errors will be displayed automatically
        }

        #endregion
    }
}
