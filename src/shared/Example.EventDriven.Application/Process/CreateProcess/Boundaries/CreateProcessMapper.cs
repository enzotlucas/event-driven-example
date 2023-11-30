using Example.EventDriven.Application.ExecuteProcess.Boundaries;
using Example.EventDriven.Application.Request.UpdateRequest.Boundaries;
using Example.EventDriven.Application.SendEvent.Boundaries;
using Example.EventDriven.Domain.Entitites;
using Example.EventDriven.Domain.ValueObjects;
using Mapster;
using System.Diagnostics.CodeAnalysis;
using FluentValidation.Results;

namespace Example.EventDriven.Application.CreateProcess.Boundaries
{
    [ExcludeFromCodeCoverage]
    public static class CreateProcessMapper
    {
        public static void Add()
        {
            TypeAdapterConfig<CreateProcessRequest, SendEventRequest>
                .NewConfig()
                .Map(destination => destination.OperationName, source => nameof(CreateProcessRequest))
                .Map(destination => destination.Value, source => source);

            TypeAdapterConfig<CreateProcessRequest, CreateProcessResponse>
                .NewConfig();

            TypeAdapterConfig<ValidationResult, CreateProcessResponse>
                .NewConfig()
                .Map(destination => destination.Value, source => new RequestEntity<ProcessEntity>
                (
                    Enum.Parse<ResponseMessage>(source.Errors[0].ErrorMessage),
                    RequestStatus.InvalidInformation
                ));

            TypeAdapterConfig<CreateProcessRequest, ProcessEntity>
                .NewConfig()
                .Map(destination => destination.Name, source => source.Name)
                .Map(destination => destination.Description, source => source.Description)
                .Map(destination => destination.Status, source => ProcessStatus.Created)
                .Map(destination => destination.CreatedAt, source => DateTime.Now)
                .Map(destination => destination.LastUpdatedAt, source => DateTime.Now);

            TypeAdapterConfig<ProcessEntity, CreateProcessResponse>
                .NewConfig()
                .Map(destination => destination.Value, source => new RequestEntity<ProcessEntity>
                (
                    source.Exists() ? ResponseMessage.ProcessAlreadyExists : ResponseMessage.Default,
                    source.Exists() ? RequestStatus.InvalidInformation : RequestStatus.Processing,
                    source
                ));

            TypeAdapterConfig<CreateProcessResponse, UpdateRequestStatusEvent>
                .NewConfig()
                .Map(destination => destination.RequestId, source => source.RequestId)
                .Map(destination => destination.Value, source => new UpdateRequestStatusRequest(source.Value, source.RequestId));

            TypeAdapterConfig<CreateProcessRequest, ExecuteProcessEvent>
                .NewConfig()
                .Map(destination => destination.Value, source => new ExecuteProcessRequest(source.Name));
        }
    }
}
