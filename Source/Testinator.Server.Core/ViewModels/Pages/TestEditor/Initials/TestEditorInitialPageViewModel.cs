﻿using System.Windows.Input;
using Testinator.Core;

namespace Testinator.Server.Core
{
    /// <summary>
    /// The view model for test editor (creator) page
    /// </summary>
    public class TestEditorInitialPageViewModel : BaseViewModel
    {
        #region Private Members

        private readonly TestEditor mTestEditor;

        #endregion

        #region Commands

        /// <summary>
        /// The command to create new test
        /// </summary>
        public ICommand CreateNewTestCommand { get; private set; }

        /// <summary>
        /// The command to enter test managment menu
        /// </summary>
        public ICommand EditManageTestCommand { get; private set; }

        /// <summary>
        /// The command to go to the criteria editor
        /// </summary>
        public ICommand EditCriteriaCommand { get; private set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public TestEditorInitialPageViewModel(TestEditor testEditor)
        {
            // Inject DI services
            mTestEditor = testEditor;

            // Create commands
            CreateNewTestCommand = new RelayCommand(() =>
            {
                mTestEditor.CreateNewTest();
                mTestEditor.Start();
            });

            EditManageTestCommand = new RelayCommand(() => ChangePage(ApplicationPage.TestEditorTestManagmentPage));
            EditCriteriaCommand = new RelayCommand(() => ChangePage(ApplicationPage.TestEditorCriteriaEditor));
        }

        #endregion

        #region Command Methods

        /// <summary>
        /// Changes page to the specified one
        /// </summary>
        /// <param name="page">The page to go to</param>
        private void ChangePage(ApplicationPage page)
        {
            // Simply change page
            DI.Application.GoToPage(page);
        }

        #endregion
    }
}
