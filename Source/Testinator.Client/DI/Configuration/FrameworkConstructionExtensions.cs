using Dna;
using Microsoft.Extensions.DependencyInjection;
using System;
using Testinator.Client.Domain;
using Testinator.Client.Logging;
using Testinator.Client.Network;
using Testinator.Client.Test;
using Testinator.Core;

namespace Testinator.Client
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
            construction.Services.AddSingleton<ITestHost, TestHost>();
            construction.Services.AddSingleton<FileManagerBase, LogsWriter>();
            construction.Services.AddSingleton<IClientModel, ClientModel>();
            construction.Services.AddSingleton<IClientNetwork, ClientNetwork>();
            construction.Services.AddSingleton<IViewModelProvider, ViewModelProvider>();
            construction.Services.AddSingleton<IUIManager, UIManager>();
            construction.Services.AddSingleton<ILogFactory>(new BaseLogFactory(new[]
            {
                // TODO: Add ApplicationSettings so we can set/edit a log location
                //       For now just log to the path where this application is running

                // TODO: Make log files ordered by a date, week-wise
                //       For now - random numbers for testing as it allows running multiple clients
                new Logging.FileLogger(($"log{new Random().Next(100000, 99999999).ToString()}.txt"), Framework.Service<FileManagerBase>())
            }));

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
