using Dna;
using Microsoft.Extensions.DependencyInjection;
using Testinator.Core;

namespace Testinator.Client.Core
{
    /// <summary>
    /// Extension methods for the <see cref="FrameworkConstruction"/>
    /// </summary>
    public static class FrameworkConstructionExtensions
    {
        /// <summary>
        /// Injects the view models needed for specifically Testinator.Client application
        /// </summary>
        /// <param name="construction">Framework's construction</param>
        public static FrameworkConstruction AddApplicationServices(this FrameworkConstruction construction)
        {
            // Bind to a single instance of specified models
            construction.Services.AddSingleton<ApplicationViewModel>();

            // Bind to a scoped instance of specified models
            construction.Services.AddSingleton<TestHost, TestHost>();
            construction.Services.AddSingleton<FileManagerBase, LogsWriter>();
            construction.Services.AddSingleton<ClientModel, ClientModel>();
            construction.Services.AddSingleton<ClientNetwork, ClientNetwork>();

            // Inject dependiencies into every page's view model
            construction.Services.AddTransient<LoginViewModel>();
            construction.Services.AddTransient<QuestionMultipleCheckboxesViewModel>();
            construction.Services.AddTransient<QuestionMultipleChoiceViewModel>();
            construction.Services.AddTransient<QuestionSingleTextBoxViewModel>();
            construction.Services.AddTransient<ResultQuestionsViewModel>();
            construction.Services.AddTransient<ResultOverviewViewModel>();
            construction.Services.AddTransient<WaitingForTestViewModel>();

            // Return the construction for chaining
            return construction;
        }
    }
}
