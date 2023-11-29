using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Example.EventDriven.DependencyInjection;
using Example.EventDriven.Process.ExecuteProcess.Application;
using System.Diagnostics.CodeAnalysis;

namespace Example.EventDriven.ExecuteProcess.Worker;

[ExcludeFromCodeCoverage]
internal static class Program
{
    private static async Task Main(string[] args)
    {
        var host = Host.CreateDefaultBuilder(args)
            .ConfigureServices((builder, services) =>
            {
                services.AddWorkerDependencyInjection(builder.Configuration);

                services.AddHostedService<ExecuteProcessService>();
            })
            .Build();

        await host.RunAsync();
    }
}