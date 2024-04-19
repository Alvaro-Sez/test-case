using DotNetCore.CAP;
using Shared;
using Write.Contacts.Commands;
using Write.Contacts.Events;
using Write.Contacts.Repository;

namespace Write.Implementation.Commands.Access.Handlers;

public class AssignHigherPermissionCommandHandler : ICommandHandler<AssignHigherPermissionCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICapPublisher _publisher;
    public AssignHigherPermissionCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork, ICapPublisher publisher)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _publisher = publisher;
    }

    public async Task<Result> HandleAsync(AssignHigherPermissionCommand command)
    {
        var user = await _userRepository.GetByIdAsync(command.UserRequestingId);
        if(user is null)
        {
            return Result.Failure(Errors.IqNotAssigned);
        }
        user.AccessLevel = Contacts.Entities.Access.High;
        await _unitOfWork.SaveChangesAsync();
        await _publisher.PublishAsync(nameof(PermissionUpgradedEvent), new PermissionUpgradedEvent(){UserId = user.Id});
        return Result.Success();//TODO notify read
    }
}