using System.Windows.Input;
using Testinator.Core;
using Testinator.TestSystem.Editors;

namespace Testinator.Server.Domain
{
    /// <summary>
    /// The view model for grading page in Test Creator
    /// This is the master page that contains specific grading types pages inside
    /// </summary>
    public class TestCreatorGradingPageViewModel : PageHostViewModel<GradingPage>
    {
        #region Private Members

        private readonly ITestCreatorService mTestCreator;
        private readonly ApplicationViewModel mApplicationVM;

        #endregion

        #region Public Properties

        /// <summary>
        /// Indicates if user is in grading editing mode
        /// </summary>
        public bool IsEditingGrading { get; set; }

        #endregion

        #region Commands

        /// <summary>
        /// The command to finish this page and move forward with test creation
        /// </summary>
        public ICommand FinishGradingCommand { get; private set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public TestCreatorGradingPageViewModel(ITestCreatorService testCreatorService, ApplicationViewModel applicationVM)
        {
            // Inject DI services
            mTestCreator = testCreatorService;
            mApplicationVM = applicationVM;

            // Create commands
            FinishGradingCommand = new RelayCommand(GoToNextPage);
        }

        #endregion

        #region Command Methods

        /// <summary>
        /// Checks if we have enough questions in the test and goes to the grading page
        /// </summary>
        private void GoToNextPage()
        {
            // TODO: Validate grading and stuff

            // Go to final page
            mApplicationVM.GoToPage(ApplicationPage.TestCreatorTestFinalize);
        }

        #endregion
    }
}
