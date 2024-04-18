using MongoDB.Bson.Serialization.Attributes;

namespace Read.Data.Models;

public class IqModel
{
    [BsonId]
    public Guid Id { get; set; }
    // public string BuildingName { get; set; } = string.Empty;
    public List<LockModel> Locks { get; set; }
}