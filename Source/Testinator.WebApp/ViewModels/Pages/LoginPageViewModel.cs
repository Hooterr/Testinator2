using Microsoft.AspNetCore.Components;
using System.Windows.Input;
using Testinator.Core;

namespace Testinator.WebApp
{
    /// <summary>
    /// The view model for login page
    /// </summary>
    public class LoginPageViewModel : BasePageViewModel
    {
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
        public LoginPageViewModel(NavigationManager navigationManager) : base(navigationManager)
        {
            // Create commands
            LoginCommand = new RelayCommand(LogIn);
            // TODO: Change that
            EnterWithoutLoginCommand = new RelayCommand(() => 
            {
                mNavigationManager.NavigateTo("dashboard");
            });
        }

        #endregion

        #region Command Methods

        /// <summary>
        /// Tries to log the user in using provided data
        /// </summary>
        private void LogIn()
        {
            // TODO: Implement this
        }

        #endregion
    }
}
