using Microsoft.EntityFrameworkCore;
using Write.Contacts.Entities;
using Write.Contacts.Repository;
using Write.Data.EF;

namespace Write.Data.Repository;

public class UserRepository : IUserRepository
{
    private readonly DbSet<User> _dbSet;
    public UserRepository(ApplicationDbContext dbContext)
    {
        _dbSet = dbContext.Users;
    }

    public async Task<User?> GetByIdAsync(Guid id)
    {
        return await _dbSet.Include(c=>c.IqAssigned).FirstOrDefaultAsync(c=>c.Id==id);
    }

    public async Task AddAsync(User entity)
    {
        await  _dbSet.AddAsync(entity);
    }
}