using Example.EventDriven.Domain.Entitites;
using Example.EventDriven.Domain.Repositories;

namespace Example.EventDriven.Infrastructure.Database.Repositories
{
    public class ProcessRepository : IProcessRepository
    {
        public Task<(bool Success, ProcessEntity Entity)> CreateAsync(ProcessEntity entity, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<ProcessEntity> GetByNameAsync(string name, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
