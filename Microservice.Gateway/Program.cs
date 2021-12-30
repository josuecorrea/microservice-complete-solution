using Ocelot.Middleware;
using Ocelot.DependencyInjection;
using Ocelot.Cache;
using Microservice.Gateway;
using Serilog;
using Microsoft.OpenApi.Models;
using System.Reflection;
using MMLib.SwaggerForOcelot.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddOcelot();
builder.Services.AddSwaggerForOcelot(builder.Configuration);

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "API Gateway",
        Description = "Microservice Gateway with Ocelot",
        TermsOfService = new Uri("https://www.linkedin.com/in/josuejtc/"),
        Contact = new OpenApiContact
        {
            Name = "Josué Corrêa",
            Email = string.Empty,
            Url = new Uri("https://www.linkedin.com/in/josuejtc/"),
        },
        License = new OpenApiLicense
        {
            Name = "Use under LICX",
            Url = new Uri("https://www.linkedin.com/in/josuejtc/"),
        }        
    });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

builder.Host.UseSerilog((ctx, lc) => lc
    .WriteTo.Console()
    .WriteTo.File(@"C:\Microservice\gateway.txt", rollingInterval: RollingInterval.Day)
    .WriteTo.Seq("http://localhost:5341"));

builder.Configuration.AddJsonFile("configuration.json", optional: true, reloadOnChange: true);
//builder.Configuration.AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", optional: true, reloadOnChange: true)

//builder.Configuration.AddOcelotWithSwaggerSupport((o) =>
//{
//    o.Folder = "Configuration";
//});

var app = builder.Build();

Log.Information("ApiGateway ONLINE!");

app.UseSwagger();

app.UseSwaggerForOcelotUI(opt =>
{
   opt.PathToSwaggerGenerator = "/swagger/docs";
});

app.MapGet("/", () => "Service Online!");

//app.UseSwaggerUI();


app.UseMiddleware<RequestResponseLoggingMiddleware>();

app.UseOcelot().Wait();

app.UseSerilogRequestLogging();

app.Run();
