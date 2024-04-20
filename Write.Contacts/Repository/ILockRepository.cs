using Write.Contacts.Entities;

namespace Write.Contacts.Repository;

public interface ILockRepository
{
    Task<Lock?> GetByIdAsync(Guid id);
}