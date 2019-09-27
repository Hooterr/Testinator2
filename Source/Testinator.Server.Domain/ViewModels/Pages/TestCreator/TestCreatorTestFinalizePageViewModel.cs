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
        private readonly ApplicationViewModel mApplicationVM;
        private readonly ITestFileManager mFileManager;

        /// <summary>
        /// The test itself, built by the editor
        /// </summary>
        private ITest mTest;

        #endregion

        #region Public Properties

        // TODO: List all the test's properties

        public string TestName => mTest.Info.Name;

        public string FileName { get; set; }

        public string Message { get; set; }

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
        public TestCreatorTestFinalizePageViewModel(ITestCreatorService testCreatorService, ITestFileManager fileManager, ApplicationViewModel applicationVM)
        {
            // Inject DI services
            mTestCreator = testCreatorService;
            mApplicationVM = applicationVM;
            mFileManager = fileManager;

            // Create commands
            SubmitTestCommand = new RelayCommand(SubmitTest);

            // Get the built test
            mTest = mTestCreator.BuildTest();

            FileName = TestName;
        }

        #endregion

        #region Command Methods

        /// <summary>
        /// Submits created test
        /// </summary>
        private void SubmitTest()
        {
            if (string.IsNullOrEmpty(TestName))
                Message = "Pusta nazwa";
            else if (mFileManager.Save(options =>
            {
                options.InApplicationFolder(ApplicationDataFolders.Tests)
                    .WithName(FileName);
            }, mTest))
            {
                Message = "Alles gut";
            }
            else
            {
                Message = "Couldn't save because of an error. Lol.";
            }
        }

        #endregion
    }
}
