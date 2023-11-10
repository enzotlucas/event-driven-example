using Example.EventDriven.Application.CreateProccess;
using Example.EventDriven.Application.GetRequestStatus;
using Microsoft.Extensions.DependencyInjection;

namespace Example.EventDriven.Application
{
    public static class ApplicationDependencyModule
    {
        public static IServiceCollection AddApplicationConfiguration(this IServiceCollection services)
        {
            return services.AddUseCases()
                           .AddValidators()
                           .AddMappers();
        }

        public static IServiceCollection AddUseCases(this IServiceCollection services)
        {
            services.AddScoped<ICreateProcess, CreateProcessInteractor>();
            services.AddScoped<IGetRequestStatus, GetRequestStatusInteractor>();

            return services;
        }

        public static IServiceCollection AddValidators(this IServiceCollection services)
        {
            return services;
        }

        public static IServiceCollection AddMappers(this IServiceCollection services)
        {
            return services;
        }
    }
}
