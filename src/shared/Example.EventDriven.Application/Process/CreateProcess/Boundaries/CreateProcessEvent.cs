using Example.EventDriven.Domain.Gateways.Event;
using System.Diagnostics.CodeAnalysis;

namespace Example.EventDriven.Application.CreateProcess.Boundaries
{
    [ExcludeFromCodeCoverage]
    public sealed class CreateProcessEvent : GenericEvent<CreateProcessRequest>
    {
        public override string OperationName => nameof(CreateProcessEvent);
    }
}
