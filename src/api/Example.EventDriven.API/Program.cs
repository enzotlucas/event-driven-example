using Example.EventDriven.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace Example.EventDriven.API;

[ExcludeFromCodeCoverage]
internal static class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddApiDependencyInjection(builder.Configuration);

        var app = builder.Build();

        app.UseApiDependencyInjection();

        app.MapControllers();

        app.Run();
    }
}