using Microsoft.EntityFrameworkCore;
using Write.Contacts.Entities;
using Write.Contacts.Repository;
using Write.Data.EF;

namespace Write.Data.Repository;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _dbContext;

    public UserRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<User?> GetByIdAsync(Guid id)
    {
        return await _dbContext.Users.Include(c=>c.IqAssigned).FirstOrDefaultAsync(c=>c.Id==id);
    }

    public async Task AddAsync(User entity)
    {
        await _dbContext.Users.AddAsync(entity);
    }
}