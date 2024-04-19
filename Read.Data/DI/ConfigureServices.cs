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
        service.AddScoped<IIqRepository,IqRepository>();
        service.AddScoped<IBindRequestRepository,BindRequestRepository>();
        service.AddScoped<IEventRepository,EventRepository>();
        service.AddScoped<IAccessRequestRepository,AccessRequestRepository>();
         
        service.AddSingleton<IMongoDatabase>(c=> 
            new MongoClient(Environment.GetEnvironmentVariable("ENV_MONGO_URL"))
                .GetDatabase("Locks")
        );

    }
}