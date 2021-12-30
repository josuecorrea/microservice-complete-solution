using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Microservice.ApiGateway
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
                    webBuilder.UseStartup<Startup>();
                }).UseSerilog((ctx, lc) => lc
                    .WriteTo.Console()
                    .WriteTo.File(@"C:\Microservice\gateway.txt", rollingInterval: RollingInterval.Day)
                    .WriteTo.Seq("http://localhost:5341"));
    }
}
