using Read.Contracts.Commands;
using Read.Contracts.Entities;
using Read.Contracts.Repository;
using Shared;

namespace Read.Implementation.Command.BindRequest.Handlers;

public class CreateBindRequestCommandHandler: ICommandHandler<CreateBindRequestCommand>
{
    private readonly IIqBuildingNamesRepository _iqBuildingNames;
    private readonly IBindRequestRepository _bindRequestRepository;

    public CreateBindRequestCommandHandler(IIqBuildingNamesRepository iqBuildingNames, IBindRequestRepository bindRequestRepository)
    {
        _iqBuildingNames = iqBuildingNames;
        _bindRequestRepository = bindRequestRepository;
    }
    public async Task<Result> HandleAsync(CreateBindRequestCommand command)
    {
        var requestedIq = await _iqBuildingNames.GetByBuildingNameAsync(command.BuildingName); 
        
        if (string.IsNullOrEmpty(requestedIq))
        {
            return Result.Failure(Errors.IqDontExist);
        }
        
        var bindRequest = new BindIqRequest()
        {
            AuthorId = command.UserToBind,
            IqBuildingName = requestedIq
        };

        await _bindRequestRepository.SetAsync(bindRequest);
        
        return Result.Success();
    }
}