using MongoDB.Bson.Serialization.Attributes;

namespace Read.Data.Models;

public class BindRequestModel
{
    [BsonId]
    public Guid Id { get; set; }
    public Guid UserRequestingAccessId { get; set; }
    public string BuildingName { get; set; } = string.Empty;
}