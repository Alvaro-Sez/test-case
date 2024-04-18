using MongoDB.Driver;
using Read.Contracts.Entities;
using Read.Contracts.Repository;
using Read.Data.Models;

namespace Read.Data.Repository;

public class IqBuildingNamesRepository : IIqBuildingNamesRepository 
{
    private readonly IMongoCollection<IqBuildingNamesModel> _iqBuildingNamesCollection;
    public IqBuildingNamesRepository(IMongoDatabase db)
    {
        _iqBuildingNamesCollection =  db.GetCollection<IqBuildingNamesModel>("iq_names");
    }
    public async Task SetAsync(IqName entity)
    {
        await _iqBuildingNamesCollection.InsertOneAsync(Map.ToModel(entity));
    }

    public Task<IqName> GetAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<IqName>> GetAllAsync()
    {
        var emptyFilter = Builders<IqBuildingNamesModel>.Filter.Empty;
        var iqs = await _iqBuildingNamesCollection.FindAsync(emptyFilter);
        return Map.ToDomain(await iqs.ToListAsync());

    }

    public async Task<bool> ExistAsync(IqName entity)
    {
        var result = await _iqBuildingNamesCollection
            .FindAsync(c => string.Equals(entity.BuildingName, c.Name));
        return await result.AnyAsync();
    }
}