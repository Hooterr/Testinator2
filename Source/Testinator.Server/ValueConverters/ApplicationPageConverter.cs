using System.Diagnostics;
using Testinator.Server.Domain;
using Testinator.UICore;

namespace Testinator.Server
{
    /// <summary>
    /// Converts the application pages enum to an actual view/page and vice versa
    /// </summary>
    public static class ApplicationPageConverter
    {
        /// <summary>
        /// Takes a <see cref="ApplicationPage"/> and a view model, if any, and creates the desired page
        /// </summary>
        public static BasePage ToBasePage(this ApplicationPage page, object viewModel = null)
        {
            // Find the appropriate page
            switch (page)
            {
                case ApplicationPage.Login:
                    return new LoginPage(viewModel as LoginViewModel ?? DI.GetInjectedPageViewModel<LoginViewModel>());

                case ApplicationPage.Home:
                    return new HomePage(viewModel as HomeViewModel ?? DI.GetInjectedPageViewModel<HomeViewModel>());

                case ApplicationPage.TestCreatorInitial:
                    return new TestCreatorInitialPage(viewModel as TestCreatorInitialPageViewModel ?? DI.GetInjectedPageViewModel<TestCreatorInitialPageViewModel>());

                case ApplicationPage.TestCreatorGradingPresets:
                    return new TestCreatorGradingPresetsPage(viewModel as TestCreatorGradingPresetsPageViewModel ?? DI.GetInjectedPageViewModel<TestCreatorGradingPresetsPageViewModel>());

                case ApplicationPage.TestCreatorTestInfo:
                    return new TestCreatorTestInfoPage(viewModel as TestCreatorTestInfoPageViewModel ?? DI.GetInjectedPageViewModel<TestCreatorTestInfoPageViewModel>());

                case ApplicationPage.TestCreatorQuestions:
                    return new TestCreatorQuestionsPage(viewModel as TestCreatorQuestionsPageViewModel ?? DI.GetInjectedPageViewModel<TestCreatorQuestionsPageViewModel>());

                case ApplicationPage.TestCreatorTestGrading:
                    return new TestCreatorTestGradingPage(viewModel as TestCreatorTestGradingPageViewModel ?? DI.GetInjectedPageViewModel<TestCreatorTestGradingPageViewModel>());

                case ApplicationPage.TestCreatorTestOptions:
                    return new TestCreatorTestOptionsPage(viewModel as TestCreatorTestOptionsPageViewModel ?? DI.GetInjectedPageViewModel<TestCreatorTestOptionsPageViewModel>());

                case ApplicationPage.TestCreatorTestFinalize:
                    return new TestCreatorTestFinalizePage(viewModel as TestCreatorTestFinalizePageViewModel ?? DI.GetInjectedPageViewModel<TestCreatorTestFinalizePageViewModel>());

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
        public static ApplicationPage ToApplicationPage(this BasePage page)
        {
            // Find application page that matches the base page
            if (page is LoginPage)
                return ApplicationPage.Login;

            if (page is HomePage)
                return ApplicationPage.Home;

            if (page is TestCreatorInitialPage)
                return ApplicationPage.TestCreatorInitial;

            if (page is TestCreatorGradingPresetsPage)
                return ApplicationPage.TestCreatorGradingPresets;

            if (page is TestCreatorTestInfoPage)
                return ApplicationPage.TestCreatorTestInfo;

            if (page is TestCreatorQuestionsPage)
                return ApplicationPage.TestCreatorQuestions;

            if (page is TestCreatorTestGradingPage)
                return ApplicationPage.TestCreatorTestGrading;

            if (page is TestCreatorTestOptionsPage)
                return ApplicationPage.TestCreatorTestOptions;

            if (page is TestCreatorTestFinalizePage)
                return ApplicationPage.TestCreatorTestFinalize;

            if (page is ScreenStreamPage)
                return ApplicationPage.ScreenStream;

            if (page is SettingsPage)
                return ApplicationPage.Settings;

            if (page is AboutPage)
                return ApplicationPage.About;

            // Alert developer of issue
            Debugger.Break();
            return default;
        }

        /// <summary>
        /// Takes a <see cref="QuestionsPage"/> and a view model, if any, and creates the desired page
        /// </summary>
        public static BasePage ToBasePage(this QuestionsPage page, object viewModel = null)
        {
            // Find the appropriate page
            switch (page)
            {
                case QuestionsPage.MultipleChoice:
                    return new QuestionsMultipleChoicePage(viewModel as QuestionsMultipleChoicePageViewModel ?? DI.GetInjectedPageViewModel<QuestionsMultipleChoicePageViewModel>());

                case QuestionsPage.MultipleCheckBoxes:
                    return new QuestionsMultipleCheckBoxesPage(viewModel as QuestionsMultipleCheckBoxesPageViewModel ?? DI.GetInjectedPageViewModel<QuestionsMultipleCheckBoxesPageViewModel>());

                default:
                    Debugger.Break();
                    return null;
            }
        }

        /// <summary>
        /// Converts a <see cref="BasePage"/> to the specific <see cref="QuestionsPage"/> that is for that type of page
        /// </summary>
        public static QuestionsPage ToQuestionsPage(this BasePage page)
        {
            // Find question page that matches the base page
            if (page is QuestionsMultipleChoicePage)
                return QuestionsPage.MultipleChoice;

            if (page is QuestionsMultipleCheckBoxesPage)
                return QuestionsPage.MultipleCheckBoxes;

            // Alert developer of issue
            Debugger.Break();
            return default;
        }
    }
}
