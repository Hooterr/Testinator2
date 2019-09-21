using System;
using System.Reflection;
using Testinator.Core;

namespace Testinator.Client.Domain
{
    /// <summary>
    /// The application state as a view model
    /// </summary>
    public class ApplicationViewModel : PageHostViewModel
    {
        #region Private Members

        private readonly IUIManager mUIManager;

        #endregion

        #region Public Properties

        /// <summary>
        /// Current version of the application
        /// </summary>
        public Version Version { get; private set; }

        #endregion

        #region Public Events

        /// <summary>
        /// Fired when main application is closing, so some operation may trigger this event 
        /// and prepare for closing
        /// </summary>
        public event Action OnAppClosing = () => { };

        #endregion

        #region Constructor

        /// <summary>
        /// Default construcotr
        /// </summary>
        public ApplicationViewModel(IUIManager uiManager, ILogFactory logger) : base(logger)
        {
            // Inject DI services
            mUIManager = uiManager;

            // Listen out for page changed event
            OnPageChanged += OnPageChange;

            // Get the current version from assebly
            var assembly = Assembly.LoadFrom("Testinator.Client.Core.dll");
            Version = assembly.GetName().Version;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Returns to the login page if there is no connection to the sevrer
        /// or to the waitingForTestPage if it is still connected
        /// <param name="isConnected">True if we are connected</param>
        /// </summary>
        public void ReturnMainScreen(bool isConnected)
        {
            mUIManager.DispatcherThreadAction(() => GoToPage(isConnected ? ApplicationPage.WaitingForTest : ApplicationPage.Login));
        }

        /// <summary>
        /// Closes the application
        /// </summary>
        public void Close()
        {
            OnAppClosing.Invoke();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Fired when application page changes
        /// </summary>
        /// <param name="newPage">The new page</param>
        private void OnPageChange(ApplicationPage newPage)
        {
            // In both cases we need small format
            if (newPage == ApplicationPage.Login || newPage == ApplicationPage.WaitingForTest)
                mUIManager.EnableSmallApplicationView();

            // NOTE: If there was only 'else' here it would cause useless calls to UIManager to disable login screen view 
            //       that has already been disabled
            else if (newPage > ApplicationPage.WaitingForTest)
                mUIManager.DisableSmallApplicationView();
        }

        #endregion
    }
}
