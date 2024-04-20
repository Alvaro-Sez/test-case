using Read.Contracts.Entities;
using Read.Contracts.Queries;
using Read.Contracts.Repository;
using Read.Implementation.Dto;
using Shared;

namespace Read.Implementation.Queries.AccessRequest.Handlers;

public class GetAccessRequestQueryHandler : IQueryHandlerT<GetAccessRequestsQuery, GetAccessRequest>
{
    private readonly IAccessRequestRepository _requestRepository;
    private readonly IUserAccessRepository _userAccessRepository;
    
    public GetAccessRequestQueryHandler(IAccessRequestRepository requestRepository, IUserAccessRepository userAccessRepository)
    {
        _requestRepository = requestRepository;
        _userAccessRepository = userAccessRepository;
    }
    public async Task<Result<GetAccessRequestsQuery>> HandleAsync(GetAccessRequest dto)
    {
        var requests = await _requestRepository.GetAllAsync();
        
        if(dto.IsAdmin)
        {
            return Result<GetAccessRequestsQuery>.From(new GetAccessRequestsQuery() { Requests = requests });
        }
        
        var userCheckingRequests = await _userAccessRepository.GetAsync(dto.UserId);
        
        if(userCheckingRequests is null)
        {
            return Result<GetAccessRequestsQuery>.Failure(Errors.IqNotAssigned);
        }
        
        if(string.Equals(userCheckingRequests.Access, AccessLevel.Low))
        {
            return Result<GetAccessRequestsQuery>.Failure(Errors.NoLevelAccess);
        }
        
        var requestAvailable = requests.Where(c => userCheckingRequests.Iqs.Contains(c.IqId));
        
        return Result<GetAccessRequestsQuery>.From(new GetAccessRequestsQuery() { Requests = requestAvailable });
    }
}