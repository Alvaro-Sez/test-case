using Read.Contracts.Entities;

namespace Read.Contracts.Repository;

public interface IBindRequestRepository: IRepository<BindIqRequest>
{
    Task<IEnumerable<BindIqRequest>> GetAllAsync();

    Task DeleteAsync(BindIqRequest request);
}