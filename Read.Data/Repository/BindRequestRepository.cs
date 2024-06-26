using MongoDB.Driver;
using Read.Contracts.Entities;
using Read.Contracts.Repository;
using Read.Data.Models;
using Read.Data.Models.Utils;

namespace Read.Data.Repository;

public class BindRequestRepository: IBindRequestRepository
{
    private readonly IMongoCollection<BindRequestModel> _collection;
    public BindRequestRepository(IMongoDatabase db)
    {
        _collection =  db.GetCollection<BindRequestModel>("bind_request");
    }
    public async Task SetAsync(BindIqRequest entity)
    {
        await _collection.InsertOneAsync(Map.ToModel(entity));
    }

    public async Task<bool> ExistAsync(BindIqRequest request)
    {
        var result = await _collection
            .FindAsync(c =>string.Equals(request.IqBuildingName, c.BuildingName) && request.AuthorId == c.UserRequestingAccessId);
        return await result.AnyAsync();
    }

    public async Task DeleteAsync(BindIqRequest request)
    {
        await _collection.DeleteOneAsync(c =>string.Equals(request.IqBuildingName, c.BuildingName) && request.AuthorId == c.UserRequestingAccessId);
    }

    public async Task<IEnumerable<BindIqRequest>> GetAllAsync()
    {
        var emptyFilter = Builders<BindRequestModel>.Filter.Empty;
        var requests = await _collection.FindAsync(emptyFilter);
        return Map.ToDomain(await requests.ToListAsync());
    }
}