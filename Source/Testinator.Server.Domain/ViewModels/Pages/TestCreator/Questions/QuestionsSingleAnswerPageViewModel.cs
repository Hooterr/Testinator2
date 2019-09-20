using Testinator.Core;
using Testinator.TestSystem.Editors;

namespace Testinator.Server.Domain
{
    /// <summary>
    /// The view model for single answer question page in Test Creator
    /// </summary>
    public class QuestionsSingleAnswerPageViewModel : BaseViewModel
    {
        #region Private Members

        private ITestCreatorService mTestCreator;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public QuestionsSingleAnswerPageViewModel(ITestCreatorService testCreatorService)
        {
            // Inject DI services
            mTestCreator = testCreatorService;
        }

        #endregion
    }
}
