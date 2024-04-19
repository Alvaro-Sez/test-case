using MongoDB.Bson.Serialization.Attributes;

namespace Read.Data.Models;

[BsonIgnoreExtraElements]
public class AccessRequestModel
{
    public Guid UserId { get; set; }
    public Guid IqId{ get; set; }
    
}