using DotNetCore.CAP;
using Microsoft.Extensions.Logging;
using Read.Contracts.Entities;
using Read.Contracts.Events;
using Read.Contracts.Repository;
using Write.Contacts.Events;

namespace Read.Implementation.EventHandlers;

public class IncreasedLockSecurityEventHandler : IEventHandler<IncreasedLockSecurityEvent> , ICapSubscribe
{
    private readonly IIqRepository _iqRepository;
    private readonly ILogger<IncreasedLockSecurityEvent> _logger;
    public IncreasedLockSecurityEventHandler(IIqRepository iqRepository, ILogger<IncreasedLockSecurityEvent> logger)
    {
        _iqRepository = iqRepository;
        _logger = logger;
    }
    
    [CapSubscribe(nameof(IncreasedLockSecurityEvent))]
    public async Task Handle(IncreasedLockSecurityEvent @event)
    {
        try
        {
            var iq = await _iqRepository.GetAsync(@event.IqId);
            
            if(iq is not null)
            {
                throw new ApplicationException("Invalid state of data");
            }
            
            var @lock = iq!.Locks.First(c => c.Id == @event.LockId);
            @lock.Access = AccessLevel.High;
            await _iqRepository.SetAsync(iq);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception ,"an Exception ocurred when proccessing the event: {Event} ",@event);
        } 
    }
}