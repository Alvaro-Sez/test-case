using DotNetCore.CAP;
using Microsoft.Extensions.Logging;
using Read.Contracts.Events;
using Write.Contacts.Events;

namespace Read.Implementation.Events;

public class HandleIqCreated:IEventHandler<IqCreated>, ICapSubscribe
{
    private readonly ILogger<HandleIqCreated> _logger;

    public HandleIqCreated(ILogger<HandleIqCreated> logger)
    {
        _logger = logger;
    }
    
    [CapSubscribe(nameof(IqCreated))]
    public void Handle(IqCreated iqCreated)
    {
        _logger.LogInformation($"Iq Created");
    }
}