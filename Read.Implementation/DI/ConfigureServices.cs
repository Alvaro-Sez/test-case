using Microsoft.Extensions.DependencyInjection;
using Read.Implementation.Events;

namespace Read.Implementation.DI;

public static class ConfigureServices
{
    public static   void  AddReadServices(this IServiceCollection service)
    {
        service.AddTransient<HandleIqCreated>();
    }
}