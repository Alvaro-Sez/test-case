using Read.Contracts.Entities;
using Read.Contracts.Events;
using Read.Contracts.Queries;
using Read.Contracts.Repository;
using Read.Implementation.Dto;
using Shared;

namespace Read.Implementation.Queries.Locks.Handlers;

public class OpenLockQueryHandler: IQueryHandlerT<OpenLockQuery, OpenLockDto>
{
    private readonly IUserAccessRepository _userAccess;
    private readonly IEventRepository _eventRepository;

    public OpenLockQueryHandler(IUserAccessRepository userAccess, IEventRepository eventRepository)
    {
        _userAccess = userAccess;
        _eventRepository = eventRepository;
    }

    public async Task<Result<OpenLockQuery>> HandleAsync(OpenLockDto openLockDto)
    {
        var user = await _userAccess.GetAsync(Guid.Parse(openLockDto.UserId));
        
        if(user is null)
        {
            return Result<OpenLockQuery>.Failure(Errors.IqNotAssigned);
        }
        
        var isAllowed = IsAllowed(user, Guid.Parse(openLockDto.LockId));
        
        try
        {
            await _eventRepository.SetAsync(EventFrom(user.UserId, openLockDto.LockId, isAllowed));
        }
        catch (Exception e)
        {
            //TODO Log
            return Result<OpenLockQuery>.From(new OpenLockQuery { Unlock = isAllowed });
        }
        return Result<OpenLockQuery>.From(new OpenLockQuery { Unlock = isAllowed });
        
    }
    private bool IsAllowed(UserAccess user, Guid lockId)
    {
        return user.Iqs.SelectMany(c => c.Locks.Select(l => l.Id)).Any(c => c == lockId);
    }
    private EventRecord EventFrom(Guid userId, string lockId, bool access)
    {
        return new EventRecord
        {
            LockId = Guid.Parse(lockId),
            UserId = userId,
            Type = access ? EventType.AccessGranted : EventType.AccessDenied,
            IssuedAt = DateTime.Now.ToUniversalTime()
        };

    }
}