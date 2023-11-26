using Example.EventDriven.Domain.Entitites;
using Example.EventDriven.Domain.Gateways.Logger;
using Example.EventDriven.Domain.Repositories;
using Example.EventDriven.Infrastructure.Database.Core;
using Microsoft.EntityFrameworkCore;

namespace Example.EventDriven.Infrastructure.Database.Repositories
{
    public sealed class ProcessRepository : IProcessRepository
    {
        private readonly SqlServerContext _context;
        private readonly ILoggerManager _logger;

        public ProcessRepository(SqlServerContext context, ILoggerManager logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<(bool Success, ProcessEntity Entity)> CreateAsync(ProcessEntity entity, CancellationToken cancellationToken)
        {
            try
            {
                await _context.Processes.AddAsync(entity, cancellationToken);

                var success = await _context.SaveChangesAsync() > 0;

                return (success, entity);
            }
            catch (Exception ex)
            {
                _logger.LogException("Unexpected error creating process", LoggerManagerSeverity.CRITICAL, ex);
                return (false, entity);
            }
        }

        public async Task<ProcessEntity> GetByNameAsync(string name, CancellationToken cancellationToken)
        {
            var entity = await _context.Processes.FirstOrDefaultAsync(p => p.Name == name, cancellationToken);

            return entity ?? new ProcessEntity();
        }

        public async Task UpdateAsync(ProcessEntity existingProcess, CancellationToken cancellationToken)
        {
            try
            {
                _context.Processes.Update(existingProcess);

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogException("Unexpected error updating process", LoggerManagerSeverity.CRITICAL, ex);
            }

        }
    }
}
