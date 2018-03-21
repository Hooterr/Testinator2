﻿using System.Diagnostics;
using Testinator.Server.Core;
using Testinator.UICore;

namespace Testinator.Server
{
    /// <summary>
    /// Converts the <see cref="ApplicationPage"/> to an actual view/page
    /// </summary>
    public static class ApplicationPageConverter
    {
        /// <summary>
        /// Takes a <see cref="ApplicationPage"/> and a view model, if any, and creates the desired page
        /// </summary>
        /// <param name="page"></param>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        public static BasePage ToBasePage(this ApplicationPage page, object viewModel = null)
        {
            // Find the appropriate page
            switch (page)
            {
                case ApplicationPage.Login:
                    return new LoginPage(viewModel as LoginViewModel);

                case ApplicationPage.Home:
                    return new HomePage(viewModel as HomeViewModel);

                case ApplicationPage.BeginTest:
                    return new BeginTestPage(viewModel as BeginTestViewModel);

                case ApplicationPage.BeginTestInitial:
                    return new BeginTestInitialPage(viewModel as BeginTestViewModel);

                case ApplicationPage.BeginTestChoose:
                    return new BeginTestChoosePage(viewModel as BeginTestViewModel);

                case ApplicationPage.BeginTestInfo:
                    return new BeginTestInfoPage(viewModel as BeginTestViewModel);

                case ApplicationPage.BeginTestInProgress:
                    return new BeginTestInProgressPage(viewModel as BeginTestViewModel);

                case ApplicationPage.BeginTestResults:
                    return new BeginTestResultsPage(viewModel as BeginTestViewModel);

                case ApplicationPage.TestEditor:
                    return new TestEditorPage(viewModel as TestEditorViewModel);

                case ApplicationPage.TestEditorAddNewCriteria:
                    return new TestEditorAddNewCriteriaPage(viewModel as TestEditorAddNewCriteriaViewModel);

                case ApplicationPage.TestEditorAddTest:
                    return new TestEditorAddNewTestPage(viewModel as TestEditorAddNewTestViewModel);

                case ApplicationPage.TestEditorAddQuestions:
                    return new TestEditorAddQuestionsPage(viewModel as TestEditorAddNewTestViewModel);

                case ApplicationPage.TestEditorEditTest:
                    return new TestEditorEditTestPage(viewModel as TestEditorEditTestViewModel);

                case ApplicationPage.TestEditorAttachCriteria:
                    return new TestEditorAttachCriteriaPage(viewModel as TestEditorAddNewTestViewModel);

                case ApplicationPage.TestEditorResult:
                    return new TestEditorResultPage(viewModel as TestEditorAddNewTestViewModel);

                case ApplicationPage.TestResultsInitial:
                    return new TestResultsInitialPage(viewModel as TestResultsViewModel);

                case ApplicationPage.TestResultsDetails:
                    return new TestResultsDetailsPage(viewModel as TestResultsDetailsViewModel);

                case ApplicationPage.TestResultsStudentsView:
                    return new TestResultsStudentsViewPage(viewModel as TestResultsDetailsViewModel);

                case ApplicationPage.TestResultsQuestionsView:
                    return new TestResultsQuestionsViewPage(viewModel as TestResultsDetailsViewModel);

                case ApplicationPage.TestResultsDetailsView:
                    return new TestResultsStudentsViewPage(viewModel as TestResultsDetailsViewModel);

                case ApplicationPage.QuestionMultipleCheckboxes:
                    return new QuestionMultipleCheckboxesPage(viewModel as QuestionMultipleCheckboxesViewModel);

                case ApplicationPage.QuestionMultipleChoice:
                    return new QuestionMultipleChoicePage(viewModel as QuestionMultipleChoiceViewModel);

                case ApplicationPage.QuestionSingleTextBox:
                    return new QuestionSingleTextBoxPage(viewModel as QuestionSingleTextBoxViewModel);

                case ApplicationPage.ResultQuestions:
                    return new ResultQuestionsPage(viewModel as ResultQuestionsViewModel);

                case ApplicationPage.ScreenStream:
                    return new ScreenStreamPage(viewModel as ScreenStreamViewModel);

                case ApplicationPage.Settings:
                    return new SettingsPage();

                case ApplicationPage.About:
                    return new AboutPage();

                default:
                    Debugger.Break();
                    return null;
            }
        }

        /// <summary>
        /// Converts a <see cref="BasePage"/> to the specific <see cref="ApplicationPage"/> that is for that type of page
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public static ApplicationPage ToApplicationPage(this BasePage page)
        {
            // Find application page that matches the base page
            if (page is LoginPage)
                return ApplicationPage.Login;

            if (page is HomePage)
                return ApplicationPage.Home;

            if (page is BeginTestPage)
                return ApplicationPage.BeginTest;

            if (page is BeginTestInitialPage)
                return ApplicationPage.BeginTestInitial;

            if (page is BeginTestChoosePage)
                return ApplicationPage.BeginTestChoose;

            if (page is BeginTestInfoPage)
                return ApplicationPage.BeginTestInfo;

            if (page is BeginTestInProgressPage)
                return ApplicationPage.BeginTestInProgress;

            if (page is BeginTestResultsPage)
                return ApplicationPage.BeginTestResults;

            if (page is TestEditorPage)
                return ApplicationPage.TestEditor;

            if (page is TestEditorAddNewCriteriaPage)
                return ApplicationPage.TestEditorAddNewCriteria;

            if (page is TestEditorAddNewTestPage)
                return ApplicationPage.TestEditorAddTest;

            if (page is TestEditorAddQuestionsPage)
                return ApplicationPage.TestEditorAddQuestions;

            if (page is TestEditorEditTestPage)
                return ApplicationPage.TestEditorEditTest;

            if (page is TestEditorAttachCriteriaPage)
                return ApplicationPage.TestEditorAttachCriteria;

            if (page is TestEditorResultPage)
                return ApplicationPage.TestEditorResult;

            if (page is TestResultsInitialPage)
                return ApplicationPage.TestResultsInitial;

            if (page is TestResultsDetailsPage)
                return ApplicationPage.TestResultsDetails;

            if (page is TestResultsQuestionsViewPage)
                return ApplicationPage.TestResultsQuestionsView;

            if (page is TestResultsDetailsViewPage)
                return ApplicationPage.TestResultsDetailsView;

            if (page is TestResultsStudentsViewPage)
                return ApplicationPage.TestResultsStudentsView;

            if (page is QuestionMultipleChoicePage)
                return ApplicationPage.QuestionMultipleChoice;

            if (page is QuestionMultipleCheckboxesPage)
                return ApplicationPage.QuestionMultipleCheckboxes;

            if (page is QuestionSingleTextBoxPage)
                return ApplicationPage.QuestionSingleTextBox;

            if (page is ResultQuestionsPage)
                return ApplicationPage.ResultQuestions;

            if (page is ScreenStreamPage)
                return ApplicationPage.ScreenStream;

            if (page is SettingsPage)
                return ApplicationPage.Settings;

            if (page is AboutPage)
                return ApplicationPage.About;

            // Alert developer of issue
            Debugger.Break();
            return default(ApplicationPage);
        }
    }
}
