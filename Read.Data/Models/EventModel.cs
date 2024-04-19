using MongoDB.Bson.Serialization.Attributes;

namespace Read.Data.Models;

[BsonIgnoreExtraElements]
public class EventModel
{
    public DateTime IssuedAt { get; set; }
    public Guid UserId { get; set; }
    public Guid LockId { get; set; }
    public string Type { get; set; } = string.Empty;
}