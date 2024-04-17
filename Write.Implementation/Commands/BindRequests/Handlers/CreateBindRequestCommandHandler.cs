using Shared;
using Write.Contacts.Commands;

namespace Write.Implementation.Commands.BindRequests.Handlers;

public class CreateBindRequestCommandHandler : ICommandHandler<CreateBindRequestCommand>
{
    public Task<Result> HandleAsync(CreateBindRequestCommand command)
    {
        throw new NotImplementedException();
    }
}