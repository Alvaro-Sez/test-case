using DotNetCore.CAP;
using Shared;
using Write.Contacts.Commands;

namespace Write.Implementation.Commands.IQ.Handlers;

public class CreateBindRequestCommandHandler : ICommandHandler<CreateBindRequest>
{
    private readonly ICapPublisher _capPublisher;

    public CreateBindRequestCommandHandler(ICapPublisher capPublisher)
    {
        _capPublisher = capPublisher;
    }

    public Task<Result> HandleAsync(CreateBindRequest command)
    {
        throw new NotImplementedException();
    }
}