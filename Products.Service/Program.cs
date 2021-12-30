using Microsoft.OpenApi.Models;
using Serilog;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "API Products",
        Description = "Microservice Products",
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
    .WriteTo.File(@"C:\Microservice\productservice.txt", rollingInterval: RollingInterval.Day)
    .WriteTo.Seq("http://localhost:5341"));


var app = builder.Build();

Log.Information("ApiProducts ONLINE!");

app.UseSwagger();

app.UseSwaggerUI();

app.UseSerilogRequestLogging();

app.MapGet("/", () => "ApiProducts ONLINE!");

app.Run();
