using Read.Contracts.Events;

namespace Read.Implementation.Queries.Events;

public class GetEventsQuery
{
    public IEnumerable<EventRecord> Events { get; set; } = Enumerable.Empty<EventRecord>();
}