using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Read.Contracts.Entities;

namespace Read.Data.Models;
public class UserAccessModel
{
    [BsonId]
    public Guid Id { get; set; }

    public List<Guid> Iqs { get; set; } = new List<Guid>();
    public string Access { get; set; } = AccessLevel.Low;
}