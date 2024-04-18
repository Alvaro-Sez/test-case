using DotNetCore.CAP;
using Microsoft.Extensions.Logging;
using Read.Contracts.Events;
using Write.Contacts.Events;

namespace Read.Implementation.EventsHandlers;

public class IqAssignedEventHandler: IEventHandler<IqAssignedEvent> , ICapSubscribe
{
    private readonly ILogger<IqAssignedEventHandler> _logger;

    public IqAssignedEventHandler(ILogger<IqAssignedEventHandler> logger)
    {
        _logger = logger;
    }

    [CapSubscribe(nameof(IqAssignedEvent))]
    public Task Handle(IqAssignedEvent @event)
    {
        throw new NotImplementedException();
    }
}