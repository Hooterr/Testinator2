using Microsoft.Extensions.DependencyInjection;
using Testinator.Core;
using Testinator.TestSystem.Editors;

namespace Testinator.WebApp
{
    /// <summary>
    /// The extensions for dependency injection structures
    /// </summary>
    public static class DIExtensions
    {
        /// <summary>
        /// Injects all the services that this application needs to the dependency injection
        /// </summary>
        public static IServiceCollection InjectApplicationServices(this IServiceCollection services)
        {
            // Inject singleton services
            // The instance is created only once
            services.AddSingleton<ITestCreatorService, TestCreatorService>();

            // Inject scoped services
            // The instance is created for every scope (in this case, for every client call)
            services.AddScoped<LoginPageViewModel>();
            services.AddScoped<RegisterPageViewModel>();
            services.AddScoped<DashboardPageViewModel>();
            services.AddScoped<TestCreatorInitialPageViewModel>();
            services.AddScoped<TestCreatorTestInfoPageViewModel>();

            // Inject transient services
            // The instance is created every single time it is requested in code
            services.AddTransient<IViewModelProvider, ViewModelProvider>();

            // Return the services for chaining
            return services;
        }
    }
}
