using Shared;
using Write.Contacts.Commands;

namespace Write.Implementation.Commands.BindRequests.Handlers;

public class AcceptBindRequestCommandHandler : ICommandHandler<AcceptBindRequestCommand>
{
    public Task<Result> HandleAsync(AcceptBindRequestCommand command)
    {
        throw new NotImplementedException();
    }
}