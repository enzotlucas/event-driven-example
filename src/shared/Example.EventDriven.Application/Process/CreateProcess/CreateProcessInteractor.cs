﻿using Example.EventDriven.Application.CreateProcess.Boundaries;
using Example.EventDriven.Application.ExecuteProcess.Boundaries;
using Example.EventDriven.Domain.Entitites;
using Example.EventDriven.Domain.Gateways.Event;
using Example.EventDriven.Domain.Gateways.Logger;
using Example.EventDriven.Domain.Repositories;
using Example.EventDriven.Domain.ValueObjects;
using FluentValidation;
using Mapster;

namespace Example.EventDriven.Application.CreateProcess
{
    public sealed class CreateProcessInteractor : ICreateProcess
    {
        private readonly ILoggerManager _logger;
        private readonly IValidator<CreateProcessRequest> _validator;
        private readonly IEventSenderManager _eventManager;
        private readonly IProcessRepository _repository;

        public CreateProcessInteractor(ILoggerManager logger,
                                       IValidator<CreateProcessRequest> validator,
                                       IEventSenderManager eventManager,
                                       IProcessRepository repository)
        {
            _logger = logger;
            _eventManager = eventManager;
            _validator = validator;
            _repository = repository;
        }

        public async Task<CreateProcessResponse> Create(CreateProcessRequest request, CancellationToken cancellationToken)
        {
            _logger.Log("Starting create process", LoggerManagerSeverity.INFORMATION, ("request", request));
            _logger.Log("Validating the request", LoggerManagerSeverity.DEBUG, ("request", request));

            var validation = await _validator.ValidateAsync(request, cancellationToken);

            if (!validation.IsValid)
            {
                _logger.Log("Request is not valid", LoggerManagerSeverity.WARNING,
                        ("request", request),
                        ("validation", validation));

                return validation.Adapt<CreateProcessResponse>();
            }

            _logger.Log("Request is valid", LoggerManagerSeverity.DEBUG, ("request", request));
            _logger.Log("Validating if process exists", LoggerManagerSeverity.DEBUG, ("name", request.Name));

            var existingProcess = await _repository.GetByNameAsync(request.Name, cancellationToken);

            if (existingProcess.Exists())
            {
                _logger.Log("Process already exists", LoggerManagerSeverity.WARNING,
                        ("request", request),
                        ("validation", validation));

                return new CreateProcessResponse
                {
                    Value = new RequestEntity<ProcessEntity>(ResponseMessage.ProcessAlreadyExists, RequestStatus.InvalidInformation, existingProcess)
                };
            }

            _logger.Log("Process don't exists, continuing with creation", LoggerManagerSeverity.DEBUG, ("name", request.Name));

            var process = request.Adapt<ProcessEntity>();

            _logger.Log("Creating process on database", LoggerManagerSeverity.DEBUG, ("process", process));

            var (success, entity) = await _repository.CreateAsync(process, cancellationToken);

            if (!success)
            {
                _logger.Log("Error creating process", LoggerManagerSeverity.ERROR,
                        ("request", request),
                        ("validation", validation));

                return new CreateProcessResponse
                {
                    Value = new RequestEntity<ProcessEntity>(ResponseMessage.ProcessAlreadyExists, RequestStatus.InvalidInformation)
                };
            }

            _logger.Log("Process created on database", LoggerManagerSeverity.DEBUG, ("process", process));
            _logger.Log("Sending execute process event", LoggerManagerSeverity.DEBUG, ("request", request));

            await _eventManager.Send<ExecuteProcessEvent, ExecuteProcessRequest>(request.Adapt<ExecuteProcessEvent>(), cancellationToken);

            _logger.Log("Event execute process sent", LoggerManagerSeverity.DEBUG, ("request", request), ("requestId", request.RequestId));
            _logger.Log("Ending process creation", LoggerManagerSeverity.INFORMATION, ("request", request), ("entity", entity));

            return new CreateProcessResponse
            {
                Value = new RequestEntity<ProcessEntity>(ResponseMessage.Default, RequestStatus.Processing, entity)
            };
        }
    }
}
