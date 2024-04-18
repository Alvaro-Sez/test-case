using Microsoft.Extensions.DependencyInjection;
using Read.Contracts.Commands;
using Read.Contracts.Entities;
using Read.Contracts.Events;
using Read.Contracts.Queries;
using Read.Implementation.Command.BindRequest;
using Read.Implementation.Command.BindRequest.Handlers;
using Read.Implementation.Dto;
using Read.Implementation.EventsHandlers;
using Read.Implementation.Queries.Access;
using Read.Implementation.Queries.Access.Handlers;
using Read.Implementation.Queries.BindRequest.Handlers;
using Read.Implementation.Queries.IQ;
using Read.Implementation.Queries.IQ.Handlers;
using Write.Contacts.Events;

namespace Read.Implementation.DI;

public static class ConfigureServices
{
    public static void AddReadServices(this IServiceCollection service)
    {
        service.AddScoped<IEventHandler<IqCreatedEvent>,IqCreatedEventHandler>();
        service.AddScoped<IEventHandler<IqAssignedEvent>, IqAssignedEventHandler>();
        service.AddScoped<IQueryHandler<IEnumerable<BindIqRequest>>,GetBindIqRequestsQueryHandler>();
        service.AddScoped<ICommandHandler<CreateBindRequestCommand>,CreateBindRequestCommandHandler>();
        service.AddScoped<IQueryHandler<GetIqsQuery>,GetIqsQueryHandler>();
        service.AddScoped<IQueryHandlerT<GetAllowedLocksQuery,UserIdDto>,GetAllowedLocksQueryHandler>();
        service.AddScoped<IQueryHandlerT<OpenLockQuery, OpenLockDto>,OpenLockQueryHandler>();
    }
}