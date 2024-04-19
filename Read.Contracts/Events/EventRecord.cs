namespace Read.Contracts.Events;

public class EventRecord
{
    public DateTime IssuedAt { get; set; }
    public Guid UserId { get; set; }
    public Guid LockId { get; set; }
    public string Type { get; set; } = string.Empty;
}