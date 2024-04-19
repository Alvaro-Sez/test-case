using Read.Contracts.Events;

namespace Read.Contracts.Repository;

public interface IEventRepository
{
    Task<IEnumerable<EventRecord>> GetByUserId(Guid id);
    Task SetAsync(EventRecord entity);
}