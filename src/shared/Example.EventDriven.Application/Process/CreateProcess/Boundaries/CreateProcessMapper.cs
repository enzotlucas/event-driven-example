using Example.EventDriven.Application.Request.UpdateRequest.Boundaries;
using Example.EventDriven.Application.SendEvent.Boundaries;
using Example.EventDriven.Domain.Entitites;
using Example.EventDriven.Domain.ValueObjects;
using Mapster;
using Microsoft.AspNetCore.Http.Features.Authentication;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

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
                    Enum.Parse<ResponseMessage>(source.ErrorMessage), 
                    RequestStatus.InvalidInformation
                ));

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
        }
    }
}
