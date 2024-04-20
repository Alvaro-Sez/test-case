using Read.Contracts.Entities;

namespace Read.Implementation.Queries.BindRequest;

public class GetBindIqRequestsQuery
{
    public IEnumerable<BindIqRequest> Requests { get; set; } = Enumerable.Empty<BindIqRequest>();
}