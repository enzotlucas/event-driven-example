using Example.EventDriven.Application.CreateProccess.Boundaries;

namespace Example.EventDriven.Application.CreateProccess
{
    public interface ICreateProcess
    {
        Task<CreateProccessResponse> Create(CreateProccessRequest request, CancellationToken cancellationToken);
    }
}
