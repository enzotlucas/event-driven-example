using Example.EventDriven.Application.CreateProcess.Boundaries;
using Mapster;
using System.Diagnostics.CodeAnalysis;

namespace Example.EventDriven.Application.SendEvent.Boundaries
{
    [ExcludeFromCodeCoverage]
    public static class SendRequestMapper
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
