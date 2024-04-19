using Read.Contracts.Entities;
using Read.Contracts.Queries;
using Read.Contracts.Repository;
using Read.Implementation.Dto;
using Shared;

namespace Read.Implementation.Queries.Locks.Handlers;

public class GetAllowedLocksQueryHandler : IQueryHandlerT<GetAllowedLocksQuery,UserIdDto>
{
    private readonly IUserAccessRepository _userAccess;
    private readonly IIqRepository _iqRepository;

    public GetAllowedLocksQueryHandler(IUserAccessRepository userAccess, IIqRepository iqRepository)
    {
        _userAccess = userAccess;
        _iqRepository = iqRepository;
    }
    
    public async Task<Result<GetAllowedLocksQuery>> HandleAsync(UserIdDto idDto)
    {
        var user = await _userAccess.GetAsync(idDto.UserId);
        
        if(user is null)
        {
            return Result<GetAllowedLocksQuery>.Failure(Errors.IqNotAssigned);
        }

        var userIqs = await _iqRepository.GetAllByIdAsync(user.Iqs);

        return Result<GetAllowedLocksQuery>.From(
            new GetAllowedLocksQuery
            {
                Locks = userIqs.SelectMany(c=> c.Locks
                    .Select(l=> new AllowedLocksDto
                    {
                        Id = l.Id,
                        AccessLevel = l.Access
                    }))
            });
    }
}