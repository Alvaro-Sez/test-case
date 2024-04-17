using Write.Contacts.Queries;

namespace Write.Implementation.Queries.BindRequest.Handlers;

public class GetBindIqRequestsQueryHandler : IQueryHandler<GetBindIqRequestsQuery, IEnumerable<Contacts.Entities.BindIqRequest>>
{
    public Task<IEnumerable<Contacts.Entities.BindIqRequest>> HandleAsync(GetBindIqRequestsQuery query)
    {
        throw new NotImplementedException();
    }
}