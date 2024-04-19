using Read.Contracts.Entities;
using Read.Contracts.Queries;
using Read.Contracts.Repository;
using Read.Implementation.Dto;
using Shared;

namespace Read.Implementation.Queries.Events.Handlers;

public class GetEventsQueryHandler: IQueryHandlerT<GetEventsQuery, GetEventsDto>
{
    private readonly IEventRepository _eventRepository;
    private readonly IUserAccessRepository _userAccess;
    
    public GetEventsQueryHandler(IEventRepository eventRepository, IUserAccessRepository userAccess)
    {
        _eventRepository = eventRepository;
        _userAccess = userAccess;
    }
    public async Task<Result<GetEventsQuery>> HandleAsync(GetEventsDto dto)
    {
        if(!await _userAccess.ExistAsync(new UserAccess{UserId = Guid.Parse(dto.UserId)}))
        {
            return Result<GetEventsQuery>.Failure(Errors.UserDontExists);
        }
        var events = await _eventRepository.GetByUserId(Guid.Parse(dto.UserId));
        return Result<GetEventsQuery>.From(new GetEventsQuery{Events = events});
    }
}