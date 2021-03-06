using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Gateway.Service
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                 .ConfigureAppConfiguration((host, config) =>
                 {
                     config.AddJsonFile("ocelot.json");
                 })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();

                }).UseSerilog((ctx, lc) => lc
                    .WriteTo.Console()
                    .WriteTo.File(@"C:\Microservice\gateway.txt", rollingInterval: RollingInterval.Day)
                    .WriteTo.Seq("http://localhost:5341"));
    }
}
