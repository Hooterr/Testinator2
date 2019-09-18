using Testinator.Core;
using Testinator.TestSystem.Editors;

namespace Testinator.Server.Domain
{
    /// <summary>
    /// The view model for TODO: Something grading page in Test Creator
    /// </summary>
    public class GradingSomethingPageViewModel : BaseViewModel
    {
        #region Private Members

        private ITestCreatorService mTestCreator;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public GradingSomethingPageViewModel(ITestCreatorService testCreatorService)
        {
            // Inject DI services
            mTestCreator = testCreatorService;
        }

        #endregion
    }
}
