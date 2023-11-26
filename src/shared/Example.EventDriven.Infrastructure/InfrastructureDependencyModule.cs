using Example.EventDriven.Domain.Gateways.Event;
using Example.EventDriven.Domain.Gateways.Logger;
using Example.EventDriven.Domain.Gateways.MemoryCache;
using Example.EventDriven.Domain.Repositories;
using Example.EventDriven.Infrastructure.Database.Core;
using Example.EventDriven.Infrastructure.Database.Repositories;
using Example.EventDriven.Infrastructure.Event;
using Example.EventDriven.Infrastructure.Logger;
using Example.EventDriven.Infrastructure.MemoryCache;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Example.EventDriven.Infrastructure
{
    public static class InfrastructureDependencyModule
    {
        public static IServiceCollection AddInfrastructureConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddLoggingManager()
                           .AddEventManager()
                           .AddMemoryCacheManager()
                           .AddDatabase(configuration);
        }

        private static IServiceCollection AddLoggingManager(this IServiceCollection services)
        {
            services.AddSingleton<ILoggerManager, ConsoleLoggerManager>();

            return services;
        }

        private static IServiceCollection AddEventManager(this IServiceCollection services)
        {
            services.AddScoped<IEventSenderManager, RabbitMqSenderManager>();
            services.AddScoped<IEventConsumerManager, RabbitMqConsumerManager>();

            return services;
        }

        private static IServiceCollection AddMemoryCacheManager(this IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddScoped<IMemoryCacheManager, MicrosoftMemoryManager>();

            return services;
        }

        private static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<SqlServerContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("SqlServer"));
            });

            services.AddScoped<SqlServerContext>();
            services.AddScoped<IProcessRepository, ProcessRepository>();
                
            using var serviceProvider = services.BuildServiceProvider();
            using var context = serviceProvider.GetRequiredService<SqlServerContext>();
            
            context.Database.Migrate();

            return services;
        }
    }
}
