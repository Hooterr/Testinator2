using System.Windows.Input;
using Testinator.Core;
using Testinator.TestSystem.Editors;
using Testinator.TestSystem.Abstractions;
using System;

namespace Testinator.Server.Domain
{
    /// <summary>
    /// The view model for test info page in Test Creator
    /// </summary>
    public class TestCreatorTestInfoPageViewModel : BaseViewModel
    {
        #region Private Members

        private readonly ITestCreatorService mTestCreator;
        private readonly ApplicationViewModel mApplicationVM;

        /// <summary>
        /// The editor for test info in this page, used to apply user input to the actual test
        /// </summary>
        private readonly ITestInfoEditor mEditor;

        #endregion

        #region Public Properties

        /// <summary>
        /// The name of the test
        /// </summary>
        public InputField<string> Name { get; set; }

        /// <summary>
        /// The description of the test
        /// </summary>
        public InputField<string> Description { get; set; }

        /// <summary>
        /// The category of the test as raw string
        /// TODO: Decide the delimiter, make converter to <see cref="Category"/>
        /// </summary>
        public InputField<string> CategoryString { get; set; }

        /// <summary>
        /// The time allowed for this test to be completed within
        /// </summary>
        public InputField<TimeSpan> CompletionTime { get; set; }

        #endregion

        #region Commands

        /// <summary>
        /// The command to submit input data on this page to the test
        /// </summary>
        public ICommand SubmitCommand { get; private set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public TestCreatorTestInfoPageViewModel(ITestCreatorService testCreatorService, ApplicationViewModel applicationVM)
        {
            // Inject DI services
            mTestCreator = testCreatorService;
            mApplicationVM = applicationVM;

            // Create commands
            SubmitCommand = new RelayCommand(Submit);

            // Get the editor associated with this page
            mEditor = mTestCreator.GetEditorTestInfo();

            // And initialize the data we display
            InitializeInputData();
        }

        #endregion

        #region Command Methods

        /// <summary>
        /// Tries to submit this page state to the test
        /// </summary>
        private void Submit()
        {
            // Pass all the changes user has made to the editor
            mEditor.Name = Name;
            mEditor.Description = Description;
            mEditor.TimeLimit = new TimeSpan(0, 30, 0); // TODO: CompletionTime;

            // Validate current state of data
            if (mEditor.Validate())
            {
                // Successful validation, go to next page
                mApplicationVM.GoToPage(ApplicationPage.TestCreatorQuestions);
            }

            // Validation failed, do not submit anything
            // Error will be displayed by previous setup, no need to do anything here
        }

        /// <summary>
        /// Initializes the input data in this page by loading it from the editor
        /// </summary>
        private void InitializeInputData()
        {
            // Copy all the test info's properties
            Name = mEditor.Name;
            Description = mEditor.Description;
            CompletionTime = mEditor.TimeLimit;

            // Catch all the errors and display them
            // TODO: Add other values here after they are done in editor
            mEditor.OnErrorFor(x => x.Name, (e) => Name.ErrorMessage = e);
            mEditor.OnErrorFor(x => x.Description, (e) => Description.ErrorMessage = e);
            mEditor.OnErrorFor(x => x.TimeLimit, (e) => CompletionTime.ErrorMessage = e);
        }

        #endregion
    }
}
