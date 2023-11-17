using Example.EventDriven.Application.CreateProccess;
using Example.EventDriven.Application.CreateProccess.Boundaries;
using Example.EventDriven.Application.GetRequestStatus;
using Example.EventDriven.Application.SendEvent.Boundaries;
using FluentValidation;
using Mapster;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

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
            services.AddValidatorsFromAssemblyContaining<CreateProcessValidator>();

            return services;
        }

        public static IServiceCollection AddMappers(this IServiceCollection services)
        {
            SendEventMapper.Add();
            CreateProcessMapper.Add();

            TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
