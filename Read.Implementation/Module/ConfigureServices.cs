using Microsoft.Extensions.DependencyInjection;

namespace Read.Implementation.Module;

public static class ConfigureServices
{
    public static   void  AddReadServices(this IServiceCollection service)
    {
        service.AddTransient<StoreIqCreated>();
    }
}