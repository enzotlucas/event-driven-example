using Example.EventDriven.Domain.Gateways.Event;
using Example.EventDriven.Domain.Gateways.Logger;
using Example.EventDriven.Domain.Gateways.MemoryCache;
using Example.EventDriven.Infrastructure.Event;
using Example.EventDriven.Infrastructure.Logger;
using Example.EventDriven.Infrastructure.MemoryCache;
using Microsoft.Extensions.DependencyInjection;

namespace Example.EventDriven.Infrastructure
{
    public static class InfrastructureDependencyModule
    {
        public static IServiceCollection AddInfrastructureConfiguration(this IServiceCollection services)
        {
            return services.AddLoggingManager()
                           .AddEventManager()
                           .AddMemoryCacheManager();
        }

        private static IServiceCollection AddLoggingManager(this IServiceCollection services)
        {
            services.AddSingleton<ILoggerManager, ConsoleLoggerManager>();

            return services;
        }

        private static IServiceCollection AddEventManager(this IServiceCollection services)
        {
            services.AddScoped<IEventSenderManager, RabbitMqSenderManager>();

            return services;
        }

        private static IServiceCollection AddMemoryCacheManager(this IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddScoped<IMemoryCacheManager, MicrosoftMemoryManager>();

            return services;
        }
    }
}
