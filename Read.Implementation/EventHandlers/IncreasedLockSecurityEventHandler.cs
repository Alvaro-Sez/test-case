using DotNetCore.CAP;
using Read.Contracts.Entities;
using Read.Contracts.Events;
using Read.Contracts.Repository;
using Write.Contacts.Events;

namespace Read.Implementation.EventHandlers;

public class IncreasedLockSecurityEventHandler : IEventHandler<IncreasedLockSecurityEvent> , ICapSubscribe
{
    private readonly IIqRepository _iqRepository;
    public IncreasedLockSecurityEventHandler(IIqRepository iqRepository)
    {
        _iqRepository = iqRepository;
    }
    
    [CapSubscribe(nameof(IncreasedLockSecurityEvent))]
    public async Task Handle(IncreasedLockSecurityEvent @event)
    {
        var iq = await _iqRepository.GetAsync(@event.IqId);
        if(iq is not null)
        {
            var @lock = iq.Locks.First(c => c.Id == @event.LockId);
            @lock.Access = AccessLevel.High;
            await _iqRepository.SetAsync(iq);
        }
    }
}