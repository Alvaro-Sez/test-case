namespace Read.Contracts.Repository;

public interface IRepository<T>
{
    Task SetAsync(T entity);
    Task<T> GetAsync(string id);
    Task<IEnumerable<T>> GetAllAsync();
}