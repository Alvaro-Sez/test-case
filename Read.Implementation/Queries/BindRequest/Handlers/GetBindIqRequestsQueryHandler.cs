using Read.Contracts.Entities;
using Read.Contracts.Queries;
using Read.Contracts.Repository;
using Shared;

namespace Read.Implementation.Queries.BindRequest.Handlers;

public class GetBindIqRequestsQueryHandler : IQueryHandler<GetBindIqRequestsQuery>
{
    private readonly IBindRequestRepository _requestRepository;
    public GetBindIqRequestsQueryHandler(IBindRequestRepository requestRepository)
    {
        _requestRepository = requestRepository;
    }
    public async Task<Result<GetBindIqRequestsQuery>> HandleAsync()
    {
        var allBindRequests = await _requestRepository.GetAllAsync();
        return Result<GetBindIqRequestsQuery>.From(new GetBindIqRequestsQuery(){Requests = allBindRequests});
    }
}