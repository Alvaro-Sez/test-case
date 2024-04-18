using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Read.Data.Models;

public class UserAccessModel
{
    [BsonId]
    public Guid Id { get; set; }
    public List<IqModel> Iqs { get; set; }   
}