using Dna;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Testinator.Core;
using Testinator.Server.Database;
using Testinator.Server.Domain;
using Testinator.Server.TestCreator;
using Testinator.TestSystem.Editors;

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
            construction.Services.AddScoped<ServerNetwork, ServerNetwork>();
            construction.Services.AddScoped<TestHost, TestHost>();
            construction.Services.AddScoped<FileManagerBase, LogsWriter>();
            construction.Services.AddScoped<TestEditor, TestEditor>();
            construction.Services.AddScoped<UserMapper>();
            construction.Services.AddScoped<ISettingsRepository, SettingsRepository>();
            construction.Services.AddScoped<IUserRepository, UserRepository>();
            construction.Services.AddScoped<IUserAccountService, UserAccountService>();
            construction.Services.AddScoped<ITestCreatorService, TestCreatorService>();

            // Inject dependiencies into every page's view model
            construction.Services.AddTransient<BeginTestViewModel>();
            construction.Services.AddTransient<HomeViewModel>();
            construction.Services.AddTransient<MultipleChoiceQuestionTestEditorViewModel>();
            construction.Services.AddTransient<TestInfoPageViewModel>();
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

        /// <summary>
        /// Injects the database for Testinator.Server application
        /// </summary>
        /// <param name="construction">Framework's construction</param>
        public static FrameworkConstruction AddDbContext(this FrameworkConstruction construction)
        {
            // Use Sqlite library
            construction.Services.AddEntityFrameworkSqlite();

            // Bind a db context to access in this application
            construction.Services.AddDbContext<TestinatorServerDbContext>();

            // Get the service provider
            var serviceProvider = construction.Services.BuildServiceProvider();
            using (var scope = serviceProvider.CreateScope())
            {
                // Get the db service
                var db = scope.ServiceProvider.GetRequiredService<TestinatorServerDbContext>();
                // Make sure its created properly and do pending migrations
                db.Database.Migrate();
            }

            // Return the construction for chaining
            return construction;
        }
    }
}
