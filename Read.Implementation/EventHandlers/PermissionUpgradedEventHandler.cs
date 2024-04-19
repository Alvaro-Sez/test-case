using DotNetCore.CAP;
using Read.Contracts.Entities;
using Read.Contracts.Events;
using Read.Contracts.Repository;
using Write.Contacts.Events;

namespace Read.Implementation.EventHandlers;

public class PermissionUpgradedEventHandler : IEventHandler<PermissionUpgradedEvent>
{
    private readonly IUserAccessRepository _userAccessRepository;
    private readonly IEventRepository _eventRepository;

    public PermissionUpgradedEventHandler(IUserAccessRepository userAccessRepository, IEventRepository eventRepository)
    {
        _userAccessRepository = userAccessRepository;
        _eventRepository = eventRepository;
    }

    [CapSubscribe(nameof(PermissionUpgradedEvent))]
    public async Task Handle(PermissionUpgradedEvent @event)
    {
        var user = await _userAccessRepository.GetAsync(@event.UserId);
        user!.Access = AccessLevel.High;
        await _userAccessRepository.SetAsync(user);
        await _eventRepository.SetAsync(new EventRecord
        {
            UserId = user.UserId, 
            IssuedAt = DateTime.Now.ToUniversalTime(), 
            Type = EventType.PermissionUpgraded 
        });
    }
}