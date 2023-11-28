using Example.EventDriven.Application.SendEvent.Boundaries;
using Example.EventDriven.Domain.Entitites;
using Example.EventDriven.Domain.ValueObjects;
using Mapster;
using System.ComponentModel.DataAnnotations;

namespace Example.EventDriven.Application.CreateProcess.Boundaries
{
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
        }
    }
}
