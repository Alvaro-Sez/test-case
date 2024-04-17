using Read.Contracts.Entities;
using Read.Contracts.Repository;

namespace Read.Data.Repository;

public class AccessRepository : IAccessRepository
{
    public Task SetAsync(Access entity)
    {
        throw new NotImplementedException();
    }

    public Task<Access> GetAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Access>> GetAllAsync()
    {
        throw new NotImplementedException();
    }
}