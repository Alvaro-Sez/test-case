using MongoDB.Driver;
using Read.Contracts.Entities;
using Read.Contracts.Repository;
using Read.Data.Models;
using Read.Data.Models.Utils;

namespace Read.Data.Repository;

public class IqRepository: IIqRepository
{
    private readonly IMongoCollection<IqModel> _collection;
    
    public IqRepository(IMongoDatabase db)
    {
        _collection = db.GetCollection<IqModel>("iqs");
    }
    public async Task SetAsync(Iq entity)
    {
        await _collection.InsertOneAsync(Map.ToModel(entity));
    }

    public Task<Iq?> GetAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ExistAsync(Iq entity)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> ExistAsync(string buildingName)
    {
        var iqs =await _collection.FindAsync(c=>string.Equals(c.BuildingName, buildingName));
        return await iqs.AnyAsync();
    }
    public async Task<IEnumerable<Iq>> GetAllAsync()
    {
        var emptyFilter = Builders<IqModel>.Filter.Empty;
        var iqs = await _collection.FindAsync(emptyFilter);
        return Map.ToDomain(await iqs.ToListAsync());
    }
    
    public async Task<IEnumerable<Iq>> GetAllByIdAsync(IEnumerable<Guid> ids)
    {
        var iqList = new List<Iq>();
        foreach (var iq in ids)
        {
            var iqs = await _collection.FindAsync(c => c.Id == iq);
            var iqModelList = await iqs.ToListAsync();
            iqList.AddRange(Map.ToDomain(iqModelList));
        }

        return iqList;
    }
}