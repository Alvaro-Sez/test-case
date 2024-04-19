using Read.Implementation.Dto;

namespace Read.Implementation.Queries.Locks;

public class GetAllowedLocksQuery 
{
    public IEnumerable<AllowedLocksDto> Locks { get; set; }    
}