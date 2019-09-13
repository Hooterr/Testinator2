using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Dna.AspNet;

namespace Testinator.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseDnaFramework()
                .UseStartup<Startup>();
    }
}
