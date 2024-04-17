using Microsoft.EntityFrameworkCore;
using Write.Contacts.Entities;
using Write.Contacts.Repository;
using Write.Data.EF;

namespace Write.Data.Repository;

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

    public async Task AddAsync(Iq entity)
    {
        await _context.Iqs.AddAsync(entity);
    }


    public Task<bool> ExistsAsync(string buildingName)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Iq>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<Iq?> GetByBuildingNameAsync(string buildingName)
    {
        return await _context.Iqs
            .FirstOrDefaultAsync(c => string.Equals(buildingName, c.BuildingName));
    }
}