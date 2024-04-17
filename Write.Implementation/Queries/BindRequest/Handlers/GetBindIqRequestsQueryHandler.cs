using Shared;
using Write.Contacts.Entities;
using Write.Contacts.Queries;

namespace Write.Implementation.Queries.BindRequest.Handlers;

public class GetBindIqRequestsQueryHandler : IQueryHandler<GetBindIqRequestsQuery, IEnumerable<Contacts.Entities.BindIqRequest>>
{
    public Task<Result<IEnumerable<BindIqRequest>>> HandleAsync(GetBindIqRequestsQuery query)
    {
        throw new NotImplementedException();
    }
}