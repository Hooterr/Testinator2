using Dna;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Testinator.Server.Database;
using Testinator.Server.Domain;
using Testinator.Server.Files;
using Testinator.Server.Network;
using Testinator.Server.Services;
using Testinator.TestSystem.Editors;

namespace Testinator.Server
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
            construction.Services.AddSingleton<ImagesEditorViewModel>();
            construction.Services.AddSingleton<MenuListViewModel>();

            // Bind to a scoped instance of specified models
            construction.Services.AddScoped<IServerNetwork, ServerNetwork>();
            construction.Services.AddScoped<UserMapper>();
            construction.Services.AddScoped<ISettingsRepository, SettingsRepository>();
            construction.Services.AddScoped<IUserRepository, UserRepository>();
            construction.Services.AddScoped<IUserAccountService, UserAccountService>();
            construction.Services.AddScoped<ITestCreatorService, TestCreatorService>();
            construction.Services.AddScoped<IUIManager, UIManager>();

            // Inject dependencies into every page's view model
            construction.Services.AddTransient<HomeViewModel>();
            construction.Services.AddTransient<LoginViewModel>();
            construction.Services.AddTransient<ScreenStreamViewModel>();
            construction.Services.AddTransient<AboutViewModel>();
            construction.Services.AddTransient<MenuListItemViewModel>();

            construction.Services.AddTransient<TestCreatorInitialPageViewModel>();
            construction.Services.AddTransient<TestCreatorTestInfoPageViewModel>();
            construction.Services.AddTransient<TestCreatorTestOptionsPageViewModel>();
            construction.Services.AddTransient<TestCreatorTestFinalizePageViewModel>();
            construction.Services.AddTransient<TestCreatorQuestionsPageViewModel>();
            construction.Services.AddTransient<TestCreatorGradingPageViewModel>();
            construction.Services.AddTransient<QuestionsMultipleChoicePageViewModel>();
            construction.Services.AddTransient<QuestionsCheckboxesPageViewModel>();
            construction.Services.AddTransient<QuestionsSingleAnswerPageViewModel>();

            construction.Services.AddScoped<ITestFileManager, TestFileManager>();

            FilesServicesInstaller.Install(construction.Services);

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
