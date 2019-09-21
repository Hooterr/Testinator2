using System;
using Testinator.Core;

namespace Testinator.Client.Domain
{
    /// <summary>
    /// The view model for PageHost containing informations about current page/VM of the page
    /// </summary>
    public class PageHostViewModel : BaseViewModel
    {
        #region Private Members

        private readonly ILogFactory mLogger;

        #endregion

        #region Public Properties

        /// <summary>
        /// The current page of the application
        /// </summary>
        public ApplicationPage CurrentPage { get; private set; } = ApplicationPage.Login;

        /// <summary>
        /// The view model to use for the current page when the CurrentPage changes
        /// NOTE: This is not a live up-to-date view model of the current page
        ///       it is simply used to set the view model of the current page 
        ///       at the time it changes
        /// </summary>
        public BaseViewModel CurrentPageViewModel { get; set; }

        #endregion

        #region Public Events

        /// <summary>
        /// Fired whenever application page changes
        /// </summary>
        public event Action<ApplicationPage> OnPageChanged = (page) => { };

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public PageHostViewModel(ILogFactory logger)
        {
            mLogger = logger;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Navigates to the specified page
        /// </summary>
        /// <param name="page">The page to go to</param>
        /// <param name="viewModel">The view model, if any, to set explicitly to the new page</param>
        public void GoToPage(ApplicationPage page, BaseViewModel viewModel = null)
        {
            // Set the view model
            CurrentPageViewModel = viewModel;

            // Set the current page
            CurrentPage = page;

            // Fire property changed events
            OnPropertyChanged(nameof(CurrentPageViewModel));
            OnPropertyChanged(nameof(CurrentPage));

            // Notify listeners
            OnPageChanged(page);

            // Log it
            mLogger.Log("Changing application page to:" + page.ToString());

        }

        #endregion
    }
}
