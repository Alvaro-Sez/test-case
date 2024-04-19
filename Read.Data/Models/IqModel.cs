using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Read.Data.Models;

public class IqModel
{
    [BsonId]
    public Guid IqId { get; set; }
    public List<LockModel> Locks { get; set; }
    public string BuildingName { get; set; } = string.Empty;
}