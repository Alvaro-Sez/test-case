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
        
        if(iq != null)
        {
            return Result.Failure(Errors.IqAlreadyExist);
        }
        await _iqRepository.AddAsync(GenerateIq(command.BuildingName));
        await _unitOfWork.SaveChangesAsync();
        
        await _capPublisher.PublishAsync(nameof(IqCreatedEvent), new IqCreatedEvent(command.BuildingName));
        
        return Result.Success();
    }
    
    private Iq GenerateIq(string buildingName)
    {
        return new Iq()
        {
            BuildingName = buildingName,
            Id = Guid.NewGuid(),
            LockPool = Enumerable.Range(0, 16).Select(c => new Lock(Guid.NewGuid()))
        };
    }
}