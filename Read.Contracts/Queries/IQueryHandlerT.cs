using Shared;

namespace Read.Contracts.Queries;

public interface IQueryHandlerT<in TDto, TQuery>
{
    Task<Result<TQuery>> HandleAsync(TDto dto);
}