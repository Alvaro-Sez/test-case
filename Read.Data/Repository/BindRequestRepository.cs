using Read.Contracts.Entities;
using Read.Contracts.Repository;

namespace Read.Data.Repository;

public class BindRequestRepository: IBindRequestRepository
{
    public Task SetAsync(BindIqRequest entity)
    {
        throw new NotImplementedException();
    }

    public Task<BindIqRequest> GetAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<BindIqRequest>> GetAllAsyncById(string id)
    {
        throw new NotImplementedException();
    }
}