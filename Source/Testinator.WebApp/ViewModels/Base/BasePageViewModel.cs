using Microsoft.AspNetCore.Components;

namespace Testinator.WebApp
{
    /// <summary>
    /// The base class for every page view model
    /// </summary>
    public class BasePageViewModel
    {
        #region Protected Members

        protected readonly NavigationManager mNavigationManager;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public BasePageViewModel(NavigationManager navigationManager)
        {
            // Inject DI services
            mNavigationManager = navigationManager;
        }

        #endregion
    }
}
