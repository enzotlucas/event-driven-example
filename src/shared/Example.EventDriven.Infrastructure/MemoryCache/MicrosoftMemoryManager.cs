using Example.EventDriven.Domain.Gateways.MemoryCache;
using Microsoft.Extensions.Caching.Memory;
using System.Diagnostics.CodeAnalysis;

namespace Example.EventDriven.Infrastructure.MemoryCache
{
    [ExcludeFromCodeCoverage]
    public sealed class MicrosoftMemoryManager : IMemoryCacheManager
    {
        private readonly IMemoryCache _memoryCache;
        private readonly MemoryCacheEntryOptions _persistenceOptions;

        public MicrosoftMemoryManager(IMemoryCache memoryCache, MemoryCacheEntryOptions persistenceOptions)
        {
            _memoryCache = memoryCache;
            _persistenceOptions = persistenceOptions;
        }

        public async Task CreateOrUpdate(Guid requestId, object data, CancellationToken cancellationToken)
        {
            await Task.Run(() => _memoryCache.Set(requestId, data, _persistenceOptions), cancellationToken);
        }

        public async Task<bool> ExistsAsync(Guid requestId, CancellationToken cancellationToken)
        {
            return await Task.Run(() => _memoryCache.TryGetValue<object>(requestId, out _), cancellationToken);
        }

        public async Task<T> GetAsync<T>(Guid requestId, CancellationToken cancellationToken)
        {
            return await Task.Run(() =>
            {
                if (_memoryCache.TryGetValue<T>(requestId, out var value))
                    return value;

                return default;
            },
            cancellationToken);
        }
    }
}
