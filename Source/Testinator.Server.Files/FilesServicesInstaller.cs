using Microsoft.Extensions.DependencyInjection;

namespace Testinator.Server.Files
{
    public static class FilesServicesInstaller
    {
        public static void Install(IServiceCollection services)
        {
            services.AddScoped<IFileAccessService, FileAccessService>();
        }
    }
}
