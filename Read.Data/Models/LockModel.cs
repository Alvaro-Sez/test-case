using MongoDB.Bson.Serialization.Attributes;
using Read.Contracts.Entities;

namespace Read.Data.Models;
public class LockModel
{
    [BsonId]
    public Guid Id { get; set; }
    public string Access { get; set; } = AccessLevel.Low;
}