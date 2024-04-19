using Read.Contracts.Entities;

namespace Read.Contracts.Repository;

public interface IAccessRequestRepository:IRepository<AccessLevelRequest>
{
    Task<IEnumerable<AccessLevelRequest>> GetAllAsync();
    Task DeleteAsync(AccessLevelRequest levelRequest);
}