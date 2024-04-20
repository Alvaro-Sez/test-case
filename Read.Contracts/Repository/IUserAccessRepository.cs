using Read.Contracts.Entities;

namespace Read.Contracts.Repository;

public interface IUserAccessRepository: IRepository<UserAccess>
{
    Task<UserAccess?> GetAsync(Guid id);
    Task<bool> ExistAsync(UserAccess entity);
}