using System.Windows.Input;
using Testinator.Core;

namespace Testinator.Server.Domain
{
    /// <summary>
    /// The view model for initial login page
    /// </summary>
    public class LoginViewModel : BaseViewModel
    {
        #region Private Members

        private readonly IUserAccountService mUserService;
        private readonly ApplicationViewModel mApplicationVM;

        #endregion

        #region Public Properties

        /// <summary>
        /// The email user has entered in the input box
        /// </summary>
        public string UserEmail { get; set; }

        /// <summary>
        /// The password user has entered in the input box
        /// </summary>
        public string UserPassword { get; set; }

        /// <summary>
        /// Indicates if user clicked submit button and is currently logging in
        /// </summary>
        public bool LoggingIsRunning { get; private set; } = false;

        /// <summary>
        /// The message to show if any error occures
        /// </summary>
        public string ErrorMessage { get; private set; }

        #endregion

        #region Commands

        /// <summary>
        /// The command to log the user in
        /// </summary>
        public ICommand LoginCommand { get; private set; }

        /// <summary>
        /// The command to go to next page without logging in
        /// </summary>
        public ICommand EnterWithoutLoginCommand { get; private set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public LoginViewModel(IUserAccountService userService, ApplicationViewModel applicationVM)
        {
            // Inject DI services
            mUserService = userService;
            mApplicationVM = applicationVM;

            // Create commands
            LoginCommand = new RelayCommand(LogInAsync);
            // TODO: Change that
            EnterWithoutLoginCommand = new RelayCommand(() => mApplicationVM.GoToPage(ApplicationPage.TestCreatorInitial));
        }

        #endregion

        #region Command Methods

        /// <summary>
        /// Tries to log the user in using provided data
        /// </summary>
        private async void LogInAsync()
        {
            // Set the logging flag until logging in is finished
            await RunCommandAsync(() => LoggingIsRunning, async () =>
            {
                // Empty the error message for the operation
                ErrorMessage = string.Empty;

                // Try to log the user in
                var error = await mUserService.LogInAsync(UserEmail, UserPassword);

                // If there are no errors...
                if (string.IsNullOrEmpty(error))
                {
                    // We successfully logged in, so change the page
                    mApplicationVM.GoToPage(ApplicationPage.Home);
                    return;
                }

                // Otherwise login failed, show the error to the user
                ErrorMessage = error;
            });
            
        }

        #endregion
    }
}
