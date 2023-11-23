using Example.EventDriven.Application.CreateProccess.Boundaries;

namespace Example.EventDriven.Application.CreateProccess
{
    public interface ICreateProcess
    {
        Task<CreateProcessResponse> Create(CreateProcessRequest request, CancellationToken cancellationToken);
    }
}
