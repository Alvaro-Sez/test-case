using DotNetCore.CAP;
using Microsoft.Extensions.Logging;
using Read.Contracts.Entities;
using Read.Contracts.Events;
using Read.Contracts.Repository;
using Write.Contacts.Events;

namespace Read.Implementation.EventsHandlers;

public class IqAssignedEventHandler: IEventHandler<IqAssignedEvent> , ICapSubscribe
{
    private readonly ILogger<IqAssignedEventHandler> _logger;
    private readonly IUserAccessRepository _userAccess;
    private readonly IBindRequestRepository _bindRequest;

    public IqAssignedEventHandler(ILogger<IqAssignedEventHandler> logger, IUserAccessRepository userAccess, IBindRequestRepository bindRequest)
    {
        _logger = logger;
        _userAccess = userAccess;
        _bindRequest = bindRequest;
    }

    [CapSubscribe(nameof(IqAssignedEvent))]
    public async Task Handle(IqAssignedEvent @event)
    { 
        var user = await _userAccess.GetAsync(@event.UserId) ?? new UserAccess { UserId = @event.UserId };
        user.Iqs.Add(CreateIqFromEvent(@event));
        
        await _userAccess.SetAsync(user);
        await _bindRequest.DeleteAsync(new BindIqRequest(@event.UserId, @event.BuildingName));
    }
    private static Iq CreateIqFromEvent(IqAssignedEvent @event)
    {
        return new Iq
        {
            Id = @event.IqId,
            Locks = @event.LockIds.Select(c=> new Lock{Id = c}).ToList()
        };
    }
}