using Shared;
using Write.Contacts.Commands;

namespace Write.Implementation.Commands.Access.Handlers;

public class RequestHigherPermissionCommandHandler : ICommandHandler<RequestHigherPermissionCommand>
{
    public Task<Result> HandleAsync(RequestHigherPermissionCommand command)
    {
        throw new NotImplementedException();
    }
}