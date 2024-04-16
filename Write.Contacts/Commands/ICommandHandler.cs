using Shared;

namespace Write.Contacts.Commands;

public interface ICommandHandler<TCommand>
{
    Task<Result> HandleAsync(TCommand command);
}
