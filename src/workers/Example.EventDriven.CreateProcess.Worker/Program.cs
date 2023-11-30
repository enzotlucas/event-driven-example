using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Example.EventDriven.DependencyInjection;
using Example.EventDriven.Application.Process.CreateProcess;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Configuration;

namespace Example.EventDriven.CreateProcess.Worker;

[ExcludeFromCodeCoverage]
internal static class Program
{
    private static async Task Main(string[] args)
    {
        IConfiguration configuration = default;

        var host = Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration(configurationBuilder =>
            {
                configurationBuilder.AddConfiguration(new ConfigurationBuilder()
                                                                     .AddJsonFile("appsettings.json")
                                                                     .AddEnvironmentVariables()
                                                                     .Build());

                configuration = configurationBuilder.Build();
            })
            .ConfigureServices((builder, services) =>
            {
                services.AddWorkerDependencyInjection(configuration);

                services.AddHostedService<CreateProcessService>();
            })
            .Build();

        await host.RunAsync();
    }
}