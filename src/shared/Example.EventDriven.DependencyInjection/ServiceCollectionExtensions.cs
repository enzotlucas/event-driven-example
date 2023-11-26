using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Example.EventDriven.Application;
using Example.EventDriven.Infrastructure;
using Example.EventDriven.DependencyInjection.Swagger;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Configuration;

namespace Example.EventDriven.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApiDependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddApiConfiguration()
                           .AddSwaggerConfiguration()
                           .AddApplicationConfiguration()
                           .AddInfrastructureConfiguration(configuration);
        }

        private static IServiceCollection AddApiConfiguration(this IServiceCollection services)
        {
            services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(o => o.SerializerOptions.Converters.Add(new JsonStringEnumConverter()));
            services.Configure<JsonOptions>(o => o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

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

        public static IServiceCollection AddWorkerDependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddWorkerConfiguration()
                           .AddApplicationConfiguration()
                           .AddInfrastructureConfiguration(configuration);
        }

        private static IServiceCollection AddWorkerConfiguration(this IServiceCollection services)
        {
            return services;
        }
    }
}