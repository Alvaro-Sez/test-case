using MongoDB.Bson.Serialization.Attributes;

namespace Read.Data.Models;

public class LockModel
{
    [BsonId]
    public Guid Id { get; set; }
}