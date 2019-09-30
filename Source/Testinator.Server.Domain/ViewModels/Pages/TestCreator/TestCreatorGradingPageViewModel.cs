using System.ComponentModel;
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
        /// Indicates if grading is in points mode
        /// </summary>
        private bool mPointsMode;

        /// <summary>
        /// The editor for grading in this page
        /// </summary>
        private readonly IGradingEditor mEditor;

        #endregion

        #region Public Properties

        /// <summary>
        /// Indicates if user is in grading creation mode
        /// </summary>
        public bool IsCreatingGrading { get; set; } = true;

        /// <summary>
        /// Indicates if grading is in points mode
        /// If it's false, grading is displayed in percentages
        /// </summary>
        public bool PointsMode
        {
            get => mPointsMode;
            set
            {
                // Get the value itself
                mPointsMode = value;

                // If its true...
                if (mPointsMode)
                {
                    // Convert current grades to points
                    Grades = Grades.Value.ToPoints(PointsForTest);
                }
                // Otherwise...
                else
                {
                    // Convert current grades to percentages
                    Grades = Grades.Value.ToPercentages(PointsForTest);
                }

                // Make sure grades are standarized at every change
                Grades.Value.ListChanged += (ss, ee) => Grades.Value.StandarizeGrades();
            }
        }

        /// <summary>
        /// The maximum amount of points reachable from test
        /// </summary>
        public int PointsForTest { get; set; }

        /// <summary>
        /// The list of grades that sums up to create grading
        /// </summary>
        public InputField<BindingList<GradeEditableViewModel>> Grades { get; set; }

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
            mEditor.ContainsPoints = PointsMode;
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
            mPointsMode = mEditor.ContainsPoints;
            Grades = mEditor.Thresholds
                // Use the amount of grades that are provided in the editor
                .ToGradeViewModels(mEditor.InitialThresholdCount,
                // If we are using points, use the test points, otherwise maximum is 100%
                mPointsMode ? PointsForTest : 100);

            // Catch all the errors and display them
            mEditor.OnErrorFor(x => x.Thresholds, (e) => Grades.ErrorMessage = e);
        }

        #endregion
    }
}
