using MongoDB.Driver;
using Read.Data.Models;

namespace Read.Data.Repository;

public class MongoDbContext
{
    public readonly IMongoCollection<Access> AccessCollection;
    public readonly IMongoCollection<IqBuildingNames> IqBuildingNamesCollection;

    public MongoDbContext()
    {
        var mongoClient = new MongoClient(Environment.GetEnvironmentVariable("ENV_MONGO_URL"));
        var mongoDb = mongoClient.GetDatabase("Locks");
        AccessCollection = mongoDb.GetCollection<Access>("Access");
        IqBuildingNamesCollection = mongoDb.GetCollection<IqBuildingNames>("Iqs");
    }
}