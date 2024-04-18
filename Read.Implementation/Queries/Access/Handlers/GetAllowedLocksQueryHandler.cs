using Read.Contracts.Queries;
using Read.Contracts.Repository;
using Read.Implementation.Dto;
using Shared;

namespace Read.Implementation.Queries.Access.Handlers;

public class GetAllowedLocksQueryHandler : IQueryHandlerT<UserDto,GetAllowedLocksQuery>
{
    private readonly IUserAccessRepository _userAccess;

    public GetAllowedLocksQueryHandler(IUserAccessRepository userAccess)
    {
        _userAccess = userAccess;
    }
    
    public async Task<Result<GetAllowedLocksQuery>> HandleAsync(UserDto dto)
    {
        var user = await _userAccess.GetAsync(Guid.Parse(dto.UserId));
        
        if(user is null)
        {
            return Result<GetAllowedLocksQuery>.Failure(Errors.IqNotAssigned);
        }

        return Result<GetAllowedLocksQuery>.From(
            new GetAllowedLocksQuery
            {
                Locks = user.Iqs
                    .SelectMany(c=>c.Locks.Select(l=>l.Id))
            });
    }
}