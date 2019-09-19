using System.Diagnostics;
using Testinator.Server.Core;
using Testinator.Server.Domain;
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
                    return new LoginPage(viewModel as LoginViewModel ?? DI.GetInjectedPageViewModel<LoginViewModel>());

                case ApplicationPage.Home:
                    return new HomePage(viewModel as HomeViewModel ?? DI.GetInjectedPageViewModel<HomeViewModel>());

                case ApplicationPage.Demo:
                    return new DemoInfo(viewModel as TestInfoPageViewModel ?? DI.GetInjectedPageViewModel<TestInfoPageViewModel>());

                case ApplicationPage.BeginTest:
                    return new BeginTestPage(viewModel as BeginTestViewModel ?? DI.GetInjectedPageViewModel<BeginTestViewModel>());

                case ApplicationPage.BeginTestInitial:
                    return new BeginTestInitialPage(viewModel as BeginTestViewModel ?? DI.GetInjectedPageViewModel<BeginTestViewModel>());

                case ApplicationPage.BeginTestChoose:
                    return new BeginTestChoosePage(viewModel as BeginTestViewModel ?? DI.GetInjectedPageViewModel<BeginTestViewModel>());

                case ApplicationPage.BeginTestInfo:
                    return new BeginTestInfoPage(viewModel as BeginTestViewModel ?? DI.GetInjectedPageViewModel<BeginTestViewModel>());

                case ApplicationPage.BeginTestInProgress:
                    return new BeginTestInProgressPage(viewModel as BeginTestViewModel ?? DI.GetInjectedPageViewModel<BeginTestViewModel>());

                case ApplicationPage.BeginTestResults:
                    return new BeginTestResultsPage(viewModel as BeginTestViewModel ?? DI.GetInjectedPageViewModel<BeginTestViewModel>());

                case ApplicationPage.TestEditorInitial:
                    return new TestEditorInitialPage(viewModel as TestEditorInitialPageViewModel ?? DI.GetInjectedPageViewModel<TestEditorInitialPageViewModel>());

                case ApplicationPage.TestEditorCriteriaEditor:
                    return new TestEditorCriteriaEditorPage(viewModel as TestEditorCriteriaEditorViewModel ?? DI.GetInjectedPageViewModel<TestEditorCriteriaEditorViewModel>());

                case ApplicationPage.TestEditorBasicInformationEditor:
                    return new TestEditorBasicInformationEditorPage(viewModel as TestEditorBasicInformationEditorViewModel ?? DI.GetInjectedPageViewModel<TestEditorBasicInformationEditorViewModel>());

                case ApplicationPage.TestEditorQuestionsEditor:
                    return new TestEditorQuestionsEditorPage(viewModel as TestEditorQuestionsEditorViewModel ?? DI.GetInjectedPageViewModel<TestEditorQuestionsEditorViewModel>());

                case ApplicationPage.TestEditorTestManagmentPage:
                    return new TestEditorTestManagmentPage(viewModel as TestEditorTestManagmentViewModel ?? DI.GetInjectedPageViewModel<TestEditorTestManagmentViewModel>());

                case ApplicationPage.TestEditorAttachCriteria:
                    return new TestEditorAttachCriteriaPage(viewModel as TestEditorAttachCriteriaViewModel ?? DI.GetInjectedPageViewModel<TestEditorAttachCriteriaViewModel>());

                case ApplicationPage.TestEditorFinalize:
                    return new TestEditorFinalizePage(viewModel as TestEditorFinalizingViewModel ?? DI.GetInjectedPageViewModel<TestEditorFinalizingViewModel>());

                case ApplicationPage.TestResultsInitial:
                    return new TestResultsInitialPage(viewModel as TestResultsViewModel ?? DI.GetInjectedPageViewModel<TestResultsViewModel>());

                case ApplicationPage.TestResultsDetails:
                    return new TestResultsDetailsPage(viewModel as TestResultsDetailsViewModel ?? DI.GetInjectedPageViewModel<TestResultsDetailsViewModel>());

                case ApplicationPage.TestResultsStudentsView:
                    return new TestResultsStudentsViewPage(viewModel as TestResultsDetailsViewModel ?? DI.GetInjectedPageViewModel<TestResultsDetailsViewModel>());

                case ApplicationPage.TestResultsQuestionsView:
                    return new TestResultsQuestionsViewPage(viewModel as TestResultsDetailsViewModel ?? DI.GetInjectedPageViewModel<TestResultsDetailsViewModel>());

                case ApplicationPage.TestResultsDetailsView:
                    return new TestResultsStudentsViewPage(viewModel as TestResultsDetailsViewModel ?? DI.GetInjectedPageViewModel<TestResultsDetailsViewModel>());

                case ApplicationPage.QuestionMultipleCheckboxes:
                    return new QuestionMultipleCheckboxesPage(viewModel as QuestionMultipleCheckboxesViewModel ?? DI.GetInjectedPageViewModel<QuestionMultipleCheckboxesViewModel>());

                case ApplicationPage.QuestionMultipleChoice:
                    return new QuestionMultipleChoicePage(viewModel as QuestionMultipleChoiceViewModel ?? DI.GetInjectedPageViewModel<QuestionMultipleChoiceViewModel>());

                case ApplicationPage.QuestionSingleTextBox:
                    return new QuestionSingleTextBoxPage(viewModel as QuestionSingleTextBoxViewModel ?? DI.GetInjectedPageViewModel<QuestionSingleTextBoxViewModel>());

                case ApplicationPage.ResultQuestions:
                    return new ResultQuestionsPage(viewModel as ResultQuestionsViewModel ?? DI.GetInjectedPageViewModel<ResultQuestionsViewModel>());

                case ApplicationPage.ScreenStream:
                    return new ScreenStreamPage(viewModel as ScreenStreamViewModel ?? DI.GetInjectedPageViewModel<ScreenStreamViewModel>());

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

            if (page is DemoInfo)
                return ApplicationPage.Demo;

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

            if (page is TestEditorInitialPage)
                return ApplicationPage.TestEditorInitial;

            if (page is TestEditorCriteriaEditorPage)
                return ApplicationPage.TestEditorCriteriaEditor;

            if (page is TestEditorBasicInformationEditorPage)
                return ApplicationPage.TestEditorBasicInformationEditor;

            if (page is TestEditorQuestionsEditorPage)
                return ApplicationPage.TestEditorQuestionsEditor;

            if (page is TestEditorTestManagmentPage)
                return ApplicationPage.TestEditorTestManagmentPage;

            if (page is TestEditorAttachCriteriaPage)
                return ApplicationPage.TestEditorAttachCriteria;

            if (page is TestEditorFinalizePage)
                return ApplicationPage.TestEditorFinalize;

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
