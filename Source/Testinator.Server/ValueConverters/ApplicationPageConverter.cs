using System.Diagnostics;
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

                case ApplicationPage.TestEditorBasicInformationEditor:
                    return new TestCreatorTestInfoPage(viewModel as TestInfoPageViewModel ?? DI.GetInjectedPageViewModel<TestInfoPageViewModel>());

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

            if (page is TestCreatorTestInfoPage)
                return ApplicationPage.TestEditorBasicInformationEditor;

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
