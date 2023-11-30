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
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace Example.EventDriven.Infrastructure
{
    [ExcludeFromCodeCoverage]
    public static class InfrastructureDependencyModule
    {
        public static IServiceCollection AddLoggingManager(this IServiceCollection services)
        {
            services.AddSingleton<ILoggerManager, ConsoleLoggerManager>();

            return services;
        }

        public static IServiceCollection AddEventManager(this IServiceCollection services)
        {
            services.AddScoped<IEventSenderManager, RabbitMqSenderManager>();
            services.AddSingleton<IEventConsumerManager, RabbitMqConsumerManager>();

            return services;
        }

        public static IServiceCollection AddMemoryCacheManager(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMemoryCache();
            services.AddScoped<IMemoryCacheManager, MicrosoftMemoryManager>();
            services.AddSingleton(new MemoryCacheEntryOptions
            {
                SlidingExpiration = TimeSpan.FromMinutes(configuration.GetValue<int>("MemoryCache:SlidingExpirationInMinutes", 2))
            });

            return services;
        }

        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
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
