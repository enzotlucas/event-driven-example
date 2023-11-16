namespace Example.EventDriven.Domain.Gateways.MemoryCache
{
    public interface IMemoryCacheManager
    {
        Task CreateOrUpdate(Guid requestId, object data);
        Task<T> GetAsync<T>(Guid requestId, CancellationToken cancellationToken);
    }
}
