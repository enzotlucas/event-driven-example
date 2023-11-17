using Example.EventDriven.Application.SendEvent.Boundaries;
using Mapster;

namespace Example.EventDriven.Application.CreateProccess.Boundaries
{
    public static class CreateProcessMapper
    {
        public static void Add()
        {
            TypeAdapterConfig<CreateProccessRequest, SendEventRequest>
                .NewConfig()
                .Map(destination => destination.OperationName, source => nameof(CreateProccessRequest))
                .Map(destination => destination.Value, source => source);
        }
    }
}
