using Read.Contracts.Queries;
using Read.Contracts.Repository;
using Shared;

namespace Read.Implementation.Queries.IQ.Handlers;

public class GetIqsQueryHandler : IQueryHandler<IEnumerable<GetIqsQuery>>
{
    private readonly IIqRepository _iqRepository;
    public GetIqsQueryHandler(IIqRepository iqRepository)
    {
        _iqRepository = iqRepository;
    }

    public async Task<Result<IEnumerable<GetIqsQuery>>> HandleAsync()
    {
        var iqs = await _iqRepository.GetAllAsync();
        var query = iqs.Select(c => new GetIqsQuery() { IqId = c.Id, IqBuildingName = c.BuildingName });
        return Result<IEnumerable<GetIqsQuery>>.From(query);
    }
}