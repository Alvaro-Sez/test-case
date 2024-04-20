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
    private readonly IIqRepository _iqRepository;
    private readonly IEventRepository _eventRepository;

    public OpenLockQueryHandler(IUserAccessRepository userAccess, IEventRepository eventRepository, IIqRepository iqRepository)
    {
        _userAccess = userAccess;
        _eventRepository = eventRepository;
        _iqRepository = iqRepository;
    }

    public async Task<Result<OpenLockQuery>> HandleAsync(OpenLockDto openLockDto)
    {
        var user = await _userAccess.GetAsync(openLockDto.UserId!);
        
        if(user is null)
        {
            return Result<OpenLockQuery>.Failure(Errors.IqNotAssigned);
        }
        
        var isAllowed = await IsAllowed(user,  openLockDto.LockId);
        
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
    private async Task<bool> IsAllowed(UserAccess user, Guid lockId)
    {
        var userIqs = await _iqRepository.GetAllByIdAsync(user.Iqs);
        var @lock = userIqs.SelectMany(c => c.Locks).FirstOrDefault(c=>c.Id == lockId);
        return @lock is not null && user.Access == @lock.Access;
    }
    private EventRecord EventFrom(Guid userId, Guid lockId, bool access)
    {
        return new EventRecord
        {
            LockId = lockId,
            UserId = userId,
            Type = access ? EventType.AccessGranted : EventType.AccessDenied,
            IssuedAt = DateTime.Now.ToUniversalTime()
        };

    }
}