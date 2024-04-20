namespace Write.Contacts.Events;

public class IqCreatedEvent
{
    public IEnumerable<Guid> Locks { get; set; } = Enumerable.Empty<Guid>();
    public Guid IqId { get; set; }
    public string BuildingName { get; set; } = string.Empty;
}