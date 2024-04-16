namespace Write.Contacts.Commands;

public interface ICommandHandler<TCommand>
{
    Task HandleAsync(TCommand command);
}
