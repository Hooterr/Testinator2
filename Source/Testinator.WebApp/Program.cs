using Dna.AspNet;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Testinator.WebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                        .UseDnaFramework()
                        .UseStartup<Startup>();
                });
    }
}
