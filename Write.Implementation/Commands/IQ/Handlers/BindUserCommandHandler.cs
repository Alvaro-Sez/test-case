using DotNetCore.CAP;
using Shared;
using Write.Contacts.Commands;

namespace Write.Implementation.Commands.IQ.Handlers;

public class BindUserCommandHandler: ICommandHandler<BindUserCommand>
{
    private readonly ICapPublisher _capPublisher;

    public BindUserCommandHandler(ICapPublisher capPublisher)
    {
        _capPublisher = capPublisher;
    }

    public Task<Result> HandleAsync(BindUserCommand command)
    {
        throw new NotImplementedException();
    }
}