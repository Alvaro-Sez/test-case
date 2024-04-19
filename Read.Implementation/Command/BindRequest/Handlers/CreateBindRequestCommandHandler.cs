using System.Data;
using Read.Contracts.Commands;
using Read.Contracts.Entities;
using Read.Contracts.Repository;
using Shared;

namespace Read.Implementation.Command.BindRequest.Handlers;

public class CreateBindRequestCommandHandler: ICommandHandler<CreateBindRequestCommand>
{
    private readonly IIqRepository _iqRepository;
    private readonly IBindRequestRepository _bindRequestRepository;

    public CreateBindRequestCommandHandler(IBindRequestRepository bindRequestRepository, IIqRepository iqRepository)
    {
        _bindRequestRepository = bindRequestRepository;
        _iqRepository = iqRepository;
    }
    public async Task<Result> HandleAsync(CreateBindRequestCommand command)
    {
        if (!await _iqRepository.ExistAsync(command.BuildingName))
        {
            return Result.Failure(Errors.IqDontExist);
        }
        
        var bindRequest = new BindIqRequest()
        {
            AuthorId = command.UserToBind,
            IqBuildingName = command.BuildingName,
            Id = Guid.NewGuid()
        };
        
        if(await _bindRequestRepository.ExistAsync(bindRequest))
        {
            return Result.Failure(Errors.RequestAlreadyPlaced);
        }
        
        
        await _bindRequestRepository.SetAsync(bindRequest);
        
        return Result.Success();
    }
}