using Read.Contracts.Queries;

namespace Read.Implementation.Queries.Access;

public class GetAllowedLocksQuery 
{
    public IEnumerable<Guid> Locks { get; set; }    
}