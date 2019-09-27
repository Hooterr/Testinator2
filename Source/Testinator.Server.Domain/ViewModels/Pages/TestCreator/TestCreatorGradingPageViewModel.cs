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
        /// The command to add new possible grade to the grading
        /// </summary>
        public ICommand AddGradeCommand { get; private set; }

        /// <summary>
        /// The command to remove last grade from the grading
        /// </summary>
        public ICommand RemoveGradeCommand { get; private set; }

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
            AddGradeCommand = new RelayCommand(AddGrade);
            RemoveGradeCommand = new RelayCommand(RemoveGrade);
            FinishGradingCommand = new RelayCommand(GoToNextPage);

            // Get the editor associated with this page
            mEditor = mTestCreator.GetEditorGrading();

            // And initialize the data we display
            InitializeInputData();
        }

        #endregion

        #region Command Methods

        /// <summary>
        /// Adds new possible grade to the current grading
        /// </summary>
        private void AddGrade()
        {
            // Make sure we don't exceed maximum grade limit
            var gradesCount = Grades.Value.Count;
            if (gradesCount >= mEditor.MaxThresholdsCount)
                return;

            // Add new grade
            Grades.Value.Add(new GradeEditableViewModel());
        }

        /// <summary>
        /// Removes last grade from the current grading
        /// </summary>
        private void RemoveGrade()
        {
            // Make sure we can remove the answer and still meet the requirement
            var gradesCount = Grades.Value.Count;
            if (gradesCount <= mEditor.MinThresholdCount)
                return;

            // Remove the last grade
            Grades.Value.RemoveAt(gradesCount - 1);
        }

        /// <summary>
        /// Checks if we have enough questions in the test and goes to the grading page
        /// </summary>
        private void GoToNextPage()
        {
            // Pass all the changes back to the editor
            mEditor.Thresholds = Grades.Value.ToThresholdsInEditor();

            // Validate grading state
            if (mEditor.Validate())
            {
                // Validation succeeded, go to the final page
                mApplicationVM.GoToPage(ApplicationPage.TestCreatorTestFinalize);
            }

            // Validation failed, do not submit anything
            // Error will be displayed by previous setup, no need to do anything here
        }

        /// <summary>
        /// Initializes the input data in this page by loading it from the editor
        /// </summary>
        private void InitializeInputData()
        {
            // Copy all the properties from the editor
            PointsForTest = mEditor.TotalPointScore;
            Grades = mEditor.Thresholds.ToGradeViewModels(mEditor.MinThresholdCount);

            // Catch all the errors and display them
            mEditor.OnErrorFor(x => x.Thresholds, (e) => Grades.ErrorMessage = e);
        }

        #endregion
    }
}
