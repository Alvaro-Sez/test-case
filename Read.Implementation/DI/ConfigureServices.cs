using Microsoft.Extensions.DependencyInjection;
using Read.Contracts.Commands;
using Read.Contracts.Entities;
using Read.Contracts.Events;
using Read.Contracts.Queries;
using Read.Implementation.Command.AccessRequest;
using Read.Implementation.Command.AccessRequest.Handlers;
using Read.Implementation.Command.BindRequest;
using Read.Implementation.Command.BindRequest.Handlers;
using Read.Implementation.Dto;
using Read.Implementation.EventHandlers;
using Read.Implementation.Queries.AccessRequest;
using Read.Implementation.Queries.AccessRequest.Handlers;
using Read.Implementation.Queries.BindRequest;
using Read.Implementation.Queries.BindRequest.Handlers;
using Read.Implementation.Queries.Events;
using Read.Implementation.Queries.Events.Handlers;
using Read.Implementation.Queries.IQ;
using Read.Implementation.Queries.IQ.Handlers;
using Read.Implementation.Queries.Locks;
using Read.Implementation.Queries.Locks.Handlers;
using Write.Contacts.Events;

namespace Read.Implementation.DI;

public static class ConfigureServices
{
    public static void AddReadServices(this IServiceCollection service)
    {
        service.AddScoped<IEventHandler<IqCreatedEvent>,IqCreatedEventHandler>();
        service.AddScoped<IEventHandler<IqAssignedEvent>, IqAssignedEventHandler>();
        service.AddScoped<IEventHandler<PermissionUpgradedEvent>,PermissionUpgradedEventHandler>();
        service.AddScoped<IEventHandler<IncreasedLockSecurityEvent>,IncreasedLockSecurityEventHandler>();
         
        
        service.AddScoped<IQueryHandler<GetBindIqRequestsQuery>,GetBindIqRequestsQueryHandler>();
        service.AddScoped<IQueryHandler<IEnumerable<GetIqsQuery>>,GetIqsQueryHandler>();
        service.AddScoped<IQueryHandlerT<GetAllowedLocksQuery,UserIdDto>,GetAllowedLocksQueryHandler>();
        service.AddScoped<IQueryHandlerT<OpenLockQuery, OpenLockDto>,OpenLockQueryHandler>();
        service.AddScoped<IQueryHandlerT<GetEventsQuery, GetEventsDto>, GetEventsQueryHandler>();
        service.AddScoped<IQueryHandlerT<GetEventsQuery, GetEventsDto>, GetEventsQueryHandler>();
        service.AddScoped<IQueryHandlerT<GetAccessRequestsQuery, GetAccessRequest>, GetAccessRequestQueryHandler>();
       
        service.AddScoped<ICommandHandler<CreateBindRequestCommand>,CreateBindRequestCommandHandler>();
        service.AddScoped<ICommandHandler<CreateHigherAccessRequestCommand>,CreateHigherAccesRequestCommandHandler>();
    }
}