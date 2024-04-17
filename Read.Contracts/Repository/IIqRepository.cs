using Read.Contracts.Entities;

namespace Read.Contracts.Repository;

public interface IIqRepository : IRepository<Iq>
{
    Task<IEnumerable<Iq>> GetAllAsync();
    
    Task<string> GetByBuildingNameAsync(string buildingName);
}