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
    private readonly IIqRepository _iqRepository;

    public IqAssignedEventHandler(ILogger<IqAssignedEventHandler> logger, IUserAccessRepository userAccess, IBindRequestRepository bindRequest, IIqRepository iqRepository)
    {
        _logger = logger;
        _userAccess = userAccess;
        _bindRequest = bindRequest;
        _iqRepository = iqRepository;
    }

    [CapSubscribe(nameof(IqAssignedEvent))]
    public async Task Handle(IqAssignedEvent @event)
    { 
        var user = await _userAccess.GetAsync(@event.UserId) ?? new UserAccess { UserId = @event.UserId };
        var iq = await _iqRepository.GetAsync(@event.IqId) ?? CreateIqFromEvent(@event);
        
        user.Iqs.Add(@event.IqId);
        
        await _userAccess.SetAsync(user);
        await _iqRepository.SetAsync(iq);
        await _bindRequest.DeleteAsync(new BindIqRequest(@event.UserId, @event.BuildingName));
    }
    private static Iq CreateIqFromEvent(IqAssignedEvent @event)
    {
        return new Iq {
            Id = @event.IqId, 
            BuildingName = @event.BuildingName, 
            Locks = @event.LockIds
                .Select(c=> new Lock 
                { 
                    Id = c, 
                    Access = AccessLevel.Low
                }).ToList()};
    }
}