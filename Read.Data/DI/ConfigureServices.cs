using Microsoft.Extensions.DependencyInjection;
using Read.Contracts.Repository;
using Read.Data.Repository;

namespace Read.Data.DI;

public static class ConfigureServices
{
    public static void AddReadDataServices(this IServiceCollection service)
    {
        service.AddSingleton<MongoDbContext>();
        service.AddScoped<IAccessRepository, AccessRepository>();
        service.AddScoped<IIqRepository,IqRepository>();
    }
}