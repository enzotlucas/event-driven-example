using Example.EventDriven.Application.ExecuteProcess.Boundaries;
using Example.EventDriven.Domain.Extensions;
using Example.EventDriven.Domain.Gateways.Logger;
using Example.EventDriven.Domain.Repositories;
using Example.EventDriven.Domain.ValueObjects;
using FluentValidation;
using Mapster;
using Microsoft.Extensions.Configuration;

namespace Example.EventDriven.Application.ExecuteProcess
{
    public sealed class ExecuteProcessInteractor : IExecuteProcess
    {
        private readonly ILoggerManager _logger;
        private readonly IValidator<ExecuteProcessRequest> _validator;
        private readonly IProcessRepository _repository;

        private readonly int _processExecutionDelayTimeInSeconds;

        public ExecuteProcessInteractor(
            ILoggerManager logger,
            IValidator<ExecuteProcessRequest> validator,
            IProcessRepository repository,
            IConfiguration configuration)
        {
            _logger = logger;
            _validator = validator;
            _repository = repository;

            _processExecutionDelayTimeInSeconds = configuration.GetValue<int>("ProcessExecutionDelayTimeInSeconds", 5);
        }

        public async Task<ExecuteProcessResponse> Execute(ExecuteProcessRequest request, CancellationToken cancellationToken)
        {
            _logger.Log("Starting process execution", LoggerManagerSeverity.INFORMATION, ("request", request));
            _logger.Log("Validating the request", LoggerManagerSeverity.DEBUG, ("request", request));

            var validation = await _validator.ValidateAsync(request, cancellationToken);

            if (!validation.IsValid)
            {
                _logger.Log("Request is not valid", LoggerManagerSeverity.WARNING,
                        ("request", request),
                        ("validation", validation));

                return validation.Adapt<ExecuteProcessResponse>();
            }

            _logger.Log("Request is valid", LoggerManagerSeverity.DEBUG, ("request", request));

            var process = await _repository.GetByNameAsync(request.Name, cancellationToken);

            if (!process.Exists())
            {
                _logger.Log("Process don't exists", LoggerManagerSeverity.WARNING,
                        ("request", request),
                        ("validation", validation));

                return process.Adapt<ExecuteProcessResponse>();
            }

            _logger.Log("Process exists", LoggerManagerSeverity.DEBUG, 
                    ("request", request), 
                    ("process", process));

            do
            {
                _logger.Log("Executing process", LoggerManagerSeverity.DEBUG, ("process", process));

                process.Status++;

                await _repository.UpdateAsync(process, cancellationToken);

                _logger.Log("Updated process sucessfully", LoggerManagerSeverity.DEBUG, ("process", process));
                _logger.Log("Waiting for a certain time for the process evolution", LoggerManagerSeverity.DEBUG,
                    ("processExecutionDelayTimeInSeconds", _processExecutionDelayTimeInSeconds));

                await Task.Delay(_processExecutionDelayTimeInSeconds.AsMiliseconds(), cancellationToken);

                _logger.Log("Wait for a certain time for the process evolution done", LoggerManagerSeverity.DEBUG,
                    ("processExecutionDelayTimeInSeconds", _processExecutionDelayTimeInSeconds));
            }
            while (process.Status != ProcessStatus.SuccessfullyFinished);

            _logger.Log("Ending process execution", LoggerManagerSeverity.INFORMATION, 
                    ("request", request), 
                    ("process", process));

            return request.Adapt<ExecuteProcessResponse>();
        }
    }
}
