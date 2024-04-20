namespace Read.Contracts.Repository;

public interface IRepository<T>
{
    Task SetAsync(T entity);
}