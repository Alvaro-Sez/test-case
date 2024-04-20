using Microsoft.EntityFrameworkCore;
using Write.Contacts.Entities;
using Write.Contacts.Repository;
using Write.Data.EF;

namespace Write.Data.Repository;

public class IqRepository : IIqRepository 
{
    private readonly DbSet<Iq> _dbSet;
    public IqRepository(ApplicationDbContext context)
    {
        _dbSet = context.Iqs;
    }
    public async Task AddAsync(Iq entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public async Task<Iq?> GetByBuildingNameAsync(string buildingName)
    {
        return await _dbSet 
            .Include(c=>c.Locks)
            .FirstOrDefaultAsync(c => string.Equals(buildingName, c.BuildingName));
    }
}