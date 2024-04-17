namespace Write.Contacts.Events;

public class IqAssignedEvent
{
    public IqAssignedEvent()
    {
        
    }
    public Guid UserId { get; set; }
    public Guid IqId { get; set; }
}