using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Read.Data.Models;

public class Access
{
    [BsonRequired]
    public string LockId { get; set; }

    [BsonRequired]
    public string UserId { get; set; }
}