
using Api.Commands.Domain.Ports;

namespace Write.Data.Repositories;

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