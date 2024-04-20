using Microsoft.EntityFrameworkCore;
using Write.Contacts.Entities;
using Write.Contacts.Repository;
using Write.Data.EF;

namespace Write.Data.Repository;

public class LockRepository:  ILockRepository
{
    private readonly DbSet<Lock> _dbSet;
    public LockRepository(ApplicationDbContext dbContext)
    {
        _dbSet = dbContext.Locks;
    }
    public async Task<Lock?> GetByIdAsync(Guid id)
    {
        return await _dbSet
            .Include(c => c.Iq)
            .FirstOrDefaultAsync(c => c.Id == id);
    }
}