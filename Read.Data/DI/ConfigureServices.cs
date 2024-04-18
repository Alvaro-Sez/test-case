using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Read.Contracts.Repository;
using Read.Data.Repository;

namespace Read.Data.DI;

public static class ConfigureServices
{
    public static void AddReadDataServices(this IServiceCollection service)
    {
        service.AddScoped<IUserAccessRepository, UserAccessRepository>();
        service.AddScoped<IIqBuildingNamesRepository,IqBuildingNamesRepository>();
        service.AddScoped<IBindRequestRepository,BindRequestRepository>();
        
        service.AddSingleton<IMongoDatabase>(c=> 
            new MongoClient(Environment.GetEnvironmentVariable("ENV_MONGO_URL"))
                .GetDatabase("Locks")
        );

    }
}