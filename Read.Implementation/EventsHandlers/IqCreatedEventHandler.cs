using DotNetCore.CAP;
using Microsoft.Extensions.Logging;
using Read.Contracts.Entities;
using Read.Contracts.Events;
using Read.Contracts.Repository;
using Write.Contacts.Events;

namespace Read.Implementation.EventsHandlers;

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
        var newIq = new Iq
        {
            Id = @event.IqId,
            BuildingName = @event.BuildingName,
            Locks = @event.Locks.Select(c=>new Lock
            {
                Id = c,
                Access = AccessLevel.Low
            }).ToList()
        };
        await _iqRepository.SetAsync(newIq);//TODO 
        _logger.LogInformation("Iq Created with name:{BuildingName}",@event.BuildingName);
    }
}