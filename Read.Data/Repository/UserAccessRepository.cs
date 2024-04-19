using MongoDB.Driver;
using Read.Contracts.Entities;
using Read.Contracts.Repository;
using Read.Data.Models;
using Read.Data.Models.Utils;

namespace Read.Data.Repository;

internal class UserAccessRepository : IUserAccessRepository
{
    private readonly IMongoCollection<UserAccessModel> _accessCollection;
    private readonly IMongoCollection<IqModel> _iqCollection;
    public UserAccessRepository(IMongoDatabase db)
    {
        _iqCollection =db.GetCollection<IqModel>("user_access");
        _accessCollection = db.GetCollection<UserAccessModel>("user_access");
    }
    public async Task SetAsync(UserAccess entity)
    {
        await _accessCollection.InsertOneAsync(Map.ToModel(entity));
    }

    public async Task<UserAccess?> GetAsync(Guid id)
    {
        var result = await _accessCollection
            .FindAsync(c=> c.Id == id);
        return  Map.ToDomain(await result.FirstOrDefaultAsync());
    }

    public async Task<bool> ExistAsync(UserAccess entity)
    {
        var result = await _accessCollection
            .FindAsync(c=> c.Id == entity.UserId);
        return await result.AnyAsync();
    }

}