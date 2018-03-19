﻿using System;
using System.Threading.Tasks;
using Testinator.Server.Core;
using System.Windows;
using Testinator.Core;
using Testinator.UICore;
using System.Globalization;

namespace Testinator.Server
{
    /// <summary>
    /// The applications implementation of the <see cref="IUIManager"/>
    /// </summary>
    public class UIManager : IUIManager
    {
        /// <summary>
        /// Changes application page by making sure that we are on UI thread beforehand
        /// </summary>
        /// <param name="page">The page to change to</param>
        /// <param name="vm">The view model</param>
        public void ChangeApplicationPageThreadSafe(ApplicationPage page, BaseViewModel vm = null)
        {
            // Get on the UI Thread
            Application.Current.Dispatcher.BeginInvoke((Action)(() =>
            {
                // Simply change page
                IoCServer.Application.GoToPage(page, null);
            }));
        }

        /// <summary>
        /// Displays a single message box to the user
        /// </summary>
        /// <param name="viewModel">The view model</param>
        /// <param name="isAlreadyOnUIThread">Indicates if caller is on UIThread, default as true</param>
        /// <returns></returns>
        public Task ShowMessage(MessageBoxDialogViewModel viewModel, bool isAlreadyOnUIThread = true)
        {
            // Prepare a dummy task to return
            Task task = null;

            // If caller isn't on UIThread, get to this thread first
            if (!isAlreadyOnUIThread)
                Application.Current.Dispatcher.BeginInvoke((Action)(() =>
                {
                    // Set the task inside UIThread
                    task = new DialogMessageBox().ShowDialog(viewModel);
                }));

            // If caller is on UIThread already, just show the dialog
            else
                task = new DialogMessageBox().ShowDialog(viewModel);

            // Finally return this task
            return task;
        }

        /// <summary>
        /// Displays a result box to the user and catch the result
        /// </summary>
        /// <param name="viewModel">The view model</param>
        /// <param name="isAlreadyOnUIThread">Indicates if caller is on UIThread, default as true</param>
        /// <returns></returns>
        public Task ShowMessage(ResultBoxDialogViewModel viewModel, bool isAlreadyOnUIThread = true)
        {
            // Prepare a dummy task to return
            Task task = null;

            // If caller isn't on UIThread already, get to this thread first
            if (!isAlreadyOnUIThread)
                Application.Current.Dispatcher.BeginInvoke((Action)(() =>
                {
                    // Set the task inside UIThread
                    task = new DialogResultBox().ShowDialog(viewModel);
                }));

            // If caller is on UIThread, just show the dialog
            else
                task = new DialogResultBox().ShowDialog(viewModel);

            // Finally return this task
            return task;
        }

        /// <summary>
        /// Displays a result box to the user and catch the result
        /// </summary>
        /// <param name="viewModel">The view model</param>
        /// <param name="isAlreadyOnUIThread">Indicates if caller is on UIThread, default as true</param>
        /// <returns></returns>
        public Task ShowMessage(AddLatecomersDialogViewModel viewModel, bool isAlreadyOnUIThread = true)
        {
            // Prepare a dummy task to return
            Task task = null;

            // If caller isn't on UIThread already, get to this thread first
            if (!isAlreadyOnUIThread)
                Application.Current.Dispatcher.BeginInvoke((Action)(() =>
                {
                    // Set the task inside UIThread
                    task = new AddLatecommersDialog().ShowDialog(viewModel);
                }));

            // If caller is on UIThread, just show the dialog
            else
                task = new AddLatecommersDialog().ShowDialog(viewModel);

            // Finally return this task
            return task;
        }



        /// <summary>
        /// Changes language in the application by specified language code
        /// </summary>
        /// <param name="langCode">The code of an language to change to</param>
        public void ChangeLanguage(string langCode) => LocalizationResource.Culture = new CultureInfo(langCode);

    }
}