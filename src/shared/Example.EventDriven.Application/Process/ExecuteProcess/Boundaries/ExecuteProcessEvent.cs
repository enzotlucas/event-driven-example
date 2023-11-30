using Example.EventDriven.Domain.Gateways.Event;
using System.Diagnostics.CodeAnalysis;

namespace Example.EventDriven.Application.ExecuteProcess.Boundaries
{
    [ExcludeFromCodeCoverage]
    public sealed class ExecuteProcessEvent : BaseEvent<ExecuteProcessRequest>
    {
        public override string OperationName => nameof(ExecuteProcessEvent);
    }
}
