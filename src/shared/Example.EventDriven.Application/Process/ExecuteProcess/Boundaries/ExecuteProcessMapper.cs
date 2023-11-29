using Example.EventDriven.Application.Request.UpdateRequest.Boundaries;
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
        }
    }
}
