namespace Write.Contacts.Events;

public class IqAssignedEvent
{
    public Guid UserId { get; set; }
    public Guid IqId { get; set; }
    public string BuildingName { get; set; } = string.Empty;
    public List<Guid> LockIds { get; set; }
}