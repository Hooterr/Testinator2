using System.ComponentModel;
using System.Windows.Input;
using Testinator.Core;
using Testinator.TestSystem.Editors;

namespace Testinator.Server.Domain
{
    /// <summary>
    /// The view model for grading presets page in Test Creator
    /// </summary>
    public class TestCreatorGradingPresetsPageViewModel : BaseViewModel
    {
        #region Private Members

        private readonly ITestCreatorService mTestCreator;
        private readonly ApplicationViewModel mApplicationVM;

        /// <summary>
        /// The editor for grading presets
        /// </summary>
        private readonly IGradingPresetEditor mEditor;

        #endregion

        #region Public Properties

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
        /// The command to save created grading preset
        /// </summary>
        public ICommand SaveGradingCommand { get; private set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public TestCreatorGradingPresetsPageViewModel(ITestCreatorService testCreatorService, ApplicationViewModel applicationVM)
        {
            // Inject DI services
            mTestCreator = testCreatorService;
            mApplicationVM = applicationVM;

            // Create commands
            AddGradeCommand = new RelayCommand(AddGrade);
            RemoveGradeCommand = new RelayCommand(RemoveGrade);
            SaveGradingCommand = new RelayCommand(SaveGrading);

            // Get the editor associated with this page
            mEditor = mTestCreator.GetEditorGradingPreset();

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
        /// Submits current grading as a preset and saves it
        /// </summary>
        private void SaveGrading()
        {
            
        }

        /// <summary>
        /// Initializes the input data in this page by loading it from the editor
        /// </summary>
        private void InitializeInputData()
        {
            // Copy all the properties from the editor
            Grades = mEditor.Thresholds
                // Use the amount of grades that are provided in the editor
                .ToGradeViewModels(mEditor.InitialThresholdCount, 100);

            // Catch all the errors and display them
            mEditor.OnErrorFor(x => x.Thresholds, Grades.ErrorMessages);
        }

        #endregion
    }
}
