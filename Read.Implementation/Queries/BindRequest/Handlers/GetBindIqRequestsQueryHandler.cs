using Read.Contracts.Entities;
using Shared;
using Write.Contacts.Queries;

namespace Read.Implementation.Queries.BindRequest.Handlers;

public class GetBindIqRequestsQueryHandler : IQueryHandler<GetBindIqRequestsQuery, IEnumerable<BindIqRequest>>
{
    public Task<Result<IEnumerable<BindIqRequest>>> HandleAsync(GetBindIqRequestsQuery query)
    {
        throw new NotImplementedException();
    }
}