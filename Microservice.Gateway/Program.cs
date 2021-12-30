using Ocelot.Middleware;
using Ocelot.DependencyInjection;
using Ocelot.Cache;
using Microservice.Gateway;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOcelot();

builder.Host.UseSerilog((ctx, lc) => lc
    .WriteTo.Console()
    .WriteTo.File(@"C:\Microservice\gateway.txt", rollingInterval: RollingInterval.Day)
    .WriteTo.Seq("http://localhost:5341"));

builder.Configuration.AddJsonFile("configuration.json", optional: true, reloadOnChange: true);
//builder.Configuration.AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", optional: true, reloadOnChange: true)

var app = builder.Build();

Log.Information("ApiGateway ONLINE!");

app.UseSwagger();

app.MapGet("/", () => "Service Online!");

app.UseSwaggerUI();

app.UseMiddleware<RequestResponseLoggingMiddleware>();

app.UseOcelot().Wait();

app.UseSerilogRequestLogging();

app.Run();
