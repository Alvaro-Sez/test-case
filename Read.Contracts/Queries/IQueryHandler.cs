using Shared;

namespace Read.Contracts.Queries;

public interface IQueryHandler<TResult>
{
    Task<Result<TResult>> HandleAsync();
}