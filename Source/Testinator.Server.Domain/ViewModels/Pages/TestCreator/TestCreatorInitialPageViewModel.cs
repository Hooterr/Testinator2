using System.ComponentModel;
using System.Windows.Input;
using Testinator.Core;
using Testinator.TestSystem.Editors;
using System.Linq;
using System.Collections.ObjectModel;

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
        private readonly ITestFileManager mTestFileManager;
        private readonly IGradingPresetFileManager mGradingPresetFileManager;
        private readonly TestMapper mTestMapper;

        #endregion

        #region Public Properties

        /// <summary>
        /// The tests that are available for editing
        /// </summary>
        public ObservableCollection<TestListItemViewModel> Tests { get; set; }

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
            ITestFileManager testFileManager,
            IGradingPresetFileManager gradingPresetFileManager,
            TestMapper testMapper)
        {
            // Inject DI services
            mTestCreator = testCreatorService;
            mApplicationVM = applicationVM;
            mTestFileManager = testFileManager;
            mGradingPresetFileManager = gradingPresetFileManager;
            mTestMapper = testMapper;

            // Create commands
            TestSelectedCommand = new RelayParameterizedCommand(TestSelected);
            NewTestCommand = new RelayCommand(NewTest);
            GradingSelectedCommand = new RelayParameterizedCommand(GradingSelected);
            NewGradingCommand = new RelayCommand(NewGrading);

            // Load any saved tests
            LoadTests();
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
            var test = mGradingPresetFileManager.Read(options =>
            {
                options.InApplicationFolder(ApplicationDataFolders.Tests)
                    .WithName(presetVM.Name);
            });

            // TODO: Load the editor and change page
        }

        /// <summary>
        /// Creates brand-new grading preset
        /// </summary>
        private void NewGrading()
        {
            // TODO: Load the editor and change page
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

            // Convert all the test to suitable view models
            var testViewModels = mTestMapper.Map(localTests);

            // Add them to the collection
            Tests = new ObservableCollection<TestListItemViewModel>(testViewModels);
        }

        #endregion
    }
}
