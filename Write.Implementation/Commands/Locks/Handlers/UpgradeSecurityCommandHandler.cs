using DotNetCore.CAP;
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

    public UpgradeSecurityCommandHandler (
        ILockRepository lockRepository,
        IUserRepository userRepository, 
        IUnitOfWork unitOfWork,
        ICapPublisher publisher
    )
    {
        _lockRepository = lockRepository;
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _publisher = publisher;
    }

    public async Task<Result> HandleAsync(UpgradeSecurityCommand command)
    {
        var user = await _userRepository.GetByIdAsync(command.RequestingUserId);
        
        if (user is null)
        {
            return Result.Failure(Errors.IqNotAssigned);
        }
        if(user.AccessLevel is Contacts.Entities.Access.Low)
        {
            return Result.Failure(Errors.NoLevelAccess);
        }

        var @lock = await _lockRepository.GetByIdAsync(command.LockId);
        if(@lock is null)
        {
            return Result.Failure(Errors.LockNotExist);
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
