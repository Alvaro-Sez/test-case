using DotNetCore.CAP;
using Microsoft.Extensions.Logging;
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
    private readonly ILogger<AcceptHigherAccessCommandHandler> _logger;
    public AcceptHigherAccessCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork, ICapPublisher publisher, ILogger<AcceptHigherAccessCommandHandler> logger)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _publisher = publisher;
        _logger = logger;
    }

    public async Task<Result> HandleAsync(AcceptHigherAccessCommand command)
    {
        var userRequesting = await _userRepository.GetByIdAsync(command.UserRequestingId);
        var userAccepting = await _userRepository.GetByIdAsync(command.UserAcceptingId);
        
        if(!command.IsAdmin)
        {
            if(userAccepting is null)
            {
                _logger.LogInformation("denied action: {command} from user: {userId}, reason: {error}",nameof(AcceptHigherAccessCommand),command.UserAcceptingId, Errors.IqNotAssigned);
                return Result.Failure(Errors.IqNotAssigned);
            }
        
            if(!IsAllowedToAccept(userAccepting, userRequesting))
            {
                _logger.LogInformation("denied action: {command} from user: {userId}, reason: {error}",nameof(AcceptHigherAccessCommand),command.UserAcceptingId, Errors.NoLevelAccess);
                return Result.Failure(Errors.NoLevelAccess);
            }
        }
        
        if(userRequesting is null)
        {
            _logger.LogInformation("denied action: {command} from user: {userId}, reason: {error}",nameof(AcceptHigherAccessCommand),command.UserRequestingId, Errors.IqNotAssigned);
            return Result.Failure(Errors.IqNotAssigned);
        }
        userRequesting.AccessLevel = Contacts.Entities.Access.High;
        await _unitOfWork.SaveChangesAsync();
        await _publisher.PublishAsync(nameof(PermissionUpgradedEvent), new PermissionUpgradedEvent(){UserId =userRequesting.Id});
        return Result.Success();
    }
    private bool IsAllowedToAccept(User userAccepting, User userRequesting)
    {
        if(userAccepting.AccessLevel is Contacts.Entities.Access.Low)
        {
            return false;
        }
        // only higher users with the iq bound can manage requests over a user with this iq
        var acceptingUserOwnershipIqs = userAccepting.IqAssigned.Select(c => c.Id);
        var requestingUserOwnershipIqs = userRequesting.IqAssigned.Select(c => c.Id);
        return acceptingUserOwnershipIqs.Any(c => requestingUserOwnershipIqs.Contains(c));
    }
}