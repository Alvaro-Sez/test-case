using Shared;
using Write.Contacts.Commands;
using Write.Contacts.Entities;
using Write.Contacts.Repository;
using Write.Implementation.Error;

namespace Write.Implementation.Commands.BindRequests.Handlers;

public class AcceptBindRequestCommandHandler : ICommandHandler<AcceptBindRequestCommand>
{
    private readonly IIqRepository _iqRepository;
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AcceptBindRequestCommandHandler(IIqRepository iqRepository, IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _iqRepository = iqRepository;
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
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
        
        return Result.Success();
    }
}