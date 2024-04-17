using Read.Contracts.Entities;
using Read.Contracts.Repository;
using Shared;
using Write.Contacts.Queries;

namespace Read.Implementation.Queries.BindRequest.Handlers;

public class GetBindIqRequestsQueryHandler : IQueryHandler<GetBindIqRequestsQuery, IEnumerable<BindIqRequest>>
{
    private readonly IBindRequestRepository _requestRepository;

    public GetBindIqRequestsQueryHandler(IBindRequestRepository requestRepository)
    {
        _requestRepository = requestRepository;
    }

    public Task<Result<IEnumerable<BindIqRequest>>> HandleAsync(GetBindIqRequestsQuery query)
    {
        throw new NotImplementedException();
    }
}