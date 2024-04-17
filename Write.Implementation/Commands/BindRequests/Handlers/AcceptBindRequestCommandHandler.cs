using DotNetCore.CAP;
using Shared;
using Write.Contacts.Commands;
using Write.Contacts.Entities;
using Write.Contacts.Events;
using Write.Contacts.Repository;

namespace Write.Implementation.Commands.BindRequests.Handlers;

public class AcceptBindRequestCommandHandler : ICommandHandler<AcceptBindRequestCommand>
{
    private readonly IIqRepository _iqRepository;
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICapPublisher _capPublisher;

    public AcceptBindRequestCommandHandler(IIqRepository iqRepository, IUserRepository userRepository, IUnitOfWork unitOfWork, ICapPublisher capPublisher)
    {
        _iqRepository = iqRepository;
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _capPublisher = capPublisher;
    }

    public async Task<Result> HandleAsync(AcceptBindRequestCommand command)
    {
        var requestedIq = await _iqRepository.GetByBuildingNameAsync(command.BuildingName); 
        
        if (requestedIq is null)
        {
            return Result.Failure(Errors.IqDontExist);
        }
        
        var user = await _userRepository.GetByIdAsync(command.UserToBind) ?? new User(command.UserToBind);
        user.IqAssigned.Add(requestedIq);
        
        await _unitOfWork.SaveChangesAsync();
        
        await _capPublisher.PublishAsync(nameof(IqAssignedEvent), new IqAssignedEvent(){
            UserId = user.Id,
            IqId = requestedIq.Id
        });
        
        return Result.Success();
    }
}