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
        private readonly IGradingPresetFileManager mGradingPresetFileManager;

        /// <summary>
        /// The editor for grading presets
        /// </summary>
        private IGradingPresetEditor mEditor;

        #endregion

        #region Public Properties

        /// <summary>
        /// The assigned name for this grading preset
        /// </summary>
        public InputField<string> Name { get; set; }

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
        public TestCreatorGradingPresetsPageViewModel(ITestCreatorService testCreatorService, ApplicationViewModel applicationVM, IGradingPresetFileManager gradingPresetFileManager)
        {
            // Inject DI services
            mTestCreator = testCreatorService;
            mApplicationVM = applicationVM;
            mGradingPresetFileManager = gradingPresetFileManager;

            // Create commands
            AddGradeCommand = new RelayCommand(AddGrade);
            RemoveGradeCommand = new RelayCommand(RemoveGrade);
            SaveGradingCommand = new RelayCommand(SaveGrading);
        }

        #endregion

        #region Command Methods

        /// <summary>
        /// Initializes this view model with provided editor for grading presets
        /// </summary>
        /// <param name="editor">The editor for grading preset containing all data</param>
        public void InitializeEditor(IGradingPresetEditor editor)
        {
            // Catch the editor itself
            mEditor = editor;

            // Initialize every property based on current editor state
            // If we are editing existing preset, editor will have it's data
            // If we are creating new one, editor will be empty but its still fine at this point
            Name = mEditor.Name;
            Grades = mEditor.Thresholds.ToGradeViewModels(mEditor.InitialThresholdCount, mEditor.MaxThresholdsCount);

            // Catch all the errors and display them
            mEditor.OnErrorFor(x => x.Name, Name.ErrorMessages);
            mEditor.OnErrorFor(x => x.Thresholds, Grades.ErrorMessages);
        }

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
            // Copy all the user input data to the editor
            mEditor.Name = Name;
            mEditor.Thresholds = Grades.Value.ToThresholdsInEditor();

            // Build the preset
            var buildOperation = mEditor.Build();

            // If editor validation fails...
            if (buildOperation.Failed)
            {
                // Don't submit anything
                // Error will be displayed by previous setup, no need to do anything here
                return;
            }

            // Validation succeeded, save the grading
            mGradingPresetFileManager.Save(options =>
            {
                options.InApplicationFolder(ApplicationDataFolders.GradingPresets);
            }, buildOperation.Result);

            // Go back to test creator initial page
            mApplicationVM.GoToPage(ApplicationPage.TestCreatorInitial);
        }

        #endregion
    }
}
