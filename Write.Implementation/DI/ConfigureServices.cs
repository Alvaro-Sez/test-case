using Microsoft.Extensions.DependencyInjection;
using Write.Contacts.Commands;
using Write.Implementation.Commands.IQ;
using Write.Implementation.Commands.IQ.Handlers;

namespace Write.Implementation.DI;

public static class ConfigureServices
{
    public static void AddWriteServices(this IServiceCollection service)
    {
        service.AddScoped<ICommandHandler<CreateIQCommand>, CreateIqCommandHandler>();
    }
}
