using Dna;
using Microsoft.Extensions.DependencyInjection;
using Testinator.Core;

namespace Testinator.Server.Core
{
    /// <summary>
    /// Extension methods for the <see cref="FrameworkConstruction"/>
    /// </summary>
    public static class FrameworkConstructionExtensions
    {
        /// <summary>
        /// Injects the view models needed for specifically Testinator.Server application
        /// </summary>
        /// <param name="construction">Framework's construction</param>
        public static FrameworkConstruction AddApplicationServices(this FrameworkConstruction construction)
        {
            // Bind to a single instance of specified models
            construction.Services.AddSingleton<ApplicationViewModel>();
            construction.Services.AddSingleton<ApplicationSettingsViewModel>();

            // Bind to a scoped instance of specified models
            construction.Services.AddSingleton<ServerNetwork, ServerNetwork>();
            construction.Services.AddSingleton<TestHost, TestHost>();
            construction.Services.AddSingleton<FileManagerBase, LogsWriter>();
            construction.Services.AddSingleton<TestEditor, TestEditor>();

            // Inject dependiencies into every page's view model
            construction.Services.AddTransient<BeginTestViewModel>();
            construction.Services.AddTransient<HomeViewModel>();
            construction.Services.AddTransient<LoginViewModel>();
            construction.Services.AddTransient<ScreenStreamViewModel>();
            construction.Services.AddTransient<AboutViewModel>();
            construction.Services.AddTransient<TestEditorAttachCriteriaViewModel>();
            construction.Services.AddTransient<TestEditorBasicInformationEditorViewModel>();
            construction.Services.AddTransient<TestEditorCriteriaEditorViewModel>();
            construction.Services.AddTransient<TestEditorFinalizingViewModel>();
            construction.Services.AddTransient<TestEditorInitialPageViewModel>();
            construction.Services.AddTransient<TestEditorQuestionsEditorViewModel>();
            construction.Services.AddTransient<TestEditorTestManagmentViewModel>();
            construction.Services.AddTransient<TestResultsDetailsViewModel>();
            construction.Services.AddTransient<TestResultsViewModel>();
            construction.Services.AddTransient<MultipleChoiceQuestionEditorViewModel>();

            // Return the construction for chaining
            return construction;
        }
    }
}
