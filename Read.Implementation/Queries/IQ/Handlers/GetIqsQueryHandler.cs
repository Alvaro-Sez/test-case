using Read.Contracts.Queries;
using Read.Contracts.Repository;
using Shared;

namespace Read.Implementation.Queries.IQ.Handlers;

public class GetIqsQueryHandler : IQueryHandler<IEnumerable<string>>
{
    private readonly IIqRepository _iqRepository;

    public GetIqsQueryHandler(IIqRepository iqRepository)
    {
        _iqRepository = iqRepository;
    }

    public async Task<Result<IEnumerable<string>>> HandleAsync()
    {
        var iqNames = await _iqRepository.GetAllAsync();
        
        return Result<IEnumerable<string>>.From(iqNames.Select(c => c.BuildingName));
    }
}