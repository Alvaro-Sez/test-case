using Microsoft.EntityFrameworkCore;
using Write.Contacts.Entities;
using Write.Contacts.Repository;
using Write.Data.EF;

namespace Write.Data.Repository;

public class LockRepository:  ILockRepository
{
    //TODO not inject the context , only the dbset
    private readonly ApplicationDbContext _dbContext;

    public LockRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<Lock?> GetByIdAsync(Guid id)
    {
        return await _dbContext.Locks
            .Include(c => c.Iq)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public Task AddAsync(Lock entity)
    {
        throw new NotImplementedException();
    }
}