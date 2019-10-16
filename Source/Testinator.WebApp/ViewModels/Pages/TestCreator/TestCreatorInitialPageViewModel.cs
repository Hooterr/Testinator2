using Microsoft.AspNetCore.Components;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Testinator.Core;
using Testinator.TestSystem.Editors;

namespace Testinator.WebApp
{
    /// <summary>
    /// The view model for initial Test Creator page
    /// </summary>
    public class TestCreatorInitialPageViewModel : BasePageViewModel
    {
        #region Private Members

        private readonly ITestCreatorService mTestCreator;
        private readonly IViewModelProvider mViewModelProvider;

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
            NavigationManager navigationManager,
            ITestCreatorService testCreatorService, 
            IViewModelProvider viewModelProvider) : base(navigationManager)
        {
            // Inject DI services
            mTestCreator = testCreatorService;
            mViewModelProvider = viewModelProvider;

            // Create commands
            NewTestCommand = new RelayCommand(NewTest);
            NewGradingCommand = new RelayCommand(NewGrading);
        }

        #endregion

        #region Command Methods

        /// <summary>
        /// Starts brand-new test in Test Creator
        /// </summary>
        private void NewTest()
        {
            // Setup Test Creator with new test
            mTestCreator.InitializeNewTest();

            // Go to the next page
            mNavigationManager.NavigateTo("testcreator/info");
        }

        /// <summary>
        /// Creates brand-new grading preset
        /// </summary>
        private void NewGrading()
        {
            // Load new editor
            var editor = mTestCreator.GetEditorGradingPreset();

            /* TODO: Implement grading
            // Get the view model for grading preset page
            var presetPageVM = mViewModelProvider.GetInjectedPageViewModel<TestCreatorGradingPresetsPageViewModel>();

            // Inject the editor to the page
            presetPageVM.InitializeEditor(editor);

            // Show the page itself
            mApplicationVM.GoToPage(ApplicationPage.TestCreatorGradingPresets, presetPageVM);*/
        }

        #endregion
    }
}
