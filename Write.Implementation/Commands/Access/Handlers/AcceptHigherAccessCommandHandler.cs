using DotNetCore.CAP;
using Shared;
using Write.Contacts.Commands;
using Write.Contacts.Entities;
using Write.Contacts.Events;
using Write.Contacts.Repository;

namespace Write.Implementation.Commands.Access.Handlers;

public class AcceptHigherAccessCommandHandler : ICommandHandler<AcceptHigherAccessCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICapPublisher _publisher;
    public AcceptHigherAccessCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork, ICapPublisher publisher)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _publisher = publisher;
    }

    public async Task<Result> HandleAsync(AcceptHigherAccessCommand command)
    {
        var userRequesting = await _userRepository.GetByIdAsync(command.UserRequestingId);
        var userAccepting = await _userRepository.GetByIdAsync(command.UserAcceptingId);
        if(userRequesting is null || userAccepting is null)
        {
            return Result.Failure(Errors.IqNotAssigned);
        }
        
        if(!IsAllowedToAccept(userAccepting, userRequesting))
        {
            return Result.Failure(Errors.NoLevelAccess);
        }
        
        userRequesting.AccessLevel = Contacts.Entities.Access.High;
        await _unitOfWork.SaveChangesAsync();
        await _publisher.PublishAsync(nameof(PermissionUpgradedEvent), new PermissionUpgradedEvent(){UserId =userRequesting.Id});
        return Result.Success();//TODO notify read
    }
    private bool IsAllowedToAccept(User userAccepting, User userRequesting)
    {
        if(userAccepting.AccessLevel is Contacts.Entities.Access.Low)
        {
            return false;
        }
        var acceptingUserOwnershipIqs = userAccepting.IqAssigned.Select(c => c.Id);
        var requestingUserOwnershipIqs = userRequesting.IqAssigned.Select(c => c.Id);
        return acceptingUserOwnershipIqs.Any(c => requestingUserOwnershipIqs.Contains(c));
    }
}