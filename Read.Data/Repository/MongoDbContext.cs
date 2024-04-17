using MongoDB.Driver;
using Read.Data.Models;

namespace Read.Data.Repository;

public class MongoDbContext
{
    public readonly IMongoCollection<UserAccess> AccessCollection;
    public readonly IMongoCollection<Iq> IqBuildingNamesCollection;

    public MongoDbContext()
    {
        var mongoClient = new MongoClient(Environment.GetEnvironmentVariable("ENV_MONGO_URL"));
        var mongoDb = mongoClient.GetDatabase("Locks");
        AccessCollection = mongoDb.GetCollection<UserAccess>("Access");
        IqBuildingNamesCollection = mongoDb.GetCollection<Iq>("Iqs");
    }
}