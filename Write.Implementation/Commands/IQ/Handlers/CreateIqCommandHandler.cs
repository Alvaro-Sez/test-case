using DotNetCore.CAP;
using Shared;
using Write.Contacts.Commands;
using Write.Contacts.Entities;
using Write.Contacts.Events;
using Write.Contacts.Repository;

namespace Write.Implementation.Commands.IQ.Handlers;

public class CreateIqCommandHandler: ICommandHandler<CreateIQCommand>
{
    private readonly ICapPublisher _capPublisher;
    private readonly IIqRepository _iqRepository;
    private readonly IUnitOfWork _unitOfWork;
    public CreateIqCommandHandler(IUnitOfWork unitOfWork, IIqRepository iqRepository, ICapPublisher capPublisher)
    {
        _unitOfWork = unitOfWork;
        _iqRepository = iqRepository;
        _capPublisher = capPublisher;
    }

    public async Task<Result> HandleAsync(CreateIQCommand command)
    {
        var iq = await _iqRepository.GetByBuildingNameAsync(command.BuildingName);
        
        if(iq is not null)
        {
            return Result.Failure(Errors.IqAlreadyExist);
        }
        await _iqRepository.AddAsync(new Iq(command.BuildingName));
        
        await _unitOfWork.SaveChangesAsync();
        
        await _capPublisher.PublishAsync(nameof(IqCreatedEvent), new IqCreatedEvent(command.BuildingName));
        
        return Result.Success();
    }
}