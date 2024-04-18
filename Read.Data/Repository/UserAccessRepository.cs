using MongoDB.Driver;
using Read.Contracts.Entities;
using Read.Contracts.Repository;

namespace Read.Data.Repository;

public class UserAccessRepository : IUserAccessRepository
{
    private readonly IMongoCollection<UserAccess> _accessCollection;
    public UserAccessRepository(IMongoDatabase db)
    {
        _accessCollection = db.GetCollection<UserAccess>("user_access");
    }
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