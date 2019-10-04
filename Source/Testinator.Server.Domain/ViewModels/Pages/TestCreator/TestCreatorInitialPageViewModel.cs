using System.Collections.ObjectModel;
using System.Windows.Input;
using Testinator.Core;
using Testinator.TestSystem.Editors;

namespace Testinator.Server.Domain
{
    /// <summary>
    /// The view model for initial Test Creator page
    /// </summary>
    public class TestCreatorInitialPageViewModel : BaseViewModel
    {
        #region Private Members

        private readonly ITestCreatorService mTestCreator;
        private readonly ApplicationViewModel mApplicationVM;
        private readonly IViewModelProvider mViewModelProvider;
        private readonly ITestFileManager mTestFileManager;
        private readonly IGradingPresetFileManager mGradingPresetFileManager;
        private readonly TestMapper mTestMapper;
        private readonly GradingMapper mGradingMapper;

        #endregion

        #region Public Properties

        /// <summary>
        /// The tests that are available for editing
        /// </summary>
        public ObservableCollection<TestListItemViewModel> Tests { get; set; }

        /// <summary>
        /// The grading presets that are available for editing
        /// </summary>
        public ObservableCollection<GradingPresetListItemViewModel> GradingPresets { get; set; }

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

        /// <summary>
        /// The command to fire when any grading preset is selected from the list for edition
        /// </summary>
        public ICommand GradingSelectedCommand { get; private set; }

        /// <summary>
        /// The command to create brand-new grading preset
        /// </summary>
        public ICommand NewGradingCommand { get; private set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public TestCreatorInitialPageViewModel(
            ITestCreatorService testCreatorService, 
            ApplicationViewModel applicationVM,
            IViewModelProvider viewModelProvider,
            ITestFileManager testFileManager,
            IGradingPresetFileManager gradingPresetFileManager,
            TestMapper testMapper,
            GradingMapper gradingMapper)
        {
            // Inject DI services
            mTestCreator = testCreatorService;
            mApplicationVM = applicationVM;
            mViewModelProvider = viewModelProvider;
            mTestFileManager = testFileManager;
            mGradingPresetFileManager = gradingPresetFileManager;
            mTestMapper = testMapper;
            mGradingMapper = gradingMapper;

            // Create commands
            TestSelectedCommand = new RelayParameterizedCommand(TestSelected);
            NewTestCommand = new RelayCommand(NewTest);
            GradingSelectedCommand = new RelayParameterizedCommand(GradingSelected);
            NewGradingCommand = new RelayCommand(NewGrading);

            // Load any saved tests
            LoadTests();

            // Load any saved grading presets
            LoadPresets();
        }

        #endregion

        #region Command Methods

        /// <summary>
        /// Fired when test is selected in the list and user wants to edit it
        /// </summary>
        private void TestSelected(object param)
        {
            // Get the selected test
            var testVM = param as TestListItemViewModel;

            // Get it's data from local file
            var test = mTestFileManager.Read(options =>
            {
                options.InApplicationFolder(ApplicationDataFolders.Tests)
                    .WithName(testVM.Name);
            });

            // Setup Test Creator with provided test
            mTestCreator.InitializeNewTest(test);

            // Go to the next page
            mApplicationVM.GoToPage(ApplicationPage.TestCreatorTestInfo);
        }

        /// <summary>
        /// Starts brand-new test in Test Creator
        /// </summary>
        private void NewTest()
        {
            // Setup Test Creator with new test
            mTestCreator.InitializeNewTest();

            // Go to the next page
            mApplicationVM.GoToPage(ApplicationPage.TestCreatorTestInfo);
        }

        /// <summary>
        /// Fired when grading preset is selected in the list and user wants to edit it
        /// </summary>
        private void GradingSelected(object param)
        {
            // Get the selected preset
            var presetVM = param as GradingPresetListItemViewModel;

            // Get it's data from local file
            var preset = mGradingPresetFileManager.Read(options =>
            {
                options.InApplicationFolder(ApplicationDataFolders.GradingPresets)
                    .WithName(presetVM.Name);
            });

            // Load the editor with pre-loaded preset
            var editor = mTestCreator.GetEditorGradingPreset(preset);

            // Get the view model for grading preset page
            var presetPageVM = mViewModelProvider.GetInjectedPageViewModel<TestCreatorGradingPresetsPageViewModel>();

            // Inject the editor to the page
            presetPageVM.InitializeEditor(editor);

            // Show the page itself
            mApplicationVM.GoToPage(ApplicationPage.TestCreatorGradingPresets, presetPageVM);
        }

        /// <summary>
        /// Creates brand-new grading preset
        /// </summary>
        private void NewGrading()
        {
            // Load new editor
            var editor = mTestCreator.GetEditorGradingPreset();

            // Get the view model for grading preset page
            var presetPageVM = mViewModelProvider.GetInjectedPageViewModel<TestCreatorGradingPresetsPageViewModel>();

            // Inject the editor to the page
            presetPageVM.InitializeEditor(editor);

            // Show the page itself
            mApplicationVM.GoToPage(ApplicationPage.TestCreatorGradingPresets, presetPageVM);
        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Loads any saved tests from both local files and web
        /// </summary>
        private void LoadTests()
        {
            // TODO: Load tests from user account in web

            // Get every locally saved test
            var localTests = mTestFileManager.GetTestContexts(options =>
            {
                options.InApplicationFolder(ApplicationDataFolders.Tests);
            });

            // Convert all the tests to suitable view models
            var testViewModels = mTestMapper.Map(localTests);

            // Add them to the collection
            Tests = new ObservableCollection<TestListItemViewModel>(testViewModels);
        }

        /// <summary>
        /// Loads any saved grading presets from both local files and web
        /// </summary>
        private void LoadPresets()
        {
            // TODO: Load presets from user account in web

            // Get every locally saved preset
            var localPresets = mGradingPresetFileManager.GetGradingPresetsContexts(options =>
            {
                options.InApplicationFolder(ApplicationDataFolders.GradingPresets);
            });

            // Convert all the presets to suitable view models
            var presetViewModels = mGradingMapper.Map(localPresets);

            // Add them to the collection
            GradingPresets = new ObservableCollection<GradingPresetListItemViewModel>(presetViewModels);
        }

        #endregion
    }
}
