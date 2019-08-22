﻿using System;
using System.Windows.Input;
using Testinator.Core;

namespace Testinator.Server.Core
{
    /// <summary>
    /// The view model for edit existing test page
    /// </summary>
    public class TestEditorTestManagmentViewModel : BaseViewModel
    {
        #region Private Members

        private readonly TestEditor mTestEditor;

        #endregion

        #region Public Properties

        /// <summary>
        /// The test binary file writer which handles tests saving/deleting from local folder
        /// </summary>
        public BinaryWriter TestFileWriter { get; private set; } = new BinaryWriter(SaveableObjects.Test);

        /// <summary>
        /// Indicates if the user can enter editing mode, meaning there is a test selected
        /// </summary>
        public bool IsEditingAvailable => TestListViewModel.Instance.IsAnySelected;

        #endregion

        #region Commands

        /// <summary>
        /// The command to change page to adding test to which we provide data from existing test
        /// </summary>
        public ICommand EnterEditingModeCommand { get; private set; }

        /// <summary>
        /// The command to delete test which is choosen in the list
        /// </summary>
        public ICommand DeleteTestCommand { get; private set; }

        /// <summary>
        /// The command to return to the previous page
        /// </summary>
        public ICommand ReturnCommand { get; private set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public TestEditorTestManagmentViewModel(TestEditor testEditor)
        {
            // Inject DI services
            mTestEditor = testEditor;

            // Create commands
            EnterEditingModeCommand = new RelayCommand(EnterEditingMode);
            DeleteTestCommand = new RelayCommand(DeleteTest);
            ReturnCommand = new RelayCommand(() => DI.Application.GoToPage(ApplicationPage.TestEditorInitial));

            // Hook up to the events
            TestListViewModel.Instance.SelectionChanged += UpdateView;

            // Make sure that test list is up to date
            TestListViewModel.Instance.LoadItems();
        }

        #endregion

        #region Command Methods

        /// <summary>
        /// Changes page to add new test page with data from selected test in this page
        /// </summary>
        private void EnterEditingMode()
        {
            mTestEditor.EditTest(TestListViewModel.Instance.SelectedItem);
            mTestEditor.Start();
        }

        /// <summary>
        /// Deletes test from the list
        /// </summary>
        private void DeleteTest()
        {
            // Check if user has selected any test
            if (!TestListViewModel.Instance.IsAnySelected)
                return;
            
            // Confirm
            var vm = new DecisionDialogViewModel
            {
                Title = "Usuwanie testu",
                Message = "Czy jesteś pewny, że chcesz usunąć ten test?",
                AcceptText = LocalizationResource.Yes,
                CancelText = LocalizationResource.No
            };

            DI.UI.ShowMessage(vm);

            // No
            if (!vm.UserResponse)
                return;

            try
            {
                // Finally try to delete selected test
                TestFileWriter.DeleteFile(TestListViewModel.Instance.SelectedItem);
            }
            catch (Exception ex)
            {
                // If an error occured, show info to the user
                DI.UI.ShowMessage(new MessageBoxDialogViewModel
                {
                    Title = LocalizationResource.DeletionError,
                    Message = "Nie udało się usunąć tego testu." + "\n" +
                              LocalizationResource.ErrorContentSemicolon + ex.Message,
                    OkText = LocalizationResource.Ok
                });

                DI.Logger.Log("Unable to delete test from local folder, error message: " + ex.Message);
            }

            // Update test list
            TestListViewModel.Instance.LoadItems();
        }

        #endregion

        #region Private Event Methods

        /// <summary>
        /// Selection in test list control changes
        /// </summary>
        private void UpdateView()
        {
            OnPropertyChanged(nameof(IsEditingAvailable));
        }

        #endregion

        #region CleanUp

        /// <summary>
        /// Disposes this viewmodel
        /// </summary>
        public override void Dispose()
        {
            TestListViewModel.Instance.SelectionChanged -= UpdateView;
        }

        #endregion
    }
}
