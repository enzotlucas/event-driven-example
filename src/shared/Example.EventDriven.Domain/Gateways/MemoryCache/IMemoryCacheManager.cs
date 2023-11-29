namespace Example.EventDriven.Domain.Gateways.MemoryCache
{
    public interface IMemoryCacheManager
    {
        Task CreateOrUpdate(Guid requestId, object data, CancellationToken cancellationToken);
        Task<bool> ExistsAsync(Guid requestId, CancellationToken cancellationToken);
        Task<T> GetAsync<T>(Guid requestId, CancellationToken cancellationToken);
    }
}
