using Example.EventDriven.Application.CreateProcess;
using Example.EventDriven.Application.CreateProcess.Boundaries;
using Example.EventDriven.Application.ExecuteProcess;
using Example.EventDriven.Application.ExecuteProcess.Boundaries;
using Example.EventDriven.Application.GetRequestStatus;
using Example.EventDriven.Application.Request.SendRequest;
using Example.EventDriven.Application.Request.SendRequest.Boundaries;
using Example.EventDriven.Application.Request.UpdateRequest;
using FluentValidation;
using Mapster;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace Example.EventDriven.Application
{
    [ExcludeFromCodeCoverage]
    public static class ApplicationDependencyModule
    {
        public static IServiceCollection AddApiApplicationConfiguration(this IServiceCollection services)
        {
            return services.AddApiUseCases()
                           .AddValidators()
                           .AddMappers();
        }

        public static IServiceCollection AddWorkerApplicationConfiguration(this IServiceCollection services)
        {
            return services.AddWorkerUseCases()
                           .AddValidators()
                           .AddMappers();
        }

        private static IServiceCollection AddApiUseCases(this IServiceCollection services)
        {
            services.AddScoped<IGetRequestStatus, GetRequestStatusInteractor>();
            services.AddScoped<ISendRequest, SendRequestInteractor>();
            services.AddScoped<IUpdateRequestStatus, UpdateRequestStatusInteractor>();

            return services;
        }

        private static IServiceCollection AddWorkerUseCases(this IServiceCollection services)
        {
            services.AddScoped<ICreateProcess, CreateProcessInteractor>();
            services.AddScoped<IExecuteProcess, ExecuteProcessInteractor>();

            return services;
        }

        private static IServiceCollection AddValidators(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<CreateProcessValidator>();

            return services;
        }

        private static IServiceCollection AddMappers(this IServiceCollection services)
        {
            CreateProcessMapper.Add();
            ExecuteProcessMapper.Add();
            SendRequestMapper.Add();

            TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
