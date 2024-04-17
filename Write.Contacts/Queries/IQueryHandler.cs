using Shared;

namespace Write.Contacts.Queries;

public interface IQueryHandler<TQuery, TResult>
{
    Task<Result<TResult>> HandleAsync(TQuery query);
}
