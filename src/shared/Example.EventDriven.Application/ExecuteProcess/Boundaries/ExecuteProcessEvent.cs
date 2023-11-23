using Example.EventDriven.Domain.Gateways.Event;

namespace Example.EventDriven.Application.ExecuteProcess.Boundaries
{
    public class ExecuteProcessEvent : GenericEvent<ExecuteProcessRequest>
    {
        public override string OperationName => nameof(ExecuteProcessEvent);
    }
}
