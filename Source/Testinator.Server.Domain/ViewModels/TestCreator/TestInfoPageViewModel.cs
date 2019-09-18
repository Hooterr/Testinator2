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
    public class TestInfoPageViewModel : BaseViewModel
    {
        #region Private Members

        private readonly ITestCreatorService mTestCreator;

        /// <summary>
        /// The editor for test info in this page, used to apply user input to the actual test
        /// </summary>
        private readonly ITestInfoEditor mEditor;

        #endregion

        #region Public Properties

        /// <summary>
        /// The name of the test
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The description of the test
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The category of the test as raw string
        /// TODO: Decide the delimiter, make converter to <see cref="Category"/>
        /// </summary>
        public string CategoryString { get; set; }

        /// <summary>
        /// The time allowed for this test to be completed within
        /// </summary>
        public TimeSpan CompletionTime { get; set; }

        /// <summary>
        /// The error message to display in case of validation failure
        /// </summary>
        public string ErrorMessage { get; set; }

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
        public TestInfoPageViewModel(ITestCreatorService testCreatorService)
        {
            // Inject DI services
            mTestCreator = testCreatorService;

            // Create commands
            SubmitCommand = new RelayCommand(Submit);

            // Get the editor associated with this page
            mEditor = mTestCreator.GetEditorTestInfo();

            // Catch all the errors and display them
            mEditor.OnErrorFor(x => x.Name, (e) => ErrorMessage = e);
            mEditor.OnErrorFor(x => x.TimeLimit, (e) => ErrorMessage = e);

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
            mEditor.TimeLimit = CompletionTime;

            // Validate current state of data
            if (mEditor.Validate())
            {
                // Successful validation, go to next page
                // TODO:
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
            CompletionTime = mEditor.TimeLimit;
        }

        #endregion
    }
}
