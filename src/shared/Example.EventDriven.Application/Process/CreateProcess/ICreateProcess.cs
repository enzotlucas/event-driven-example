using Example.EventDriven.Application.CreateProcess.Boundaries;

namespace Example.EventDriven.Application.CreateProcess
{
    public interface ICreateProcess
    {
        Task<CreateProcessResponse> Create(CreateProcessRequest request, CancellationToken cancellationToken);
    }
}
