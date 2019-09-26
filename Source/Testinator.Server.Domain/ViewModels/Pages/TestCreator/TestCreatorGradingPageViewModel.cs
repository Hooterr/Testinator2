using System.Collections.ObjectModel;
using System.Windows.Input;
using Testinator.Core;
using Testinator.TestSystem.Editors;

namespace Testinator.Server.Domain
{
    /// <summary>
    /// The view model for grading page in Test Creator
    /// </summary>
    public class TestCreatorGradingPageViewModel : BaseViewModel
    {
        #region Private Members

        private readonly ITestCreatorService mTestCreator;
        private readonly ApplicationViewModel mApplicationVM;

        /// <summary>
        /// The editor for grading in this page
        /// </summary>
        private readonly IGradingEditor mEditor;

        #endregion

        #region Public Properties

        /// <summary>
        /// Indicates if user is in grading editing mode
        /// </summary>
        public bool IsEditingGrading { get; set; }

        /// <summary>
        /// The maximum amount of points reachable from test
        /// </summary>
        public int PointsForTest { get; set; }

        /// <summary>
        /// The collection of grades that sums up to create grading
        /// </summary>
        public InputField<ObservableCollection<GradeEditableViewModel>> Grades { get; set; }

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

            // Get the editor for grading
            mEditor = mTestCreator.GetEditorGrading();
        }

        #endregion

        #region Command Methods

        /// <summary>
        /// Checks if we have enough questions in the test and goes to the grading page
        /// </summary>
        private void GoToNextPage()
        {
            // Validate grading state
            if (mEditor.Validate())
            {
                // Validation succeeded, go to the final page
                mApplicationVM.GoToPage(ApplicationPage.TestCreatorTestFinalize);
            }

            // TODO: Validation failed
        }

        /// <summary>
        /// Initializes the input data in this page by loading it from the editor
        /// </summary>
        private void InitializeInputData()
        {
            // Catch all the errors and display them
            mEditor.OnErrorFor(x => x.CustomThresholds, (e) => Grades.ErrorMessage = e);
        }

        #endregion
    }
}
