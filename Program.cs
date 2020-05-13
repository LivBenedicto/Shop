using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

// self-hosting aplication : self-management, exempt IIS (Interet Information Services).

namespace Shop
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        // abstratact for self-hosting
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    // calling Startup document
                    webBuilder.UseStartup<Startup>();
                });
    }
}
