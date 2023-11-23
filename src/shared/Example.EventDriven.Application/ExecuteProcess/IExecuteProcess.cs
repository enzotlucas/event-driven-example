using Example.EventDriven.Application.ExecuteProcess.Boundaries;

namespace Example.EventDriven.Application.ExecuteProcess
{
    public interface IExecuteProcess
    {
        Task Execute(ExecuteProcessRequest request, CancellationToken cancellationToken);
    }
}
