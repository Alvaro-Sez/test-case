using Read.Contracts.Entities;

namespace Read.Contracts.Repository;

public interface IIqRepository : IRepository<Iq>
{
    Task<bool> ExistAsync(string buildingName);
    Task<IEnumerable<Iq>> GetAllAsync();
    Task<IEnumerable<Iq>> GetAllByIdAsync(IEnumerable<Guid> ids);
    Task<Iq?> GetAsync(Guid id);
}
