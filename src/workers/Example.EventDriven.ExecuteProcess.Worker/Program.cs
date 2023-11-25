using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Example.EventDriven.DependencyInjection;
using Example.EventDriven.ExecuteProcess.Worker;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddWorkerDependencyInjection();

        services.AddHostedService<ExecuteProcessService>();
    })
    .Build();

await host.RunAsync();