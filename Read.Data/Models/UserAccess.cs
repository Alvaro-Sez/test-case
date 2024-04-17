using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Read.Data.Models;

public class UserAccess
{
    [BsonId]
    public Guid UserId { get; set; }
    
    public IEnumerable<Guid> AllowedDevices { get; set; }   
}