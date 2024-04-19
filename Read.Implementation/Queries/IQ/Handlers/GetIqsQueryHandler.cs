using Read.Contracts.Queries;
using Read.Contracts.Repository;
using Shared;

namespace Read.Implementation.Queries.IQ.Handlers;

public class GetIqsQueryHandler : IQueryHandler<GetIqsQuery>
{
    private readonly IIqRepository _iqRepository;
    public GetIqsQueryHandler(IIqRepository iqRepository)
    {
        _iqRepository = iqRepository;
    }

    public async Task<Result<GetIqsQuery>> HandleAsync()
    {
        var iqNames = await _iqRepository.GetAllAsync();
        var query = new GetIqsQuery { IqBuildingNames = iqNames.Select(c => c.BuildingName)};
        return Result<GetIqsQuery>.From(query);
    }
}