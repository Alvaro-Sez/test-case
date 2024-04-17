using Read.Contracts.Entities;
using Read.Contracts.Repository;

namespace Read.Data.Repository;

public class UserAccessRepository : IUserAccessRepository
{
    public Task SetAsync(UserAccess entity)
    {
        throw new NotImplementedException();
    }

    public Task<UserAccess> GetAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<UserAccess> GetAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<UserAccess>> GetAllAsync()
    {
        throw new NotImplementedException();
    }
}