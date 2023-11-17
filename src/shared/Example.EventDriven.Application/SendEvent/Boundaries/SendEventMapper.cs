using Example.EventDriven.Domain.Gateways.Event;
using Mapster;

namespace Example.EventDriven.Application.SendEvent.Boundaries
{
    public static class SendEventMapper
    {
        public static void Add()
        {
            TypeAdapterConfig<SendEventRequest, GenericEvent>
                .NewConfig()
                .Map(destination => destination.OperationName, source => source.OperationName)
                .Map(destination => destination.Value, source => source.Value);
        }
    }
}
