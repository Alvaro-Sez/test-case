using Shared;
using Write.Contacts.Commands;

namespace Write.Implementation.Commands.BindRequests;

public class CreateBindRequestCommand : ICommandHandler<CreateBindRequestCommand>
{
    public Task<Result> HandleAsync(CreateBindRequestCommand command)
    {
        throw new NotImplementedException();
    }
}