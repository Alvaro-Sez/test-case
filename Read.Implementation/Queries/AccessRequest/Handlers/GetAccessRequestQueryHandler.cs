using Read.Contracts.Entities;
using Read.Contracts.Queries;
using Read.Contracts.Repository;
using Read.Implementation.Dto;
using Shared;

namespace Read.Implementation.Queries.AccessRequest.Handlers;

public class GetAccessRequestQueryHandler : IQueryHandlerT<GetAccessRequests, GetAccessRequestDto>
{
    private readonly IAccessRequestRepository _requestRepository;
    private readonly IUserAccessRepository _userAccessRepository;
    
    public GetAccessRequestQueryHandler(IAccessRequestRepository requestRepository, IUserAccessRepository userAccessRepository)
    {
        _requestRepository = requestRepository;
        _userAccessRepository = userAccessRepository;
    }
    public async Task<Result<GetAccessRequests>> HandleAsync(GetAccessRequestDto dto)
    {
        var requests = await _requestRepository.GetAllAsync();
        if(dto.IsAdmin)
        {
            return Result<GetAccessRequests>.From(new GetAccessRequests() { requests = requests });
        }
        
        var userCheckingRequests = await _userAccessRepository.GetAsync(Guid.Parse(dto.UserId));
        
        if(userCheckingRequests is null)
        {
            return Result<GetAccessRequests>.Failure(Errors.IqNotAssigned);
        }
        
        if(string.Equals(userCheckingRequests.Access, AccessLevel.Low))
        {
            return Result<GetAccessRequests>.Failure(Errors.NoLevelAccess);
        }
        
        var requestAvailable = requests
            .Where(c => userCheckingRequests.Iqs.Contains(c.IqId));
        
        return Result<GetAccessRequests>.From(new GetAccessRequests() { requests = requestAvailable });
    }
}