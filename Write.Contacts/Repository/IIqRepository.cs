using Api.Commands.Domain.Ports;
using Write.Contacts.Entities;

namespace Write.Contacts.Repository;

public interface IIqRepository : IRepository<Iq>
{
    Task<bool> ExistsAsync(string buildingName);
    Task<IEnumerable<Iq>> GetAllAsync();
    Task<Iq?> GetByBuildingNameAsync(string buildingName);
}