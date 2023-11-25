using Example.EventDriven.Domain.Entitites;

namespace Example.EventDriven.Domain.Repositories
{
    public interface IProcessRepository
    {
        Task<ProcessEntity> GetByNameAsync(string name, CancellationToken cancellationToken);
        Task<(bool Success, ProcessEntity Entity)> CreateAsync(ProcessEntity entity, CancellationToken cancellationToken);
    }
}
