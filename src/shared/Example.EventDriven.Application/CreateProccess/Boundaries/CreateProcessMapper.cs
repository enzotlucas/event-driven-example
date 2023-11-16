using Example.EventDriven.Domain.Entitites;
using Example.EventDriven.Domain.Gateways.Event.Events;
using Mapster;

namespace Example.EventDriven.Application.CreateProccess.Boundaries
{
    public static class CreateProcessMapper
    {
        public static void Add()
        {
            TypeAdapterConfig<CreateProccessRequest, ProcessEntity>
                .NewConfig();

            TypeAdapterConfig<ProcessEntity, CreateProcessEvent>
                .NewConfig();
        }
    }
}
