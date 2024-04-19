using Read.Contracts.Entities;

namespace Read.Implementation.Queries.AccessRequest;

public class GetAccessRequests
{
    public IEnumerable<AccessLevelRequest> requests { get; set; }
}