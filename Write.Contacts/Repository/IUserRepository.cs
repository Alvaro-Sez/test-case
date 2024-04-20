using Write.Contacts.Entities;

namespace Write.Contacts.Repository;

public interface IUserRepository 
{
    Task<User?> GetByIdAsync(Guid id);
    Task AddAsync(User entity);
}