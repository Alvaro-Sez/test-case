using Write.Contacts.Entities;
using Write.Contacts.Repository;

namespace Write.Data.Repositories;

public class IqRepository : IIqRepository 
{
    private readonly ApplicationDbContext _context;
    public IqRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public Task<Iq?> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task AddAsync(Iq entity)
    {
        throw new NotImplementedException();
    }


    public Task<bool> ExistsAsync(string buildingName)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Iq>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Iq?> GetByBuildingNameAsync(string buildingName)
    {
        throw new NotImplementedException();
    }
}