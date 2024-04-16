namespace Api.Commands.Domain.Ports;

public interface IRepository<T>
{
    Task<T?> GetByIdAsync(Guid id);
    Task AddAsync(T entity);
}