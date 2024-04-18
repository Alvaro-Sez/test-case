using Shared;

namespace Read.Contracts.Queries;

public interface IQueryHandler<TQuery>
{
    Task<Result<TQuery>> HandleAsync();
}