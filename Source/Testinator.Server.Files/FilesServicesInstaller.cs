using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Testinator.Server.Files
{
    public static class FilesServicesInstaller
    {
        public static void Install(IServiceCollection services)
        {
            services.AddScoped<IFileAccessService, FileAccessService>()
                .AddScoped<IFileService, FileService>();
        }
    }
}
