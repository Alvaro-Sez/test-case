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

    public Task<string> GetByBuildingNameAsync(string buildingName)
    {
        throw new NotImplementedException();
    }
    public Task<IqName> GetAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<IqName>> GetAllAsync()
    {
        throw new NotImplementedException();
    }
}