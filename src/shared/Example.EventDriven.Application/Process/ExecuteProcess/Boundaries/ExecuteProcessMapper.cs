using Example.EventDriven.Application.CreateProcess.Boundaries;
using Example.EventDriven.Application.Request.UpdateRequest.Boundaries;
using Mapster;

namespace Example.EventDriven.Application.ExecuteProcess.Boundaries
{
    public static class ExecuteProcessMapper
    {
        public static void Add()
        {
            TypeAdapterConfig<ExecuteProcessResponse, UpdateRequestStatusEvent>
               .NewConfig()
               .Map(destination => destination.RequestId, source => source.RequestId)
               .Map(destination => destination.Value, source => source.Value);
        }
    }
}
