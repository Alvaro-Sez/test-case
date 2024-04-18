using Read.Contracts.Entities;

namespace Read.Contracts.Repository;

public interface IIqBuildingNamesRepository: IRepository<IqName>
{
    Task<IEnumerable<IqName>> GetAllAsync();
    
    Task<string> GetByBuildingNameAsync(string buildingName);
}