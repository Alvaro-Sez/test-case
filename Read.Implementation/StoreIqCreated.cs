using DotNetCore.CAP;
using Microsoft.Extensions.Logging;
using Write.Contacts;

namespace Read.Implementation;

public class StoreIqCreated: ICapSubscribe
{
    private readonly ILogger<StoreIqCreated> _logger;

    public StoreIqCreated(ILogger<StoreIqCreated> logger)
    {
        _logger = logger;
    }
    
    [CapSubscribe(nameof(IqCreated))]
    public void Handle(IqCreated iqCreated)
    {
        _logger.LogInformation($"Iq Created");
    }
}