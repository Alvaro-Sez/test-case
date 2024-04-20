using MongoDB.Driver;
using Read.Contracts.Entities;
using Read.Contracts.Repository;
using Read.Data.Models;
using Read.Data.Models.Utils;

namespace Read.Data.Repository;

public class AccessRequestRepository : IAccessRequestRepository
{
    private readonly IMongoCollection<AccessRequestModel> _collection;
    
    public AccessRequestRepository(IMongoDatabase db)
    {
        _collection =  db.GetCollection<AccessRequestModel>("access_request");
    }
    public async Task SetAsync(AccessLevelRequest entity)
    {
        await _collection.InsertOneAsync(Map.ToModel(entity));
    }

    public async Task<bool> ExistAsync(AccessLevelRequest levelRequest)
    {
        var result = await _collection
            .FindAsync(c =>levelRequest.UserId == c.UserId && levelRequest.IqId == c.IqId);
        return await result.AnyAsync();
    }

    public async Task<IEnumerable<AccessLevelRequest>> GetAllAsync()
    {
        var emptyFilter = Builders<AccessRequestModel>.Filter.Empty;
        var requests = await _collection.FindAsync(emptyFilter);
        return Map.ToDomain(await requests.ToListAsync());
    }

    public async Task DeleteAsync(AccessLevelRequest levelRequest)
    {
        await _collection.DeleteOneAsync(c=>c.UserId == levelRequest.UserId);
    }
}