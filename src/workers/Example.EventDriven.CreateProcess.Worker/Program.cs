using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Example.EventDriven.DependencyInjection;
using Example.EventDriven.Application.Process.CreateProcess;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((builder, services) =>
    {
        services.AddWorkerDependencyInjection(builder.Configuration);

        services.AddHostedService<CreateProcessService>();
    })
    .Build();

await host.RunAsync();