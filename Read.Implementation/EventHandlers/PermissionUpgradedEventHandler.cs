using DotNetCore.CAP;
using Microsoft.Extensions.Logging;
using Read.Contracts.Entities;
using Read.Contracts.Events;
using Read.Contracts.Repository;
using Write.Contacts.Events;

namespace Read.Implementation.EventHandlers;

public class PermissionUpgradedEventHandler : IEventHandler<PermissionUpgradedEvent>, ICapSubscribe
{
    private readonly IUserAccessRepository _userAccessRepository;
    private readonly IEventRepository _eventRepository;
    private readonly IAccessRequestRepository _accessRequestRepository;
    private readonly ILogger<PermissionUpgradedEventHandler> _logger;

    public PermissionUpgradedEventHandler(IUserAccessRepository userAccessRepository, IEventRepository eventRepository, IAccessRequestRepository accessRequestRepository, ILogger<PermissionUpgradedEventHandler> logger)
    {
        _userAccessRepository = userAccessRepository;
        _eventRepository = eventRepository;
        _accessRequestRepository = accessRequestRepository;
        _logger = logger;
    }

    [CapSubscribe(nameof(PermissionUpgradedEvent))]
    public async Task Handle(PermissionUpgradedEvent @event)
    {
        try
        {
            var user = await _userAccessRepository.GetAsync(@event.UserId);
            if(user is null)
            {
                throw new ApplicationException("Invalid state of data");
            }
            user!.Access = AccessLevel.High;
            await _userAccessRepository.SetAsync(user);
            await _accessRequestRepository.DeleteAsync(new AccessLevelRequest(){UserId = @event.UserId});
            await _eventRepository.SetAsync(new EventRecord
            {
                UserId = user.UserId, 
                IssuedAt = DateTime.Now.ToUniversalTime(), 
                Type = EventType.PermissionUpgraded 
            });

        }
        catch (Exception exception)
        {
            _logger.LogError(exception ,"an Exception ocurred when proccessing the event: {Event} ", @event);
        }
    }
}