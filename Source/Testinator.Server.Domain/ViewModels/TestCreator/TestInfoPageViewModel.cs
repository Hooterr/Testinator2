using Testinator.Core;
using Testinator.TestSystem.Editors;

namespace Testinator.Server.Domain
{
    /// <summary>
    /// The view model for test info page in Test Creator
    /// </summary>
    public class TestInfoPageViewModel : BaseViewModel
    {
        #region Private Members

        private ITestCreatorService mTestCreator;

        #endregion

        #region Public Properties



        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public TestInfoPageViewModel(ITestCreatorService testCreatorService)
        {
            // Inject DI services
            mTestCreator = testCreatorService;
        }

        #endregion
    }
}
