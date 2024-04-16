using Write.Contacts.Entities;
using Write.Contacts.Repository;

namespace Write.Data.Repositories;

public class BindRequestRepository: IBindRequestRepository
{
    public Task<BindIqRequest?> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task AddAsync(BindIqRequest entity)
    {
        throw new NotImplementedException();
    }
}