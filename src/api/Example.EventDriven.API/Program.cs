using Example.EventDriven.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace Example.EventDriven.API;

[ExcludeFromCodeCoverage]
internal static class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Configuration
               .SetBasePath(builder.Environment.ContentRootPath)
               .AddJsonFile("appsettings.json", true, true)
               .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true)
               .AddEnvironmentVariables();

        builder.Services.AddApiDependencyInjection(builder.Configuration);

        var app = builder.Build();

        app.UseApiDependencyInjection();

        app.MapControllers();

        app.Run();
    }
}