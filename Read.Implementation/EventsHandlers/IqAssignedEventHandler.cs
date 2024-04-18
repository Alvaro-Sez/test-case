using DotNetCore.CAP;
using Microsoft.Extensions.Logging;
using Read.Contracts.Events;
using Read.Contracts.Repository;
using Write.Contacts.Events;

namespace Read.Implementation.EventsHandlers;

public class IqAssignedEventHandler: IEventHandler<IqAssignedEvent> , ICapSubscribe
{
    private readonly ILogger<IqAssignedEventHandler> _logger;
    private readonly IUserAccessRepository _userAccess;

    public IqAssignedEventHandler(ILogger<IqAssignedEventHandler> logger, IUserAccessRepository userAccess)
    {
        _logger = logger;
        _userAccess = userAccess;
    }

    [CapSubscribe(nameof(IqAssignedEvent))]
    public Task Handle(IqAssignedEvent @event)
    {//TODO work in progress
        if()
        
    }
}