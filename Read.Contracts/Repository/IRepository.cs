namespace Read.Contracts.Repository;

public interface IRepository<T>
{
    Task SetAsync(T entity);
    Task<T?> GetAsync(Guid id);
    Task<bool> ExistAsync(T entity);
}