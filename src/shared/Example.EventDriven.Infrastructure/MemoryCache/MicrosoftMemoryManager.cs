using Example.EventDriven.Domain.Gateways.MemoryCache;
using Microsoft.Extensions.Caching.Memory;

namespace Example.EventDriven.Infrastructure.MemoryCache
{
    public sealed class MicrosoftMemoryManager : IMemoryCacheManager
    {
        private readonly IMemoryCache _memoryCache;

        public MicrosoftMemoryManager(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public Task CreateOrUpdate(Guid requestId, object data)
        {
            throw new NotImplementedException();
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
