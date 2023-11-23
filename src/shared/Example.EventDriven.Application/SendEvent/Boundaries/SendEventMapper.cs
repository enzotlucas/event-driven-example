using Example.EventDriven.Application.CreateProccess.Boundaries;
using Mapster;

namespace Example.EventDriven.Application.SendEvent.Boundaries
{
    public static class SendEventMapper
    {
        public static void Add()
        {
            TypeAdapterConfig<SendEventRequest, CreateProcessEvent>
                .NewConfig()
                .Map(destination => destination.Value, source => source.Value)
                .Map(destination => destination.Timestamp, source => DateTime.UtcNow)
                .Map(destination => destination.RequestId, source => Guid.Empty);
        }
    }
}
