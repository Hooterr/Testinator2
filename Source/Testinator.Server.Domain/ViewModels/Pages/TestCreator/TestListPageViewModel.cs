using System.Windows.Input;
using Testinator.Core;
using Testinator.TestSystem.Editors;

namespace Testinator.Server.Domain
{
    /// <summary>
    /// The view model for initial test list page in Test Creator
    /// </summary>
    public class TestListPageViewModel : BaseViewModel
    {
        #region Private Members

        private readonly ITestCreatorService mTestCreator;

        #endregion

        #region Public Properties

        // TODO: Test list etc.

        #endregion

        #region Commands

        /// <summary>
        /// The command to fire when any test is selected from the list for edition
        /// </summary>
        public ICommand TestSelectedCommand { get; private set; }

        /// <summary>
        /// The command to create brand-new test in Test Creator
        /// </summary>
        public ICommand NewTestCommand { get; private set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public TestListPageViewModel(ITestCreatorService testCreatorService)
        {
            // Inject DI services
            mTestCreator = testCreatorService;

            // Create commands
            TestSelectedCommand = new RelayParameterizedCommand(TestSelected);
            NewTestCommand = new RelayCommand(NewTest);
        }

        #endregion

        #region Command Methods

        /// <summary>
        /// Fired when test is selected in the list and user wants to edit it
        /// </summary>
        private void TestSelected(object param)
        {
            // Get the selected test
            var test = param as TestSystem.Implementation.Test;

            // Setup Test Creator with provided test
            mTestCreator.InitializeNewTest(test);

            // TODO: Go to the next page
        }

        /// <summary>
        /// Starts brand-new test in Test Creator
        /// </summary>
        private void NewTest()
        {
            // Setup Test Creator with new test
            mTestCreator.InitializeNewTest();

            // TODO: Go to the next page
        }

        #endregion
    }
}
