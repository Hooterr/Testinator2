using Testinator.Core;
using Testinator.TestSystem.Editors;

namespace Testinator.Server.Domain
{
    /// <summary>
    /// The view model for checkboxes question page in Test Creator
    /// </summary>
    public class QuestionsCheckboxesPageViewModel : BaseViewModel
    {
        #region Private Members

        private ITestCreatorService mTestCreator;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public QuestionsCheckboxesPageViewModel(ITestCreatorService testCreatorService)
        {
            // Inject DI services
            mTestCreator = testCreatorService;
        }

        #endregion
    }
}
