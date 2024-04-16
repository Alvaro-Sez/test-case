using DotNetCore.CAP;
using Microsoft.Extensions.Logging;
using Read.Contracts.Events;
using Read.Contracts.Repository;
using Write.Contacts.Events;

namespace Read.Implementation.Events;

public class HandleIqCreated:  IEventHandler<IqCreated> ,ICapSubscribe
{
    private readonly ILogger<HandleIqCreated> _logger;
    private readonly IIqRepository _iqRepository;

    public HandleIqCreated(ILogger<HandleIqCreated> logger, IIqRepository iqRepository)
    {
        _logger = logger;
        _iqRepository = iqRepository;
    }
    
    [CapSubscribe(nameof(IqCreated))]
    public void Handle(IqCreated iqCreated)
    {
        _logger.LogInformation($"Iq Created");
    }
}