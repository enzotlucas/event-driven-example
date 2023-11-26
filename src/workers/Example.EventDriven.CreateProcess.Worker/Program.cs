using Example.EventDriven.CreateProcess.Worker;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Example.EventDriven.DependencyInjection;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((builder, services) =>
    {
        services.AddWorkerDependencyInjection(builder.Configuration);

        services.AddHostedService<CreateProcessService>();
    })
    .Build();

await host.RunAsync();