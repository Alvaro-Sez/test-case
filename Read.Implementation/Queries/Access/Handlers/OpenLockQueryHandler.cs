using Read.Contracts.Entities;
using Read.Contracts.Queries;
using Read.Contracts.Repository;
using Read.Implementation.Dto;
using Shared;

namespace Read.Implementation.Queries.Access.Handlers;

public class OpenLockQueryHandler: IQueryHandlerT<OpenLockQuery, OpenLockDto>
{
    private readonly IUserAccessRepository _userAccess;

    public OpenLockQueryHandler(IUserAccessRepository userAccess)
    {
        _userAccess = userAccess;
    }

    public async Task<Result<OpenLockQuery>> HandleAsync( OpenLockDto openLockDto)
    {
        var user = await _userAccess.GetAsync(Guid.Parse(openLockDto.UserId));
        
        if(user is null)
        {
            return Result<OpenLockQuery>.Failure(Errors.IqNotAssigned);
        }

        return Result<OpenLockQuery>.From(
            new OpenLockQuery { Unlock = IsAllowed(user, Guid.Parse(openLockDto.LockId)) 
            });
    }
    private bool IsAllowed(UserAccess user, Guid lockId)
    {
        return user.Iqs.SelectMany(c => c.Locks.Select(l => l.Id)).Any(c => c == lockId);
    }
}