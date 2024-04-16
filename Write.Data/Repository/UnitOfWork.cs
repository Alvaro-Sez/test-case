
using Api.Commands.Domain.Ports;
using Write.Contacts.Repository;

namespace Write.Data.Repository;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _dbContext;

    public UnitOfWork(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task SaveChangesAsync()
    {
        throw new NotImplementedException();
    }
}