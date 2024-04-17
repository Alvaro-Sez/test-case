using DotNetCore.CAP;
using Microsoft.Extensions.Logging;
using Read.Contracts.Entities;
using Read.Contracts.Events;
using Read.Contracts.Repository;
using Write.Contacts.Events;

namespace Read.Implementation.Events;

public class IqCreatedEventHandler:  IEventHandler<IqCreatedEvent> ,ICapSubscribe
{
    private readonly ILogger<IqCreatedEventHandler> _logger;
    private readonly IIqRepository _iqRepository;

    public IqCreatedEventHandler(ILogger<IqCreatedEventHandler> logger, IIqRepository iqRepository)
    {
        _logger = logger;
        _iqRepository = iqRepository;
    }
    
    [CapSubscribe(nameof(IqCreatedEvent))]
    public async Task Handle(IqCreatedEvent @event)
    {
        _logger.LogInformation($"Iq Created");
        await _iqRepository.SetAsync(new Iq(@event.BuildingName));
    }
}