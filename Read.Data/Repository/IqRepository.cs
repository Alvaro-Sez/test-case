using Read.Contracts.Entities;
using Read.Contracts.Repository;

namespace Read.Data.Repository;

public class IqRepository : IIqRepository
{
    public Task SetAsync(Iq entity)
    {
        throw new NotImplementedException();
    }
    public Task<string> GetByBuildingNameAsync(string buildingName)
    {
        throw new NotImplementedException();
    }
    public Task<Iq> GetAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Iq>> GetAllAsync()
    {
        throw new NotImplementedException();
    }
}