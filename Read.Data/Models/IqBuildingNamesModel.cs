using MongoDB.Bson.Serialization.Attributes;

namespace Read.Data.Models;

[BsonIgnoreExtraElements]
public class IqBuildingNamesModel
{
    public string Name { get; set; } = string.Empty;
}