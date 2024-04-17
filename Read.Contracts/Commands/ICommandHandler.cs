using Shared;

namespace Read.Contracts.Commands;

public interface ICommandHandler<TCommand>
{
    Task<Result> HandleAsync(TCommand command);
}