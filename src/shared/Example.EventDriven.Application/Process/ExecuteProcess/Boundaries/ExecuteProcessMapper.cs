using Example.EventDriven.Application.CreateProcess.Boundaries;
using Example.EventDriven.Application.Request.UpdateRequest.Boundaries;
using Example.EventDriven.Domain.Entitites;
using Example.EventDriven.Domain.ValueObjects;
using FluentValidation.Results;
using Mapster;
using System.Diagnostics.CodeAnalysis;

namespace Example.EventDriven.Application.ExecuteProcess.Boundaries
{
    [ExcludeFromCodeCoverage]
    public static class ExecuteProcessMapper
    {
        public static void Add()
        {
            TypeAdapterConfig<ExecuteProcessResponse, UpdateRequestStatusEvent>
               .NewConfig()
               .Map(destination => destination.RequestId, source => source.RequestId)
               .Map(destination => destination.Value, source => new UpdateRequestStatusRequest(source.Value, source.RequestId));

            TypeAdapterConfig<ValidationResult, ExecuteProcessResponse>
                .NewConfig()
                .Map(destination => destination.Value, source => new RequestEntity<ProcessEntity>
                (
                    Enum.Parse<ResponseMessage>(source.Errors[0].ErrorMessage),
                    RequestStatus.InvalidInformation
                ));

              TypeAdapterConfig<ProcessEntity, ExecuteProcessResponse>
                .NewConfig()
                .Map(destination => destination.Value, source => new RequestEntity<ProcessEntity>
                (
                    source.Exists() ? ResponseMessage.Default : ResponseMessage.ProcessDontExists,
                    RequestStatus.Canceled,
                    source
                ));
        }
    }
}
