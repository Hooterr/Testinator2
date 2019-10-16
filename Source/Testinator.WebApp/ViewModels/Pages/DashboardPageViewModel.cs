using Microsoft.AspNetCore.Components;
using System;
using System.Windows.Input;
using Testinator.Core;

namespace Testinator.WebApp
{
    /// <summary>
    /// The view model for dashboard page
    /// </summary>
    public class DashboardPageViewModel : BasePageViewModel
    {
        #region Public Properties

        /// <summary>
        /// The user's first name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// The user's second name (surname)
        /// </summary>
        public string SecondName { get; set; }

        #endregion

        #region Commands

        /// <summary>
        /// The command to go to test creator initial page
        /// </summary>
        public ICommand GoToTestCreatorCommand { get; private set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public DashboardPageViewModel(NavigationManager navigationManager) : base(navigationManager)
        {
            // Create commands
            GoToTestCreatorCommand = new RelayCommand(GoToTestCreator);
        }

        #endregion

        #region Command Methods

        /// <summary>
        /// Changes current page to test creator initial page
        /// </summary>
        private void GoToTestCreator()
        {
            mNavigationManager.NavigateTo("testCreator");
        }

        #endregion
    }
}