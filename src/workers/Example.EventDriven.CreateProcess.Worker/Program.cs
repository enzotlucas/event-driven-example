using Example.EventDriven.CreateProcess.Worker;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Example.EventDriven.DependencyInjection;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddWorkerDependencyInjection();

        services.AddHostedService<CreateProcessService>();
    })
    .Build();

await host.RunAsync();