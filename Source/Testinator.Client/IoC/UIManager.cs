﻿using System;
using System.Threading.Tasks;
using System.Windows;
using Testinator.Client.Core;
using Testinator.Core;

namespace Testinator.Client
{
    /// <summary>
    /// The application implementation of the <see cref="IUIManager"/>
    /// </summary>
    public class UIManager : IUIManager
    {
        /// <summary>
        /// Changes page in the application
        /// </summary>
        /// <param name="page">Target page</param>
        /// <param name="viewmodel">Corresponding viewmodel</param>
        public void ChangePage(ApplicationPage page, BaseViewModel viewmodel = null)
        {
            // Get back on UI thread
            Application.Current.Dispatcher.Invoke(() => 
            {
                // Change page
                IoCClient.Application.GoToPage(page, viewmodel);

                // Log it
                IoCClient.Logger.Log("Changing application page to " + page.ToString());
            });
        }

        /// <summary>
        /// Displays a single message box to the user
        /// </summary>
        /// <param name="viewModel">The view model</param>
        /// <returns></returns>
        public Task ShowMessage(MessageBoxDialogViewModel viewModel)
        {
            return new DialogMessageBox().ShowDialog(viewModel);
        }

        /// <summary>
        /// Displays a result box to the user and catch the result
        /// </summary>
        /// <param name="viewModel">The view model</param>
        /// <returns></returns>
        public Task ShowMessage(ResultBoxDialogViewModel viewModel)
        {
            return new DialogResultBox().ShowDialog(viewModel);
        }
    }
}
