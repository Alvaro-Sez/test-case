using Shared;

namespace Read.Contracts.Queries;

public interface IQueryHandlerT<TQuery,in TDto>
{
    Task<Result<TQuery>> HandleAsync(TDto dto);
}