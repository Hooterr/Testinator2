using System.Windows.Input;
using Testinator.Core;
using Testinator.TestSystem.Abstractions.Tests;
using Testinator.TestSystem.Editors;

namespace Testinator.Server.Domain
{
    /// <summary>
    /// The view model for test finalization page in Test Creator
    /// </summary>
    public class TestCreatorTestFinalizePageViewModel : BaseViewModel
    {
        #region Private Members

        private readonly ITestCreatorService mTestCreator;

        /// <summary>
        /// The test itself, built by the editor
        /// </summary>
        private ITest mTest;

        #endregion

        #region Public Properties

        // TODO: List all the test's properties

        public string Name => mTest.Info.Name;

        #endregion

        #region Commands

        /// <summary>
        /// The command to submit the test
        /// </summary>
        public ICommand SubmitTestCommand { get; private set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public TestCreatorTestFinalizePageViewModel(ITestCreatorService testCreatorService)
        {
            // Inject DI services
            mTestCreator = testCreatorService;

            // Create commands
            SubmitTestCommand = new RelayCommand(SubmitTest);

            // Get the built test
            mTest = mTestCreator.BuildTest();
        }

        #endregion

        #region Command Methods

        /// <summary>
        /// Submits created test
        /// </summary>
        private void SubmitTest()
        {
            // TODO: Save to file, save to user, etc.
        }

        #endregion
    }
}
