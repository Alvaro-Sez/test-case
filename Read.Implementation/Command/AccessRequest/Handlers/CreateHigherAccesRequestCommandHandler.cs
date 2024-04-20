using Microsoft.Extensions.Logging;
using Read.Contracts.Commands;
using Read.Contracts.Entities;
using Read.Contracts.Repository;
using Shared;

namespace Read.Implementation.Command.AccessRequest.Handlers;

public class CreateHigherAccesRequestCommandHandler: ICommandHandler<CreateHigherAccessRequestCommand>
{
    private readonly IAccessRequestRepository _accessRequestRepository;
    private readonly IUserAccessRepository _userAccessRepository;
    private readonly ILogger<CreateHigherAccessRequestCommand> _logger;

    public CreateHigherAccesRequestCommandHandler(IAccessRequestRepository accessRequestRepository, IUserAccessRepository userAccessRepository, ILogger<CreateHigherAccessRequestCommand> logger)
    {
        _accessRequestRepository = accessRequestRepository;
        _userAccessRepository = userAccessRepository;
        _logger = logger;
    }

    public async Task<Result> HandleAsync(CreateHigherAccessRequestCommand command)
    {
        if (!await _userAccessRepository.ExistAsync(new UserAccess(){UserId = command.UserId}))
        {
            _logger.LogInformation("denied action: {command} from user: {userId}, reason: {error}",nameof(CreateHigherAccessRequestCommand ),command.UserId, Errors.IqNotAssigned);
            return Result.Failure(Errors.IqNotAssigned);
        }

        var userRequesting = await _userAccessRepository.GetAsync(command.UserId);
        
        if(!userRequesting?.Iqs.Contains(command.IqId) ?? true)
        {
            _logger.LogInformation("denied action: {command} from user: {userId}, reason: {error}",nameof(CreateHigherAccessRequestCommand ),command.UserId, Errors.UserNotAssignedToThisIq);
            return Result.Failure(Errors.UserNotAssignedToThisIq);
        }
        
        var bindRequest = new AccessLevelRequest(){UserId = command.UserId, IqId = command.IqId};
        
        if(await _accessRequestRepository.ExistAsync(bindRequest))
        {
            _logger.LogInformation("denied action: {command} from user: {userId}, reason: {error}",nameof(CreateHigherAccessRequestCommand ),command.UserId, Errors.RequestAlreadyPlaced);
            return Result.Failure(Errors.RequestAlreadyPlaced);
        }
        
        await _accessRequestRepository.SetAsync(bindRequest);
        
        return Result.Success();
    }
}