using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Example.EventDriven.Application;
using Example.EventDriven.Infrastructure;
using Example.EventDriven.DependencyInjection.Swagger;

namespace Example.EventDriven.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApiDependencyInjection(this IServiceCollection services)
        {
            return services.AddApiConfiguration()
                           .AddSwaggerConfiguration()
                           .AddApplicationConfiguration()
                           .AddInfrastructureConfiguration();
        }

        private static IServiceCollection AddApiConfiguration(this IServiceCollection services)
        {
            services.AddControllers();

            services.AddEndpointsApiExplorer();

            services.AddApiVersioning(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.ReportApiVersions = true;
            });

            services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            return services;
        }

        public static IServiceCollection AddWorkerDependencyInjection(this IServiceCollection services)
        {
            return services.AddWorkerConfiguration()
                           .AddApplicationConfiguration()
                           .AddInfrastructureConfiguration();
        }

        private static IServiceCollection AddWorkerConfiguration(this IServiceCollection services)
        {
            return services;
        }
    }
}