using DotNetCore.CAP;
using Read.Implementation.Events;
using Shared;
using Write.Contacts.Commands;
using Write.Contacts.Repository;

namespace Write.Implementation.Commands.BindRequests.Handlers;

public class CreateBindRequestCommandHandler : ICommandHandler<CreateBindRequestCommand>
{
    private readonly ICapPublisher _publisher;
    private readonly IIqRepository _iqRepository;
    private readonly IUnitOfWork _unitOfWork;
    
    public CreateBindRequestCommandHandler(ICapPublisher publisher, IIqRepository iqRepository, IUnitOfWork unitOfWork)
    {
        _publisher = publisher;
        _iqRepository = iqRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task<Result> HandleAsync(CreateBindRequestCommand command)
    {
        var requestedIq = await _iqRepository.GetByBuildingNameAsync(command.BuildingName); 
        
        if (requestedIq is null)
        {
            return Result.Failure(Errors.IqDontExist);
        }
        
        var bindRequest = new BindIqRequestAccepted()
        {
            AuthorId = command.UserToBind,
            IqId = requestedIq.Id.ToString()
        };

        await _publisher.PublishAsync(nameof(BindIqRequestAccepted),bindRequest);
        
        return Result.Success();
    }
}