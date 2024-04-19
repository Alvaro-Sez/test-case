using Read.Contracts.Entities;

namespace Read.Implementation.Queries.AccessRequest;

public class GetAccessRequestsQuery
{
    public IEnumerable<AccessLevelRequest> Requests { get; set; }
}