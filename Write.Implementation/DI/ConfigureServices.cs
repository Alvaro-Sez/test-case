using Microsoft.Extensions.DependencyInjection;
using Write.Contacts.Commands;
using Write.Implementation.Commands.Access;
using Write.Implementation.Commands.Access.Handlers;
using Write.Implementation.Commands.BindRequests;
using Write.Implementation.Commands.BindRequests.Handlers;
using Write.Implementation.Commands.IQ;
using Write.Implementation.Commands.IQ.Handlers;
using Write.Implementation.Commands.Locks;
using Write.Implementation.Commands.Locks.Handlers;

namespace Write.Implementation.DI;

public static class ConfigureServices
{
    public static void AddWriteServices(this IServiceCollection service)
    {
        service.AddScoped<ICommandHandler<CreateIQCommand>, CreateIqCommandHandler>();
        service.AddScoped<ICommandHandler<AcceptBindRequestCommand>,AcceptBindRequestCommandHandler>();
        service.AddScoped<ICommandHandler<UpgradeSecurityCommand>,UpgradeSecurityCommandHandler>();
        service.AddScoped<ICommandHandler<AcceptHigherAccessCommand>,AcceptHigherAccessCommandHandler>();
        
    }
}
