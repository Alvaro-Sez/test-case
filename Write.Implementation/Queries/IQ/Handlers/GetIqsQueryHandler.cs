using Write.Contacts.Entities;
using Write.Contacts.Queries;

namespace Write.Implementation.Queries.IQ.Handlers;

public class GetIqsQueryHandler : IQueryHandler<GetIqsQuery, IEnumerable<Iq>>
{
    public Task<IEnumerable<Iq>> HandleAsync(GetIqsQuery query)
    {
        throw new NotImplementedException();
    }
}