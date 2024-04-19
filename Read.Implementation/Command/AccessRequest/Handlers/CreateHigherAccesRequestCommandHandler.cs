using Read.Contracts.Commands;
using Read.Contracts.Entities;
using Read.Contracts.Repository;
using Shared;

namespace Read.Implementation.Command.AccessRequest.Handlers;

public class CreateHigherAccesRequestCommandHandler: ICommandHandler<CreateHigherAccessRequestCommand>
{
    private readonly IAccessRequestRepository _accessRequestRepository;
    private readonly IUserAccessRepository _userAccessRepository;

    public CreateHigherAccesRequestCommandHandler(IAccessRequestRepository accessRequestRepository, IUserAccessRepository userAccessRepository)
    {
        _accessRequestRepository = accessRequestRepository;
        _userAccessRepository = userAccessRepository;
    }

    public async Task<Result> HandleAsync(CreateHigherAccessRequestCommand command)
    {
        if (!await _userAccessRepository.ExistAsync(new UserAccess(){UserId = command.UserId}))
        {
            return Result.Failure(Errors.IqNotAssigned);
        }

        var userRequesting = await _userAccessRepository.GetAsync(command.UserId);
        
        if(!userRequesting?.Iqs.Contains(command.IqId) ?? true)
        {
            return Result.Failure(Errors.UserNotAssignedToThisIq);
        }
        
        var bindRequest = new AccessLevelRequest(){UserId = command.UserId, IqId = command.IqId};
        
        if(await _accessRequestRepository.ExistAsync(bindRequest))
        {
            return Result.Failure(Errors.RequestAlreadyPlaced);
        }
        
        await _accessRequestRepository.SetAsync(bindRequest);
        
        return Result.Success();
    }
}