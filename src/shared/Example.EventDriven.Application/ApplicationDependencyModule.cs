﻿using Example.EventDriven.Application.CreateProcess;
using Example.EventDriven.Application.CreateProcess.Boundaries;
using Example.EventDriven.Application.ExecuteProcess;
using Example.EventDriven.Application.ExecuteProcess.Boundaries;
using Example.EventDriven.Application.GetRequestStatus;
using Example.EventDriven.Application.Request.UpdateRequest;
using Example.EventDriven.Application.SendEvent;
using Example.EventDriven.Application.SendEvent.Boundaries;
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
        public static IServiceCollection AddApplicationConfiguration(this IServiceCollection services)
        {
            return services.AddUseCases()
                           .AddValidators()
                           .AddMappers();
        }

        public static IServiceCollection AddUseCases(this IServiceCollection services)
        {
            services.AddScoped<ICreateProcess, CreateProcessInteractor>();
            services.AddScoped<IExecuteProcess, ExecuteProcessInteractor>();

            services.AddScoped<IGetRequestStatus, GetRequestStatusInteractor>();
            services.AddScoped<ISendRequest, SendRequestInteractor>();
            services.AddScoped<IUpdateRequestStatus, UpdateRequestStatusInteractor>();

            return services;
        }

        public static IServiceCollection AddValidators(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<CreateProcessValidator>();

            return services;
        }

        public static IServiceCollection AddMappers(this IServiceCollection services)
        {
            CreateProcessMapper.Add();
            ExecuteProcessMapper.Add();

            SendRequestMapper.Add();

            TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
