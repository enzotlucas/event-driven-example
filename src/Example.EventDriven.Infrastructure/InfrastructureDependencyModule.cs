using Microsoft.Extensions.DependencyInjection;

namespace Example.EventDriven.Infrastructure
{
    public static class InfrastructureDependencyModule
    {
        public static IServiceCollection AddInfrastructureConfiguration(this IServiceCollection services)
        {
            return services;
        }
    }
}
