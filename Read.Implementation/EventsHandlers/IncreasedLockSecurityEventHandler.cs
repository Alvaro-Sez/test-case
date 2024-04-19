using DotNetCore.CAP;
using Read.Contracts.Events;
using Write.Contacts.Events;

namespace Read.Implementation.EventsHandlers;

public class IncreasedLockSecurityEventHandler : IEventHandler<IncreasedLockSecurityEvent> , ICapSubscribe
{
    public IncreasedLockSecurityEventHandler()
    {
        
    }
    
    [CapSubscribe(nameof(IncreasedLockSecurityEvent ))]
    public Task Handle(IncreasedLockSecurityEvent @event)
    {
        throw new NotImplementedException();
    }
}