using Testinator.Core;
using Testinator.TestSystem.Editors;

namespace Testinator.Server.Domain
{
    /// <summary>
    /// The view model for multiple choice question page in Test Creator
    /// </summary>
    public class QuestionsMultipleChoicePageViewModel : BaseViewModel
    {
        #region Private Members

        private ITestCreatorService mTestCreator;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public QuestionsMultipleChoicePageViewModel(ITestCreatorService testCreatorService)
        {
            // Inject DI services
            mTestCreator = testCreatorService;
        }

        #endregion
    }
}
