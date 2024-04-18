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
    private readonly IIqBuildingNamesRepository _iqRepository;

    public IqCreatedEventHandler(ILogger<IqCreatedEventHandler> logger, IIqBuildingNamesRepository iqRepository)
    {
        _logger = logger;
        _iqRepository = iqRepository;
    }
    
    [CapSubscribe(nameof(IqCreatedEvent))]
    public async Task Handle(IqCreatedEvent @event)
    {
        try
        {
            await _iqRepository.SetAsync(new IqName { BuildingName = @event.BuildingName });
            _logger.LogInformation("Iq Created with name:{BuildingName}",@event.BuildingName);
        }
        catch (Exception e)
        {
            _logger.LogError("something went wrong: {e} ", e);
            Console.WriteLine(e);
            throw;
        }
    }
}