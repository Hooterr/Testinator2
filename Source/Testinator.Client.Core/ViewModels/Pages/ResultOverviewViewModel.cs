﻿using System;
using System.Windows.Input;
using Testinator.Core;

namespace Testinator.Client.Core
{
    /// <summary>
    /// The view model for test result page 
    /// </summary>
    public class ResultOverviewViewModel : BaseViewModel
    {
        #region Private Members

        private readonly TestHost mTestHost;

        #endregion

        #region Public Properties

        /// <summary>
        /// The name of the test user has completed
        /// </summary>
        public string TestName => mTestHost.CurrentTest.Info.Name;

        /// <summary>
        /// The time in which user has completed the test
        /// </summary>
        public TimeSpan CompletionTime => mTestHost.CurrentTest.Info.Duration - DI.Application.TimeLeft;

        /// <summary>
        /// The score user achieved in a string format
        /// </summary>
        public string UserScore => $"{mTestHost.UserScore} / {mTestHost.CurrentTest.TotalPointScore}";

        /// <summary>
        /// Indicates if the server app has allowed user to check his answers just after he finishes his test
        /// </summary>
        public bool IsResultPageAllowed => mTestHost.AreResultsAllowed;

        /// <summary>
        /// The tooltip string for the "See results" button
        /// </summary>
        public string ToolTipResultPage => mTestHost.AreResultsAllowed ? "" : "Wgląd do wyników został wyłączony przez administratora";

        /// <summary>
        /// The mark user has achieved by doing the test
        /// </summary>
        public Marks UserMark => mTestHost.UserMark;

        #endregion

        #region Commands

        /// <summary>
        /// The command to exit the result page and go back to waiting for test page
        /// </summary>
        public ICommand ExitCommand { get; private set; }

        /// <summary>
        /// The command to open question list with answers
        /// </summary>
        public ICommand GoToQuestionsCommand { get; private set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public ResultOverviewViewModel(TestHost testHost)
        {
            // Inject DI services
            mTestHost = testHost;

            // Create commands
            ExitCommand = new RelayCommand(Exit);
            GoToQuestionsCommand = new RelayCommand(GoToQuestions);
        }

        #endregion

        #region Command Methods

        /// <summary>
        /// Cleans the previous test and goes back to the waiting for test page
        /// </summary>
        private void Exit()
        {
            // Reset the test host
            mTestHost.Reset();

            DI.Application.ReturnMainScreen();

            // Indicate we are ready for another test now
            DI.Application.Network.SendData(new DataPackage(PackageType.ReadyForTest));
        }

        /// <summary>
        /// Changes page to the question list with answers
        /// </summary>
        private void GoToQuestions()
        {
            // If server has forbidden this action
            if (!IsResultPageAllowed)
            {
                // Show the message box with this info and don't change page
                DI.UI.ShowMessage(new MessageBoxDialogViewModel
                {
                    Title = "Wstęp wzbroniony",
                    Message = "Dostęp do szczegółowych wyników testu został zabroniony przez serwer.",
                    OkText = "OK"
                });

                return;
            }

            // Create view model for the page
            var viewmodel = Dna.Framework.Service<ResultQuestionsViewModel>();

            // Change the page
            DI.Application.GoToPage(ApplicationPage.ResultQuestionsPage, viewmodel);

            // And show the first question
            viewmodel.ShowFirstQuestion();
        }

        #endregion
    }
}
