using Write.Contacts.Entities;

namespace Write.Contacts.Repository;

public interface IIqRepository 
{
    Task<Iq?> GetByBuildingNameAsync(string buildingName);
    
    Task AddAsync(Iq entity);
}