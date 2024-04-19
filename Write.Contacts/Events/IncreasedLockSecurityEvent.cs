namespace Write.Contacts.Events;

public class IncreasedLockSecurityEvent
{
    public Guid LockId { get; set; }
    public Guid IqId { get; set; }
}