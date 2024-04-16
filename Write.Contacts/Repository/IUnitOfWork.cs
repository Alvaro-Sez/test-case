namespace Api.Commands.Domain.Ports;

public interface IUnitOfWork
{
    Task SaveChangesAsync();
}