using Microsoft.Extensions.DependencyInjection;
using Testinator.WebApp.Data;

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
            services.AddSingleton<WeatherForecastService>();

            // Inject scoped services
            // The instance is created for every scope (in this case, for every client call)
            services.AddScoped<LoginPageViewModel>();

            // Inject transient services
            // The instance is created every single time it is requested in code

            // Return the services for chaining
            return services;
        }
    }
}
