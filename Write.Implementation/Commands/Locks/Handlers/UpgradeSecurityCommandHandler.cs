using DotNetCore.CAP;
using Microsoft.Extensions.Logging;
using Shared;
using Write.Contacts.Commands;
using Write.Contacts.Events;
using Write.Contacts.Repository;

namespace Write.Implementation.Commands.Locks.Handlers;

public class UpgradeSecurityCommandHandler : ICommandHandler<UpgradeSecurityCommand>
{
    private readonly ILockRepository _lockRepository;
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICapPublisher _publisher;
    private readonly ILogger<UpgradeSecurityCommandHandler> _logger;

    public UpgradeSecurityCommandHandler (
        ILockRepository lockRepository,
        IUserRepository userRepository, 
        IUnitOfWork unitOfWork,
        ICapPublisher publisher, ILogger<UpgradeSecurityCommandHandler> logger)
    {
        _lockRepository = lockRepository;
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _publisher = publisher;
        _logger = logger;
    }

    public async Task<Result> HandleAsync(UpgradeSecurityCommand command)
    {
        var user = await _userRepository.GetByIdAsync(command.RequestingUserId);
        
        if (user is null)
        {
            _logger.LogInformation("denied action: {command} from user: {userId}, reason: {error}",nameof(UpgradeSecurityCommand),command.RequestingUserId, Errors.IqNotAssigned);
            return Result.Failure(Errors.IqNotAssigned);
        }
        if(user.AccessLevel is Contacts.Entities.Access.Low)
        {
            _logger.LogInformation("denied action: {command} from user: {userId}, reason: {error}",nameof(UpgradeSecurityCommand),command.RequestingUserId, Errors.NoLevelAccess);
            return Result.Failure(Errors.NoLevelAccess);
        }
        
        var @lock = await _lockRepository.GetByIdAsync(command.LockId);
        
        if(@lock is null)
        {
            _logger.LogInformation("denied action: {command} from user: {userId}, reason: {error}",nameof(UpgradeSecurityCommand),command.RequestingUserId, Errors.LockNotExist);
            return Result.Failure(Errors.LockNotExist);
        }
        
        if(user.IqAssigned.Select(c=>c.Id).Contains(command.LockId))
        {
            _logger.LogInformation("denied action: {command} from user: {userId}, reason: {error}",nameof(UpgradeSecurityCommand),command.RequestingUserId, Errors.NoAccessToThisLock);
            return Result.Failure(Errors.NoAccessToThisLock);
        }
        
        @lock.UpgradeSecurity();
        await _unitOfWork.SaveChangesAsync();
        await _publisher.PublishAsync(nameof(IncreasedLockSecurityEvent),
            new IncreasedLockSecurityEvent
            {
                LockId = @lock.Id,
                IqId = @lock.Iq.Id
            });
        return Result.Success();
    }
}
