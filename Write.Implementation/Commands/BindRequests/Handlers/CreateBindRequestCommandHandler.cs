using Shared;
using Write.Contacts.Commands;
using Write.Contacts.Entities;
using Write.Contacts.Repository;
using Write.Implementation.Error;

namespace Write.Implementation.Commands.BindRequests.Handlers;

public class CreateBindRequestCommandHandler : ICommandHandler<CreateBindRequestCommand>
{
    private readonly IBindRequestRepository _requestRepository;
    private readonly IIqRepository _iqRepository;
    private readonly IUnitOfWork _unitOfWork;
    
    public CreateBindRequestCommandHandler(IBindRequestRepository requestRepository, IIqRepository iqRepository, IUnitOfWork unitOfWork)
    {
        _requestRepository = requestRepository;
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
        
        var bindRequest = new BindIqRequest()
        {
            Id = Guid.NewGuid(),
            AuthorId = command.UserToBind,
            IqId = requestedIq.Id
        };
        await _requestRepository.AddAsync(bindRequest);
        await _unitOfWork.SaveChangesAsync();
        
        return Result.Success();
    }
}